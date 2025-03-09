import React, { useEffect, useState } from "react";
import { useSelector } from "react-redux";

import PostBody from "@components/Posts/PostBody";

/**
 * @component
 * PostsContainer component to display a list of posts.
 *
 * @returns {JSX.Element} The posts container jsx element.
 */
function PostsContainer() {
	const AllPostsStoreData = useSelector(
		(state) => state.PostsReducer.allPostsData
	);

	const [allPosts, setAllPosts] = useState([]);

	useEffect(() => {
		if (allPosts !== AllPostsStoreData) {
			let sortedData = AllPostsStoreData.sort(
				(a, b) =>
					new Date(b.postCreatedDate) - new Date(a.postCreatedDate)
			);
			setAllPosts(sortedData);
		}
	}, [AllPostsStoreData]);

	return (
		Object.keys(allPosts).length > 0 && (
			<div className="container">
				{allPosts.map((post) => (
					<PostBody key={post.PostId} post={post} />
				))}
			</div>
		)
	);
}

export default PostsContainer;
