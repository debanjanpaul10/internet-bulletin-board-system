import { useEffect, useState } from "react";

import PostBody from "@/Components/Posts/Components/PostBody";
import NoPostsContainer from "@/Components/Posts/Components/NoPosts";
import AnimatedContent from "@animations/AnimatedFadeInContent";
import { useAppSelector } from "@/index";
import { Post } from "@/Models/Interfaces/IPost";

/**
 * PostsContainer component to display a list of posts.
 */
export default function PostsContainer() {
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
    }, [AllPostsStoreData, allPosts]);

    if (IsPostsDataLoading) {
        return <div className="container"></div>;
    }

    return (
        <div className="posts-container">
            {allPosts.length > 0 ? (
                <>
                    <div className="posts-header">
                        <h2 className="posts-title">Latest Discussions</h2>
                        <p className="posts-subtitle">
                            Discover what the community is talking about
                        </p>
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
