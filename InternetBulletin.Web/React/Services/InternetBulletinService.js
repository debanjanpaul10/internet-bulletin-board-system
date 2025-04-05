import { GetAsync, PostAIAsync, PostAsync } from "@helpers/HttpUtility";
import mockPostsData from "../Mock/Posts.json";

// #region POSTS

/**
 * Gets the post data from api.
 * @param {string} postId The post id.
 * @returns {Promise} The promise of the response from api.
 */
export const GetPostApiAsync = async (postId) => {
	return await GetAsync(`Posts/GetPost?postId=${postId}`);
};

/**
 * Gets all the posts data from api.
 * @returns {Promise} The promise of the response from api.
 */
export const GetAllPostsApiAsync = async () => {
	// return mockPostsData;
	return await GetAsync(`Posts/GetAllPosts`);
};

/**
 * Adds a new post data to api.
 * @param {Object} newPostData The new post data.
 * @returns {Promise} The promise of the response from api.
 */
export const AddNewPostApiAsync = async (newPostData) => {
	return await PostAsync(`Posts/AddPost`, newPostData);
};

/**
 * Updates an existing post data from api.
 * @param {Object} updatedPostData The updated post data.
 * @returns {Promise} The promise of the response from api.
 */
export const UpdatePostApiAsync = async (updatedPostData) => {
	return await PostAsync(`Posts/UpdatePost`, updatedPostData);
};

/**
 * Deletes an existing post data from api.
 * @param {string} postId The post id.
 * @returns {Promise} The promise of the response from api.
 */
export const DeletePostApiAsync = async (postId) => {
	return await PostAsync(`Posts/DeletePost`, postId);
};

// #endregion

// #region USERS

/**
 * Gets the user data from api.
 * @param {Object} userData The user id.
 * @returns {Promise} The promise of the response from api.
 */
export const GetUserApiAsync = (userData) => {
	return PostAsync(`Users/GetUser`, userData);
};

/**
 * Gets all the users data from api.
 * @returns {Promise} The promise of the response from api.
 */
export const GetAllUsersApiAsync = async () => {
	return await GetAsync(`Users/GetAllUsers`);
};

/**
 * Adds a new user data to api.
 * @param {Object} userData The new user data.
 * @returns {Promise} The promise of the response from api.
 */
export const AddNewUserApiAsync = async (userData) => {
	return await PostAsync(`Users/NewUser`, userData);
};

/**
 * Gets the user profile data from api.
 * @param {number} userId The user id.
 * @returns {Promise} The promise of the response from api.
 */
export const GetUserProfileDataApiAsync = async (userId) => {
    return await GetAsync(`Profiles/GetUserProfileData?userid=${userId}`);
}

// #endregion

// #region Configuration

/**
 * Gets the configuration value.
 * @param {string} keyName The configuration key name.
 * @returns {Promise} The promise of the response from api.
 */
export const GetConfigurationApiAsync = async (keyName) => {
	return await GetAsync(`Configuration/GetConfiguration?keyName=${keyName}`);
};

// #endregion

// #region AI

/**
 * Posts the rewrite story with ai api.
 * @param {string} storyText The story text.
 * @returns {Promise} The promise of the response from api.
 */
export const PostRewriteStoryWithAiApiAsync = async (storyText) => {
	return await PostAIAsync(storyText);
};

// #endregion
