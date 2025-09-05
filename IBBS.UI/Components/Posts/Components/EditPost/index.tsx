import { useState, useEffect, useMemo } from "react";
import {
	Dialog,
	DialogSurface,
	Button,
	Card,
	CardHeader,
	Label,
	CardPreview,
	Spinner,
	Tooltip,
	Skeleton,
	SkeletonItem,
} from "@fluentui/react-components";
import { useAuth0 } from "@auth0/auth0-react";
import ReactQuill from "react-quill-new";

import AiButton from "@assets/Images/ai-icon.svg";
import { ToggleEditPostDialog, UpdatePostAsync } from "@store/Posts/Actions";
import { useStyles } from "@/Components/Posts/Components/EditPost/styles";
import { CreatePostPageConstants } from "@helpers/ibbs.constants";
import UpdatePostDtoModel from "@models/UpdatePostDto";
import UserStoryRequestDtoModel from "@models/UserStoryRequestDto";
import { useAppDispatch, useAppSelector } from "@/index";
import {
	RewriteStoryWithAiAsync,
	RewriteStoryWithAiSuccess,
} from "@/Store/AiServices/Actions";

/**
 * @component
 * `EditPostComponent` A dialog component for editing existing posts with AI-powered text rewriting capabilities.
 *
 * @features
 * - Edit post title and content
 * - Rich text editing with ReactQuill
 * - AI-powered text rewriting
 * - Form validation
 * - Loading states for edit and AI operations
 * - Automatic content restoration on dialog close
 *
 * @state
 * @property {boolean} isDialogOpen - Controls dialog visibility
 * @property {boolean} isEditPostLoading - Loading state for post editing
 * @property {Object} postData - Current post data (title, content, id)
 * @property {string} originalContent - Stores original content before AI rewrite
 * @property {Object} errors - Form validation errors
 *
 * @redux
 * @property {boolean} IsEditModelOpenStoreData - Dialog open state from Redux
 * @property {Object} EditPostStoreData - Post data from Redux
 * @property {boolean} IsEditPostDataLoadingStoreData - Loading state from Redux
 * @property {string} AiRewrittenStoryStoreData - AI rewritten content from Redux
 * @property {boolean} IsRewriteLoadingStoreData - AI rewrite loading state from Redux
 *
 * @returns A dialog containing the post edit form with AI rewrite capabilities
 */
