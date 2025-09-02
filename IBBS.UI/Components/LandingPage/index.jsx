import React, { useEffect, useState, useRef } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useAuth0 } from "@auth0/auth0-react";
import { ArrowDown32Filled, ArrowUp32Filled } from "@fluentui/react-icons";

import { HomePageConstants } from "@helpers/ibbs.constants";
import Spinner from "@components/Common/Spinner";
import PostsContainer from "@components/Posts";
import { GetAllPostsAsync } from "@store/Posts/Actions";
import EditPostComponent from "@components/Posts/Components/EditPost";
import FooterComponent from "@components/Common/Footer";
import { useStyles } from "./styles";
import * as ibbs_styles from "../IBBS/styles";

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
 *
 * @state
 * @property {boolean} isTokenRetrieved - Tracks the status of token retrieval
 *
 * @dependencies
 * - React Redux for state management
 * - Auth0 for authentication
 * - Fluent UI for styling
 *
 * @returns {JSX.Element} The rendered landing page component
 */
function LandingPageComponent() {
    const styles = useStyles();

    const dispatch = useDispatch();
    const { user, isAuthenticated, getAccessTokenSilently } = useAuth0();

    const IsPostsDataLoading = useSelector(
        (state) => state.PostsReducer.isPostsDataLoading
    );

    const [isTokenRetrieved, setIsTokenRetrieved] = useState(false);
    const [currentSection, setCurrentSection] = useState(0); // 0 = hero, 1 = content
    const containerRef = useRef(null);
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

    useEffect(() => {
        if (isAuthenticated && user) {
            fetchPostsWithToken();
        }
    }, [isAuthenticated, user]);

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
            console.error(error);
            // If token retrieval fails, fall back to unauthenticated access
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
            const token = await getAccessTokenSilently();
            return token;
        } catch (error) {
            console.error(error);
            throw error;
        }
    };

    /**
     * Handles down arrow click to navigate to content section
     */
    const handleDownArrowClick = (e) => {
        e.preventDefault();
        e.stopPropagation();
        setCurrentSection(1);
    };

    /**
     * Handles up arrow click to navigate back to hero section
     */
    const handleUpArrowClick = (e) => {
        e.preventDefault();
        e.stopPropagation();
        setCurrentSection(0);
    };

    /**
     * Allow mouse wheel scrolling only within the content section when it's active
     */
    useEffect(() => {
        const container = containerRef.current;
        if (!container) return;

        const handleWheel = (e) => {
            // Don't interfere with button clicks
            if (e.target.closest("button")) {
                return;
            }

            // If we're not in the content section, prevent all scrolling
            if (currentSection !== 1) {
                e.preventDefault();
                return;
            }

            // If we're in content section, check if the scroll is happening within the content area
            const contentSection = contentSectionRef.current;
            if (!contentSection) {
                e.preventDefault();
                return;
            }

            // Allow scrolling only if the target is within the content section
            const isWithinContent = contentSection.contains(e.target);
            if (!isWithinContent) {
                e.preventDefault();
            }
        };

        // Disable wheel scrolling on the main container except for content area
        container.addEventListener("wheel", handleWheel, { passive: false });

        return () => {
            container.removeEventListener("wheel", handleWheel);
        };
    }, [currentSection]);

    return (
        <div ref={containerRef} className={styles.landingContainer}>
            <Spinner isLoading={IsPostsDataLoading || !isTokenRetrieved} />

            {/* Hero Section - Always visible when currentSection === 0 */}
            {currentSection === 0 && (
                <div className={styles.heroSection}>
                    <div className={styles.heroContent}>
                        <div className={styles.mainHeading}>
                            <div className="col-sm-12">
                                <h1 className={styles.mainHeadingText}>
                                    {HomePageConstants.Headings.WelcomeMessage}
                                </h1>
                            </div>
                        </div>
                        <p className={styles.heroSubtext}>
                            Connect, share, and discover amazing ideas from our
                            vibrant community
                        </p>
                    </div>
                </div>
            )}

            {/* Throbbing Down Arrow Button - Outside sections for better accessibility */}
            {currentSection === 0 && (
                <button
                    className={styles.downArrowButton}
                    onClick={handleDownArrowClick}
                    onMouseDown={handleDownArrowClick}
                    onTouchStart={handleDownArrowClick}
                    aria-label="Scroll down to content"
                    type="button"
                    style={{ pointerEvents: "all" }}
                >
                    <ArrowDown32Filled />
                </button>
            )}

            {/* Content Section - Visible when currentSection === 1 */}
            {currentSection === 1 && (
                <div ref={contentSectionRef} className={styles.contentSection}>
                    <PostsContainer />
                    <EditPostComponent />
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

            {/* Footer - Only visible on content section */}
            {currentSection === 1 && (
                <div className={ibbs_styles.footerContent}>
                    <FooterComponent />
                </div>
            )}
        </div>
    );
}

export default LandingPageComponent;
