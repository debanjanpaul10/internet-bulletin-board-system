import React from "react";
import { Spinner as FluentSpinner } from "@fluentui/react-components";

import AppLogo from "@assets/Images/IBBS_logo.png";
import { useStyles } from "./styles";

/**
 * @component
 * The Spinner Component used when pages are loading. Is a blocking action.
 *
 * @param {Object} props - The component props.
 * @param {boolean} props.isLoading - The boolean for loading screen.
 *
 * @returns {JSX.Element}
 */
function Spinner({ isLoading }) {
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
