import {
	AddNewUserAsync,
	GetAllUsersAsync,
	GetUserAsync,
} from "@helpers/InternetBulletinService";
import {
	ADD_NEW_USER_DATA,
	GET_ALL_USERS_DATA,
	GET_USER_DATA,
	REMOVE_USER_DATA,
	START_SPINNER,
	STOP_SPINNER,
	USER_DATA_FAIL,
} from "@store/Users/ActionTypes";

/**
 * Saves the loader start status to redux store.
 * @returns {Object} The action type 
 */
export const StartLoader = () => {
	return {
		type: START_SPINNER,
	};
};

/**
 * Saves the loader stop status to redux store.
 * @returns {Object} The action type 
 */
export const StopLoader = () => {
	return {
		type: STOP_SPINNER,
	};
};

/**
 * Gets the user data from api.
 * @param {Object} userData The user data.
 * @returns {Promise} The promise from the api response.
 */
export const GetUserDataAsync = (userData) => {
	return async (dispatch) => {
		try {
			dispatch(StartLoader());
			const response = await GetUserAsync(userData);
			if (response?.statusCode === 200) {
				dispatch(GetUserDataSuccess(response?.data));
			}
		} catch (error) {
			console.error(error);
			if (error.data) toast.error(error.data);
		} finally {
			dispatch(StopLoader());
		}
	};
};

/**
 * Saves the user data to redux store.
 * @param {Object} data The api response.
 * @returns {Object} The action type and payload data.
 */
const GetUserDataSuccess = (data) => {
	return {
		type: GET_USER_DATA,
		payload: data,
	};
};

/**
 * Gets all users data.
 * @returns {Promise} The promise from the api response.
 */
export const GetAllUsersDataAsync = () => {
	return async (dispatch) => {
		try {
			const response = await GetAllUsersAsync();
			if (response?.statusCode === 200) {
				dispatch(GetAllUsersDataSuccess(response?.data));
			}
		} catch (error) {
			console.error(error);
			if (error.data) toast.error(error.data);
		}
	};
};

/**
 * Saves the all users data to redux store
 * @param {Object} data The api response.
 * @returns {Object} The action type and payload data.
 */
const GetAllUsersDataSuccess = (data) => {
	return {
		type: GET_ALL_USERS_DATA,
		payload: data,
	};
};

/**
 * Adds an new user data.
 * @param {Object} userData The user data object.
 * @returns {Promise} The promise from the api response.
 */
export const AddNewUserDataAsync = (userData) => {
	return async (dispatch) => {
		try {
			dispatch(StartLoader());
			const response = await AddNewUserAsync(userData);
			if (response?.statusCode === 200) {
				dispatch(AddNewUserDataSuccess(response?.data));
			}
		} catch (error) {
			console.error(error);
			dispatch(UserDataFailure(error.data));
		} finally {
			dispatch(StopLoader());
		}
	};
};

/**
 * Saves the new user data to redux store.
 * @param {Object} data The api response.
 * @returns {Object} The action type and payload data.
 */
const AddNewUserDataSuccess = (data) => {
	return {
		type: ADD_NEW_USER_DATA,
		payload: data,
	};
};

/**
 * Saves the user failure data to redux store.
 * @param {Object} data The api response.
 * @returns {Object} The action type and payload data.
 */
export const UserDataFailure = (data) => {
	return {
		type: USER_DATA_FAIL,
		payload: data,
	};
};

/**
 * Removes the current logged in user data from redux store.
 * @returns {Object} The action type.
 */
export const RemoveCurrentLoggedInUserData = () => {
	return {
		type: REMOVE_USER_DATA,
	};
};
