import { GetAsync, PostAsync } from "@helpers/HttpUtility";
import mockPostsData from "../Mock/Posts.json";

// #region POSTS

/**
 * Gets the post data from api.
 * @param {string} postId The post id.
 * @returns {Promise} The promise of the response from api.
 */
export const GetPostAsync = async (postId) => {
	return await GetAsync(`Posts/GetPost?postId=${postId}`);
};

/**
 * Gets all the posts data from api.
 * @returns {Promise} The promise of the response from api.
 */
export const GetAllPostsAsync = async () => {
	// return mockPostsData;
	return await GetAsync(`Posts/GetAllPosts`);
};

/**
 * Adds a new post data to api.
 * @param {Object} newPostData The new post data.
 * @returns {Promise} The promise of the response from api.
 */
export const AddNewPostAsync = async (newPostData) => {
	return await PostAsync(`Posts/AddPost`, newPostData);
};

/**
 * Updates an existing post data from api.
 * @param {Object} updatedPostData The updated post data.
 * @returns {Promise} The promise of the response from api.
 */
export const UpdatePostAsync = async (updatedPostData) => {
	return await PostAsync(`Posts/UpdatePost`, updatedPostData);
};

/**
 * Deletes an existing post data from api.
 * @param {string} postId The post id.
 * @returns {Promise} The promise of the response from api.
 */
export const DeletePostAsync = async (postId) => {
	return await PostAsync(`Posts/DeletePost`, postId);
};

// #endregion

// #region USERS

/**
 * Gets the user data from api.
 * @param {Object} userData The user id.
 * @returns {Promise} The promise of the response from api.
 */
export const GetUserAsync = (userData) => {
	return PostAsync(`Users/GetUser`, userData);
};

/**
 * Gets all the users data from api.
 * @returns {Promise} The promise of the response from api.
 */
export const GetAllUsersAsync = async () => {
	return await GetAsync(`Users/GetAllUsers`);
};

/**
 * Adds a new user data to api.
 * @param {Object} userData The new user data.
 * @returns {Promise} The promise of the response from api.
 */
export const AddNewUserAsync = async (userData) => {
	return await PostAsync(`Users/NewUser`, userData);
};

// #endregion

// #region Configuration

/**
 * Gets the configuration value.
 * @param {string} keyName The configuration key name.
 * @returns {Promise} The promise of the response from api.
 */
export const GetConfigurationAsync = async (keyName) => {
	return await GetAsync(`Configuration/GetConfiguration?keyName=${keyName}`);
};

// #endregion
