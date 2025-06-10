import React from "react";
import { LargeTitle } from "@fluentui/react-components";

import { ErrorPageConstants } from "@helpers/ibbs.constants";
import { useStyles } from "./styles";
import Noise from "@components/Animations/Noise";
import BlurText from "@components/Animations/BlurText";

/**
 * @component
 * The Page Not Found error component that is shown when an
 * invalid url is accessed.
 * @returns {JSX.Element} The page not found component JSX element.
 */
function PageNotFound() {
    const styles = useStyles();

    return (
        <div className={styles.noiseDiv}>
            <Noise
                patternSize={250}
                patternScaleX={2}
                patternScaleY={2}
                patternRefreshInterval={2}
                patternAlpha={25}
            />
            <BlurText
                text={ErrorPageConstants.PageNotFoundMessage}
                delay={150}
                animateBy="words"
                direction="top"
                className={styles.notFoundHeader}
            />
        </div>
    );
}

export default PageNotFound;
