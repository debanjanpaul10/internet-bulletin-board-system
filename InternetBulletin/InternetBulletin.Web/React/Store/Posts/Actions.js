import {
	GetAllPostsAsync,
	GetPostAsync,
} from "@helpers/InternetBulletinService";
import {
	GET_ALL_POSTS_DATA,
	GET_POST_DATA,
	START_SPINNER,
	STOP_SPINNER,
} from "@store/Posts/ActionTypes";

export const StartLoader = () => {
	return {
		type: START_SPINNER,
	};
};

export const StopLoader = () => {
	return {
		type: STOP_SPINNER,
	};
};

export const GetPostDataAsync = (postId) => {
	return async (dispatch) => {
		try {
			dispatch(StartLoader());
			const response = await GetPostAsync(postId);
			if (response?.statusCode === 200) {
				dispatch(GetPostDataSuccess(response.data));
			}
		} catch (error) {
			console.error(error);
		} finally {
			dispatch(StopLoader());
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
			dispatch(StartLoader());
			const response = await GetAllPostsAsync();
			if (response?.statusCode === 200) {
				dispatch(GetAllPostsDataSuccess(response.data));
			}
		} catch (error) {
			console.error(error);
		} finally {
			dispatch(StopLoader());
		}
	};
};

const GetAllPostsDataSuccess = (data) => {
	return {
		type: GET_ALL_POSTS_DATA,
		payload: data,
	};
};
