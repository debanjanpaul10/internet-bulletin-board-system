import { useEffect, useState, useRef } from "react";
import { useAuth0 } from "@auth0/auth0-react";
import { ArrowDown32Filled, ArrowUp32Filled } from "@fluentui/react-icons";

import { HomePageConstants } from "@helpers/ibbs.constants";
import Spinner from "@/Components/Common/Spinner";
import PostsContainer from "@components/Posts";
import { GetAllPostsAsync } from "@store/Posts/Actions";
import EditPostComponent from "@/Components/Posts/Components/EditPost";
import FooterComponent from "@components/Common/Footer";
import { useStyles } from "./styles";
import { useAppDispatch, useAppSelector } from "@/index";
import { SnapScrollHandler } from "@/Helpers/common.utility";

/**
 * @component
 * @description The main landing page component that serves as the user's dashboard.
 * This component handles authentication state, token retrieval, and displays posts.
 *
 * @features
 * - Manages authentication state using Auth0
 * - Fetches posts with authentication token when user is logged in
 * - Falls back to unauthenticated access if token retrieval fails
 * - Displays a loading spinner during data fetching
 * - Renders posts container and edit post components
 * - Also has a snap scroll effect on the landing page
 *
 * @dependencies
 * - React Redux for state management
 * - Auth0 for authentication
 * - Fluent UI for styling
 *
 * @returns The rendered landing page component
 */
export default function LandingPageComponent() {
	const styles = useStyles();

	const dispatch = useAppDispatch();
	const { user, isAuthenticated, getIdTokenClaims } = useAuth0();

	const IsPostsDataLoading = useAppSelector(
		(state: any) => state.PostsReducer.isPostsDataLoading
	);

	const [isTokenRetrieved, setIsTokenRetrieved] = useState(false);
	const [currentSection, setCurrentSection] = useState(0); // 0 = hero, 1 = content
	const containerRef = useRef(null);
	const heroSectionRef = useRef(null);
	const contentSectionRef = useRef(null);

	/**
	 * Initial load -> Check if user is logged in
	 */
	useEffect(() => {
		if (!isAuthenticated) {
			dispatch(GetAllPostsAsync(""));
			setIsTokenRetrieved(true);
		}
	}, []);

	/**
	 * If user is authenticated then get all the posts with token.
	 */
	useEffect(() => {
		if (isAuthenticated && user) {
			fetchPostsWithToken();
		}
	}, [isAuthenticated, user]);

	/**
	 * Handle snap scroll behavior for mouse wheel and touch
	 */
	useEffect(() => {
		const container: any = containerRef.current;
		if (!container) return;

		let snapScrollData = SnapScrollHandler(
			container,
			currentSection,
			setCurrentSection,
			contentSectionRef
		);

		return () => {
			container.removeEventListener("wheel", snapScrollData.handleWheel);
			container.removeEventListener(
				"touchstart",
				snapScrollData.handleTouchStart
			);
			container.removeEventListener(
				"touchmove",
				snapScrollData.handleTouchMove
			);
			container.removeEventListener(
				"touchend",
				snapScrollData.handleTouchEnd
			);
			clearTimeout(snapScrollData.scrollTimeout);
		};
	}, [currentSection]);

	/**
	 * Fetches posts with authentication token
	 */
	const fetchPostsWithToken = async () => {
		try {
			setIsTokenRetrieved(false);
			const token = await getAccessToken();
			setIsTokenRetrieved(true);
			token && dispatch(GetAllPostsAsync(token));
		} catch (error) {
			console.error(error);
			setIsTokenRetrieved(true);
			dispatch(GetAllPostsAsync(""));
		}
	};

	/**
	 * Gets the access token silently using Auth0.
	 * @returns {string} The access token.
	 */
	const getAccessToken = async () => {
		try {
			const idToken = await getIdTokenClaims();
			return idToken?.__raw;
		} catch (error) {
			console.error(error);
			throw error;
		}
	};

	/**
	 * Handles down arrow click to navigate to content section
	 */
	const handleDownArrowClick = (e: React.MouseEvent | React.TouchEvent) => {
		e.preventDefault();
		e.stopPropagation();
		setCurrentSection(1);
	};

	/**
	 * Handles up arrow click to navigate back to hero section
	 */
	const handleUpArrowClick = (e: React.MouseEvent | React.TouchEvent) => {
		e.preventDefault();
		e.stopPropagation();
		setCurrentSection(0);
	};

	return (
		<div ref={containerRef} className={styles.landingContainer}>
			<Spinner isLoading={IsPostsDataLoading || !isTokenRetrieved} />

			{/* Hero Section - Always visible but transforms based on currentSection */}
			<div
				ref={heroSectionRef}
				className={styles.heroSection}
				style={{
					opacity: currentSection === 0 ? 1 : 0,
					transform:
						currentSection === 0
							? "translateY(0) scale(1)"
							: "translateY(-50px) scale(0.98)",
					pointerEvents: currentSection === 0 ? "all" : "none",
				}}
			>
				<div className={styles.heroContent}>
					<div className={styles.mainHeading}>
						<div className="col-sm-12">
							<h1 className={styles.mainHeadingText}>
								{HomePageConstants.Headings.WelcomeMessage}
							</h1>
						</div>
					</div>
					<p className={styles.heroSubtext}>
						{HomePageConstants.Headings.SubText}
					</p>
				</div>
			</div>

			{/* Throbbing Down Arrow Button - Outside sections for better accessibility */}
			<button
				className={styles.downArrowButton}
				onClick={handleDownArrowClick}
				onMouseDown={handleDownArrowClick}
				onTouchStart={handleDownArrowClick}
				aria-label="Scroll down to content"
				type="button"
				style={{
					pointerEvents: "all",
					opacity: currentSection === 0 ? 1 : 0,
					visibility: currentSection === 0 ? "visible" : "hidden",
					transition:
						"opacity 0.5s ease-in-out, visibility 0.5s ease-in-out",
				}}
			>
				<ArrowDown32Filled />
			</button>

			{/* Content Section - Always rendered but transforms based on currentSection */}
			<div
				className={styles.contentWrapper}
				style={{
					opacity: currentSection === 1 ? 1 : 0,
					transform:
						currentSection === 1
							? "translateY(0) scale(1)"
							: "translateY(10px) scale(1)",
					pointerEvents: currentSection === 1 ? "all" : "none",
				}}
			>
				<div ref={contentSectionRef} className={styles.contentSection}>
					<div className={styles.contentMain}>
						<PostsContainer />
						<EditPostComponent />
					</div>
				</div>
			</div>

			{/* Footer - Fixed to bottom when in content section */}
			{currentSection === 1 && (
				<div className={styles.footerFixed}>
					<FooterComponent />
				</div>
			)}

			{/* Throbbing Up Arrow Button - Outside sections for better accessibility */}
			{currentSection === 1 && (
				<button
					className={styles.upArrowButton}
					onClick={handleUpArrowClick}
					onMouseDown={handleUpArrowClick}
					onTouchStart={handleUpArrowClick}
					aria-label="Scroll up to landing section"
					type="button"
					style={{ pointerEvents: "all" }}
				>
					<ArrowUp32Filled />
				</button>
			)}
		</div>
	);
}
