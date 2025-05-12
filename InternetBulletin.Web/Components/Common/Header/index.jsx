import React, { useEffect, useState, useContext } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { useAuth0 } from "@auth0/auth0-react";
import { AddCircle32Filled } from "@fluentui/react-icons";

import {
	HeaderPageConstants,
	HomePageConstants,
	PageConstants,
} from "@helpers/ibbs.constants";
import AppLogo from "@assets/Images/IBBS_logo.png";
import { CustomDarkModeToggleSwitch } from "@helpers/common.utility";
import ThemeContext from "@context/ThemeContext";
import { Button } from "@fluentui/react-components";
import useStyles from "@components/Common/Header/styles";

/**
 * @component
 * Header component that renders the navigation bar.
 *
 * @returns {JSX.Element} The rendered component.
 */
function Header() {
	const location = useLocation();
	const { themeMode, toggleThemeMode } = useContext(ThemeContext);
	const { loginWithRedirect, logout, user, isAuthenticated, isLoading } =
		useAuth0();
	const navigate = useNavigate();
	const styles = useStyles();

	const { Headings, ButtonTitles } = HeaderPageConstants;

	const [currentLoggedInUser, setCurrentLoggedInUser] = useState({});

	useEffect(() => {
		if (isAuthenticated) {
			setCurrentLoggedInUser(user);
		}

		setCurrentLoggedInUser();
	}, []);

	useEffect(() => {
		if (
			isAuthenticated &&
			Object.keys(user).length > 0 &&
			user !== currentLoggedInUser
		) {
			setCurrentLoggedInUser(user);
		}
	}, [isAuthenticated, user, currentLoggedInUser, isLoading]);

	/**
	 * Handles the user logout event.
	 */
	const handleLogout = () => {
		logout({
			logoutParams: window.location.origin,
		});
	};

	/**
	 * Checks if user logged in.
	 * @returns {boolean} The boolean value of user login.
	 */
	const isUserLoggedIn = () => {
		return (
			currentLoggedInUser !== null &&
			currentLoggedInUser !== undefined &&
			currentLoggedInUser?.username !== ""
		);
	};

	/**
	 * Handles the login event.
	 */
	const handleLoginEvent = () => {
		loginWithRedirect();
	};

	/**
	 * Handles the profile click redirection.
	 */
	const handleProfileRedirect = () => {
		navigate(Headings.MyProfile.Link);
	};

	/**
	 * Handles the home page redirection.
	 */
	const handleHomePageRedirect = () => {
		navigate(Headings.Home.Link);
	};

	/**
	 * Handles the Add new post page redirection.
	 */
	const handleAddNewPostPageRedirect = () => {
		navigate(Headings.CreatePost.Link);
	};

	return (
		<nav className="navbar navbar-expand-lg">
			<div className="d-flex w-100">
				<div className="navbar-nav mr-auto">
					<Button
						onClick={handleHomePageRedirect}
						className={styles.homeButton}
						title={ButtonTitles.HomeButton}
						appearance="subtle"
					>
						<img src={AppLogo} height={"30px"} />
						&nbsp; {HomePageConstants.Headings.IBBS}
					</Button>
				</div>

				<div className="navbar-nav mx-auto">
					{isUserLoggedIn() &&
						location.pathname !== Headings.CreatePost.Link && (
							<Button
								onClick={handleAddNewPostPageRedirect}
								title={ButtonTitles.Create}
								className="create-link"
								appearance="transparent"
							>
								<AddCircle32Filled className="icon-large" />
								&nbsp;
								<span className="create-text">
									{ButtonTitles.Create}
								</span>
							</Button>
						)}
				</div>

				<div className="navbar-nav ml-auto">
					{!isUserLoggedIn() ? (
						<Button
							className={styles.button}
							title={ButtonTitles.Login}
							onClick={handleLoginEvent}
							shape="circular"
							appearance="outline"
						>
							{Headings.Login.Name}
						</Button>
					) : (
						<Button
							className={styles.logoutButton}
							onClick={handleLogout}
							title={ButtonTitles.Logout}
							shape="circular"
							appearance="outline"
						>
							{Headings.Logout.Name}
						</Button>
					)}

					{isUserLoggedIn() && (
						<Button
							className={styles.button}
							title={ButtonTitles.MyProfile}
							onClick={handleProfileRedirect}
							shape="circular"
							appearance="outline"
						>
							{Headings.MyProfile.Name}
						</Button>
					)}

					<div
						className="mr-3 pr-2"
						style={{ marginRight: "10px", marginTop: "5px" }}
					>
						<CustomDarkModeToggleSwitch
							onChange={toggleThemeMode}
							checked={themeMode === PageConstants.LightConstant}
							title={
								themeMode === PageConstants.DarkConstant
									? ButtonTitles.TurnOnLight
									: ButtonTitles.TurnOnDark
							}
						/>
					</div>
				</div>
			</div>
		</nav>
	);
}

export default Header;
