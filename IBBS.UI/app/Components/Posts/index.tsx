import { useEffect, useState } from "react";

import PostBody from "@components/Posts/Components/PostBody";
import NoPostsContainer from "@components/Posts/Components/NoPosts";
import AnimatedContent from "@animations/AnimatedFadeInContent";
import { useAppSelector } from "@/index";
import { Post } from "@models/Interfaces/IPost";
import { LandingPageConstants } from "@helpers/ibbs.constants";
import { useStyles } from "./styles";

/**
 * PostsContainer component to display a list of posts.
 */
export default function PostsContainer() {
	const styles = useStyles();

	const AllPostsStoreData = useAppSelector(
		(state) => state.PostsReducer.allPostsData
	);
	const IsPostsDataLoading = useAppSelector(
		(state) => state.PostsReducer.isPostsDataLoading
	);

	const [allPosts, setAllPosts] = useState<Post[]>([]);

	useEffect(() => {
		if (allPosts !== AllPostsStoreData) {
			let sortedData = [...AllPostsStoreData].sort(
				(a, b) =>
					new Date(b.postCreatedDate).getTime() -
					new Date(a.postCreatedDate).getTime()
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
						{allPosts.map((post, index) => (
							<div className="post-item" key={index}>
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
