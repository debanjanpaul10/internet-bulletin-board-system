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
     * Handle snap scroll behavior for mouse wheel and touch
     */
    useEffect(() => {
        const container = containerRef.current;
        if (!container) return;

        let isScrolling = false;
        let scrollTimeout;
        let touchStartY = 0;
        let touchEndY = 0;

        const handleWheel = (e) => {
            // Don't interfere with button clicks
            if (e.target.closest("button")) {
                return;
            }

            // Debounce scroll events to prevent rapid firing
            if (isScrolling) return;

            // If we're in the content section
            if (currentSection === 1) {
                const contentSection = contentSectionRef.current;
                if (contentSection && contentSection.contains(e.target)) {
                    // Check if we're at the top of content and scrolling up
                    if (e.deltaY < 0 && contentSection.scrollTop <= 5) {
                        e.preventDefault();
                        isScrolling = true;
                        setCurrentSection(0);

                        scrollTimeout = setTimeout(() => {
                            isScrolling = false;
                        }, 300);
                        return;
                    }
                    // Allow normal scrolling within content
                    return;
                }

                // If scrolling outside content area while in content section
                if (e.deltaY < 0) {
                    e.preventDefault();
                    isScrolling = true;
                    setCurrentSection(0);

                    scrollTimeout = setTimeout(() => {
                        isScrolling = false;
                    }, 300);
                    return;
                }
            }

            // Hero section - prevent default and handle snap
            e.preventDefault();
            isScrolling = true;

            // Clear any existing timeout
            clearTimeout(scrollTimeout);

            // If scrolling down from hero section, snap to content
            if (currentSection === 0 && e.deltaY > 0) {
                setCurrentSection(1);
            }

            // Reset scrolling flag after a short delay
            scrollTimeout = setTimeout(() => {
                isScrolling = false;
            }, 300);
        };

        const handleTouchStart = (e) => {
            touchStartY = e.touches[0].clientY;
        };

        const handleTouchMove = (e) => {
            // If we're in content section and touching within it, allow normal scrolling
            if (currentSection === 1) {
                const contentSection = contentSectionRef.current;
                if (contentSection && contentSection.contains(e.target)) {
                    return; // Allow normal touch scrolling within content
                }
            }

            // Prevent default touch scrolling for hero section
            e.preventDefault();
        };

        const handleTouchEnd = (e) => {
            touchEndY = e.changedTouches[0].clientY;
            const touchDiff = touchStartY - touchEndY;
            const minSwipeDistance = 50; // Minimum distance for a swipe

            // Don't interfere with button touches
            if (e.target.closest("button")) {
                return;
            }

            // Debounce touch events
            if (isScrolling) return;

            // If we're in content section and the touch ended within it
            if (currentSection === 1) {
                const contentSection = contentSectionRef.current;
                if (contentSection && contentSection.contains(e.target)) {
                    // Check if we're at the top of content and swiping down (up gesture)
                    if (
                        touchDiff < -minSwipeDistance &&
                        contentSection.scrollTop <= 5
                    ) {
                        isScrolling = true;
                        setCurrentSection(0);
                        setTimeout(() => {
                            isScrolling = false;
                        }, 300);
                        return;
                    }
                    return; // Allow normal touch behavior within content
                }

                // If swiping down outside content area while in content section
                if (touchDiff < -minSwipeDistance) {
                    isScrolling = true;
                    setCurrentSection(0);
                    setTimeout(() => {
                        isScrolling = false;
                    }, 300);
                    return;
                }
            }

            isScrolling = true;

            // Swipe up (touch diff positive) - go to content section
            if (currentSection === 0 && touchDiff > minSwipeDistance) {
                setCurrentSection(1);
            }
            // Swipe down (touch diff negative) - go to hero section
            else if (currentSection === 1 && touchDiff < -minSwipeDistance) {
                setCurrentSection(0);
            }

            // Reset scrolling flag
            setTimeout(() => {
                isScrolling = false;
            }, 300);
        };

        container.addEventListener("wheel", handleWheel, { passive: false });
        container.addEventListener("touchstart", handleTouchStart, {
            passive: true,
        });
        container.addEventListener("touchmove", handleTouchMove, {
            passive: false,
        });
        container.addEventListener("touchend", handleTouchEnd, {
            passive: true,
        });

        return () => {
            container.removeEventListener("wheel", handleWheel);
            container.removeEventListener("touchstart", handleTouchStart);
            container.removeEventListener("touchmove", handleTouchMove);
            container.removeEventListener("touchend", handleTouchEnd);
            clearTimeout(scrollTimeout);
        };
    }, [currentSection]);

    return (
        <div ref={containerRef} className={styles.landingContainer}>
            <Spinner isLoading={IsPostsDataLoading || !isTokenRetrieved} />

            {/* Hero Section - Always visible when currentSection === 0 */}
            {currentSection === 0 && (
                <div ref={heroSectionRef} className={styles.heroSection}>
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
                <div className={styles.contentWrapper}>
                    <div
                        ref={contentSectionRef}
                        className={styles.contentSection}
                    >
                        <div className={styles.contentMain}>
                            <PostsContainer />
                            <EditPostComponent />
                        </div>
                    </div>
                </div>
            )}

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

export default LandingPageComponent;
