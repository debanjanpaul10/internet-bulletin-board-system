import {
    HANDLE_POST_AI_MODERATION,
    REWRITE_STORY_AI,
    TOGGLE_REWRITE_LOADER,
} from "./ActionTypes";

const initialState: any = {
    isRewriteLoading: false,
    aiRewrittenStory: "",
    aiModerationData: {},
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
        default: {
            return state;
        }
    }
};
