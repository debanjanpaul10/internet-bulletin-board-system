import React, { useEffect, useState, useContext } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { AddCircle32Filled } from "@fluentui/react-icons";
import { useMsal } from "@azure/msal-react";
import { useDispatch } from "react-redux";
import { Button, Tooltip } from "@fluentui/react-components";

import {
	HeaderPageConstants,
	HomePageConstants,
	LoginPageConstants,
	PageConstants,
} from "@helpers/ibbs.constants";
import AppLogo from "@assets/Images/IBBS_logo.png";
import { CustomDarkModeToggleSwitch } from "@helpers/common.utility";
import ThemeContext from "@context/ThemeContext";
import useStyles from "@components/Common/Header/styles";
import { loginRequests } from "@services/auth.config";
import {
	ToggleErrorToaster,
	ToggleSuccessToaster,
} from "@store/Common/Actions";
import { StartLoader, StopLoader } from "@store/Posts/Actions";

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
	const { instance, accounts } = useMsal();
	const navigate = useNavigate();
	const styles = useStyles();

	const { Headings, ButtonTitles } = HeaderPageConstants;

	const [currentLoggedInUser, setCurrentLoggedInUser] = useState({});

	useEffect(() => {
		if (accounts.length > 0) {
			const userName = accounts[0].idTokenClaims?.extension_UserName;
			setCurrentLoggedInUser(userName);
		} else {
			setCurrentLoggedInUser();
		}
	}, [instance, accounts]);

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
	 * Gets the access token silently using msal.
	 * @returns {string} The access token.
	 */
	const getAccessToken = async () => {
		const tokenData = await instance.acquireTokenSilent({
			...loginRequests,
			account: accounts[0],
		});

		return tokenData.accessToken;
	};

	/**
	 * Handles the login event.
	 */
	const handleLoginEvent = () => {
		dispatch(StartLoader());
		instance
			.loginRedirect(loginRequests)
			.then(async () => {
				await handleLoginSuccess();
			})
			.catch((error) => {
				dispatch(
					ToggleErrorToaster({
						shouldShow: true,
						errorMessage: error,
					})
				);
				console.error(error);
			})
			.finally(() => {
				dispatch(StopLoader());
			});
	};

	/**
	 * Handles the successful login event.
	 */
	async function handleLoginSuccess() {
		dispatch(
			ToggleSuccessToaster({
				shouldShow: true,
				successMessage: LoginPageConstants.LoginSuccess,
			})
		);

		let token = "";
		if (accounts.length > 0) {
			token = await getAccessToken();
		}
		dispatch(GetAllPostsAsync(token));
	}

	/**
	 * Handles the user logout event.
	 */
	const handleLogout = () => {
		dispatch(StartLoader());
		instance
			.logoutRedirect({
				postLogoutRedirectUri: window.location.origin,
			})
			.then(() => {
				dispatch(
					ToggleSuccessToaster({
						shouldShow: true,
						successMessage: LoginPageConstants.LogoutSuccess,
					})
				);
			})
			.catch((error) => {
				dispatch(
					ToggleErrorToaster({
						shouldShow: true,
						errorMessage: error,
					})
				);
				console.error(error);
			})
			.finally(() => {
				dispatch(StopLoader());
			});
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

				<div className="navbar-nav mx-auto">
					{isUserLoggedIn() &&
						location.pathname !== Headings.CreatePost.Link && (
							<Tooltip
								content={ButtonTitles.Create}
								relationship="label"
							>
								<Button
									onClick={handleAddNewPostPageRedirect}
									className="create-link"
									appearance="transparent"
								>
									<AddCircle32Filled className="icon-large" />
									&nbsp;
									<span className="create-text">
										{ButtonTitles.Create}
									</span>
								</Button>
							</Tooltip>
						)}
				</div>

				<div className="navbar-nav ml-auto">
					{!isUserLoggedIn() ? (
						<Tooltip
							content={ButtonTitles.Login}
							relationship="label"
						>
							<Button
								className={styles.button}
								onClick={handleLoginEvent}
								shape="circular"
								appearance="outline"
							>
								{Headings.Login.Name}
							</Button>
						</Tooltip>
					) : (
						<Tooltip
							content={ButtonTitles.Logout}
							relationship="label"
						>
							<Button
								className={styles.logoutButton}
								onClick={handleLogout}
								shape="circular"
								appearance="outline"
							>
								{Headings.Logout.Name}
							</Button>
						</Tooltip>
					)}

					{isUserLoggedIn() && (
						<Tooltip
							content={ButtonTitles.MyProfile}
							relationship="label"
						>
							<Button
								className={styles.button}
								onClick={handleProfileRedirect}
								shape="circular"
								appearance="outline"
							>
								{Headings.MyProfile.Name}
							</Button>
						</Tooltip>
					)}

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
							<CustomDarkModeToggleSwitch
								onChange={toggleThemeMode}
								checked={
									themeMode === PageConstants.LightConstant
								}
							/>
						</Tooltip>
					</div>
				</div>
			</div>
		</nav>
	);
}

export default Header;
