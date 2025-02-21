import {
	AddNewUserAsync,
	GetAllUsersAsync,
	GetUserAsync,
} from "@helpers/InternetBulletinService";
import {
	ADD_NEW_USER_DATA,
	GET_ALL_USERS_DATA,
	GET_USER_DATA,
} from "@store/Users/ActionTypes";

export const GetUserDataAsync = (userId) => {
	return async (dispatch) => {
		try {
			const response = await GetUserAsync(userId);
			if (response?.status === 200) {
				dispatch(GetUserDataSuccess(response?.data));
			}
		} catch (error) {
			console.error(error);
		}
	};
};

const GetUserDataSuccess = (data) => {
	return {
		type: GET_USER_DATA,
		payload: data,
	};
};

export const GetAllUsersDataAsync = () => {
	return async (dispatch) => {
		try {
			const response = await GetAllUsersAsync();
			if (response?.status === 200) {
				dispatch(GetAllUsersDataSuccess(response?.data));
			}
		} catch (error) {
			console.error(error);
		}
	};
};

const GetAllUsersDataSuccess = (data) => {
	return {
		type: GET_ALL_USERS_DATA,
		payload: data,
	};
};

export const AddNewUserDataAsync = (userData) => {
	return async (dispatch) => {
		try {
			const response = await AddNewUserAsync(userData);
			if (response?.status === 200) {
				dispatch(AddNewUserDataSuccess(response?.data));
			}
		} catch (error) {
			console.error(error);
		}
	};
};

const AddNewUserDataSuccess = (data) => {
	return {
		type: ADD_NEW_USER_DATA,
		payload: data,
	};
};
