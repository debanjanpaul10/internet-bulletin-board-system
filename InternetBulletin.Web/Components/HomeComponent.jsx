import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";

import { HomePageConstants } from "@helpers/ibbs.constants";
import Spinner from "@components/Common/Spinner";
import PostsContainer from "@components/Posts/PostsContainer";
import { GetAllPostsAsync } from "@store/Posts/Actions";
import { useMsal } from "@azure/msal-react";
import { loginRequests } from "@services/auth.config";

/**
 * @component
 * The Home Component that is the user's default home page and the Dashboard.
 * @returns {JSX.Element} The home component JSX element.
 */
function HomeComponent() {
	const dispatch = useDispatch();
	const { instance, accounts } = useMsal();

	const IsPostsDataLoading = useSelector(
		(state) => state.PostsReducer.isPostsDataLoading
	);

	useEffect(() => {
		/**
		 * Gets all posts data at init.
		 */
		async function getAllPostsData() {
			let token = "";
			if (accounts.length > 0) {
				token = await getAccessToken();
			}
			dispatch(GetAllPostsAsync(token));
		}

		getAllPostsData();
	}, []);

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
			<Spinner isLoading={IsPostsDataLoading} />
			<div className="row">
				<div className="col-sm-12 mt-4">
					<h1 className="architectDaughterfont text-center">
						{HomePageConstants.Headings.WelcomeMessage}
					</h1>
				</div>
			</div>
			<div className="row">
				<PostsContainer />
			</div>
		</div>
	);
}

export default HomeComponent;
