import { Spinner as FluentSpinner } from "@fluentui/react-components";

import AppLogo from "@assets/Images/IBBS_logo.png";
import { useStyles } from "./styles";

/**
 * The Spinner Component used when pages are loading. Is a blocking action.
 */
function Spinner({ isLoading = false }) {
    const styles = useStyles();

    return (
        isLoading && (
            <div className={styles.spinnerOverlay}>
                <div className={styles.spinnerContent}>
                    <img
                        src={AppLogo}
                        height={"60px"}
                        className={styles.spinnerLogo}
                        alt="IBBS Logo"
                    />
                    <FluentSpinner size="large" className={styles.spinner} />
                    <p className={styles.loadingText}>
                        Loading amazing content...
                    </p>
                </div>
            </div>
        )
    );
}

export default Spinner;
