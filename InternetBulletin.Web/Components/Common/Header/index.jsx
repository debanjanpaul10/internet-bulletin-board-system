import React from "react";
import { useNavigate } from "react-router-dom";
import { BugRegular, PanelLeftExpand28Regular } from "@fluentui/react-icons";
import { useDispatch } from "react-redux";
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
import useStyles from "@components/Common/Header/styles";
import { ToggleSideBar } from "@store/Common/Actions";

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
 * @returns {JSX.Element} A navigation bar containing the application header elements
 */
function HeaderComponent() {
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
     * Handles the bug reporting event.
     */
    const handleBugReportSubmission = () => {
        alert(
            "This feature is being actively worked upon and is not yet ready!"
        );
    };

    return (
        <nav className={styles.navbar}>
            <div className={styles.navContent}>
                {/* SIDE BAR */}
                <div className="navbar-nav mr-auto">
                    <Tooltip
                        content={ButtonTitles.SideDrawer}
                        relationship="label"
                        positioning="after"
                    >
                        <Button
                            appearance="subtle"
                            className={styles.sideBarButton}
                            onClick={toggleSideBar}
                            {...restoreFocusTargetAttributes}
                        >
                            <PanelLeftExpand28Regular />
                        </Button>
                    </Tooltip>
                </div>

                {/* IBBS LOGO */}
                <div className="navbar-nav mx-auto">
                    <Tooltip
                        content={ButtonTitles.HomeButton}
                        relationship="label"
                    >
                        <Button
                            onClick={handleHomePageRedirect}
                            className={styles.homeButton}
                            appearance="subtle"
                        >
                            <img src={AppLogo} height={"30px"} />
                            &nbsp; {HomePageConstants.Headings.IBBS}
                        </Button>
                    </Tooltip>
                </div>

                {/* REPORT A BUG */}
                <div className="navbar-nav mr-auto">
                    <Tooltip
                        content={ButtonTitles.BugButton}
                        relationship="label"
                    >
                        <Button
                            onClick={handleBugReportSubmission}
                            appearance="subtle"
                            className={styles.bugButton}
                        >
                            <BugRegular fontSize={30} />
                        </Button>
                    </Tooltip>
                </div>
            </div>
        </nav>
    );
}

export default HeaderComponent;
