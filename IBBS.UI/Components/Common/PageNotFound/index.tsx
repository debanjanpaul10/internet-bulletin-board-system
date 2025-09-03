import { ErrorPageConstants } from "@helpers/ibbs.constants";
import { useStyles } from "./styles";
import Noise from "@animations/Noise";
import BlurText from "@animations/BlurText";

/**
 * @component
 * `PageNotFound` error component that is shown when an invalid url is accessed.
 * @returns The page not found component JSX element.
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
