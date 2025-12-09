import { Action, Dispatch } from "@reduxjs/toolkit";
import {
	GET_BUG_SEVERITY_AI_STATUS,
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
	GetBugSeverityStatusApiAsync,
	GetChatbotResponseAsync,
	ModerateContentDataApiAsync,
	PostAiResultFeedbackApiAsync,
	PostRewriteStoryWithAiApiAsync,
} from "@services/ibbs.apiservice";
import { ToggleErrorToaster } from "../Common/Actions";
import UserStoryRequestDtoModel from "@models/UserStoryRequestDto";
import { HandleCreatePostPageLoader, PostDataFailure } from "../Posts/Actions";
import { UserQueryRequestDTO } from "@models/DTOs/user-query-request.dto";
import { AIChatbotResponseDTO } from "@models/DTOs/ai-chatbot-response.dto";
import { AIResponseFeedbackDTO } from "@models/DTOs/ai-response-feedback.dto";
import { BugSeverityAIRequestDTO } from "@models/DTOs/bug-severity-ai-request.dto";
import { TOGGLE_BUG_REPORT_SPINNER } from "../Common/ActionTypes";

/**
 * Rewrites story with AI using createAsyncThunk.
 */
export const RewriteStoryWithAiAsync = (
	requestDto: any,
	accessToken: string
) => {
	return async (dispatch: Dispatch<Action>) => {
		try {
			dispatch({
				type: TOGGLE_REWRITE_LOADER,
				payload: true,
			});
			const response = await PostRewriteStoryWithAiApiAsync(
				requestDto,
				accessToken
			);
			if (response?.statusCode === 200) {
				dispatch(RewriteStoryWithAiSuccess(response.data));
			}
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
			dispatch({
				type: TOGGLE_REWRITE_LOADER,
				payload: false,
			});
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
			dispatch({
				type: TOGGLE_CHATBOT_LOADING,
				payload: true,
			});
			const response = await GetChatbotResponseAsync(
				userQueryRequest,
				accessToken
			);
			if (response?.data) {
				dispatch({
					type: GET_CHATBOT_RESPONSE,
					payload: response.data,
				});
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
			dispatch({
				type: TOGGLE_CHATBOT_LOADING,
				payload: false,
			});
		}
	};
};

/**
 * Handles the AI response feedback from user.
 * @param responseFeedback The response feedback from user.
 * @param accessToken The access token.
 * @returns The payload data from the API response.
 */
export const HandleAiResultFeedbackAsync = (
	responseFeedback: AIResponseFeedbackDTO,
	accessToken: string
) => {
	return async (dispatch: Dispatch<Action>) => {
		try {
			dispatch({
				type: TOGGLE_BUG_REPORT_SPINNER,
				payload: true,
			});
			const response = await PostAiResultFeedbackApiAsync(
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
			dispatch(
				ToggleErrorToaster({
					shouldShow: true,
					errorMessage: error,
				})
			);
		} finally {
			dispatch({
				type: TOGGLE_AI_FEEDBACK_SPINNER,
				payload: false,
			});
		}
	};
};

/**
 * Gets the bug severity status.
 * @param bugSeverityInput The bug severity request input dto.
 * @param accessToken The access token.
 * @returns The payload data from API response.
 */
export const GetBugSeverityStatusAsync = (
	bugSeverityInput: BugSeverityAIRequestDTO,
	accessToken: string
) => {
	return async (dispatch: Dispatch<Action>) => {
		try {
			dispatch({
				type: TOGGLE_BUG_REPORT_SPINNER,
				payload: true,
			});

			const response = await GetBugSeverityStatusApiAsync(
				bugSeverityInput,
				accessToken
			);
			if (response?.data) {
				dispatch({
					type: GET_BUG_SEVERITY_AI_STATUS,
					payload: response.data,
				});
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
			dispatch({
				type: TOGGLE_BUG_REPORT_SPINNER,
				payload: false,
			});
		}
	};
};
