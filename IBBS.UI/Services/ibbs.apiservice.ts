// #region POSTS

import HttpUtility from "@/Helpers/http.utility";
import AddPostDtoModel from "@/Models/AddPostDto";
import { AIResponseFeedbackDTO } from "@/Models/DTOs/ai-response-feedback.dto";
import { UserQueryRequestDTO } from "@/Models/DTOs/user-query-request.dto";
import PostRatingDtoModel from "@/Models/PostRatingDto";
import UpdatePostDtoModel from "@/Models/UpdatePostDto";
import UserStoryRequestDtoModel from "@/Models/UserStoryRequestDto";

/**
 * Gets the post data from api.
 * @param postId The post id.
 * @param accessToken The access token.
 *
 * @returns The promise of the response from api.
 */
export const GetPostApiAsync = async (postId: string, accessToken: string) => {
	return await HttpUtility.GetAsync(
		`Posts/GetPost?postId=${postId}`,
		accessToken
	);
};

/**
 * Gets all the posts data from api.
 * @returns The promise of the response from api.
 */
export const GetAllPostsApiAsync = async (accessToken: string) => {
	return await HttpUtility.GetAsync(`Posts/GetAllPosts`, accessToken);
};

/**
 * Adds a new post data to api.
 * @param newPostData The new post data.
 * @param accessToken The access token.
 *
 * @returns The promise of the response from api.
 */
export const AddNewPostApiAsync = async (
	newPostData: AddPostDtoModel,
	accessToken: string
) => {
	return await HttpUtility.PostAsync(
		`Posts/AddPost`,
		newPostData,
		accessToken
	);
};

/**
 * Updates an existing post data from api.
 * @param updatedPostData The updated post data.
 * @param accessToken The access token.
 *
 * @returns The promise of the response from api.
 */
export const UpdatePostApiAsync = async (
	updatedPostData: UpdatePostDtoModel,
	accessToken: string
) => {
	return await HttpUtility.PostAsync(
		`Posts/UpdatePost`,
		updatedPostData,
		accessToken
	);
};

/**
 * Deletes an existing post data from api.
 * @param postId The post id.
 * @param accessToken The access token.
 *
 * @returns The promise of the response from api.
 */
export const DeletePostApiAsync = async (
	postId: string,
	accessToken: string
) => {
	return await HttpUtility.PostAsync(
		`Posts/DeletePost?postId=${postId}`,
		null,
		accessToken
	);
};

/**
 * Update the post rating data to api.
 * @param postRatingModel The post rating dto model.
 * @param accessToken The access token.
 *
 * @returns The promise of the response from api.
 */
export const UpdateRatingApiAsync = async (
	postRatingModel: PostRatingDtoModel,
	accessToken: string
) => {
	return await HttpUtility.PostAsync(
		`PostRatings/UpdateRating`,
		postRatingModel,
		accessToken
	);
};

// #endregion

// #region CONFIGURATION

/**
 * Gets the configuration value.
 * @param keyName The configuration key name.
 * @returns The promise of the response from api.
 */
export const GetConfigurationApiAsync = async (keyName: string) => {
	return await HttpUtility.GetAsync(
		`Configuration/GetConfiguration?keyName=${keyName}`,
		""
	);
};

// #endregion

// #region AI Services

/**
 * Posts the rewrite story with ai api.
 * @param storyText The story text.
 * @param accessToken The access token.
 * @returns The promise of the response from api.
 */
export const PostRewriteStoryWithAiApiAsync = async (
	storyText: string,
	accessToken: string
) => {
	return await HttpUtility.PostAsync(
		"AiServices/RewriteWithAI",
		storyText,
		accessToken
	);
};

export const GenerateTagForStoryApiAsync = async (
	storyText: UserStoryRequestDtoModel,
	accessToken: string
) => {
	return await HttpUtility.PostAsync(
		"AiServices/GenerateGenreTag",
		storyText,
		accessToken
	);
};

export const ModerateContentDataApiAsync = async (
	storyText: UserStoryRequestDtoModel,
	accessToken: string
) => {
	return await HttpUtility.PostAsync(
		"AiServices/ModerateContent",
		storyText,
		accessToken
	);
};

export const GetChatbotResponseAsync = async (
	userQueryRequest: UserQueryRequestDTO,
	accessToken: string
) => {
	return await HttpUtility.PostAsync(
		"AiServices/respond",
		userQueryRequest,
		accessToken
	);
};

export const PostAiResultFeedbackAsync = async (
	aiResponseFeedback: AIResponseFeedbackDTO,
	accessToken: string
) => {
	return await HttpUtility.PostAsync(
		"AiServices/aifeedback",
		aiResponseFeedback,
		accessToken
	);
};

// #endregion

// #region Profile

/**
 * Gets the user profiles data.
 * @param accessToken The access token.
 * @returns The promise of the response from api.
 */
export const GetUserProfilesDataApiAsync = async (accessToken: string) => {
	return await HttpUtility.GetAsync(
		"Profiles/GetUserProfileData",
		accessToken
	);
};

// #endregion
