import React from "react";
import { Routes, Route } from "react-router-dom";

import { HeaderPageConstants } from "@helpers/ibbs.constants";
import Header from "@components/Common/Header";
import PageNotFound from "@components/Common/PageNotFound";
import HomeComponent from "@components/Home";
import CreatePostComponent from "@components/Posts/CreatePost";
import ToasterComponent from "@components/Common/Toaster";
import ProfileComponent from "@components/Profile";
import SideDrawerComponent from "@components/Common/SideDrawer";
import { useStyles } from "./styles";

/**
 * @component
 * IBBS (Internet Bulletin Board System) main component.
 *
 * @description
 * This component serves as the main interface for the Internet Bulletin Board System,
 * providing the core functionality and user interface for the application. It handles:
 * - User authentication and authorization
 * - Bulletin board content display and management
 * - User interactions and post management
 * - Real-time updates and notifications
 *
 * @state {Object} [state] - Component state management
 * @state {Array} [state.posts] - List of bulletin board posts
 * @state {boolean} [state.isLoading] - Loading state indicator
 * @state {Object} [state.user] - Current user information
 *
 * @effect
 * On mount:
 * - Fetches initial bulletin board data
 * - Sets up real-time listeners
 * - Initializes user session
 *
 * @returns {JSX.Element} The main IBBS interface component with all necessary features and functionality
 */
function IBBS() {
	const styles = useStyles();
	const { Headings } = HeaderPageConstants;

	return (
		<div className="main-content">
			<div className={styles.headerNav}>
				<Header />
			</div>
			<div className={styles.bodyContent}>
				{" "}
				<ToasterComponent />
				<SideDrawerComponent />
				<Routes>
					<Route
						path={Headings.Home.Link}
						element={<HomeComponent />}
					/>
					<Route
						path={Headings.CreatePost.Link}
						element={<CreatePostComponent />}
					/>
					<Route path="*" element={<PageNotFound />} />
					<Route
						path={Headings.MyProfile.Link}
						element={<ProfileComponent />}
					/>
				</Routes>
			</div>
		</div>
	);
}

export default IBBS;
