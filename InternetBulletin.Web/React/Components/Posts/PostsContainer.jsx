import React, { useEffect, useState } from "react";
import { useSelector } from "react-redux";

import PostBody from "@components/Posts/PostBody";
import NoPostsContainer from "@components/Posts/NoPostsContainer";

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
	const IsPostsDataLoading = useSelector(
		(state) => state.PostsReducer.isPostsDataLoading
	);

	const [allPosts, setAllPosts] = useState([]);

	useEffect(() => {
		if (allPosts !== AllPostsStoreData) {
			let sortedData = [...AllPostsStoreData].sort(
				(a, b) =>
					new Date(b.postCreatedDate) - new Date(a.postCreatedDate)
			);
			setAllPosts(sortedData);
		}
	}, [AllPostsStoreData]);

	if (IsPostsDataLoading) {
		return <div className="container"></div>;
	}

	return (
		<div className="container">
			{allPosts.length > 0 ? (
				allPosts.map((post, index) => (
					<PostBody key={index} post={post} />
				))
			) : (
				<NoPostsContainer />
			)}
		</div>
	);
}

export default PostsContainer;
