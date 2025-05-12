import React, { useEffect, useState, useContext } from "react";
import { NavLink, useLocation } from "react-router-dom";
import { useAuth0 } from "@auth0/auth0-react";

import {
	HeaderPageConstants,
	HomePageConstants,
	PageConstants,
} from "@helpers/ibbs.constants";
import AppLogo from "@assets/Images/IBBS_logo.png";
import { CustomDarkModeToggleSwitch } from "@helpers/common.utility";
import ThemeContext from "@context/ThemeContext";

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

	return (
		<nav className="navbar navbar-expand-lg navbar-dark bg-dark">
			<div className="d-flex w-100">
				<div className="navbar-nav mr-auto">
					<NavLink
						to={Headings.Home.Link}
						className="nav-link"
						title={ButtonTitles.HomeButton}
					>
						<img src={AppLogo} height={"30px"} />
						&nbsp; {HomePageConstants.Headings.IBBS}
					</NavLink>
				</div>

				<div className="navbar-nav mx-auto">
					{isUserLoggedIn() &&
						location.pathname !== Headings.CreatePost.Link && (
							<NavLink
								to={Headings.CreatePost.Link}
								style={({ isActive }) =>
									isActive ? { color: "#F15B2A" } : undefined
								}
								className="nav-link create-link"
								title={ButtonTitles.Create}
							>
								<i className="fa fa-plus-circle icon-large"></i>{" "}
								&nbsp;
								<span className="create-text">
									{ButtonTitles.Create}
								</span>
							</NavLink>
						)}
				</div>

				<div className="navbar-nav ml-auto">
					{!isUserLoggedIn() ? (
						<NavLink
							to={"/"}
							onClick={handleLoginEvent}
							className="nav-link buttonStyle"
							title={ButtonTitles.Login}
						>
							{Headings.Login.Name}
						</NavLink>
					) : (
						<NavLink
							className="nav-link buttonStyle"
							onClick={handleLogout}
							title={ButtonTitles.Logout}
							to={"/"}
						>
							{Headings.Logout.Name}
						</NavLink>
					)}

					{isUserLoggedIn() && (
						<NavLink
							to={Headings.MyProfile.Link}
							className="nav-link buttonStyle"
							title={ButtonTitles.MyProfile}
						>
							{Headings.MyProfile.Name}
						</NavLink>
					)}

					<div
						className="mt-1 mr-3 pr-2"
						style={{ marginRight: "10px" }}
					>
						<CustomDarkModeToggleSwitch
							onChange={toggleThemeMode}
							checked={themeMode === PageConstants.DarkConstant}
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
