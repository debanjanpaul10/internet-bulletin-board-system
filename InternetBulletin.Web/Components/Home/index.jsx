import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useMsal } from "@azure/msal-react";

import { HomePageConstants } from "@helpers/ibbs.constants";
import Spinner from "@components/Common/Spinner";
import PostsContainer from "@components/Posts/PostsContainer";
import { GetAllPostsAsync } from "@store/Posts/Actions";
import { loginRequests } from "@services/auth.config";
import EditPostComponent from "@components/Posts/EditPost";
import { LargeTitle } from "@fluentui/react-components";
import { useStyles } from "./styles";

/**
 * @component
 * The Home Component that is the user's default home page and the Dashboard.
 * @returns {JSX.Element} The home component JSX element.
 */
function HomeComponent() {
	const styles = useStyles();

	const dispatch = useDispatch();
	const { instance, accounts } = useMsal();

	const IsPostsDataLoading = useSelector(
		(state) => state.PostsReducer.isPostsDataLoading
	);

	const [isTokenRetrieved, setIsTokenRetrieved] = useState(false);

	// Initial load - check if user is logged in
	useEffect(() => {
		if (accounts.length === 0) {
			dispatch(GetAllPostsAsync(""));
			setIsTokenRetrieved(true);
		}
	}, []);

	// Handle token retrieval and post fetching when user is logged in
	useEffect(() => {
		if (accounts.length > 0) {
			fetchPostsWithToken();
		}
	}, [accounts]);

	/**
	 * Fetches posts with authentication token
	 */
	const fetchPostsWithToken = async () => {
		try {
			setIsTokenRetrieved(false);
			const token = await getAccessToken();
			setIsTokenRetrieved(true);
			dispatch(GetAllPostsAsync(token));
		} catch (error) {
			console.error("Error getting token:", error);
			// If token retrieval fails, fall back to unauthenticated access
			setIsTokenRetrieved(true);
			dispatch(GetAllPostsAsync(""));
		}
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

	return (
		<div className="container">
			<Spinner isLoading={IsPostsDataLoading || !isTokenRetrieved} />
			<div className="row">
				<div className="col-sm-12 mt-4">
					<LargeTitle className={styles.mainHeading}>
						{HomePageConstants.Headings.WelcomeMessage}
					</LargeTitle>
				</div>
			</div>
			<div className="row position-relative">
				<div className="col-12">
					<PostsContainer />
					<EditPostComponent />
				</div>
			</div>
		</div>
	);
}

export default HomeComponent;
