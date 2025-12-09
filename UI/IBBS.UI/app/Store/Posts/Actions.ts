import { Action, Dispatch } from "@reduxjs/toolkit";

import PostRatingDtoModel from "@models/PostRatingDto";
import UpdatePostDtoModel from "@models/UpdatePostDto";
import {
	AddNewPostApiAsync,
	DeletePostApiAsync,
	GetAllPostsApiAsync,
	GetPostApiAsync,
	UpdatePostApiAsync,
	UpdateRatingApiAsync,
} from "@services/ibbs.apiservice";
import { ToggleErrorToaster } from "@store/Common/Actions";
import {
	ADD_NEW_POST_DATA,
	DELETE_POST_DATA,
	GET_ALL_POSTS_DATA,
	GET_EDIT_POST_DATA,
	GET_POST_DATA,
	IS_CREATE_POST_LOADING,
	POST_DATA_FAIL,
	START_SPINNER,
	STOP_SPINNER,
	TOGGLE_EDIT_POST_DIALOG,
	TOGGLE_EDIT_POST_LOADER,
	TOGGLE_VOTING_LOADER,
	UPDATE_POST_DATA,
	UPDATE_POST_RATING,
} from "@store/Posts/ActionTypes";

/**
 * Saves the loader start status to redux store.
 */
export const StartLoader = () => {
	return {
		type: START_SPINNER,
	};
};

/**
 * Saves the loader stop status to redux store.
 */
export const StopLoader = () => {
	return {
		type: STOP_SPINNER,
	};
};

/**
 * Gets all posts data using createAsyncThunk.
 */
export const GetAllPostsAsync = (accessToken: string) => {
	return async (dispatch: Dispatch<Action>) => {
		try {
			dispatch(StartLoader());
			const response = await GetAllPostsApiAsync(accessToken);
			if (response?.statusCode === 200) {
				dispatch({
					type: GET_ALL_POSTS_DATA,
					payload: response.data,
				});
			}
		} catch (error: any) {
			console.error(error);
			dispatch(PostDataFailure(error.data ?? error.title));
			dispatch(
				ToggleErrorToaster({
					shouldShow: true,
					errorMessage: error.data ?? error.title ?? error,
				})
			);
			throw error;
		} finally {
			dispatch(StopLoader());
		}
	};
};

/**
 * Gets the post data using createAsyncThunk.
 */
export const GetPostAsync = (postId: string, accessToken: string) => {
	return async (dispatch: Dispatch<Action>) => {
		try {
			dispatch(StartLoader());
			const response = await GetPostApiAsync(postId, accessToken);
			if (response?.statusCode === 200) {
				dispatch({
					type: GET_POST_DATA,
					payload: response?.data,
				});
			}
		} catch (error: any) {
			console.error(error);
			dispatch(PostDataFailure(error.data));
			dispatch(
				ToggleErrorToaster({
					shouldShow: true,
					errorMessage: error,
				})
			);
			throw error;
		} finally {
			dispatch(StopLoader());
		}
	};
};

/**
 * Adds a new post data.
 */
export const AddNewPostAsync = (postData: any, accessToken: string) => {
	return async (dispatch: Dispatch<Action>) => {
		try {
			dispatch(HandleCreatePostPageLoader(true));
			const response = await AddNewPostApiAsync(postData, accessToken);
			if (response?.statusCode === 200) {
				dispatch({
					type: ADD_NEW_POST_DATA,
					payload: response?.data,
				});
			}
		} catch (error) {
			console.error(error);
			dispatch(
				ToggleErrorToaster({
					shouldShow: true,
					errorMessage: error,
				})
			);
			throw error;
		} finally {
			dispatch(HandleCreatePostPageLoader(false));
		}
	};
};

/**
 * Saves the posts failure data to redux store.
 * @param data The api response.
 * @returns The action type and payload data.
 */
export const PostDataFailure = (data: any) => {
	return {
		type: POST_DATA_FAIL,
		payload: data,
	};
};

/**
 * Saves the create post page loader status to redux store.
 * @param isLoading The loader boolean flag.
 * @returns The action type and payload data.
 */
