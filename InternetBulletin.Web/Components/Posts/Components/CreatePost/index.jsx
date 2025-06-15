import React, { useEffect, useMemo, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import ReactQuill from "react-quill-new";
import { useNavigate } from "react-router-dom";
import {
    CardPreview,
    Button,
    CardHeader,
    Tooltip,
    SkeletonItem,
    Skeleton,
    TagGroup,
    Tag,
} from "@fluentui/react-components";
import { useMsal } from "@azure/msal-react";

import {
    CreatePostPageConstants,
    HeaderPageConstants,
} from "@helpers/ibbs.constants";
import {
    AddNewPostAsync,
    HandlePostAiModerationTasksAsync,
    RewriteStoryWithAiAsync,
} from "@store/Posts/Actions";
import AddPostDtoModel from "@models/AddPostDto";
import PageNotFound from "@components/Common/PageNotFound";
import AiButton from "@assets/Images/ai-icon.svg";
import UserStoryRequestDtoModel from "@models/UserStoryRequestDto";
import Spinner from "@components/Common/Spinner";
import { useStyles } from "@components/Posts/Components/CreatePost/styles";
import { loginRequests } from "@services/auth.config";
import { UserNameConstant } from "@helpers/config.constants";
import SpotlightCard from "@animations/SpotlightCard";
import BlurText from "@animations/BlurText";
import ShinyText from "@animations/ShinyText";
import CancelModalComponent from "./Components/CancelModal";

/**
 * @component
 * `CreatePostComponent` is a React component that provides functionality for creating new posts in the bulletin board system.
 * The component includes features such as:
 * - Rich text editing using ReactQuill
 * - Form validation for title and content
 * - AI-powered content rewriting
 * - Authentication integration with MSAL
 * - Responsive design with Fluent UI components
 *
 * @requires {Object} `React` - The React library
 * @requires {Object} `ReactQuill` - Rich text editor component
 * @requires {Object} `@fluentui/react-components` - UI component library
 * @requires {Object} `@azure/msal-react` - Microsoft Authentication Library
 *
 * @state {Object} postData - Contains the post information (Title, Content, CreatedBy)
 * @state {Object} errors - Contains validation errors for the form
 * @state {Object} currentLoggedInUser - Stores the current authenticated user information
 * @state {Object} isDialogOpen - Indicates whether the confirmation dialog is open
 *
 * @function handleCreatePost - Handles form submission and post creation
 * @function handleFormChange - Manages form input changes
 * @function handleContentChange - Manages rich text editor content changes
 * @function handleAiRewrite - Handles AI-powered content rewriting
 * @function handleCancelClick - Handles navigation back to home page
 * @function handleConfirmCancel - Handles confirmation of cancellation
 *
 * @returns {JSX.Element} Returns either the post creation form or a PageNotFound component based on authentication status
 */
function CreatePostComponent() {
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const styles = useStyles();
    const { instance, accounts } = useMsal();

    const IsCreatePostLoadingStoreData = useSelector(
        (state) => state.PostsReducer.isCreatePostLoading
    );
    const AiRewrittenStoryStoreData = useSelector(
        (state) => state.PostsReducer.aiRewrittenStory
    );
    const IsRewriteLoadingStoreData = useSelector(
        (state) => state.PostsReducer.isRewriteLoading
    );
    const AIModerationStoreData = useSelector(
        (state) => state.PostsReducer.aiModerationData
    );

    const [postData, setPostData] = useState({
        Title: "",
        Content: "",
        CreatedBy: "",
        isNsfw: false,
        genreTag: "",
    });
    const [errors, setErrors] = useState({
        Title: "",
        Content: "",
    });
    const [currentLoggedInUser, setCurrentLoggedInUser] = useState({});

    // #region Side Effects

    useEffect(() => {
        if (accounts.length > 0) {
            const userName = accounts[0].idTokenClaims[UserNameConstant];
            setCurrentLoggedInUser(userName);
        } else {
            setCurrentLoggedInUser();
        }
    }, [instance, accounts]);

    useEffect(() => {
        if (
            AiRewrittenStoryStoreData !== "" &&
            postData.Content !== "" &&
            AiRewrittenStoryStoreData !== postData.Content
        ) {
            setPostData({
                ...postData,
                Content: AiRewrittenStoryStoreData,
            });
        }
    }, [AiRewrittenStoryStoreData]);

    /**
     * Memoized function to process moderation data from AI.
     * @returns {Object|null} Processed moderation data containing nsfwTag and genreTag, or null if no data.
     */
    const processModerationData = useMemo(() => {
        if (
            !AIModerationStoreData ||
            Object.values(AIModerationStoreData).length === 0
        ) {
            return null;
        }

        const nsfwTag = AIModerationStoreData?.moderationData
            ?.replace(/<[^>]*>?/gm, "")
            .trim();
        const genreTag = AIModerationStoreData?.tagData
            ?.replace(/<[^>]*>?/gm, "")
            .trim();

        return { nsfwTag, genreTag };
    }, [AIModerationStoreData]);

    useEffect(() => {
        if (!processModerationData) return;

        const { nsfwTag, genreTag } = processModerationData;

        setPostData((prevState) => {
            const updates = {};

            if (nsfwTag && prevState.isNsfw !== (nsfwTag === "NSFW")) {
                updates.isNsfw = nsfwTag === "NSFW";
            }

            if (genreTag && prevState.genreTag !== genreTag) {
                updates.genreTag = genreTag;
            }

            return Object.keys(updates).length > 0
                ? { ...prevState, ...updates }
                : prevState;
        });
    }, [processModerationData]);

    // #endregion

    /**
     * Gets the access token silently using msal.
     * @returns {Promise<string>} The access token.
     */
    const getAccessToken = async () => {
        const tokenResponse = await instance.acquireTokenSilent({
            ...loginRequests,
            account: accounts[0],
        });

        return tokenResponse.idToken;
    };

    /**
     * Checks if user is logged in.
     * @returns {boolean} True if user is logged in, false otherwise.
     */
    const isUserLoggedIn = () => {
        return (
            currentLoggedInUser !== null &&
            currentLoggedInUser !== undefined &&
            currentLoggedInUser?.username !== ""
        );
    };

    /**
     * Handles the form submit event for creating a new post.
     * @param {Event} event - The submit event.
     * @returns {Promise<void>}
     */
    const handleCreatePost = async (event) => {
        event.preventDefault();

        const validations = CreatePostPageConstants.validations;
        errors.Title =
            postData.Title === ""
                ? validations.TitleRequired
                : postData.Title.length > 50
                ? validations.MaxTitleLength
                : "";
        errors.Content =
            postData.Content === "" ? validations.ContentRequired : "";
        setErrors({ ...errors });

        if (errors.Content === "" && errors.Title === "") {
            const addPostData = new AddPostDtoModel(
                postData.Title,
                postData.Content,
                postData.CreatedBy
            );

            const accessToken = await getAccessToken();
            dispatch(AddNewPostAsync(addPostData, accessToken))
                .then(() => {
                    navigate("/");
                })
                .catch((error) => {
                    console.error(error);
                });
        }
    };

    /**
     * Handles the form change event for input fields.
     * @param {Event} event - The change event.
     */
    const handleFormChange = (event) => {
        event.persist();
        const target = event.target;
        const value = target.value;

        // Add character limit validation for title
        if (target.name === "Title") {
            if (value.length > 50) {
                setErrors({
                    ...errors,
                    Title: CreatePostPageConstants.validations.MaxTitleLength,
                });
                return;
            } else {
                // Clear the error if length is valid
                setErrors({
                    ...errors,
                    Title: "",
                });
            }
        }

        setPostData({
            ...postData,
            [target.name]: value,
        });
    };

    /**
     * Handles the key down event to prevent form submission on Enter key press.
     * @param {KeyboardEvent} event - The key down event.
     */
    const handleKeyDown = (event) => {
        if (event.key === "Enter") {
            event.preventDefault();
        }
    };

    /**
     * Handles the content change event for the rich text editor.
     * @param {string} content - The content of the editor.
     */
    const handleContentChange = useMemo(
        () => (content) => {
            setPostData({
                ...postData,
                Content: content,
            });
        },
        [postData]
    );

    /**
     * Handles the AI rewrite event for content enhancement.
     * @param {Event} event - The rewrite event.
     * @returns {Promise<void>}
     */
    const handleAiRewrite = async (event) => {
        event.preventDefault();
        const strippedContent = postData.Content.replace(
            /<[^>]*>?/gm,
            ""
        ).trim();
        if (strippedContent !== "") {
            var requestDto = new UserStoryRequestDtoModel(strippedContent);
            const accessToken = await getAccessToken();
            dispatch(RewriteStoryWithAiAsync(requestDto, accessToken));
        }
    };

    /**
     * The modules for React Quill
     */
    const modules = useMemo(
        () => ({
            toolbar: {
                container: [
                    [{ header: "1" }, { header: "2" }],
                    ["bold", "italic", "underline", "blockquote"],
                    [{ list: "ordered" }, { list: "bullet" }],
                    ["link"],
                    ["clean"],
                ],
            },
        }),
        []
    );

    /**
     * Handles the moderation button click event to process content through AI moderation.
     * @param {Event} event - The click event.
     * @returns {Promise<void>}
     */
    const handleModerateButtonClick = async (event) => {
        event.preventDefault();

        const strippedContent = postData.Content.replace(
            /<[^>]*>?/gm,
            ""
        ).trim();
        if (strippedContent !== "") {
            var requestDto = new UserStoryRequestDtoModel(strippedContent);
            const accessToken = await getAccessToken();
            dispatch(HandlePostAiModerationTasksAsync(requestDto, accessToken));
        }
    };

    /**
     * Renders the create post action buttons based on moderation state.
     * @returns {JSX.Element} The rendered buttons component.
     */
    const renderCreatePostButtons = () => {
        return (
            <div className="text-center">
                {Object.values(AIModerationStoreData).length <= 0 ? (
                    <Tooltip
                        content={
                            CreatePostPageConstants.Headings
                                .ModerateWithAIButtonTexts.TooltipText
                        }
                        relationship="label"
                        positioning="top"
                    >
                        <Button
                            type="submit"
                            onClick={handleModerateButtonClick}
                            className={styles.moderateWithAiButton}
                        >
                            <ShinyText
                                text={
                                    CreatePostPageConstants.Headings
                                        .ModerateWithAIButtonTexts.ButtonText
                                }
                                disabled={false}
                                speed={3}
                                className={styles.moderateWithAiButtonText}
                            />
                        </Button>
                    </Tooltip>
                ) : (
                    <Button
                        type="submit"
                        onClick={handleCreatePost}
                        className={styles.createButton}
                    >
                        {"Create"}
                    </Button>
                )}
                &nbsp;&nbsp;
                <CancelModalComponent />
            </div>
        );
    };

    /**
     * Renders the moderation tags based on AI moderation results.
     * @returns {JSX.Element} The rendered tags component.
     */
    const renderTags = () => {
        const nsfwTag = AIModerationStoreData?.moderationData
            ?.replace(/<[^>]*>?/gm, "")
            .trim();
        const genreTag = AIModerationStoreData?.tagData
            ?.replace(/<[^>]*>?/gm, "")
            .trim();
        return (
            <TagGroup>
                {nsfwTag && (
                    <Tag className={nsfwTag === "NSFW" ? styles.nsfwTag : null}>
                        {nsfwTag}
                    </Tag>
                )}
                {genreTag && <Tag className={styles.genreTag}>{genreTag}</Tag>}
            </TagGroup>
        );
    };

    return isUserLoggedIn() ? (
        <div className="container d-flex flex-column mt-5">
            <Spinner isLoading={IsCreatePostLoadingStoreData} />
            <div className="row">
                <div className="col-sm-12">
                    <BlurText
                        text={CreatePostPageConstants.Headings.Header}
                        delay={150}
                        animateBy="words"
                        direction="top"
                        className={styles.addNewHeading}
                    />
                </div>
                <form onKeyDown={handleKeyDown} className="addpost">
                    <SpotlightCard
                        className={`custom-spotlight-card ${styles.card}`}
                        spotlightColor="rgba(0, 229, 255, 0.2)"
                    >
                        <CardHeader
                            className={styles.cardHeader}
                            header={
                                <div className="col sm-12 mb-3 mb-sm-0">
                                    <div className="row p-2">
                                        <input
                                            type="text"
                                            name="Title"
                                            onChange={handleFormChange}
                                            value={postData.Title}
                                            className="form-control mt-0"
                                            id="Title"
                                            placeholder={
                                                CreatePostPageConstants.Headings
                                                    .TitleBarPlaceholder
                                            }
                                        />
                                        {errors.Title && (
                                            <span className="alert alert-danger ml-10 mt-3">
                                                {errors.Title}
                                            </span>
                                        )}
                                    </div>
                                </div>
                            }
                        />
                        <CardPreview className={styles.cardPreview}>
                            <div className="form-group row mt-3">
                                <div className="col sm-12 mb-3 mb-sm-0 p-3">
                                    {IsRewriteLoadingStoreData ? (
                                        <Skeleton
                                            aria-label="Profile data loading"
                                            as="div"
                                            className="row"
                                        >
                                            <div className="col-12 col-sm-12">
                                                <SkeletonItem
                                                    className={
                                                        styles.rewriteTextSkeleton
                                                    }
                                                    appearance="translucent"
                                                    animation="pulse"
                                                    as="div"
                                                    size={128}
                                                />
                                            </div>
                                        </Skeleton>
                                    ) : (
                                        <>
                                            <ReactQuill
                                                value={postData.Content}
                                                onChange={handleContentChange}
                                                id="Content"
                                                className="text-editor"
                                                placeholder={
                                                    CreatePostPageConstants
                                                        .Headings
                                                        .ContentBoxPlaceholder
                                                }
                                                modules={modules}
                                            />
                                            {errors.Content && (
                                                <span className="alert alert-danger ml-10 mt-3">
                                                    {errors.Content}
                                                </span>
                                            )}
                                            <Tooltip
                                                content={
                                                    CreatePostPageConstants
                                                        .Headings
                                                        .RewriteAIButtonTexts
                                                        .TooltipText
                                                }
                                                relationship="label"
                                                positioning="after"
                                            >
                                                <Button
                                                    type="button"
                                                    className={
                                                        styles.aiEditButton
                                                    }
                                                    onClick={handleAiRewrite}
                                                >
                                                    <img
                                                        src={AiButton}
                                                        style={{
                                                            height: "20px",
                                                            marginRight: "10px",
                                                        }}
                                                    />
                                                    <ShinyText
                                                        text={
                                                            CreatePostPageConstants
                                                                .Headings
                                                                .RewriteAIButtonTexts
                                                                .ButtonText
                                                        }
                                                        disabled={false}
                                                        speed={3}
                                                    />
                                                </Button>
                                            </Tooltip>
                                            &nbsp;
                                            <span className="ms-3">
                                                {renderTags()}
                                            </span>
                                        </>
                                    )}
                                </div>
                                {renderCreatePostButtons()}
                            </div>
                        </CardPreview>
                    </SpotlightCard>
                </form>
            </div>
        </div>
    ) : (
        <PageNotFound />
    );
}

export default CreatePostComponent;
