import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { NavLink } from "react-router-dom";
import Cookies from "js-cookie";

import { CookiesConstants, HeaderPageConstants } from "@helpers/Constants";
import AppLogo from "@assets/IBBS_logo.png";
import { RemoveCurrentLoggedInUserData } from "@store/Users/Actions";

/**
 * @component
 * Header component that renders the navigation bar.
 *
 * @returns {JSX.Element} The rendered component.
 */
function Header() {
	const dispatch = useDispatch();

	const activeStyle = { color: "#F15B2A" };
	const { Headings } = HeaderPageConstants;
	const { ButtonTitles } = HeaderPageConstants;

	const UserStoreData = useSelector((state) => state.UsersReducer.userData);

	const [isDarkMode, setIsDarkMode] = useState(false);
	const [currentLoggedInUser, setCurrentLoggedInUser] = useState({});

	useEffect(() => {
		const savedDarkModeSettings =
			Cookies.get(CookiesConstants.IsDarkModeCookie) === "true";
		setIsDarkMode(savedDarkModeSettings);
		document.body.classList.toggle("dark-mode", savedDarkModeSettings);

		const currentLoggedInUserCookies = Cookies.get(
			CookiesConstants.CurrentLoggedInUserCookie
		);
		if (
			currentLoggedInUserCookies !== "" &&
			currentLoggedInUserCookies !== undefined
		) {
			setCurrentLoggedInUser(JSON.parse(currentLoggedInUserCookies));
		}
	}, []);

	useEffect(() => {
		if (
			UserStoreData &&
			Object.keys(UserStoreData).length > 0 &&
			currentLoggedInUser !== UserStoreData
		) {
			setCurrentLoggedInUser(UserStoreData);
		}
	}, [UserStoreData, currentLoggedInUser]);

	/**
	 * Handles the dark mode - light moddle toggle.
	 */
	const toggleDarkMode = () => {
		const newDarkMode = !isDarkMode;
		setIsDarkMode(newDarkMode);
		document.body.classList.toggle("dark-mode", newDarkMode);
		Cookies.set(CookiesConstants.IsDarkModeCookie, newDarkMode, {
			expires: 120,
		});
	};

	/**
	 * Handles the icon rendering.
	 * @param {bool} isDarkMode The boolean flag for dark mode.
	 * @returns {string} The icon props classes.
	 */
	const handleShowIcon = (isDarkMode) => {
		var extraIcon = isDarkMode
			? "fa fa-sun-o lightgrey"
			: "fa fa-moon-o lightgrey";
		return `buttonStyle ${extraIcon} p-2 mt-2`;
	};

	/**
	 * Handles the user logout event.
	 */
	const handleLogout = () => {
		dispatch(RemoveCurrentLoggedInUserData());
		Cookies.remove(CookiesConstants.CurrentLoggedInUserCookie);
		setCurrentLoggedInUser({});
	};

	return (
		<nav className="navbar navbar-expand-lg navbar-dark bg-dark p-2">
			<div className="navbar-nav">
				<NavLink
					to={Headings.Home.Link}
					className="nav-link"
					title={ButtonTitles.HomeButton}
				>
					<img src={AppLogo} height={"30px"} />
				</NavLink>
			</div>
			<div className="navbar-nav ml-right">
				{Object.keys(currentLoggedInUser).length === 0 ? (
					<NavLink
						to={Headings.Login.Link}
						activeStyle={activeStyle}
						className="nav-link"
						title={ButtonTitles.Login}
					>
						{Headings.Login.Name}
					</NavLink>
				) : (
					<button
						className="nav-link buttonStyle mt-0"
						onClick={handleLogout}
						title={ButtonTitles.Logout}
					>
						Logout
					</button>
				)}

				<NavLink
					to={Headings.Register.Link}
					activeStyle={activeStyle}
					className="nav-link"
					title={ButtonTitles.Register}
				>
					{Headings.Register.Name}
				</NavLink>

				<i
					onClick={toggleDarkMode}
					className={handleShowIcon(isDarkMode)}
					title={
						isDarkMode
							? ButtonTitles.TurnOnLight
							: ButtonTitles.TurnOnDark
					}
				></i>
			</div>
		</nav>
	);
}

export default Header;
