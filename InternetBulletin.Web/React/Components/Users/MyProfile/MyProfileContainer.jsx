import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import Cookies from "js-cookie";

import { GetUserProfileAsync } from "@store/Users/Actions";
import { CookiesConstants } from "@helpers/Constants";
import Spinner from "@components/Common/Spinner";
import MyProfileDetailsComponent from "@components/Users/MyProfile/MyProfileDetails";

/**
 * @component
 * MyProfileContainer displays the user's profile information.
 *
 * @returns {JSX.Element} The MyProfileComponent JSX element.
 */
function MyProfileContainer() {
	const dispatch = useDispatch();

	const UserStoreData = useSelector((state) => state.UsersReducer.userData);
	const IsUserDataLoading = useSelector(
		(state) => state.UsersReducer.isUserDataLoading
	);
	const UserProfileStoreData = useSelector(
		(state) => state.UsersReducer.userProfileData
	);

	const [profileDetails, setProfileDetails] = useState({});

	useEffect(() => {
		const userData =
			Object.keys(UserStoreData).length > 0
				? UserStoreData
				: JSON.parse(Cookies.get(CookiesConstants.LoggedInUser.Name));
		dispatch(GetUserProfileAsync(userData?.userId));
	}, []);

	useEffect(() => {
		if (
			Object.keys(UserProfileStoreData).length > 0 &&
			UserProfileStoreData !== profileDetails
		) {
			setProfileDetails(UserProfileStoreData);
		}
	}, [UserProfileStoreData]);

	return (
		<div className="container">
			<Spinner isLoading={IsUserDataLoading} />
			<MyProfileDetailsComponent myProfileDetails={profileDetails} />
		</div>
	);
}

export default MyProfileContainer;
