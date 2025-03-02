import React from "react";

function Post({ post }) {
	return (
		<>
			<div className="container">
				<div className="row">
					<h1>{post.postTitle}</h1>
				</div>
				<div className="row">
					<p>{post.postContent}</p>
				</div>
			</div>
		</>
	);
}

export default Post;
