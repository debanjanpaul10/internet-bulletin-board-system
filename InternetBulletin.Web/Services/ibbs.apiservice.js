// #region POSTS

import HttpUtility from "@helpers/http.utility";
import PostRatingDtoModel from "@models/PostRatingDto";

/**
 * Gets the post data from api.
 * @param {string} postId The post id.
 * @param {string} accessToken The access token.
 *
 * @returns {Promise} The promise of the response from api.
 */
export const GetPostApiAsync = async (postId, accessToken) => {
	return await HttpUtility.GetAsync(
		`Posts/GetPost?postId=${postId}`,
		accessToken
	);
};

/**
 * Gets all the posts data from api.
 * @returns {Promise} The promise of the response from api.
 */
export const GetAllPostsApiAsync = async (accessToken) => {
	return await HttpUtility.GetAsync(`Posts/GetAllPosts`, accessToken);
};

/**
 * Adds a new post data to api.
 * @param {Object} newPostData The new post data.
 * @param {string} accessToken The access token.
 *
 * @returns {Promise} The promise of the response from api.
 */
export const AddNewPostApiAsync = async (newPostData, accessToken) => {
	return await HttpUtility.PostAsync(
		`Posts/AddPost`,
		newPostData,
		accessToken
	);
};

/**
 * Updates an existing post data from api.
 * @param {Object} updatedPostData The updated post data.
 * @param {string} accessToken The access token.
 *
 * @returns {Promise} The promise of the response from api.
 */
export const UpdatePostApiAsync = async (updatedPostData, accessToken) => {
	return await HttpUtility.PostAsync(
		`Posts/UpdatePost`,
		updatedPostData,
		accessToken
	);
};

/**
 * Deletes an existing post data from api.
 * @param {string} postId The post id.
 * @param {string} accessToken The access token.
 *
 * @returns {Promise} The promise of the response from api.
 */
export const DeletePostApiAsync = async (postId, accessToken) => {
	return await HttpUtility.PostAsync(
		`Posts/DeletePost?postId=${postId}`,
		null,
		accessToken
	);
};

/**
 * Update the post rating data to api.
 * @param {PostRatingDtoModel} postRatingModel The post rating dto model.
 * @param {string} accessToken The access token.
 *
 * @returns {Promise} The promise of the response from api.
 */
export const UpdateRatingApiAsync = async (postRatingModel, accessToken) => {
	return await HttpUtility.PostAsync(
		`PostRatings/UpdateRating`,
		postRatingModel,
		accessToken
	);
};

// #endregion

// #region USERS

/**
 * Gets the user data from api.
 * @param {Object} userData The user id.
 * @returns {Promise} The promise of the response from api.
 */
export const GetUserApiAsync = (userData) => {
	return HttpUtility.PostAsync(`Users/GetUser`, userData);
};

/**
 * Gets all the users data from api.
 * @returns {Promise} The promise of the response from api.
 */
export const GetAllUsersApiAsync = async () => {
	return await HttpUtility.GetAsync(`Users/GetAllUsers`);
};

/**
 * Adds a new user data to api.
 * @param {Object} userData The new user data.
 * @returns {Promise} The promise of the response from api.
 */
export const AddNewUserApiAsync = async (userData) => {
	return await HttpUtility.PostAsync(`Users/NewUser`, userData);
};

/**
 * Gets the user profile data from api.
 * @param {number} userId The user id.
 * @returns {Promise} The promise of the response from api.
 */
export const GetUserProfileDataApiAsync = async (userId) => {
	return await HttpUtility.GetAsync(
		`Profiles/GetUserProfileData?userid=${userId}`
	);
};

// #endregion

// #region Configuration

/**
 * Gets the configuration value.
 * @param {string} keyName The configuration key name.
 * @returns {Promise} The promise of the response from api.
 */
export const GetConfigurationApiAsync = async (keyName) => {
	return await HttpUtility.GetAsync(
		`Configuration/GetConfiguration?keyName=${keyName}`
	);
};

// #endregion

// #region AI

/**
 * Posts the rewrite story with ai api.
 * @param {string} storyText The story text.
 * @returns {Promise} The promise of the response from api.
 */
export const PostRewriteStoryWithAiApiAsync = async (storyText) => {
	return await HttpUtility.PostAIAsync(storyText);
};

// #endregion
