import React, { useEffect, useState } from "react";

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
	const [postData, setPostData] = useState({});

	useEffect(() => {
		if (postData !== post) {
			setPostData(post);
		}
	}, [post]);

    /**
     * Formats the date.
     * @param {String} date The date.
     * @returns {string} The formatted date.
     */
	const formatDate = (date) => {
		return new Date(date).toDateString();
	};

	return (
		Object.keys(postData).length > 0 && (
			<>
				<div className="card mt-2">
					<h5 className="card-header mb-2">{postData.postTitle}</h5>
					<h6 className="card-title mb-4">
						By {postData.postCreatedBy} on{" "}
						{formatDate(postData.postCreatedDate)}
					</h6>
					<p
						dangerouslySetInnerHTML={{
							__html: postData.postContent.replace(
								/\n/g,
								"<br />"
							),
						}}
					></p>
				</div>
				<br />
			</>
		)
	);
}

export default PostBody;
