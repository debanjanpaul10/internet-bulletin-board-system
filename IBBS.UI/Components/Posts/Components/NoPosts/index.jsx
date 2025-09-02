import React from "react";
import { LargeTitle } from "@fluentui/react-components";

import { NoPostsMessageConstant } from "@helpers/ibbs.constants";
import { useStyles } from "./styles";
import DecryptedText from "@animations/DecryptedText";

/**
 * @component
 * `NoPostsContainer` Displays a message when there are no posts.
 *
 * @returns {JSX.Element} The No Posts container JSX element.
 */
function NoPostsContainer() {
    const styles = useStyles();

    return (
        <div className={styles.noPostsContainer}>
            <LargeTitle className={styles.noPostsHeading}>
                <DecryptedText
                    text={NoPostsMessageConstant.Heading}
                    animateOn="view"
                    revealDirection="center"
                />
            </LargeTitle>
        </div>
    );
}

export default NoPostsContainer;
