import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useAuth0 } from "@auth0/auth0-react";

import { HomePageConstants } from "@helpers/ibbs.constants";
import Spinner from "@components/Common/Spinner";
import PostsContainer from "@components/Posts/PostsContainer";
import { GetAllPostsAsync } from "@store/Posts/Actions";

/**
 * @component
 * The Home Component that is the user's default home page and the Dashboard.
 * @returns {JSX.Element} The home component JSX element.
 */
function HomeComponent() {
	const dispatch = useDispatch();
	const { getIdTokenClaims } = useAuth0();

	const IsPostsDataLoading = useSelector(
		(state) => state.PostsReducer.isPostsDataLoading
	);

	useEffect(() => {
		dispatch(GetAllPostsAsync(getIdTokenClaims));
	}, []);

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
