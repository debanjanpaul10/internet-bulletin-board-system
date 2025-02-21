import {
	GetAllPostsAsync,
	GetPostAsync,
} from "@helpers/InternetBulletinService";
import { GET_ALL_POSTS_DATA, GET_POST_DATA } from "@store/Posts/ActionTypes";

export const GetPostDataAsync = (postId) => {
	return async (dispatch) => {
		try {
			const response = await GetPostAsync(postId);
			dispatch(GetPostDataSuccess(response.data));
		} catch (error) {
			console.error(error);
		}
	};
};

const GetPostDataSuccess = (data) => {
	return {
		type: GET_POST_DATA,
		payload: data,
	};
};

export const GetAllPostsDataAsync = () => {
	return async (dispatch) => {
		try {
			const response = await GetAllPostsAsync();
			dispatch(GetAllPostsDataSuccess(response.data));
		} catch (error) {
			console.error(error);
		}
	};
};

const GetAllPostsDataSuccess = (data) => {
	return {
		type: GET_ALL_POSTS_DATA,
		payload: data,
	};
};
