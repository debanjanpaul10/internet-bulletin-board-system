import { useEffect } from "react";
import { Routes, Route } from "react-router-dom";
import { useAuth0 } from "@auth0/auth0-react";

import { useStyles } from "@components/ibbs/styles";
import { HeaderPageConstants } from "@helpers/ibbs.constants";
import PageNotFound from "@components/common/page-not-found";
import CreatePostComponent from "@components/posts/components/create-post";
import ToasterComponent from "@components/common/toaster";
import ProfileComponent from "@components/profile";
import SideDrawerComponent from "@components/common/side-drawer";
import HeaderComponent from "@components/common/header";
import LandingPageComponent from "@components/landing-page";
import Aurora from "@animations/aurora-background";
import ChatbotComponent from "@components/chat-bot";
import BugReportComponent from "@components/bug-report";
import { useAppDispatch } from "@/index";
import { GetLookupMasterDataAsync } from "@store/common/actions";

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
 * @returns The main IBBS interface component with all necessary features and functionality
 */
export default function IBBSComponent() {
	const dispatch = useAppDispatch();

	const styles = useStyles();
	const { isAuthenticated } = useAuth0();
	const { Headings } = HeaderPageConstants;

	useEffect(() => {
		dispatch(GetLookupMasterDataAsync());
	}, []);

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
				<BugReportComponent />
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
