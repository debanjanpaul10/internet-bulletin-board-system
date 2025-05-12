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

import { useStyles } from "@components/Posts/PostBody/styles";

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

	const [postData, setPostData] = useState({});
	const [showFullText, setShowFullText] = useState(false);
	const [isTextOverflowing, setIsTextOverflowing] = useState(false);

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

	return (
		Object.keys(postData).length > 0 && (
			<Card className={styles.card} appearance="filled-alternative">
				<CardHeader
					className={styles.cardHeader}
					header={
						<Body1>
							<b>{postData.postTitle}</b>
						</Body1>
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
									.replace(/\n/g, "<br>") // Replace \n with <br>
									.replace(/<br\s*\/?>/g, "<br>"), // Normalize <br /> to <br>
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
