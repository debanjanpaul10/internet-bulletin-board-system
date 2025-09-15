import { useEffect } from "react";
import {
	Button,
	DrawerBody,
	DrawerHeader,
	DrawerHeaderTitle,
	OverlayDrawer,
	mergeClasses,
	Tooltip,
	useRestoreFocusSource,
} from "@fluentui/react-components";
import {
	SignOut24Regular,
	Dismiss28Filled,
	AddStarburst28Color,
	Person28Color,
	PersonAdd28Color,
} from "@fluentui/react-icons";
import { useAuth0 } from "@auth0/auth0-react";
import { useNavigate } from "react-router-dom";

import { useAppDispatch, useAppSelector } from "@/index";
import { useStyles } from "./styles";
import { ToggleErrorToaster, ToggleSideBar } from "@store/Common/Actions";
import {
	HeaderPageConstants,
	HomePageConstants,
} from "@helpers/ibbs.constants";
import AppLogo from "@assets/Images/IBBS_logo.png";
import {
	GetAllPostsAsync,
	StartLoader,
	StopLoader,
} from "@store/Posts/Actions";
import { DrawerMotion } from "./motion";

/**
 * SideDrawerComponent - A responsive side navigation drawer component that provides
 * navigation and authentication controls for the IBBS application.
 *
 * @component
 * @description
 * This component renders a side drawer that contains:
 * - Application logo and home navigation
 * - Create post button (for logged-in users)
 * - Login/Logout functionality
 * - User profile access
 *
 * The drawer integrates with Auth0 for authentication
 * and uses Redux for state management.
 *
 * @returns A Fluent UI OverlayDrawer component with navigation controls
 *
 * @property {Object} styles - Styles object from useStyles hook
 * @property {Function} navigate - React Router navigation function
 * @property {Function} dispatch - Redux dispatch function
 * @property {Object} restoreFocusSourceAttributes - Focus management attributes
 * @property {Object} user - Auth0 user information
 * @property {boolean} isAuthenticated - Auth0 authentication status
 * @property {boolean} IsSideBarOpen - Redux state for drawer open/close
 * @property {boolean} isSideBarOpenState - Local state for drawer open/close
 * @property {Object} currentLoggedInUser - Current user information
 *
 * @see {@link https://react.fluentui.dev/?path=/docs/components-overlaydrawer--default OverlayDrawer Documentation}
 * @see {@link https://auth0.com/docs/libraries/auth0-react Auth0 React Documentation}
 */
