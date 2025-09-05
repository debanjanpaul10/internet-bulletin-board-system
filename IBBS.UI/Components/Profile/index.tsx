import { useEffect, useState } from "react";
import { useAuth0 } from "@auth0/auth0-react";
import { Skeleton, SkeletonItem } from "@fluentui/react-components";

import { GetUserProfileDataAsync } from "@store/Users/Actions";
import { useStyles } from "./styles";
import { MyProfilePageConstants } from "@helpers/ibbs.constants";
import AlienImage from "@assets/Images/alien-pfp.jpg";
import PageNotFound from "@/Components/Common/PageNotFound";
import Magnet from "@animations/Magnet";
import { useAppDispatch, useAppSelector } from "@/index";
import UserDetailsComponent from "./Components/UserDetails";
import UserPostsComponent from "./Components/UserPosts";
import UserRatingsComponent from "./Components/UserRatings";

/**
 * @component
 * `ProfileComponent` Renders the user's profile page, displaying their personal information,
 * activity (posts and ratings), and handling authentication states.
 *
 * This component integrates with Auth0 for user authentication.
 * On successful login, it fetches and displays the user's display name,
 * email address, username, their posts, and their ratings.
 * It shows a skeleton loading UI while data is being fetched.
 * If the user is not logged in, it renders a PageNotFound component.
 */
export default function ProfileComponent() {
	const dispatch = useAppDispatch();
	const styles = useStyles();
	const { user, isAuthenticated, getIdTokenClaims } = useAuth0();
	const { Headings } = MyProfilePageConstants;

	const UserProfileStoreData = useAppSelector(
		(state) => state.UserReducer.userProfileData
	);
	const IsUserProfileDataLoadingStoreData = useAppSelector(
		(state) => state.UserReducer.isUserProfileDataLoading
	);

	const [userStateData, setUserStateData] = useState({
		displayName: "",
		emailAddress: "",
		userName: "",
		userPosts: [],
		userPostRatings: [],
	});
	const [isUserDataLoading, setIsUserDataLoading] = useState(true);
	const [_, setCurrentLoggedInUser] = useState({});

	// #region SIDE EFFECTS

	useEffect(() => {
		const getProfileData = async () => {
			const accessToken = await getAccessToken();
			accessToken && dispatch(GetUserProfileDataAsync(accessToken));
		};
		isUserLoggedIn() && getProfileData();
	}, []);

	useEffect(() => {
		if (
			UserProfileStoreData !== null &&
			UserProfileStoreData !== undefined &&
			Object.values(UserProfileStoreData).length > 0 &&
			UserProfileStoreData !== userStateData
		) {
			setUserStateData(UserProfileStoreData);
		}
	}, [UserProfileStoreData]);

	useEffect(() => {
		if (isUserDataLoading !== IsUserProfileDataLoadingStoreData) {
			setIsUserDataLoading(IsUserProfileDataLoadingStoreData);
		}
	}, [IsUserProfileDataLoadingStoreData]);

	useEffect(() => {
		if (isAuthenticated && user) {
			setCurrentLoggedInUser(user);
		}
	}, [isAuthenticated, user]);

	/**
	 * Checks if user is logged in.
	 * @returns The boolean value for logged in status.
	 */
	const isUserLoggedIn = () => {
		return isAuthenticated && user;
	};

	// #endregion

	/**
	 * Gets the access token silently using Auth0.
	 * @returns The access token.
	 */
	const getAccessToken = async () => {
		try {
			const token = await getIdTokenClaims();
			return token?.__raw;
		} catch (error) {
			console.error("Error getting access token:", error);
			return null;
		}
	};

	return isUserLoggedIn() ? (
		<div
			className="container"
			style={{ marginTop: "76px", paddingTop: "20px" }}
		>
			<div className="row">
				<div className="col-sm-12">
					<h1 className={styles.profileHeading}>
						{Headings.WelcomeMessage}
					</h1>
				</div>

				{/* USER DETAILS */}
				<div
					className="row position-relative"
					style={{ marginTop: "20px" }}
				>
					{isUserDataLoading || !userStateData?.displayName ? (
						<Skeleton
							aria-label="Profile data loading"
							as="div"
							className="row"
						>
							<div className="col-12 col-sm-2">
								<SkeletonItem
									appearance="translucent"
									animation="pulse"
									as="div"
									shape="circle"
									className={styles.userImgSkeleton}
								></SkeletonItem>
							</div>
							<div className="col-12 col-sm-10">
								<SkeletonItem
									className={styles.userDataSkeleton}
									appearance="translucent"
									animation="pulse"
									as="div"
								/>
							</div>
						</Skeleton>
					) : (
						<div className="row">
							<div className="col-12 col-sm-2">
								<div className={styles.imageContainer}>
									<Magnet disabled={false} magnetStrength={5}>
										<img
											src={AlienImage}
											alt="Profile"
											className={styles.profileImage}
										/>
									</Magnet>
								</div>
							</div>
							<div className="col-12 col-sm-10">
								<div className={styles.userDetailsContainer}>
									<UserDetailsComponent
										displayName={userStateData.displayName}
										emailAddress={
											userStateData.emailAddress
										}
										userName={userStateData.userName}
									/>
								</div>
							</div>
						</div>
					)}
				</div>

				{/* USER ACTIVITY */}
				<div className="row position-relative mt-4">
					{/* USER POSTS */}
					{isUserDataLoading || !userStateData?.userPosts ? (
						<div className="col-12 col-sm-6">
							<Skeleton>
								<SkeletonItem
									className={styles.userDataSkeleton}
									appearance="translucent"
									animation="pulse"
									as="div"
								/>
							</Skeleton>
						</div>
					) : (
						<div className="col-12 col-sm-6">
							<div className={styles.userPostsContainer}>
								<UserPostsComponent
									userPosts={userStateData.userPosts}
								/>
							</div>
						</div>
					)}

					{/* USER RATINGS */}
					{isUserDataLoading || !userStateData?.userPostRatings ? (
						<div className="cl-12 col-sm-6">
							<Skeleton
								aria-label="Profile data loading"
								as="div"
								className="row"
							>
								<SkeletonItem
									appearance="translucent"
									animation="pulse"
									as="div"
									className={styles.userImgSkeleton}
								></SkeletonItem>
							</Skeleton>
						</div>
					) : (
						<div className="col-12 col-sm-6">
							<div className={styles.userRatingsContainer}>
								<UserRatingsComponent
									userPostRatings={
										userStateData.userPostRatings
									}
								/>
							</div>
						</div>
					)}
				</div>
			</div>
		</div>
	) : (
		<PageNotFound />
	);
}
