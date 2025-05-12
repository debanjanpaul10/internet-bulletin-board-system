import {
	AddNewPostApiAsync,
	DeletePostApiAsync,
	GetAllPostsApiAsync,
	GetPostApiAsync,
	PostRewriteStoryWithAiApiAsync,
} from "@services/InternetBulletinService";
import { ToggleErrorToaster } from "@store/Common/Actions";
import {
	ADD_NEW_POST_DATA,
	DELETE_POST_DATA,
	GET_ALL_POSTS_DATA,
	GET_POST_DATA,
	IS_CREATE_POST_LOADING,
	POST_DATA_FAIL,
	REWRITE_STORY_AI,
	START_SPINNER,
	STOP_SPINNER,
} from "@store/Posts/ActionTypes";

/**
 * Saves the loader start status to redux store.
 * @returns {Object} The action type
 */
export const StartLoader = () => {
	return {
		type: START_SPINNER,
	};
};

/**
 * Saves the loader stop status to redux store.
 * @returns {Object} The action type
 */
export const StopLoader = () => {
	return {
		type: STOP_SPINNER,
	};
};

/**
 * Gets the post data from api.
 * @param {string} postId The user data.
 * @param {Promise} getIdTokenClaims Gets the access token.
 * @returns {Promise} The promise from the api response.
 */
export const GetPostAsync = (postId, getIdTokenClaims) => {
	return async (dispatch) => {
		try {
			dispatch(StartLoader());
			const response = await GetPostApiAsync(postId, getIdTokenClaims);
			if (response?.statusCode === 200) {
				dispatch(GetPostSuccess(response.data));
			}
		} catch (error) {
			console.error(error);
			dispatch(PostDataFailure(error.data));
			dispatch(
				ToggleErrorToaster({
					shouldShow: true,
					errorMessage: error,
				})
			);
		} finally {
			dispatch(StopLoader());
		}
	};
};

/**
 * Saves the post data to redux store.
 * @param {Object} data The api response.
 * @returns {Object} The action type and payload data.
 */
const GetPostSuccess = (data) => {
	return {
		type: GET_POST_DATA,
		payload: data,
	};
};

/**
 * Gets all posts data.
 * @param {Promise} getIdTokenClaims Gets the access token.
 * @returns {Promise} The promise from the api response.
 */
export const GetAllPostsAsync = (getIdTokenClaims) => {
	return async (dispatch) => {
		try {
			dispatch(StartLoader());
			const response = await GetAllPostsApiAsync(getIdTokenClaims);
			if (response?.statusCode === 200) {
				dispatch(GetAllPostsSuccess(response.data));
			}
		} catch (error) {
			console.error(error);
			dispatch(PostDataFailure(error.data));
			dispatch(
				ToggleErrorToaster({
					shouldShow: true,
					errorMessage: error,
				})
			);
		} finally {
			dispatch(StopLoader());
		}
	};
};

/**
 * Saves the all posts data to redux store
 * @param {Object} data The api response.
 * @returns {Object} The action type and payload data.
 */
const GetAllPostsSuccess = (data) => {
	return {
		type: GET_ALL_POSTS_DATA,
		payload: data,
	};
};

/**
 * Adds an new post data.
 * @param {Object} userData The user data object.
 * @param {Promise} getIdTokenClaims Gets the access token.
 * @returns {Promise} The promise from the api response.
 */
export const AddNewPostAsync = (postData, getIdTokenClaims) => {
	return async (dispatch) => {
		try {
			dispatch(HandleCreatePostPageLoader(true));
			const response = await AddNewPostApiAsync(
				postData,
				getIdTokenClaims
			);
			if (response?.statusCode === 200) {
				dispatch(AddNewPostSuccess(response?.data));
			}
		} catch (error) {
			console.error(error);
			dispatch(
				ToggleErrorToaster({
					shouldShow: true,
					errorMessage: error,
				})
			);
		} finally {
			dispatch(HandleCreatePostPageLoader(false));
		}
	};
};

/**
 * Saves the new post data to redux store.
 * @param {Object} data The api response.
 * @returns {Object} The action type and payload data.
 */
const AddNewPostSuccess = (data) => {
	return {
		type: ADD_NEW_POST_DATA,
		payload: data,
	};
};

/**
 * Saves the posts failure data to redux store.
 * @param {Object} data The api response.
 * @returns {Object} The action type and payload data.
 */
export const PostDataFailure = (data) => {
	return {
		type: POST_DATA_FAIL,
		payload: data,
	};
};

/**
 * Rewrites the story with AI.
 * @param {string} story The story string.
 * @returns {Promise} The promise from the api response.
 */
export const RewriteStoryWithAiAsync = (story) => {
	return async (dispatch) => {
		try {
			dispatch(HandleCreatePostPageLoader(true));
			const response = await PostRewriteStoryWithAiApiAsync(story);
			if (response?.statusCode === 200) {
				dispatch(RewriteStoryWithAiSuccess(response?.data));
			}
		} catch (error) {
			console.error(error);
			dispatch(
				ToggleErrorToaster({
					shouldShow: true,
					errorMessage: error,
				})
			);
		} finally {
			dispatch(HandleCreatePostPageLoader(false));
		}
	};
};

/**
 * Saves the AI response data to redux store.
 * @param {Object} data The api response.
 * @returns {Object} The action type and payload data.
 */
export const RewriteStoryWithAiSuccess = (data) => {
	return {
		type: REWRITE_STORY_AI,
		payload: data,
	};
};

/**
 * Saves the create post page loader status to redux store.
 * @param {boolean} isLoading The loader boolean flag.
 * @returns {Object} The action type and payload data.
 */
export const HandleCreatePostPageLoader = (isLoading) => {
	return {
		type: IS_CREATE_POST_LOADING,
		payload: isLoading,
	};
};

/**
 * Deletes a post data asynchronously.
 * @param {string} postId The post id.
 * @param {Promise} getIdTokenClaims Gets the access token.
 * @returns {Promise} Gets the API response.
 */
export const DeletePostAsync = (postId, getIdTokenClaims) => {
	return async (dispatch) => {
		try {
			dispatch(StartLoader());
			const response = await DeletePostApiAsync(postId, getIdTokenClaims);
			if (response?.statusCode === 200) {
				dispatch(DeletePostAsyncSuccess(response?.data));
				dispatch(GetAllPostsAsync(getIdTokenClaims));
			}
		} catch (error) {
			console.error(error);
			dispatch(PostDataFailure(error.data));
			dispatch(
				ToggleErrorToaster({
					shouldShow: true,
					errorMessage: error,
				})
			);
		} finally {
			dispatch(StopLoader());
		}
	};
};

/**
 * Saves the delete post api response to redux store.
 * @param {boolean} data The API response.
 * @returns {Object} The action type and payload data.
 */
const DeletePostAsyncSuccess = (data) => {
	return {
		type: DELETE_POST_DATA,
		payload: data,
	};
};