export default function EditPostComponent() {
	const dispatch = useAppDispatch();
	const styles = useStyles();
	const { getIdTokenClaims } = useAuth0();

	const IsEditModelOpenStoreData = useAppSelector(
		(state) => state.PostsReducer.isEditModalOpen
	);
	const EditPostStoreData = useAppSelector(
		(state) => state.PostsReducer.editPostData
	);
	const IsEditPostDataLoadingStoreData = useAppSelector(
		(state) => state.PostsReducer.isEditPostDataLoading
	);
	const AiRewrittenStoryStoreData = useAppSelector(
		(state) => state.PostsReducer.aiRewrittenStory
	);
	const IsRewriteLoadingStoreData = useAppSelector(
		(state) => state.PostsReducer.isRewriteLoading
	);

	const [isDialogOpen, setIsDialogOpen] = useState(false);
	const [isEditPostLoading, setIsEditPostLoading] = useState(false);
	const [postData, setPostData] = useState({
		postTitle: "",
		postContent: "",
		postId: "",
	});
	const [originalContent, setOriginalContent] = useState("");
	const [errors, setErrors] = useState({
		postTitle: "",
		postContent: "",
		Title: "",
	});

	// #region SIDE EFFECTS

	useEffect(() => {
		if (
			EditPostStoreData !== null &&
			EditPostStoreData !== undefined &&
			Object.values(EditPostStoreData).length > 0 &&
			EditPostStoreData !== postData
		) {
			setPostData(EditPostStoreData);
		}
	}, [EditPostStoreData]);

	useEffect(() => {
		if (IsEditModelOpenStoreData !== isDialogOpen) {
			setIsDialogOpen(IsEditModelOpenStoreData);
		}
	}, [IsEditModelOpenStoreData]);

	useEffect(() => {
		if (IsEditPostDataLoadingStoreData !== isEditPostLoading) {
			setIsEditPostLoading(IsEditPostDataLoadingStoreData);
		}
	}, [IsEditPostDataLoadingStoreData]);

	useEffect(() => {
		if (
			AiRewrittenStoryStoreData !== "" &&
			postData.postContent !== "" &&
			AiRewrittenStoryStoreData !== postData.postContent
		) {
			setOriginalContent(postData.postContent);
			setPostData({
				...postData,
				postContent: AiRewrittenStoryStoreData,
			});
		}
	}, [AiRewrittenStoryStoreData]);

	// #endregion

	/**
	 * Gets the access token silently using Auth0.
	 * @returns {string} The access token.
	 */
	const getAccessToken = async () => {
		try {
			const token = await getIdTokenClaims();
			return token?.__raw;
		} catch (error) {
			console.error("Error getting access token:", error);
			return null;
		}
	};

	/**
	 * Handles the key down event.
	 * @param event The key down event.
	 */
	const handleKeyDown = (event: any) => {
		if (event.key === "Enter") {
			event.preventDefault();
		}
	};

	/**
	 * Handles the form change event.
	 * @param event The form change event.
	 */
	const handleFormChange = (event: any) => {
		event.persist();
		const target = event.target;

		// Add a character limit validation for the title
		if (target.name === "postTitle") {
			if (target.value.length > 50) {
				setErrors({
					...errors,
					postTitle:
						CreatePostPageConstants.validations.MaxTitleLength,
				});
				return;
			} else {
				setErrors({
					...errors,
					postTitle: "",
				});
			}
		}

		setPostData({
			...postData,
			[target.name]: target.value,
		});
	};

	/**
	 * Handles the content change event for the rich text editor.
	 * @param {string} content The content of the editor.
	 */
	const handleContentChange = useMemo(
		() => (content: any) => {
			setPostData({
				...postData,
				postContent: content,
			});
		},
		[postData]
	);

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
	 * Handles the update post event.
	 * @param event The update post event.
	 */
	const handleUpdatePost = async (event: any) => {
		event.preventDefault();

		const validations = CreatePostPageConstants.validations;
		errors.Title =
			postData.postTitle === ""
				? validations.TitleRequired
				: postData.postTitle.length > 50
				? validations.MaxTitleLength
				: "";
		errors.postContent =
			postData.postContent === "" ? validations.ContentRequired : "";
		setErrors({ ...errors });

		if (errors.postContent === "" && errors.postTitle === "") {
			const updatePostData = new UpdatePostDtoModel(
				postData.postId,
				postData.postTitle,
				postData.postContent,
				0,
				false,
				""
			);
			const accessToken = await getAccessToken();
			if (accessToken) {
				dispatch(UpdatePostAsync(updatePostData, accessToken));
			}
		}
	};

	/**
	 * Handles the AI rewrite text event.
	 * @param event The rewrite event.
	 */
	const handleAiRewrite = async (event: any) => {
		event.preventDefault();
		const strippedContent = postData.postContent
			.replace(/<[^>]*>?/gm, "")
			.trim();
		if (strippedContent !== "") {
			var requestDto = new UserStoryRequestDtoModel(postData.postContent);
			const accessToken = await getAccessToken();
			if (accessToken) {
				dispatch(
					RewriteStoryWithAiAsync(requestDto.toString(), accessToken)
				);
			}
		}
	};

	/**
	 * Handles the edit modal close event.
	 */
	const handleModalClose = () => {
		setIsDialogOpen(false);
		dispatch(ToggleEditPostDialog(false));
		dispatch(RewriteStoryWithAiSuccess(""));
		if (originalContent) {
			setPostData({
				...postData,
				postContent: originalContent,
			});
			setOriginalContent("");
		}
	};

	return (
		<Dialog open={isDialogOpen}>
			<DialogSurface className={styles.dialogSurface}>
				<div style={{ position: "relative" }}>
					{isEditPostLoading && (
						<div
							style={{
								position: "absolute",
								top: 0,
								left: 0,
								right: 0,
								bottom: 0,
								display: "flex",
								justifyContent: "center",
								alignItems: "center",
								zIndex: 1000,
							}}
						>
							<Spinner size="large" />
						</div>
					)}
					<form onKeyDown={handleKeyDown} className="addPost">
						<Card appearance="subtle" className={styles.card}>
							<CardHeader
								header={
									<div className="col sm-12 mb-3 mb-sm-0">
										<div className="row p-2">
											<Label
												htmlFor="Title"
												className={`mb-2 ${styles.label}`}
											>
												Title
											</Label>
											<input
												type="text"
												name="postTitle"
												onChange={handleFormChange}
												value={postData.postTitle}
												className={styles.titleInput}
												id="Title"
												placeholder={
													CreatePostPageConstants
														.Headings
														.TitleBarPlaceholder
												}
											/>
											{errors.postTitle && (
												<span className={styles.errorMessage}>
													{errors.postTitle}
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
													value={postData.postContent}
													onChange={
														handleContentChange
													}
													id="postContent"
													className={
														styles.rewriteTextBox
													}
													placeholder={
														CreatePostPageConstants
															.Headings
															.ContentBoxPlaceholder
													}
													modules={modules}
												/>
												{errors.postContent && (
													<span className={styles.errorMessage}>
														{errors.postContent}
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
															styles.aiButton
														}
														onClick={
															handleAiRewrite
														}
													>
														<span
															className={
																styles.gradientTextButton
															}
														>
															<img
																src={AiButton}
																style={{
																	height: "20px",
																}}
															/>{" "}
															{
																CreatePostPageConstants
																	.Headings
																	.RewriteAIButtonTexts
																	.ButtonText
															}
														</span>
													</Button>
												</Tooltip>
											</>
										)}
									</div>

									<div className="text-center">
										<Button
											type="submit"
											onClick={handleUpdatePost}
											className={styles.editButton}
										>
											{"Edit"}
										</Button>
										&nbsp;
										<Button
											onClick={handleModalClose}
											className={styles.cancelButton}
										>
											{"Close"}
										</Button>
									</div>
								</div>
							</CardPreview>
						</Card>
					</form>
				</div>
			</DialogSurface>
		</Dialog>
	);
}
