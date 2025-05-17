import { useEffect, useRef, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import {
	Body1,
	Caption1,
	Card,
	CardHeader,
	CardPreview,
	Body2,
	Button,
	Spinner,
	Tooltip,
} from "@fluentui/react-components";
import {
	Edit28Filled,
	Delete28Filled,
	ArrowCircleUp28Regular,
	ArrowCircleUp28Filled,
} from "@fluentui/react-icons";
import { useMsal } from "@azure/msal-react";

import { useStyles } from "@components/Posts/PostBody/styles";
import { DeletePostAsync, UpdateRatingAsync } from "@store/Posts/Actions";
import PostRatingDtoModel from "@models/PostRatingDto";
import { PostBodyConstants } from "@helpers/ibbs.constants";
import { loginRequests } from "@services/auth.config";

/**
 * @component
 * `PostBody` component to display a singular posts.
 *
 * @param {Object} param0 The props passed from parent component.
 * @param {Object} param0.post The posts data.
 *
 * @returns {JSX.Element} The post jsx element.
 */
function PostBody({ post }) {
	const contentRef = useRef(null);
	const styles = useStyles();
	const dispatch = useDispatch();
	const { instance, accounts } = useMsal();

	const { ButtonText } = PostBodyConstants;

	const UpdatedRatingData = useSelector(
		(state) => state.PostsReducer.updatedRatingData
	);
	const IsVotingLoaderOn = useSelector(
		(state) => state.PostsReducer.isVotingLoaderOn
	);

	const [postData, setPostData] = useState({});
	const [showFullText, setShowFullText] = useState(false);
	const [isTextOverflowing, setIsTextOverflowing] = useState(false);
	const [showEditAndDelete, setShowEditAndDelete] = useState(false);
	const [postRatingLoader, setPostRatingLoader] = useState(false);
	const [isUserLoggedIn, setIsUserLoggedIn] = useState(false);
	const [postUpdatedRatingData, setPostUpdatedRatingData] = useState(false);

	useEffect(() => {
		if (postData !== post) {
			setPostData(post);
		}
	}, [post]);

	useEffect(() => {
		if (contentRef.current) {
			const isOverflowing =
				contentRef.current.scrollHeight >
				contentRef.current.clientHeight;
			setIsTextOverflowing(isOverflowing);
		}
	}, [postData, showFullText]);

	useEffect(() => {
		if (accounts.length > 0) {
			setShowEditAndDelete(
				post.postOwnerUserName ==
					accounts[0].idTokenClaims?.extension_UserName
			);
			setIsUserLoggedIn(true);
		} else {
			setShowEditAndDelete(false);
			setIsUserLoggedIn(false);
		}
	}, [instance, accounts]);

	useEffect(() => {
		if (IsVotingLoaderOn !== postRatingLoader) {
			setPostRatingLoader(IsVotingLoaderOn);
		}
	}, [IsVotingLoaderOn]);

	useEffect(() => {
		if (
			Object.values(UpdatedRatingData).length > 0 &&
			UpdatedRatingData !== postUpdatedRatingData
		) {
			setPostUpdatedRatingData(UpdatedRatingData);
		}
	}, [UpdatedRatingData]);

	/**
	 * Gets the access token silently using msal.
	 * @returns {string} The access token.
	 */
	const getAccessToken = async () => {
		const tokenData = await instance.acquireTokenSilent({
			...loginRequests,
			account: accounts[0],
		});

		return tokenData.accessToken;
	};

	/**
	 * Formats the date to date string format.
	 * @param {Date} date The unformatted date
	 * @returns {string} The formatted date.
	 */
	const formatDate = (date) => {
		return new Date(date).toDateString();
	};

	/**
	 * Handle the toggle event for text.
	 */
	const handleToggleText = () => {
		setShowFullText(!showFullText);
	};

	const handleEdit = (postId) => {};

	/**
	 * Handles the post delete operation.
	 * @param {string} postId The post id.
	 */
	const handleDelete = async (postId) => {
		const accessToken = await getAccessToken();
		dispatch(DeletePostAsync(postId, accessToken));
	};

	const handleVoting = async (postId) => {
		const accessToken = await getAccessToken();
		const postRatingDtoModel = new PostRatingDtoModel(postId, false);
		dispatch(UpdateRatingAsync(postRatingDtoModel, accessToken));
	};

	const renderRatingButtonIcons = (button) => {
		return postRatingLoader ? <Spinner size="tiny" /> : <>{button}</>;
	};

	return (
		Object.keys(postData).length > 0 && (
			<Card className={styles.card} appearance="filled-alternative">
				<CardHeader
					className={styles.cardHeader}
					header={
						<div className={styles.headerContainer}>
							<Body1 className={styles.headerTitle}>
								<b>{postData.postTitle}</b>
							</Body1>

							<div className={styles.headerButtons}>
								{!showEditAndDelete && isUserLoggedIn && (
									<Tooltip
										content={
											postData.previousRatingValue === 1
												? ButtonText.AlreadyRatedButtonTooltipText
												: ButtonText.RatingsButtonTooltipText
										}
										relationship="label"
									>
										<Button
											disabled={postRatingLoader}
											appearance="subtle"
											shape="circular"
											onClick={() =>
												handleVoting(postData.postId)
											}
										>
											{renderRatingButtonIcons(
												postData.previousRatingValue ===
													1 ? (
													<ArrowCircleUp28Filled />
												) : (
													<ArrowCircleUp28Regular />
												)
											)}
										</Button>
									</Tooltip>
								)}
								{showEditAndDelete && (
									<>
										<Tooltip
											content={
												ButtonText.EditButtonTooltipText
											}
											relationship="label"
										>
											<Button
												className={styles.editButton}
												appearance="subtle"
												shape="circular"
												onClick={() =>
													handleEdit(postData.postId)
												}
											>
												<Edit28Filled />
											</Button>
										</Tooltip>
										<Tooltip
											content={
												ButtonText.DeleteButtonTooltipText
											}
											relationship="label"
										>
											<Button
												className={styles.deleteButton}
												appearance="subtle"
												shape="circular"
												onClick={() =>
													handleDelete(
														postData.postId
													)
												}
											>
												<Delete28Filled />
											</Button>
										</Tooltip>
									</>
								)}
							</div>
						</div>
					}
					description={
						<Caption1>
							By {postData.postOwnerUserName} on{" "}
							{formatDate(postData.postCreatedDate)}
						</Caption1>
					}
				/>
				<CardPreview className={styles.cardPreview}>
					<Body2>
						<p
							ref={contentRef}
							className={`${styles.postContent} ${
								showFullText ? "full-text" : ""
							}`}
							style={{
								maxHeight: showFullText ? "none" : "100px",
							}}
							dangerouslySetInnerHTML={{
								__html: postData.postContent
									.replace(/\n/g, "<br>")
									.replace(/<br\s*\/?>/g, "<br>"),
							}}
						></p>
						{isTextOverflowing && !showFullText && (
							<Button
								className={styles.button}
								onClick={handleToggleText}
							>
								Show More
							</Button>
						)}
						{showFullText && (
							<Button
								className={styles.button}
								onClick={handleToggleText}
							>
								Show Less
							</Button>
						)}
					</Body2>
				</CardPreview>
			</Card>
		)
	);
}

export default PostBody;
