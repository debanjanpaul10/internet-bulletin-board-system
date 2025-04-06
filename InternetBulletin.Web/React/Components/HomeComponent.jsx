import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";

import { HomePageConstants } from "@helpers/Constants";
import Spinner from "@components/Common/Spinner";
import PostsContainer from "@components/Posts/PostsContainer";
import { GetAllPostsAsync } from "@store/Posts/Actions";
import LoginComponent from "@components/Users/Login";
import RegisterComponent from "@components/Users/Register";

/**
 * @component
 * The Home Component that is the user's default home page and the Dashboard.
 * @returns {JSX.Element} The home component JSX element.
 */
function HomeComponent() {
	const dispatch = useDispatch();

	const IsPostsDataLoading = useSelector(
		(state) => state.PostsReducer.isPostsDataLoading
	);
	const IsLoginModalOpen = useSelector(
		(state) => state.UsersReducer.isLoginModalOpen
	);
	const IsRegisterModalOpen = useSelector(
		(state) => state.UsersReducer.isRegisterModalOpen
	);

	const [isLoginModalOpen, setIsLoginModalOpen] = useState(false);
	const [isRegisterModalOpen, setIsRegisterModalOpen] = useState(false);

	useEffect(() => {
		dispatch(GetAllPostsAsync());
	}, []);

	useEffect(() => {
		if (isLoginModalOpen !== IsLoginModalOpen) {
			setIsLoginModalOpen(IsLoginModalOpen);
		}
	}, [IsLoginModalOpen]);

	useEffect(() => {
		if (isRegisterModalOpen !== IsRegisterModalOpen) {
			setIsRegisterModalOpen(IsRegisterModalOpen);
		}
	}, [IsRegisterModalOpen]);

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
				{isLoginModalOpen && <LoginComponent />}
				{isRegisterModalOpen && <RegisterComponent />}
			</div>
		</div>
	);
}

export default HomeComponent;
