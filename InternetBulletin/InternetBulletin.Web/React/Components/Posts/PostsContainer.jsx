import React, { useEffect, useState } from "react";
import { useSelector } from "react-redux";

import Post from "@components/Posts/Post";

function PostsContainer() {
	const AllPostsStoreData = useSelector(
		(state) => state.PostsReducer.allPostsData
	);

	const [allPosts, setAllPosts] = useState([]);

	useEffect(() => {
		if (allPosts !== AllPostsStoreData) {
			setAllPosts(AllPostsStoreData);
		}
	}, [AllPostsStoreData]);

	return (
		Object.keys(allPosts).length > 0 && (
			<>
				{allPosts.map((post) => (
					<Post key={post.PostId} post={post} />
				))}
			</>
		)
	);
}

export default PostsContainer;
