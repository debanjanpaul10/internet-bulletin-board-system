import { useEffect, useState } from "react";
import { useMsal } from "@azure/msal-react";
import { useDispatch, useSelector } from "react-redux";
import { LargeTitle, Skeleton, SkeletonItem } from "@fluentui/react-components";

import { loginRequests } from "@services/auth.config";
import { GetUserProfileDataAsync } from "@store/Users/Actions";
import { useStyles } from "./styles";
import { MyProfilePageConstants } from "@helpers/ibbs.constants";
import UserDetailsComponent from "./Components/UserDetails";
import AlienImage from "@assets/Images/alien-pfp.jpg";

function ProfileComponent() {
  const dispatch = useDispatch();
  const styles = useStyles();
  const { instance, accounts } = useMsal();
  const { Headings } = MyProfilePageConstants;

  const UserProfileStoreData = useSelector(
    (state) => state.UserReducer.userProfileData
  );
  const IsUserProfileDataLoadingStoreData = useSelector(
    (state) => state.UserReducer.isUserProfileDataLoading
  );

  const [userStateData, setUserStateData] = useState({});
  const [isUserDataLoading, setIsUserDataLoading] = useState(true);

  useEffect(() => {
    const getProfileData = async () => {
      const accessToken = await getAccessToken();
      dispatch(GetUserProfileDataAsync(accessToken));
    };
    getProfileData();
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

  return (
    <div className="container">
      <div className="row">
        <div className="col-sm-12 mt-4">
          <LargeTitle className={styles.profileHeading}>
            {Headings.WelcomeMessage}
          </LargeTitle>
        </div>

        {/* USER DETAILS */}
        <div className="row position-relative mt-4">
          {isUserDataLoading || !userStateData?.displayName ? (
            <Skeleton
              aria-label="Profile data loading"
              as="div"
              className="row"
            >
              <div className="col-sm-2">
                <SkeletonItem
                  appearance="translucent"
                  animation="pulse"
                  as="div"
                  shape="circle"
                  className={styles.userImgSkeleton}
                ></SkeletonItem>
              </div>
              <div className="col-sm-10">
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
              <div className="col-sm-2">
                <div className={styles.imageContainer}>
                  <img
                    src={AlienImage}
                    alt="Profile"
                    className={styles.profileImage}
                  />
                </div>
              </div>
              <div className="col-sm-10">
                <UserDetailsComponent
                  displayName={userStateData.displayName}
                  emailAddress={userStateData.emailAddress}
                  userName={userStateData.userName}
                />
              </div>
            </div>
          )}

          {/* USER ACTIVITY */}
          {/* USER POSTS */}
          {/* USER RATINGS */}
        </div>
      </div>
    </div>
  );
}

export default ProfileComponent;
