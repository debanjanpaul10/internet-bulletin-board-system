import React, { useEffect, useState } from "react";
import { useSelector } from "react-redux";

import PostBody from "@components/Posts/Components/PostBody";
import NoPostsContainer from "@components/Posts/Components/NoPosts";
import AnimatedContent from "@components/Animations/AnimatedFadeInContent";
import { useStyles } from "./styles";

/**
 * @component
 * PostsContainer component to display a list of posts.
 *
 * @returns {JSX.Element} The posts container jsx element.
 */
function PostsContainer() {
    const styles = useStyles();

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
        <div className={"container"}>
            {allPosts.length > 0 ? (
                <div className="row">
                    {allPosts.map((post, index) => (
                        <div className="col-md-6 mb-4" key={index}>
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
            ) : (
                <NoPostsContainer />
            )}
        </div>
    );
}

export default PostsContainer;
