import React, { useContext } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { AppFolder32Filled } from "@fluentui/react-icons";
import { useDispatch } from "react-redux";
import {
  Button,
  Tooltip,
  useRestoreFocusTarget,
} from "@fluentui/react-components";

import {
  HeaderPageConstants,
  HomePageConstants,
  PageConstants,
} from "@helpers/ibbs.constants";
import AppLogo from "@assets/Images/IBBS_logo.png";
import ThemeContext from "@context/ThemeContext";
import useStyles from "@components/Common/Header/styles";
import { ToggleSideBar } from "@store/Common/Actions";
import LightModeIcon from "@assets/Images/light-bulb-on.png";
import DarkModeIcon from "@assets/Images/light-bulb-off.png";

/**
 * @component
 * Header component that renders the navigation bar.
 *
 * @returns {JSX.Element} The rendered component.
 */
function Header() {
  const location = useLocation();
  const { themeMode, toggleThemeMode } = useContext(ThemeContext);
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

  return (
    <nav className="navbar navbar-expand-lg">
      <div className="d-flex w-100">
        <div className="navbar-nav mr-auto">
          <Button
            appearance="subtle"
            className={styles.sidecarButton}
            onClick={toggleSideBar}
            {...restoreFocusTargetAttributes}
          >
            <AppFolder32Filled />
          </Button>
        </div>

        {/* IBBS LOGO */}
        <div className="navbar-nav mx-auto">
          <Tooltip content={ButtonTitles.HomeButton} relationship="label">
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

        <div className="navbar-nav ml-auto">
          {/* TOGGLE THEME BUTTON */}
          <div
            className="mr-3 pr-2"
            style={{ marginRight: "10px", marginTop: "5px" }}
          >
            <Tooltip
              content={
                themeMode === PageConstants.DarkConstant
                  ? ButtonTitles.TurnOnLight
                  : ButtonTitles.TurnOnDark
              }
              relationship="label"
            >
              <Button
                className={styles.themeToggleButton}
                onClick={toggleThemeMode}
                appearance="transparent"
              >
                {themeMode === PageConstants.LightConstant ? (
                  <img src={DarkModeIcon} width={40} height={35} />
                ) : (
                  <img src={LightModeIcon} width={40} height={35} />
                )}
              </Button>
            </Tooltip>
          </div>
        </div>
      </div>
    </nav>
  );
}

export default Header;
