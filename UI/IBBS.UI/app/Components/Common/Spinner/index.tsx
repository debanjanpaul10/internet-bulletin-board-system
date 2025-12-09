import { Spinner as FluentSpinner } from "@fluentui/react-components";

import AppLogo from "@assets/Images/IBBS_logo.png";
import { useStyles } from "./styles";
import { PageConstants } from "@helpers/ibbs.constants";

/**
 * @component
 * The Spinner component.
 * @param param0 The props containing the isLoading value and the message
 * @returns The Spinner JSX element.
 */
function Spinner({
	isLoading = false,
	message = PageConstants.DefaultLoaderMessage,
}) {
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
					<p className={styles.loadingText}>{message}</p>
				</div>
			</div>
		)
	);
}

export default Spinner;
