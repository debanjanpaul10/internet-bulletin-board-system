import { useNavigate } from "react-router-dom";
import { useDispatch } from "react-redux";
import { BugRegular, Form28Color } from "@fluentui/react-icons";
import {
	Button,
	Tooltip,
	useRestoreFocusTarget,
} from "@fluentui/react-components";

import {
	HeaderPageConstants,
	HomePageConstants,
} from "@helpers/ibbs.constants";
import AppLogo from "@assets/Images/IBBS_logo.png";
import useStyles from "@/Components/Common/Header/styles";
import { ToggleBugReportDrawer, ToggleSideBar } from "@store/Common/Actions";

/**
 * @component
 * `Header` A React component that renders the main navigation header of the application.
 *
 * The component includes:
 * - A sidebar toggle button with tooltip
 * - The IBBS logo that redirects to the home page
 * - A theme toggle button that switches between light and dark modes
 *
 * Features:
 * - Uses Fluent UI components for consistent styling
 * - Implements theme context for dark/light mode switching
 * - Integrates with Redux for sidebar state management
 * - Uses React Router for navigation
 * - Includes tooltips for better user experience
 *
 * @returns A navigation bar containing the application header elements
 */
export default function HeaderComponent() {
	const dispatch = useDispatch();
	const navigate = useNavigate();
	const styles = useStyles();
	const restoreFocusTargetAttributes = useRestoreFocusTarget();

	const { Headings, ButtonTitles } = HeaderPageConstants;

	/**
	 * Handles the home page redirection.
	 */
	const handleHomePageRedirect = () => {
		navigate(Headings.Home.Link);
	};

	/**
	 * Toggles the side car.
	 */
	const toggleSideBar = () => {
		dispatch(ToggleSideBar(true));
	};

	/**
	 * Handles the bug report drawer open event.
	 */
	const openBugReportDrawer = () => {
		dispatch(ToggleBugReportDrawer(true));
	};

	return (
		<nav className={styles.navbar}>
			<div className={styles.navContent}>
				{/* LEFT SIDE - SIDEBAR BUTTON */}
				<div className={styles.leftSection}>
					<Tooltip
						content={ButtonTitles.SideDrawer}
						relationship="label"
						positioning="after"
					>
						<Button
							className={styles.sideBarButton}
							onClick={toggleSideBar}
							{...restoreFocusTargetAttributes}
							icon={<Form28Color />}
						></Button>
					</Tooltip>
				</div>

				{/* CENTER - IBBS LOGO */}
				<div className={styles.centerSection}>
					<Tooltip
						content={ButtonTitles.HomeButton}
						relationship="label"
					>
						<Button
							onClick={handleHomePageRedirect}
							className={styles.homeButton}
							appearance="subtle"
						>
							<img src={AppLogo} height={"24px"} />
							{HomePageConstants.Headings.IBBS}
						</Button>
					</Tooltip>
				</div>

				{/* RIGHT SIDE - BUG REPORT BUTTON */}
				<div className={styles.rightSection}>
					<Tooltip
						content={ButtonTitles.BugButton}
						relationship="label"
					>
						<Button
							className={styles.bugButton}
							onClick={openBugReportDrawer}
							{...restoreFocusTargetAttributes}
							icon={<BugRegular />}
						></Button>
					</Tooltip>
				</div>
			</div>
		</nav>
	);
}
