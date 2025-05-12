import { useEffect, useRef, useState } from "react";
import {
	Body1,
	Caption1,
	Card,
	CardHeader,
	CardPreview,
	Body2,
	Button,
} from "@fluentui/react-components";
import {
	Edit28Filled,
	Delete28Filled,
	ArrowCircleUp28Regular,
} from "@fluentui/react-icons";

import { useStyles } from "@components/Posts/PostBody/styles";
import { useAuth0 } from "@auth0/auth0-react";
import { useDispatch } from "react-redux";
import { DeletePostAsync } from "@store/Posts/Actions";

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
	const { getIdTokenClaims, user, isAuthenticated, isLoading } = useAuth0();
	const dispatch = useDispatch();

	const [postData, setPostData] = useState({});
	const [showFullText, setShowFullText] = useState(false);
	const [isTextOverflowing, setIsTextOverflowing] = useState(false);
	const [showEditAndDelete, setShowEditAndDelete] = useState(false);

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
		if (
			user !== null &&
			user !== undefined &&
			Object.values(user).length > 0 &&
			isAuthenticated &&
			!isLoading
		) {
			setShowEditAndDelete(post.postOwnerUserName == user.username);
		} else {
			setShowEditAndDelete(false);
		}
	}, [user, isAuthenticated, isLoading]);

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
	const handleDelete = (postId) => {
		dispatch(DeletePostAsync(postId, getIdTokenClaims));
	};

	const handleVoting = (postId) => {};

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
								{!showEditAndDelete && (
									<Button
										className={styles.upVoteButton}
										appearance="subtle"
										shape="circular"
										onClick={handleVoting(postData.postId)}
									>
										<ArrowCircleUp28Regular />
									</Button>
								)}
								{showEditAndDelete && (
									<>
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
										<Button
											className={styles.deleteButton}
											appearance="subtle"
											shape="circular"
											onClick={() =>
												handleDelete(postData.postId)
											}
										>
											<Delete28Filled />
										</Button>
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