export const HandleCreatePostPageLoader = (isLoading: boolean) => {
	return {
		type: IS_CREATE_POST_LOADING,
		payload: isLoading,
	};
};

/**
 * Deletes a post using createAsyncThunk.
 */
export const DeletePostAsync = (postId: string, accessToken: string) => {
	return async (dispatch: Dispatch<Action>) => {
		try {
			dispatch(StartLoader());
			const response = await DeletePostApiAsync(postId, accessToken);
			if (response?.statusCode === 200) {
				// Refresh posts after deletion
				dispatch(GetAllPostsAsync(accessToken) as any);
				dispatch({
					type: DELETE_POST_DATA,
					payload: response?.data,
				});
			}
		} catch (error: any) {
			console.error(error);
			dispatch(PostDataFailure(error.data));
			dispatch(
				ToggleErrorToaster({
					shouldShow: true,
					errorMessage: error,
				})
			);
			throw error;
		} finally {
			dispatch(StopLoader());
		}
	};
};

/**
 * Updates a post using createAsyncThunk.
 */
export const UpdatePostAsync = (
	updatePostData: UpdatePostDtoModel,
	accessToken: string
) => {
	return async (dispatch: Dispatch<Action>) => {
		try {
			dispatch(ToggleEditPostSpinner(true));
			const response = await UpdatePostApiAsync(
				updatePostData,
				accessToken
			);
			if (response?.statusCode === 200) {
				dispatch(GetAllPostsAsync(accessToken) as any);
				dispatch({
					type: UPDATE_POST_DATA,
					payload: response?.data,
				});
				return response?.data;
			}
			throw new Error("Failed to update post");
		} catch (error: any) {
			console.error(error);
			dispatch(PostDataFailure(error.data));
			dispatch(
				ToggleErrorToaster({
					shouldShow: true,
					errorMessage: error,
				})
			);
			throw error;
		} finally {
			dispatch(ToggleEditPostSpinner(false));
			dispatch(ToggleEditPostDialog(false));
		}
	};
};

/**
 * Updates the post rating.
 * @param postRatingDtoModel The post rating dto model.
 * @param accessToken The access token
 * @returns The promise from the api response.
 */
export const UpdateRatingAsync = (
	postRatingDtoModel: PostRatingDtoModel,
	accessToken: string
) => {
	return async (dispatch: Dispatch<Action>) => {
		try {
			dispatch(ToggleRatingLoader(true));
			const response = await UpdateRatingApiAsync(
				postRatingDtoModel,
				accessToken
			);
			if (response?.statusCode === 200) {
				dispatch(GetAllPostsAsync(accessToken) as any);
				dispatch({
					type: UPDATE_POST_RATING,
					payload: response.data,
				});
			}
		} catch (error: any) {
			console.error(error);
			dispatch(PostDataFailure(error.data));
			dispatch(
				ToggleErrorToaster({
					shouldShow: true,
					errorMessage: error,
				})
			);
			throw error;
		} finally {
			dispatch(ToggleRatingLoader(false));
		}
	};
};

/**
 * Saves the rating loader toggle event data to redux store.
 * @param isLooading The boolean value for loading status.
 * @returns The action type and payload data.
 */
const ToggleRatingLoader = (isLoading: boolean) => {
	return {
		type: TOGGLE_VOTING_LOADER,
		payload: isLoading,
	};
};

/**
 * Stores the edit post dialog toggle state to redux store.
 * @param isOpen The is open boolean flag.
 * @returns The action type and payload data.
 */
export const ToggleEditPostDialog = (isOpen: boolean) => {
	return {
		type: TOGGLE_EDIT_POST_DIALOG,
		payload: isOpen,
	};
};

/**
 * Saves the edit post data to redux store.
 * @param data The edit post data.
 * @returns The action type and payload data.
 */
export const GetEditPostData = (data: any) => {
	return {
		type: GET_EDIT_POST_DATA,
		payload: data,
	};
};

/**
 * Stores the edit post spinner status to redux store.
 * @param isLoading The is loading boolean flag.
 * @returns The action type and payload data.
 */
export const ToggleEditPostSpinner = (isLoading: boolean) => {
	return {
		type: TOGGLE_EDIT_POST_LOADER,
		payload: isLoading,
	};
};
