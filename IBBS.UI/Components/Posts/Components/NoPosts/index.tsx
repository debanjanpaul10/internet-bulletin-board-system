import { LargeTitle, Body1, Button } from "@fluentui/react-components";
import { AddRegular, PeopleRegular } from "@fluentui/react-icons";
import { useNavigate } from "react-router-dom";
import { useAuth0 } from "@auth0/auth0-react";

import {
    NoPostsMessageConstant,
    HeaderPageConstants,
} from "@helpers/ibbs.constants";
import DecryptedText from "@animations/DecryptedText";
import { useStyles } from "./styles";

/**
 * @component
 * `NoPostsContainer` Displays an engaging empty state when there are no posts.
 *
 * @returns The No Posts container JSX element.
 */
export default function NoPostsContainer() {
    const styles = useStyles();
    const navigate = useNavigate();
    const { isAuthenticated, loginWithRedirect } = useAuth0();

    const handleCreatePost = (): void => {
        if (isAuthenticated) {
            navigate(HeaderPageConstants.Headings.CreatePost.Link);
        } else {
            loginWithRedirect();
        }
    };

    return (
        <div className={styles.noPostsContainer}>
            <div className={styles.emptyStateIcon}>
                <PeopleRegular />
            </div>

            <LargeTitle className={styles.noPostsHeading}>
                <DecryptedText
                    text={NoPostsMessageConstant.Heading}
                    animateOn="view"
                    revealDirection="center"
                />
            </LargeTitle>

            <Body1 className={styles.noPostsSubtext}>
                Be the first to share your thoughts and ideas with the
                community. Your voice matters and could spark amazing
                discussions!
            </Body1>

            <div className={styles.actionButtons}>
                <Button
                    appearance="primary"
                    size="large"
                    icon={<AddRegular />}
                    onClick={handleCreatePost}
                    className={styles.createPostButton}
                >
                    <span className="text-dark">Create Your First Post</span>
                </Button>
            </div>
        </div>
    );
}
