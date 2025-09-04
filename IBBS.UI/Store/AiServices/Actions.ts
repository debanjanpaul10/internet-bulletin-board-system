import { Action, Dispatch } from "@reduxjs/toolkit";
import {
	GET_APPLICATION_INFORMATION,
	GET_CHATBOT_RESPONSE,
	HANDLE_POST_AI_MODERATION,
	REWRITE_STORY_AI,
	TOGGLE_ABOUT_US_SPINNER,
	TOGGLE_CHATBOT_LOADING,
	TOGGLE_REWRITE_LOADER,
} from "./ActionTypes";
import {
	GenerateTagForStoryApiAsync,
	GetApplicationInformationDataApiAsync,
	GetChatbotResponseAsync,
	ModerateContentDataApiAsync,
	PostRewriteStoryWithAiApiAsync,
} from "@/Services/ibbs.apiservice";
import { ToggleErrorToaster } from "../Common/Actions";
import UserStoryRequestDtoModel from "@/Models/UserStoryRequestDto";
import { HandleCreatePostPageLoader, PostDataFailure } from "../Posts/Actions";
import { UserQueryRequestDTO } from "@/Models/DTOs/user-query-request.dto";
import { AIChatbotResponseDTO } from "@/Models/DTOs/ai-chatbot-response.dto";

/**
 * Stores the toggle event for rewrite text loading.
 * @param isLoading The is loading boolean flag.
 * @returns The action type and payload data.
 */
export const ToggleRewriteLoader = (isLoading: boolean) => {
	return {
		type: TOGGLE_REWRITE_LOADER,
		payload: isLoading,
	};
};

/**
 * Gets the application information data.
 * @param accessToken The access token.
 * @returns The promise of the api response.
 */
export const GetApplicationInformationDataAsync = (
	accessToken: string = ""
) => {
	return async (dispatch: Dispatch<Action>) => {
		try {
			dispatch(ToggleAboutUsSpinner(true));
			const response = await GetApplicationInformationDataApiAsync(
				accessToken
			);

			dispatch({
				type: GET_APPLICATION_INFORMATION,
				payload: response?.data,
			});
		} catch (error: any) {
			console.error(error);
			dispatch(
				ToggleErrorToaster({
					shouldShow: true,
					errorMessage: error.data ?? error.title ?? error,
				})
			);
		} finally {
			dispatch(ToggleAboutUsSpinner(false));
		}
	};
};

/**
 * Toggles the About Us spinner.
 * @param isLoading The is loading flag.
 * @returns The action type and payload data.
 */
const ToggleAboutUsSpinner = (isLoading: boolean) => {
	return {
		type: TOGGLE_ABOUT_US_SPINNER,
		payload: isLoading,
	};
};

/**
 * Rewrites story with AI using createAsyncThunk.
 */
export const RewriteStoryWithAiAsync = (
	requestDto: any,
	accessToken: string
) => {
	return async (dispatch: Dispatch<Action>) => {
		try {
			dispatch(ToggleRewriteLoader(true));
			const response = await PostRewriteStoryWithAiApiAsync(
				requestDto,
				accessToken
			);
			if (response?.statusCode === 200) {
				dispatch(RewriteStoryWithAiSuccess(response.data));
			}
			throw new Error("Failed to rewrite story");
		} catch (error: any) {
			console.error(error);
			dispatch(
				ToggleErrorToaster({
					shouldShow: true,
					errorMessage: error.title,
				})
			);
			throw error;
		} finally {
			dispatch(ToggleRewriteLoader(false));
		}
	};
};

/**
 * Saves the AI response data to redux store.
 * @param data The api response.
 * @returns The action type and payload data.
 */
export const RewriteStoryWithAiSuccess = (data: any) => {
	return {
		type: REWRITE_STORY_AI,
		payload: data,
	};
};

/**
 * Handles AI moderation tasks using createAsyncThunk.
 */
export const HandlePostAiModerationTasksAsync = (
	userStoryRequestDto: UserStoryRequestDtoModel,
	accessToken: string
) => {
	return async (dispatch: Dispatch<Action>) => {
		try {
			dispatch(HandleCreatePostPageLoader(true));
			const tagResponseTask = GenerateTagForStoryApiAsync(
				userStoryRequestDto,
				accessToken
			);
			const moderateContentResponseTask = ModerateContentDataApiAsync(
				userStoryRequestDto,
				accessToken
			);

			const [tagResponse, moderationContentResponse] = await Promise.all([
				tagResponseTask,
				moderateContentResponseTask,
			]);

			if (tagResponse?.data && moderationContentResponse?.data) {
				dispatch(
					HandlePostAiModerationTasksSuccess(
						tagResponse?.data,
						moderationContentResponse?.data
					)
				);
			}
			throw new Error("Failed to get AI moderation data");
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
			dispatch(HandleCreatePostPageLoader(false));
		}
	};
};

/**
 * Stores the AI moderation data to redux store.
 * @param tagData The tag data.
 * @param moderationData The NSFW flag.
 * @returns The action type and payload data.
 */
export const HandlePostAiModerationTasksSuccess = (
	tagData: any,
	moderationData: any
) => {
	return {
		type: HANDLE_POST_AI_MODERATION,
		payload: {
			tagData: tagData,
			moderationData: moderationData,
		},
	};
};

export const HandleChatbotResponseAsync = (
	userQueryRequest: UserQueryRequestDTO,
	accessToken: string
) => {
	return async (dispatch: Dispatch<Action>) => {
		try {
			dispatch(ToggleChatbotLoading(true));
			const response = await GetChatbotResponseAsync(
				userQueryRequest,
				accessToken
			);
			if (response?.data) {
				dispatch(GetChatbotResponseSuccess(response.data));
				return response.data as AIChatbotResponseDTO;
			}
			return null;
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
			dispatch(ToggleChatbotLoading(false));
		}
	};
};

export const ToggleChatbotLoading = (isLoading: boolean) => {
	return {
		type: TOGGLE_CHATBOT_LOADING,
		payload: isLoading,
	};
};

export const GetChatbotResponseSuccess = (data: AIChatbotResponseDTO) => {
	return {
		type: GET_CHATBOT_RESPONSE,
		payload: data,
	};
};
