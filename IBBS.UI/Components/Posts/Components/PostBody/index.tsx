import { useEffect, useRef, useState } from "react";
import {
	Body1,
	Caption1,
	CardHeader,
	CardPreview,
	Body2,
	Button,
	Spinner,
	Tooltip,
	Badge,
	Subtitle2,
	TagGroup,
	Tag,
} from "@fluentui/react-components";
import {
	Star28Regular,
	EditColor,
	DismissCircleColor,
	StarColor,
} from "@fluentui/react-icons";
import { useAuth0 } from "@auth0/auth0-react";

import { useStyles } from "@/Components/Posts/Components/PostBody/styles";
import {
	DeletePostAsync,
	GetEditPostData,
	ToggleEditPostDialog,
	UpdateRatingAsync,
} from "@store/Posts/Actions";
import PostRatingDtoModel from "@models/PostRatingDto";
import { NSFWConstant, PostBodyConstants } from "@helpers/ibbs.constants";
import SpotlightCard from "@animations/SpotlightCard";
import { useAppDispatch, useAppSelector } from "@/index";

/**
 * @component
 * `PostBody` component to display a singular posts.
 *
 * @param param0 The props passed from parent component.
 * @param param0.post The posts data.
 *
 * @returns The post jsx element.
 */
export default function PostBody({ post }: { post: any }) {
	const contentRef: any = useRef(null);
	const styles = useStyles();
	const dispatch = useAppDispatch();
	const { user, isAuthenticated, getIdTokenClaims } = useAuth0();

	const { ButtonText } = PostBodyConstants;

	const UpdatedRatingData = useAppSelector(
		(state) => state.PostsReducer.updatedRatingData
	);
	const IsVotingLoaderOn = useAppSelector(
		(state) => state.PostsReducer.isVotingLoaderOn
	);

	const [postData, setPostData]: any = useState({});
	const [showFullText, setShowFullText] = useState(false);
	const [isTextOverflowing, setIsTextOverflowing] = useState(false);
	const [showEditAndDelete, setShowEditAndDelete] = useState(false);
	const [postRatingLoader, setPostRatingLoader] = useState(false);
	const [isUserLoggedIn, setIsUserLoggedIn] = useState(false);
	const [postUpdatedRatingData, setPostUpdatedRatingData] = useState(false);

	// #region SIDE EFFECTS

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
		if (isAuthenticated && user) {
			const userEmail = user.email;
			const isOwner = post.postOwnerUserName === userEmail;

			setShowEditAndDelete(isOwner);
			setIsUserLoggedIn(true);
		} else {
			setShowEditAndDelete(false);
			setIsUserLoggedIn(false);
		}
	}, [user, isAuthenticated, postData]);

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

	// #endregion

	/**
	 * Gets the access token silently using msal.
	 * @returns The access token.
	 */
	const getAccessToken = async () => {
		try {
			const token = await getIdTokenClaims();
			return token?.__raw;
		} catch (error) {
			console.error(error);
			return null;
		}
	};

	/**
	 * Formats the date to date string format.
	 * @param date The unformatted date
	 * @returns The formatted date.
	 */
	const formatDate = (date: Date) => {
		return new Date(date).toDateString();
	};

	/**
	 * Handle the toggle event for text.
	 */
	const handleToggleText = () => {
		setShowFullText(!showFullText);
	};

	/**
	 * Handles the post edit event.
	 * @param postData The post data to be edited.
	 */
	const handleEdit = (postData: any) => {
		dispatch(ToggleEditPostDialog(true));
		dispatch(GetEditPostData(postData));
	};

	/**
	 * Handles the post delete operation.
	 * @param postId The post id.
	 */
	const handleDelete = async (postId: string) => {
		const accessToken = await getAccessToken();
		accessToken && dispatch(DeletePostAsync(postId, accessToken));
	};

	/**
	 * Handles the post voting event.
	 * @param postId The post id.
	 */
	const handleVoting = async (postId: string) => {
		const accessToken = await getAccessToken();
		const postRatingDtoModel = new PostRatingDtoModel(postId, false);
		accessToken &&
			dispatch(UpdateRatingAsync(postRatingDtoModel, accessToken));
	};

	/**
	 * Handles the rating button icons rendering.
	 * @param button The button variant.
	 * @returns The post rating button element.
	 */
	const renderRatingButtonIcons = (button: any) => {
		return postRatingLoader ? <Spinner size="tiny" /> : <>{button}</>;
	};

	/**
	 * Renders the card buttons and tags.
	 * @returns The tags and buttons element.
	 */
	const renderCardButtonsAndTags = () => {
		const isNsfwContent = postData?.isNsfw;
		const tagData = postData?.genreTag;

		return (
			<div className={styles.bottomContainer}>
				<div className={styles.buttonContainer}>
					{(isTextOverflowing || showFullText) && (
						<Button
							className={styles.textSizeButton}
							onClick={handleToggleText}
						>
							{showFullText ? "Show Less" : "Show More"}
						</Button>
					)}
				</div>
				<TagGroup>
					{isNsfwContent && (
						<Tag className={styles.nsfwTag}>{NSFWConstant}</Tag>
					)}
					{tagData && (
						<Tag className={styles.genreTag}>{tagData}</Tag>
					)}
				</TagGroup>
			</div>
		);
	};

	return (
		Object.keys(postData).length > 0 && (
			<SpotlightCard
				className={styles.card}
				spotlightColor="rgba(0, 229, 255, 0.2)"
			>
				<CardHeader
					className={styles.cardHeader}
					header={
						<div className={styles.headerContainer}>
							<Body1 className={styles.headerTitle}>
								<b>{postData.postTitle}</b>
							</Body1>

							{/* RATINGS BUTTON */}
							<div className={styles.headerButtons}>
								{!showEditAndDelete && isUserLoggedIn && (
									<Tooltip
										content={
											postData.ratingValue === 1
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
											size="small"
											className={styles.starButton}
										>
											{renderRatingButtonIcons(
												postData.ratingValue === 1 ? (
													<StarColor fontSize={28} />
												) : (
													<Star28Regular />
												)
											)}
											&nbsp;
											{postData.ratings > 0 && (
												<Badge
													appearance="outline"
													color="success"
													size="large"
												>
													{postData.ratings}
												</Badge>
											)}
										</Button>
									</Tooltip>
								)}

								{/* EDIT AND DELETE BUTTONS */}
								{showEditAndDelete && isUserLoggedIn && (
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
													handleEdit(postData)
												}
												size="small"
											>
												<EditColor fontSize={28} />
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
												size="small"
											>
												<DismissCircleColor
													fontSize={28}
												/>
											</Button>
										</Tooltip>
									</>
								)}
							</div>
						</div>
					}
					description={
						<Caption1>
							<Subtitle2>{postData.postOwnerUserName}</Subtitle2>{" "}
							on{" "}
							<Subtitle2>
								{formatDate(postData.postCreatedDate)}
							</Subtitle2>
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
								maxHeight: showFullText ? "none" : "50px",
							}}
							dangerouslySetInnerHTML={{
								__html: postData.postContent
									.replace(/\n/g, "<br>")
									.replace(/<br\s*\/?>/g, "<br>"),
							}}
						/>
						{renderCardButtonsAndTags()}
					</Body2>
				</CardPreview>
			</SpotlightCard>
		)
	);
}
