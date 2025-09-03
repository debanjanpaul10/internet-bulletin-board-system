import {
	GET_CHATBOT_RESPONSE,
	HANDLE_POST_AI_MODERATION,
	REWRITE_STORY_AI,
	TOGGLE_CHATBOT_LOADING,
	TOGGLE_REWRITE_LOADER,
} from "./ActionTypes";

const initialState: any = {
	isRewriteLoading: false,
	aiRewrittenStory: "",
	aiModerationData: {},
	isChatbotLoading: false,
	chatbotResponse: {},
};

export const AiServicesReducer = (state = initialState, action: any) => {
	switch (action.type) {
		case REWRITE_STORY_AI: {
			return {
				...state,
				aiRewrittenStory: action.payload,
			};
		}
		case TOGGLE_REWRITE_LOADER: {
			return {
				...state,
				isRewriteLoading: action.payload,
			};
		}
		case HANDLE_POST_AI_MODERATION: {
			return {
				...state,
				aiModerationData: action.payload,
			};
		}
		case TOGGLE_CHATBOT_LOADING: {
			return {
				...state,
				isChatbotLoading: action.payload,
			};
		}
		case GET_CHATBOT_RESPONSE: {
			return {
				...state,
				chatbotResponse: action.payload,
			};
		}
		default: {
			return state;
		}
	}
};
