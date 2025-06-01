import { useState, useEffect, useMemo } from "react";
import { useDispatch, useSelector } from "react-redux";
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
import { useMsal } from "@azure/msal-react";
import ReactQuill from "react-quill-new";

import AiButton from "@assets/Images/ai-icon.svg";
import {
	RewriteStoryWithAiAsync,
	RewriteStoryWithAiSuccess,
	ToggleEditPostDialog,
	UpdatePostAsync,
} from "@store/Posts/Actions";
import { useStyles } from "@components/Posts/Components/EditPost/styles";
import { CreatePostPageConstants } from "@helpers/ibbs.constants";
import UpdatePostDtoModel from "@models/UpdatePostDto";
import { loginRequests } from "@services/auth.config";
import RewriteRequestDtoModel from "@models/RewriteRequestDto";

/**
 * @component EditPostComponent
 * @description A dialog component for editing existing posts with AI-powered text rewriting capabilities.
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
 * @property {boolean} IsEditModalOpen - Dialog open state from Redux
 * @property {Object} EditPostData - Post data from Redux
 * @property {boolean} IsEditPostDataLoading - Loading state from Redux
 * @property {string} AiRewrittenStory - AI rewritten content from Redux
 * @property {boolean} IsRewriteLoading - AI rewrite loading state from Redux
 * 
 * @returns {JSX.Element} A dialog containing the post edit form with AI rewrite capabilities
 */
function EditPostComponent() {
	const dispatch = useDispatch();
	const styles = useStyles();
	const { instance, accounts } = useMsal();

	const EditPostsStoreData = useSelector(({ PostsReducer }) => ({
		IsEditModalOpen: PostsReducer.isEditModalOpen,
		EditPostData: PostsReducer.editPostData,
		IsEditPostDataLoading: PostsReducer.isEditPostDataLoading,
		AiRewrittenStory: PostsReducer.aiRewrittenStory,
		IsRewriteLoading: PostsReducer.isRewriteLoading
	}));

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
	});

	// #region SIDE EFFECTS

	useEffect(() => {
		if (
			EditPostsStoreData.EditPostData !== null &&
			EditPostsStoreData.EditPostData !== undefined &&
			Object.values(EditPostsStoreData.EditPostData).length > 0 &&
			EditPostsStoreData.EditPostData !== postData
		) {
			setPostData(EditPostsStoreData.EditPostData);
		}
	}, [EditPostsStoreData.EditPostData]);

	useEffect(() => {
		if (EditPostsStoreData.IsEditModalOpen !== isDialogOpen) {
			setIsDialogOpen(EditPostsStoreData.IsEditModalOpen);
		}
	}, [EditPostsStoreData.IsEditModalOpen]);

	useEffect(() => {
		if (EditPostsStoreData.IsEditPostDataLoading !== isEditPostLoading) {
			setIsEditPostLoading(EditPostsStoreData.IsEditPostDataLoading);
		}
	}, [EditPostsStoreData.IsEditPostDataLoading]);

	useEffect(() => {
		if (
			EditPostsStoreData.AiRewrittenStory !== '' &&
			postData.postContent !== '' &&
			EditPostsStoreData.AiRewrittenStory !== postData.postContent
		) {
			setOriginalContent(postData.postContent);
			setPostData({
				...postData,
				postContent: EditPostsStoreData.AiRewrittenStory
			});
		}
	}, [EditPostsStoreData.AiRewrittenStory]);

	// #endregion

	/**
	 * Gets the access token silently using msal.
	 * @returns {string} The access token.
	 */
	const getAccessToken = async () => {
		const tokenData = await instance.acquireTokenSilent({
			...loginRequests,
			account: accounts[0],
		});

		return tokenData.accessToken;
	};

	/**
	 * Handles the key down event.
	 * @param {Event} event The key down event.
	 */
	const handleKeyDown = (event) => {
		if (event.key === "Enter") {
			event.preventDefault();
		}
	};

	/**
	 * Handles the form change event.
	 * @param {Event} event The form change event.
	 */
	const handleFormChange = (event) => {
		event.persist();
		const target = event.target;
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
		() => (content) => {
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
	 * @param {Event} event The update post event.
	 */
	const handleUpdatePost = async (event) => {
		event.preventDefault();

		const validations = CreatePostPageConstants.validations;
		errors.Title =
			postData.postTitle === "" ? validations.TitleRequired : "";
		errors.postContent =
			postData.postContent === "" ? validations.ContentRequired : "";
		setErrors({ ...errors });

		if (errors.postContent === "" && errors.postTitle === "") {
			const updatePostData = new UpdatePostDtoModel(
				postData.postId,
				postData.postTitle,
				postData.postContent,
				0
			);
			const accessToken = await getAccessToken();
			dispatch(UpdatePostAsync(updatePostData, accessToken));
		}
	};

	/**
	 * Handles the AI rewrite text event.
	 * @param {Event} event The rewrite event.
	 */
	const handleAiRewrite = async (event) => {
		event.preventDefault();
		const strippedContent = postData.postContent.replace(
			/<[^>]*>?/gm,
			""
		).trim();
		if (strippedContent !== "") {
			var requestDto = new RewriteRequestDtoModel(postData.postContent);
			const accessToken = await getAccessToken();
			dispatch(RewriteStoryWithAiAsync(requestDto, accessToken));
		}
	};

	/**
	 * Handles the edit modal close event.
	 */
	const handleModalClose = () => {
		setIsDialogOpen(false);
		dispatch(ToggleEditPostDialog(false));
		dispatch(RewriteStoryWithAiSuccess(''));
		if (originalContent) {
			setPostData({
				...postData,
				postContent: originalContent
			});
			setOriginalContent("");
		}
	};

	return (
		<Dialog open={isDialogOpen}>
			<DialogSurface>
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
						<Card appearance="subtle">
							<CardHeader
								header={
									<div className="col sm-12 mb-3 mb-sm-0">
										<div className="row p-2">
											<Label for="Title" className="mb-2">
												Title
											</Label>
											<input
												type="text"
												name="postTitle"
												onChange={handleFormChange}
												value={postData.postTitle}
												className="form-control"
												id="Title"
												placeholder={
													CreatePostPageConstants
														.Headings
														.TitleBarPlaceholder
												}
											/>
											{errors.postTitle && (
												<span className="alert alert-danger ml-10 mt-3">
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
										{EditPostsStoreData.IsRewriteLoading ? (
											<Skeleton
												aria-label="Profile data loading"
												as="div"
												className="row"
											>
												<div className="col-12 col-sm-12">
													<SkeletonItem
														className={styles.rewriteTextSkeleton}
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
													onChange={handleContentChange}
													id="postContent"
													className="text-editor"
													placeholder={
														CreatePostPageConstants.Headings
															.ContentBoxPlaceholder
													}
													modules={modules}
												/>
												{errors.postContent && (
													<span className="alert alert-danger ml-10 mt-3">
														{errors.postContent}
													</span>
												)}
												<Tooltip
													content={CreatePostPageConstants.Headings.RewriteAIButtonTexts.TooltipText}
													relationship="label"
													positioning="after"
												>
													<Button
														type="button"
														className={styles.button}
														onClick={handleAiRewrite}
													>
														<img
															src={AiButton}
															style={{ height: "20px" }}
														/>{" "}
														{CreatePostPageConstants.Headings.RewriteAIButtonTexts.ButtonText}
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

export default EditPostComponent;
