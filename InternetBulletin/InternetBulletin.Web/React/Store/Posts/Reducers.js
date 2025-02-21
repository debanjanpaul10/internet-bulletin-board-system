import { GET_ALL_POSTS_DATA, GET_POST_DATA } from "@store/Posts/ActionTypes";

const initialState = {
	postData: null,
	allPostsData: [],
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
		default: {
			return state;
		}
	}
};

export default PostsReducer;