export default function SideDrawerComponent() {
	const styles = useStyles();
	const navigate = useNavigate();
	const dispatch = useAppDispatch();
	const restoreFocusSourceAttributes = useRestoreFocusSource();
	const {
		user,
		isAuthenticated,
		loginWithRedirect,
		logout,
		getIdTokenClaims,
	} = useAuth0();

	const { Headings, ButtonTitles } = HeaderPageConstants;

	const IsSideBarOpen = useAppSelector(
		(state: any) => state.CommonReducer.isSideBarOpen
	);

	useEffect(() => {
		if (isAuthenticated && user) {
			handleLoginSuccess();
		}
	}, [isAuthenticated, user]);

	/**
	 * Gets the access token silently using Auth0.
	 * @returns The access token.
	 */
	const getAccessToken = async () => {
		try {
			const idToken = await getIdTokenClaims();
			return idToken?.__raw;
		} catch (error) {
			console.error(error);
			throw error;
		}
	};

	/**
	 * Checks if user logged in.
	 * @returns {boolean} The boolean value of user login.
	 */
	const isUserLoggedIn = () => {
		return isAuthenticated && user;
	};

	const handleSideBarClose = () => {
		dispatch(ToggleSideBar(false));
	};

	/**
	 * Handles the home page redirection.
	 */
	const handleHomePageRedirect = () => {
		navigate(Headings.Home.Link);
		dispatch(ToggleSideBar(false));
	};

	/**
	 * Handles the login event.
	 */
	const handleLoginEvent = () => {
		dispatch(StartLoader());
		loginWithRedirect()
			.catch((error) => {
				dispatch(
					ToggleErrorToaster({
						shouldShow: true,
						errorMessage: error.message,
					})
				);
				console.error(error);
			})
			.finally(() => {
				dispatch(StopLoader());
			});

		dispatch(ToggleSideBar(false));
	};

	/**
	 * Handles the successful login event.
	 */
	async function handleLoginSuccess() {
		let token: any;
		if (isAuthenticated) {
			token = await getAccessToken();
		}
		dispatch(GetAllPostsAsync(token));
	}

	/**
	 * Handles the user logout event.
	 */
	const handleLogout = () => {
		dispatch(StartLoader());
		logout({
			logoutParams: {
				returnTo: window.location.origin,
			},
		})
			.catch((error) => {
				dispatch(
					ToggleErrorToaster({
						shouldShow: true,
						errorMessage: error.message,
					})
				);
				console.error(error);
			})
			.finally(() => {
				dispatch(StopLoader());
			});
		dispatch(ToggleSideBar(false));
	};

	/**
	 * Handles the profile click redirection.
	 */
	const handleProfileRedirect = async () => {
		navigate(Headings.MyProfile.Link);
		dispatch(ToggleSideBar(false));
	};

	/**
	 * Handles the Add new post page redirection.
	 */
	const handleAddNewPostPageRedirect = () => {
		navigate(Headings.CreatePost.Link);
		dispatch(ToggleSideBar(false));
	};

	return (
		<OverlayDrawer
			{...restoreFocusSourceAttributes}
			as="aside"
			open={IsSideBarOpen}
			onOpenChange={(_, { open }) => {
				dispatch(ToggleSideBar(open));
			}}
			surfaceMotion={{
				children: (_, props) => <DrawerMotion {...props} />,
			}}
			size="small"
		>
			<DrawerHeader className={styles.drawerHeader}>
				<DrawerHeaderTitle
					action={
						<Button
							appearance="subtle"
							aria-label="Close"
							onClick={handleSideBarClose}
							className={styles.closeButton}
							icon={<Dismiss28Filled />}
						/>
					}
				>
					{/* HOME BUTTON */}
					<Button
						onClick={handleHomePageRedirect}
						className={styles.homeButton}
						appearance="subtle"
					>
						<img src={AppLogo} height={"30px"} />
						&nbsp;&nbsp;&nbsp;&nbsp;
						<span className="text-dark">
							{HomePageConstants.Headings.IBBS}
						</span>
					</Button>
				</DrawerHeaderTitle>
			</DrawerHeader>

			<DrawerBody className={styles.drawerBody}>
				{/* CREATE NEW POST */}
				{isUserLoggedIn() && (
					<div className="row">
						{location.pathname !== Headings.CreatePost.Link && (
							<Tooltip
								content={ButtonTitles.Create}
								relationship="label"
								positioning="after"
							>
								<Button
									onClick={handleAddNewPostPageRedirect}
									className={mergeClasses(
										styles.button,
										styles.buttonOverride
									)}
									appearance="transparent"
								>
									<AddStarburst28Color />
									<span>{Headings.CreatePost.Name}</span>
								</Button>
							</Tooltip>
						)}
					</div>
				)}

				{/* MY PROFILE */}
				{isUserLoggedIn() && (
					<div className="row">
						{location.pathname !== Headings.MyProfile.Link && (
							<Tooltip
								content={ButtonTitles.MyProfile}
								relationship="label"
								positioning="after"
							>
								<Button
									className={mergeClasses(
										styles.button,
										styles.buttonOverride
									)}
									onClick={handleProfileRedirect}
									appearance="transparent"
								>
									<Person28Color />
									<span>{Headings.MyProfile.Name}</span>
								</Button>
							</Tooltip>
						)}
					</div>
				)}

				{/* LOGIN AND LOGOUT */}
				<div className="row">
					{!isUserLoggedIn() ? (
						<Tooltip
							content={ButtonTitles.Login}
							relationship="label"
							positioning="after"
						>
							<Button
								className={mergeClasses(
									styles.button,
									styles.buttonOverride
								)}
								onClick={handleLoginEvent}
								appearance="transparent"
							>
								<PersonAdd28Color />
								<span>{Headings.Login.Name}</span>
							</Button>
						</Tooltip>
					) : (
						<Tooltip
							content={ButtonTitles.Logout}
							relationship="label"
							positioning="after"
						>
							<Button
								className={mergeClasses(
									styles.button,
									styles.buttonOverride
								)}
								onClick={handleLogout}
								appearance="transparent"
							>
								<SignOut24Regular
									className={styles.signoutImg}
								/>
								<span>{Headings.Logout.Name}</span>
							</Button>
						</Tooltip>
					)}
				</div>
			</DrawerBody>
		</OverlayDrawer>
	);
}
