import React, { useEffect, useState, useContext } from "react";
import { useDispatch, useSelector } from "react-redux";
import { NavLink, useLocation } from "react-router-dom";
import Cookies from "js-cookie";

import {
	CookiesConstants,
	HeaderPageConstants,
	HomePageConstants,
	PageConstants,
} from "@helpers/Constants";
import AppLogo from "../../../Images/IBBS_logo.png";
import { RemoveCurrentLoggedInUserData } from "@store/Users/Actions";
import { CustomDarkModeToggleSwitch } from "@helpers/CommonUtility";
import ThemeContext from "@context/ThemeContext";

/**
 * @component
 * Header component that renders the navigation bar.
 *
 * @returns {JSX.Element} The rendered component.
 */
function Header() {
	const dispatch = useDispatch();
	const location = useLocation();
	const { themeMode, toggleThemeMode } = useContext(ThemeContext);

	const activeStyle = { color: "#F15B2A" };
	const { Headings } = HeaderPageConstants;
	const { ButtonTitles } = HeaderPageConstants;

	const UserStoreData = useSelector((state) => state.UsersReducer.userData);

	const [currentLoggedInUser, setCurrentLoggedInUser] = useState({});

	useEffect(() => {
		const currentLoggedInUserCookies = Cookies.get(
			CookiesConstants.LoggedInUser.Name
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
	 * Handles the user logout event.
	 */
	const handleLogout = () => {
		dispatch(RemoveCurrentLoggedInUserData());
		Cookies.remove(CookiesConstants.LoggedInUser.Name);
		setCurrentLoggedInUser({});
	};

	/**
	 * Checks if user logged in.
	 * @returns {boolean} The boolean value of user login.
	 */
	const isUserLoggedIn = () => {
		return Object.keys(currentLoggedInUser).length > 0;
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
								activeStyle={activeStyle}
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
							to={Headings.Login.Link}
							activeStyle={activeStyle}
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

					{!isUserLoggedIn() ? (
						<NavLink
							to={Headings.Register.Link}
							activeStyle={activeStyle}
							className="nav-link"
							title={ButtonTitles.Register}
						>
							{Headings.Register.Name}
						</NavLink>
					) : (
						<NavLink
							to={Headings.MyProfile.Link}
							className="nav-link buttonStyle"
							activeStyle={activeStyle}
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
