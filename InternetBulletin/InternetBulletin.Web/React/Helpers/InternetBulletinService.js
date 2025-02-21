import { GetAsync, PostAsync } from "@helpers/HttpUtility";

// #region POSTS

/**
 * Gets the post data from api.
 * @param {string} postId The post id.
 * @returns {Promise} The promise of the response from api.
 */
export const GetPostAsync = (postId) => {
	return GetAsync(`Posts/GetPost?postId=${postId}`);
};

/**
 * Gets all the posts data from api.
 * @returns {Promise} The promise of the response from api.
 */
export const GetAllPostsAsync = () => {
	return GetAsync(`Posts/GetAllPosts`);
};

/**
 * Adds a new post data to api.
 * @param {Object} newPostData The new post data.
 * @returns {Promise} The promise of the response from api.
 */
export const AddNewPostAsync = (newPostData) => {
	return PostAsync(`Posts/AddPost`, newPostData);
};

/**
 * Updates an existing post data from api.
 * @param {Object} updatedPostData The updated post data.
 * @returns {Promise} The promise of the response from api.
 */
export const UpdatePostAsync = (updatedPostData) => {
	return PostAsync(`Posts/UpdatePost`, updatedPostData);
};

/**
 * Deletes an existing post data from api.
 * @param {string} postId The post id.
 * @returns {Promise} The promise of the response from api.
 */
export const DeletePostAsync = (postId) => {
	return PostAsync(`Posts/DeletePost`, postId);
};

// #endregion

// #region USERS

/**
 * Gets the user data from api.
 * @param {string} userId The user id.
 * @returns {Promise} The promise of the response from api.
 */
export const GetUserAsync = (userId) => {
	return GetAsync(`Users/GetUser?userId=${userId}`);
};

/**
 * Gets all the users data from api.
 * @returns {Promise} The promise of the response from api.
 */
export const GetAllUsersAsync = () => {
	return GetAsync(`Users/GetAllUsers`);
};

/**
 * Adds a new user data to api.
 * @param {Object} userData The new user data.
 * @returns {Promise} The promise of the response from api.
 */
export const AddNewUserAsync = (userData) => {
    return PostAsync(`Users/NewUser`, userData);
}

// #endregion