import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";

import { HomePageConstants } from "@helpers/ibbs.constants";
import Spinner from "@components/Common/Spinner";
import PostsContainer from "@components/Posts/PostsContainer";
import { GetAllPostsAsync } from "@store/Posts/Actions";
import { useMsal } from "@azure/msal-react";
import { loginRequests } from "@services/auth.config";
import EditPostComponent from "@components/Posts/EditPost";

/**
 * @component
 * The Home Component that is the user's default home page and the Dashboard.
 * @returns {JSX.Element} The home component JSX element.
 */
function HomeComponent() {
	const dispatch = useDispatch();
	const { instance, accounts } = useMsal();
	const [isTokenRetrieved, setIsTokenRetrieved] = useState(false);

	const IsPostsDataLoading = useSelector(
		(state) => state.PostsReducer.isPostsDataLoading
	);

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
					<h1 className="architectDaughterfont text-center">
						{HomePageConstants.Headings.WelcomeMessage}
					</h1>
				</div>
			</div>
			<div className="row">
				<PostsContainer />
				<EditPostComponent />
			</div>
		</div>
	);
}

export default HomeComponent;
