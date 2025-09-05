import { Routes, Route } from "react-router-dom";

import { useStyles } from "./styles";
import { HeaderPageConstants } from "@helpers/ibbs.constants";
import PageNotFound from "@/Components/Common/PageNotFound";
import CreatePostComponent from "@/Components/Posts/Components/CreatePost";
import ToasterComponent from "@/Components/Common/Toaster";
import ProfileComponent from "@/Components/Profile";
import SideDrawerComponent from "@/Components/Common/SideDrawer";
import HeaderComponent from "@/Components/Common/Header";
import LandingPageComponent from "@/Components/LandingPage";
import Aurora from "@animations/AuroraBackground";
import ChatbotComponent from "../Chatbot";
import { useAuth0 } from "@auth0/auth0-react";

/**
 * @component
 * `IBBS (Internet Bulletin Board System)` main component.
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
 * @returns The main IBBS interface component with all necessary features and functionality
 */
export default function IBBSComponent() {
	const styles = useStyles();
	const { isAuthenticated } = useAuth0();
	const { Headings } = HeaderPageConstants;

	return (
		<div className="main-content">
			<Aurora
				colorStops={["#3A29FF", "#FF94B4", "#FF3232"]}
				blend={0.8}
				amplitude={1.5}
				speed={0.8}
			/>
			<div className={styles.headerNav}>
				<HeaderComponent />
			</div>
			<div className={styles.bodyContent}>
				<ToasterComponent />
				<SideDrawerComponent />
				{isAuthenticated && <ChatbotComponent />}
				<Routes>
					<Route
						path={Headings.Home.Link}
						element={<LandingPageComponent />}
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
