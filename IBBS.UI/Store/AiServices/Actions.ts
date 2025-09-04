import { Action, Dispatch } from "@reduxjs/toolkit";
import {
	GET_CHATBOT_RESPONSE,
	HANDLE_POST_AI_MODERATION,
	REWRITE_STORY_AI,
	SAVE_AI_FEEDBACK_RESPONSE,
	TOGGLE_AI_FEEDBACK_SPINNER,
	TOGGLE_CHATBOT_LOADING,
	TOGGLE_REWRITE_LOADER,
} from "./ActionTypes";
import {
	GenerateTagForStoryApiAsync,
	GetChatbotResponseAsync,
	ModerateContentDataApiAsync,
	PostAiResultFeedbackAsync,
	PostRewriteStoryWithAiApiAsync,
} from "@/Services/ibbs.apiservice";
import { ToggleErrorToaster } from "../Common/Actions";
import UserStoryRequestDtoModel from "@/Models/UserStoryRequestDto";
import { HandleCreatePostPageLoader, PostDataFailure } from "../Posts/Actions";
import { UserQueryRequestDTO } from "@/Models/DTOs/user-query-request.dto";
import { AIChatbotResponseDTO } from "@/Models/DTOs/ai-chatbot-response.dto";
import { AIResponseFeedbackDTO } from "@/Models/DTOs/ai-response-feedback.dto";

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

export const HandleAiResultFeedbackAsync = (
	responseFeedback: AIResponseFeedbackDTO,
	accessToken: string
) => {
	return async (dispatch: Dispatch<Action>) => {
		try {
			dispatch(ToggleFeedbackSpinner(true));
			const response = await PostAiResultFeedbackAsync(
				responseFeedback,
				accessToken
			);
			if (response?.data) {
				dispatch({
					type: SAVE_AI_FEEDBACK_RESPONSE,
					payload: response.data,
				});
			}
		} catch (error) {
			console.error(error);
		} finally {
			dispatch(ToggleFeedbackSpinner(false));
		}
	};
};

export const ToggleFeedbackSpinner = (isLoading: boolean) => {
	return {
		type: TOGGLE_AI_FEEDBACK_SPINNER,
		payload: isLoading,
	};
};
