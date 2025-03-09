import {
	GET_ALL_POSTS_DATA,
	GET_POST_DATA,
	START_SPINNER,
	STOP_SPINNER,
} from "@store/Posts/ActionTypes";

const initialState = {
	postData: {},
	allPostsData: [],
	isPostsDataLoading: false,
};

const PostsReducer = (state = initialState, action) => {
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
				allPostsData: action.payload,
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
		default: {
			return state;
		}
	}
};

export default PostsReducer;
