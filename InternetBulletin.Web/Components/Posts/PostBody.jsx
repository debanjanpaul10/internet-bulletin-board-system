import { useEffect, useRef, useState } from "react";

/**
 * @component
 * Post component to display a single post.
 *
 * @param {Object} props - The component props.
 * @param {Object} props.post - The post data.
 * @param {string} props.post.postTitle - The title of the post.
 * @param {string} props.post.postCreatedBy - The author of the post.
 * @param {string} props.post.postCreatedDate - The creation date of the post.
 * @param {string} props.post.postContent - The content of the post.
 *
 * @returns {JSX.Element} The post jsx element.
 */
function PostBody({ post }) {
	const contentRef = useRef(null);

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
	 * Formats the date.
	 * @param {String} date The date.
	 * @returns {string} The formatted date.
	 */
	const formatDate = (date) => {
		return new Date(date).toDateString();
	};

    /**
     * Handles the text toggle.
     */
	const handleToggleText = () => {
		setShowFullText(!showFullText);
	};

	return (
		Object.keys(postData).length > 0 && (
			<>
				<div className="card mt-2">
					<h5 className="card-header mb-2">{postData.postTitle}</h5>
					<h6 className="card-title mb-4">
						By {postData.postOwnerUserName} on{" "}
						{formatDate(postData.postCreatedDate)}
					</h6>
					<p
						ref={contentRef}
						className={`post-content ${
							showFullText ? "full-text" : ""
						}`}
						dangerouslySetInnerHTML={{
							__html: postData.postContent.replace(
								/\n/g,
								"<br />"
							),
						}}
					></p>
					{isTextOverflowing && !showFullText && (
						<button
							className="btn btn-link"
							onClick={handleToggleText}
						>
							Show More
						</button>
					)}
					{showFullText && (
						<button
							className="btn btn-link"
							onClick={handleToggleText}
						>
							Show Less
						</button>
					)}
				</div>
				<br />
			</>
		)
	);
}

export default PostBody;
