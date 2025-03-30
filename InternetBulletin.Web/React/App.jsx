import React from "react";
import { Switch, Route } from "react-router-dom";

import { HeaderPageConstants } from "@helpers/Constants";
import Header from "@components/Common/Header";
import PageNotFound from "@components/Common/PageNotFound";
import HomeComponent from "@components/HomeComponent";
import LoginComponent from "@components/Users/Login";
import RegisterComponent from "@components/Users/Register";
import CreatePostComponent from "@components/Posts/CreatePost";
import MyProfileContainer from "@components/Users/MyProfile/MyProfileContainer";
import Toaster from "@components/Common/Toaster";

/**
 * @component
 * The Main App component.
 *
 * @returns {JSX.Element} The App JSX element.
 */
function App() {
	const { Headings } = HeaderPageConstants;

	return (
		<>
			<Header />
            <Toaster />
			<Switch>
				<Route
					path={Headings.Home.Link}
					component={HomeComponent}
					exact
				/>
				<Route
					path={Headings.CreatePost.Link}
					component={CreatePostComponent}
					exact
				/>
				<Route
					path={Headings.MyProfile.Link}
					component={MyProfileContainer}
					exact
				/>
				<Route component={PageNotFound} />
			</Switch>
		</>
	);
}

export default App;
