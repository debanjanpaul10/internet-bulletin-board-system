import { GetAsync, PostAIAsync, PostAsync } from "@helpers/http.utility";

// #region POSTS

/**
 * Gets the post data from api.
 * @param {string} postId The post id.
 * @returns {Promise} The promise of the response from api.
 */
export const GetPostApiAsync = async (postId, getIdTokenClaims) => {
	return await GetAsync(`Posts/GetPost?postId=${postId}`, getIdTokenClaims);
};

/**
 * Gets all the posts data from api.
 * @param {Promse} getAccessTokenSilently Gets the access token.
 * @returns {Promise} The promise of the response from api.
 */
export const GetAllPostsApiAsync = async (getIdTokenClaims) => {
	// return mockPostsData;
	return await GetAsync(`Posts/GetAllPosts`, getIdTokenClaims);
};

/**
 * Adds a new post data to api.
 * @param {Object} newPostData The new post data.
 * @returns {Promise} The promise of the response from api.
 */
export const AddNewPostApiAsync = async (newPostData, getIdTokenClaims) => {
	return await PostAsync(`Posts/AddPost`, newPostData, getIdTokenClaims);
};

/**
 * Updates an existing post data from api.
 * @param {Object} updatedPostData The updated post data.
 * @returns {Promise} The promise of the response from api.
 */
export const UpdatePostApiAsync = async (updatedPostData, getIdTokenClaims) => {
	return await PostAsync(
		`Posts/UpdatePost`,
		updatedPostData,
		getIdTokenClaims
	);
};

/**
 * Deletes an existing post data from api.
 * @param {string} postId The post id.
 * @returns {Promise} The promise of the response from api.
 */
export const DeletePostApiAsync = async (postId, getIdTokenClaims) => {
	return await PostAsync(`Posts/DeletePost`, postId, getIdTokenClaims);
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
};

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
