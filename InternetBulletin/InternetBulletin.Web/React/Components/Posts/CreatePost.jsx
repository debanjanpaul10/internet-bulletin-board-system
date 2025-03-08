import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import Cookies from "js-cookie";
import ReactQuill from "react-quill-new";

import { CookiesConstants, CreatePostPageConstants } from "@helpers/Constants";
import { AddNewPostDataAsync } from "@store/Posts/Actions";
import AddPostDtoModel from "@models/AddPostDto";
import PageNotFound from "@components/Common/PageNotFound";

function CreatePostComponent() {
	const dispatch = useDispatch();

	const UserStoreData = useSelector((state) => state.UsersReducer.userData);

	const [postData, setPostData] = useState({
		Title: "",
		Content: "",
		CreatedBy: "",
	});
	const [errors, setErrors] = useState({
		Title: "",
		Content: "",
	});
	const [currentLoggedInUser, setCurrentLoggedInUser] = useState({});

	useEffect(() => {
		const currentLoggedInUserCookies = Cookies.get(
			CookiesConstants.LoggedInUser.Name
		);
		if (
			currentLoggedInUserCookies !== "" &&
			currentLoggedInUserCookies !== undefined
		) {
			setCurrentLoggedInUser(JSON.parse(currentLoggedInUserCookies));
		}
	}, []);

	useEffect(() => {
		if (
			UserStoreData &&
			Object.keys(UserStoreData).length > 0 &&
			currentLoggedInUser !== UserStoreData
		) {
			setCurrentLoggedInUser(UserStoreData);
			setPostData({
				...postData,
				CreatedBy: currentLoggedInUser.userAlias,
			});
		}
	}, [UserStoreData, currentLoggedInUser]);

	/**
	 * Checks if user logged in.
	 * @returns {boolean} The boolean value of user login.
	 */
	const isUserLoggedIn = () => {
		return Object.keys(currentLoggedInUser).length > 0;
	};

	/**
	 * Handles the form submit event.
	 * @param {Event} event The submit event.
	 */
	const handleCreatePost = (event) => {
		event.preventDefault();

		const validations = CreatePostPageConstants.validations;
		errors.Title = postData.Title === "" ? validations.TitleRequired : "";
		errors.Content =
			postData.Content === "" ? validations.ContentRequired : "";
		setErrors({ ...errors });

		if (errors.Content === "" && errors.Title === "") {
			const addPostData = new AddPostDtoModel(
				postData.Title,
				postData.Content,
				postData.CreatedBy
			);

			dispatch(AddNewPostDataAsync(addPostData));
		}
	};

	/**
	 * Handles the form change event.
	 * @param {Event} event The on change event.
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
	 * Handles the key down event to prevent form submission on Enter key press.
	 * @param {Event} event The key down event.
	 */
	const handleKeyDown = (event) => {
		if (event.key === "Enter") {
			event.preventDefault();
		}
	};

	/**
	 * Handles the content change event for the rich text editor.
	 * @param {string} content The content of the editor.
	 */
	const handleContentChange = (content) => {
		setPostData({
			...postData,
			Content: content,
		});
	};

	const AIRewrite = () => {
		alert("AI Rewrite feature is under progress.");
	};

	/**
	 * Custom toolbar configuration.
	 */
	const modules = {
		toolbar: {
			container: [
				[{ header: "1" }, { header: "2" }],
				["bold", "italic", "underline", "blockquote"],
				[{ list: "ordered" }, { list: "bullet" }],
				["link"],
				["clean"],
				[{ aiRewrite: "AIRewrite" }], // Custom toolbar icon
			],
			handlers: {
				aiRewrite: AIRewrite, // Custom handler
			},
		},
	};

	return isUserLoggedIn() ? (
		<div className="container d-flex flex-column">
			<div className="row">
				<div className="col-sm-12 mt-5">
					<h1 className="architectDaughterfont text-center">
						{CreatePostPageConstants.Headings.Header}
					</h1>
				</div>
				<form
					onSubmit={handleCreatePost}
					onKeyDown={handleKeyDown}
					className="addpost"
				>
					<div className="form-group row card mt-3">
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

						<div className="col sm-12 mb-3 mb-sm-0 p-3">
							<ReactQuill
								value={postData.Content}
								onChange={handleContentChange}
								id="Content"
								className="text-editor"
								placeholder={
									CreatePostPageConstants.Headings
										.ContentBoxPlaceholder
								}
								modules={modules}
							/>
							{errors.Content && (
								<span className="alert alert-danger ml-10 mt-3">
									{errors.Content}
								</span>
							)}
						</div>

						<div className="text-center">
							<button
								type="submit"
								className="btn btn-block btn-success"
							>
								{"Create"}
							</button>
						</div>
					</div>
				</form>
			</div>
		</div>
	) : (
		<PageNotFound />
	);
}

export default CreatePostComponent;
