import React, { useEffect, useMemo, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import ReactQuill from "react-quill-new";
import { useNavigate } from "react-router-dom";
import {
	Card,
	CardPreview,
	Button,
	CardHeader,
	LargeTitle,
} from "@fluentui/react-components";

import {
	CreatePostPageConstants,
	HeaderPageConstants,
} from "@helpers/ibbs.constants";
import { AddNewPostAsync, RewriteStoryWithAiAsync } from "@store/Posts/Actions";
import AddPostDtoModel from "@models/AddPostDto";
import PageNotFound from "@components/Common/PageNotFound";
import AiButton from "@assets/Images/ai-icon.svg";
import RewriteRequestDtoModel from "@models/RewriteRequestDto";
import Spinner from "@components/Common/Spinner";
import { useStyles } from "@components/Posts/Components/CreatePost/styles";
import { useMsal } from "@azure/msal-react";
import { loginRequests } from "@services/auth.config";

/**
 * @component
 * This component allows users to create a new post.
 *
 * @returns {JSX.Element} The CreatePostComponent JSX element.
 */
function CreatePostComponent() {
	const dispatch = useDispatch();
	const navigate = useNavigate();
	const styles = useStyles();
	const { instance, accounts } = useMsal();

	const IsCreatePostLoadingStoreData = useSelector(
		( state ) => state.PostsReducer.isCreatePostLoading
	);
	const AiRewrittenStoryStoreData = useSelector(
		( state ) => state.PostsReducer.aiRewrittenStory
	);

	const [ postData, setPostData ] = useState( {
		Title: "",
		Content: "",
		CreatedBy: "",
	} );
	const [ errors, setErrors ] = useState( {
		Title: "",
		Content: "",
	} );
	const [ currentLoggedInUser, setCurrentLoggedInUser ] = useState( {} );

	useEffect( () => {
		if ( accounts.length > 0 ) {
			const userName = accounts[ 0 ].idTokenClaims?.extension_UserName;
			setCurrentLoggedInUser( userName );
		} else {
			setCurrentLoggedInUser();
		}
	}, [ instance, accounts ] );

	useEffect( () => {
		if (
			AiRewrittenStoryStoreData !== "" &&
			postData.Content !== "" &&
			AiRewrittenStoryStoreData !== postData.Content
		) {
			setPostData( {
				...postData,
				Content: AiRewrittenStoryStoreData,
			} );
		}
	}, [ AiRewrittenStoryStoreData ] );

	/**
	 * Gets the access token silently using msal.
	 * @returns {string} The access token.
	 */
	const getAccessToken = async () => {
		const tokenResponse = await instance.acquireTokenSilent( {
			...loginRequests,
			account: accounts[ 0 ],
		} );

		return tokenResponse.accessToken;
	};

	/**
	 * Checks if user logged in.
	 * @returns {boolean} The boolean value of user login.
	 */
	const isUserLoggedIn = () => {
		return (
			currentLoggedInUser !== null &&
			currentLoggedInUser !== undefined &&
			currentLoggedInUser?.username !== ""
		);
	};

	/**
	 * Handles the form submit event.
	 * @param {Event} event The submit event.
	 */
	const handleCreatePost = async ( event ) => {
		event.preventDefault();

		const validations = CreatePostPageConstants.validations;
		errors.Title = postData.Title === "" ? validations.TitleRequired : "";
		errors.Content =
			postData.Content === "" ? validations.ContentRequired : "";
		setErrors( { ...errors } );

		if ( errors.Content === "" && errors.Title === "" ) {
			const addPostData = new AddPostDtoModel(
				postData.Title,
				postData.Content,
				postData.CreatedBy
			);

			const accessToken = await getAccessToken();
			dispatch( AddNewPostAsync( addPostData, accessToken ) )
				.then( () => {
					navigate( "/" );
				} )
				.catch( ( error ) => {
					console.error( error );
				} );
		}
	};

	/**
	 * Handles the form change event.
	 * @param {Event} event The on change event.
	 */
	const handleFormChange = ( event ) => {
		event.persist();
		const target = event.target;
		setPostData( {
			...postData,
			[ target.name ]: target.value,
		} );
	};

	/**
	 * Handles the key down event to prevent form submission on Enter key press.
	 * @param {Event} event The key down event.
	 */
	const handleKeyDown = ( event ) => {
		if ( event.key === "Enter" ) {
			event.preventDefault();
		}
	};

	/**
	 * Handles the content change event for the rich text editor.
	 * @param {string} content The content of the editor.
	 */
	const handleContentChange = useMemo(
		() => ( content ) => {
			setPostData( {
				...postData,
				Content: content,
			} );
		},
		[ postData ]
	);

	/**
	 * Handles the ai rewrite event.
	 * @param {Event} event The rewrite event.
	 */
	const handleAiRewrite = ( event ) => {
		event.preventDefault();
		const strippedContent = postData.Content.replace(
			/<[^>]*>?/gm,
			""
		).trim();
		if ( strippedContent !== "" ) {
			var requestDto = new RewriteRequestDtoModel( postData.Content );
			dispatch( RewriteStoryWithAiAsync( requestDto ) );
		}
	};

	/**
	 * The modules for React Quill
	 */
	const modules = useMemo(
		() => ( {
			toolbar: {
				container: [
					[ { header: "1" }, { header: "2" } ],
					[ "bold", "italic", "underline", "blockquote" ],
					[ { list: "ordered" }, { list: "bullet" } ],
					[ "link" ],
					[ "clean" ],
				],
			},
		} ),
		[]
	);

	/**
	 * Handles the cancel click event.
	 */
	const handleCancelClick = () => {
		navigate( HeaderPageConstants.Headings.Home.Link );
	};

	return isUserLoggedIn() ? (
		<div className="container d-flex flex-column">
			<Spinner isLoading={ IsCreatePostLoadingStoreData } />
			<div className="row">
				<div className="col-sm-12 mt-5">
					<LargeTitle className={ styles.addNewHeading }>
						{ CreatePostPageConstants.Headings.Header }
					</LargeTitle>
				</div>
				<form onKeyDown={ handleKeyDown } className="addpost">
					<Card
						className={ styles.card }
						appearance="filled-alternative"
					>
						<CardHeader
							className={ styles.cardHeader }
							header={
								<div className="col sm-12 mb-3 mb-sm-0">
									<div className="row p-2">
										<input
											type="text"
											name="Title"
											onChange={ handleFormChange }
											value={ postData.Title }
											className="form-control mt-0"
											id="Title"
											placeholder={
												CreatePostPageConstants.Headings
													.TitleBarPlaceholder
											}
										/>
										{ errors.Title && (
											<span className="alert alert-danger ml-10 mt-3">
												{ errors.Title }
											</span>
										) }
									</div>
								</div>
							}
						/>
						<CardPreview className={ styles.cardPreview }>
							<div className="form-group row mt-3">
								<div className="col sm-12 mb-3 mb-sm-0 p-3">
									<ReactQuill
										value={ postData.Content }
										onChange={ handleContentChange }
										id="Content"
										className="text-editor"
										placeholder={
											CreatePostPageConstants.Headings
												.ContentBoxPlaceholder
										}
										modules={ modules }
									/>
									{ errors.Content && (
										<span className="alert alert-danger ml-10 mt-3">
											{ errors.Content }
										</span>
									) }
									<Button
										type="button"
										className={ styles.aiEditButton }
										onClick={ handleAiRewrite }
									>
										<img
											src={ AiButton }
											style={ { height: "20px" } }
										/>{ " " }
										Rewrite with AI
									</Button>
								</div>

								<div className="text-center">
									<Button
										type="submit"
										onClick={ handleCreatePost }
										className={ styles.createButton }
									>
										{ "Create" }
									</Button>
									&nbsp;
									<Button
										className={ styles.cancelButton }
										onClick={ handleCancelClick }
									>
										{ "Cancel" }
									</Button>
								</div>
							</div>
						</CardPreview>
					</Card>
				</form>
			</div>
		</div>
	) : (
		<PageNotFound />
	);
}

export default CreatePostComponent;
