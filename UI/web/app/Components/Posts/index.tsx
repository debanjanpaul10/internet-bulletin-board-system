import { useEffect, useState } from "react";

import PostBody from "@components/posts/components/post-body";
import NoPostsContainer from "@components/posts/components/no-post";
import AnimatedContent from "@animations/animated-fadein-content/index.tsx";
import { useAppSelector } from "@/index";
import { Post } from "@models/interfaces/IPost.js";
import { LandingPageConstants } from "@helpers/ibbs.constants";
import { useStyles } from "./styles.ts";

/**
 * PostsContainer component to display a list of posts.
 */
export default function PostsContainer() {
	const styles = useStyles();

	const AllPostsStoreData = useAppSelector(
		(state) => state.PostsReducer.allPostsData,
	);
	const IsPostsDataLoading = useAppSelector(
		(state) => state.PostsReducer.isPostsDataLoading,
	);

	const [allPosts, setAllPosts] = useState<Post[]>([]);

	useEffect(() => {
		if (allPosts !== AllPostsStoreData) {
			let sortedData = [...AllPostsStoreData].sort(
				(a, b) =>
					new Date(b.postCreatedDate).getTime() -
					new Date(a.postCreatedDate).getTime(),
			);
			setAllPosts(sortedData);
		}
	}, [AllPostsStoreData]);

	if (IsPostsDataLoading) {
		return <div className="container"></div>;
	}

	return (
		<div className="posts-container">
			{allPosts.length > 0 ? (
				<>
					<div className={styles.postsHeading}>
						<h1 className={styles.postsTitle}>
							{
								LandingPageConstants.PostsContainerConstants
									.DiscoverMessageConstant
							}
						</h1>
					</div>
					<div className="posts-grid">
						{allPosts.map((post) => (
							<div
								className="post-item"
								key={post.postCreatedDate}
							>
								<AnimatedContent
									distance={150}
									direction="vertical"
									reverse={false}
									duration={1.2}
									ease="power3.out"
									initialOpacity={0.2}
									animateOpacity
									scale={1.1}
									threshold={0.2}
									delay={0.3}
								>
									<PostBody post={post} />
								</AnimatedContent>
							</div>
						))}
					</div>
				</>
			) : (
				<NoPostsContainer />
			)}
		</div>
	);
}
