import {
    GET_ALL_POSTS_DATA,
    GET_POST_DATA,
    START_SPINNER,
    STOP_SPINNER,
    IS_CREATE_POST_LOADING,
    DELETE_POST_DATA,
    UPDATE_POST_RATING,
    TOGGLE_VOTING_LOADER,
    TOGGLE_EDIT_POST_DIALOG,
    GET_EDIT_POST_DATA,
    TOGGLE_EDIT_POST_LOADER,
} from "@store/Posts/ActionTypes";

const initialState: any = {
    postData: {},
    allPostsData: [],
    isPostsDataLoading: false,
    isCreatePostLoading: false,
    isPostDataDeleted: false,
    updatedRatingData: {},
    isVotingLoaderOn: false,
    isEditModalOpen: false,
    editPostData: {},
    isEditPostDataLoading: false,
};

/**
 * The Posts Reducer.
 * @param state The state.
 * @param action The action.
 * @returns The updated state.
 */
const PostsReducer = (state = initialState, action: any) => {
    switch (action.type) {
        case GET_POST_DATA: {
            return {
                ...state,
                postData: action.payload,
            };
        }
        case GET_ALL_POSTS_DATA: {
            return {
                ...state,
                allPostsData: [...action.payload],
            };
        }
        case START_SPINNER: {
            return {
                ...state,
                isPostsDataLoading: true,
            };
        }
        case STOP_SPINNER: {
            return {
                ...state,
                isPostsDataLoading: false,
            };
        }

        case IS_CREATE_POST_LOADING: {
            return {
                ...state,
                isCreatePostLoading: action.payload,
            };
        }
        case DELETE_POST_DATA: {
            return {
                ...state,
                isPostDataDeleted: action.payload,
            };
        }
        case UPDATE_POST_RATING: {
            return {
                ...state,
                updatedRatingData: action.payload,
            };
        }
        case TOGGLE_VOTING_LOADER: {
            return {
                ...state,
                isVotingLoaderOn: action.payload,
            };
        }
        case TOGGLE_EDIT_POST_DIALOG: {
            return {
                ...state,
                isEditModalOpen: action.payload,
            };
        }
        case GET_EDIT_POST_DATA: {
            return {
                ...state,
                editPostData: action.payload,
            };
        }
        case TOGGLE_EDIT_POST_LOADER: {
            return {
                ...state,
                isEditPostDataLoading: action.payload,
            };
        }

        default: {
            return state;
        }
    }
};

export { PostsReducer };
