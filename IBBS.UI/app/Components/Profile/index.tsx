import { useEffect, useState } from "react";
import { useAuth0, User } from "@auth0/auth0-react";
import { Skeleton, SkeletonItem } from "@fluentui/react-components";

import { GetUserProfileDataAsync } from "@store/Users/Actions";
import { useStyles } from "./styles";
import { MyProfilePageConstants } from "@helpers/ibbs.constants";
import PageNotFound from "@components/Common/PageNotFound";
import Magnet from "@animations/Magnet";
import { useAppDispatch, useAppSelector } from "@/index";
import UserPostsComponent from "./Components/UserPosts";
import UserRatingsComponent from "./Components/UserRatings";
import { UserProfileDto } from "@models/DTOs/user-profile.dto";

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

	const [userStateData, setUserStateData] = useState<UserProfileDto>({
		emailAddress: "",
		userPostRatings: [],
		userPosts: [],
	});
	const [isUserDataLoading, setIsUserDataLoading] = useState(true);
	const [currentLoggedInUser, setCurrentLoggedInUser] = useState<User>({});

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
			console.error(error);
			return null;
		}
	};

	return isUserLoggedIn() ? (
		<div className={styles.profileContainer}>
			{/* Hero Section */}
			<div className={`${styles.heroSection} ${styles.heroSection768}`}>
				<div
					className={`${styles.heroContent} ${styles.heroContent768}`}
				>
					<h1
						className={`${styles.profileHeading} ${styles.profileHeading768} ${styles.profileHeading480}`}
					>
						{Headings.WelcomeMessage}
					</h1>

					{/* Profile Image Section */}
					<div className={styles.profileImageSection}>
						{isUserDataLoading || !userStateData?.emailAddress ? (
							<Skeleton
								aria-label="Profile data loading"
								as="div"
								className={styles.profileImageSkeleton}
							>
								<SkeletonItem
									appearance="translucent"
									animation="pulse"
									as="div"
									shape="circle"
									className={styles.userImgSkeleton}
								/>
							</Skeleton>
						) : (
							<div className={styles.profileImageWrapper}>
								<Magnet disabled={false} magnetStrength={8}>
									<div
										className={`${styles.profileImageContainer} ${styles.profileImageContainer768} ${styles.profileImageContainer480}`}
									>
										<img
											src={currentLoggedInUser.picture}
											alt="Profile"
											className={styles.profileImage}
										/>
										<div
											className={styles.profileImageGlow}
										/>
									</div>
								</Magnet>
							</div>
						)}
					</div>
				</div>
			</div>

			{/* Content Cards Section */}
			<div
				className={`${styles.contentSection} ${styles.contentSection480}`}
			>
				<div
					className={`${styles.cardsGrid} ${styles.cardsGrid768} ${styles.cardsGrid480}`}
				>
					{/* USER POSTS */}
					{isUserDataLoading || !userStateData?.userPosts ? (
						<div className={styles.cardSkeleton}>
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
						<div className={styles.cardWrapper}>
							<UserPostsComponent
								userPosts={userStateData.userPosts}
							/>
						</div>
					)}

					{/* USER RATINGS */}
					{isUserDataLoading || !userStateData?.userPostRatings ? (
						<div className={styles.cardSkeleton}>
							<Skeleton
								aria-label="Profile data loading"
								as="div"
							>
								<SkeletonItem
									appearance="translucent"
									animation="pulse"
									as="div"
									className={styles.userDataSkeleton}
								/>
							</Skeleton>
						</div>
					) : (
						<div className={styles.cardWrapper}>
							<UserRatingsComponent
								userPostRatings={userStateData.userPostRatings}
							/>
						</div>
					)}
				</div>
			</div>
		</div>
	) : (
		<PageNotFound />
	);
}
