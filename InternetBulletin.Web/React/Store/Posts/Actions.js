import {
	AddNewPostAsync,
	GetAllPostsAsync,
	GetPostAsync,
} from "@helpers/InternetBulletinService";
import {
	ADD_NEW_POST_DATA,
	GET_ALL_POSTS_DATA,
	GET_POST_DATA,
	POST_DATA_FAIL,
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
 * @returns {Promise} The promise from the api response.
 */
export const GetPostDataAsync = (postId) => {
	return async (dispatch) => {
		try {
			dispatch(StartLoader());
			const response = await GetPostAsync(postId);
			if (response?.statusCode === 200) {
				dispatch(GetPostDataSuccess(response.data));
			}
		} catch (error) {
			console.error(error);
			dispatch(PostDataFailure(error.data));
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
const GetPostDataSuccess = (data) => {
	return {
		type: GET_POST_DATA,
		payload: data,
	};
};

/**
 * Gets all posts data.
 * @returns {Promise} The promise from the api response.
 */
export const GetAllPostsDataAsync = () => {
	return async (dispatch) => {
		try {
			dispatch(StartLoader());
			const response = await GetAllPostsAsync();
			if (response?.statusCode === 200) {
				dispatch(GetAllPostsDataSuccess(response.data));
			}
		} catch (error) {
			console.error(error);
			dispatch(PostDataFailure(error.data));
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
const GetAllPostsDataSuccess = (data) => {
	return {
		type: GET_ALL_POSTS_DATA,
		payload: data,
	};
};

/**
 * Adds an new post data.
 * @param {Object} userData The user data object.
 * @returns {Promise} The promise from the api response.
 */
export const AddNewPostDataAsync = (postData) => {
	return async (dispatch) => {
		try {
			dispatch(StartLoader());
			const response = await AddNewPostAsync(postData);
			if (response?.statusCode === 200) {
				dispatch(AddNewPostDataSuccess(response?.data));
			}
		} catch (error) {
			console.error();
		}
	};
};

/**
 * Saves the new post data to redux store.
 * @param {Object} data The api response.
 * @returns {Object} The action type and payload data.
 */
const AddNewPostDataSuccess = (data) => {
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
