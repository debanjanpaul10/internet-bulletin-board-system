import {
	ADD_NEW_USER_DATA,
	GET_ALL_USERS_DATA,
	GET_USER_DATA,
	REMOVE_USER_DATA,
	START_SPINNER,
	STOP_SPINNER,
	USER_DATA_FAIL,
} from "@store/Users/ActionTypes";

const initialState = {
	userData: {},
	allUsersData: [],
	newUserData: {},
	updatedUserData: {},
	userDataError: null,
	isUserDataLoading: false,
};

const UsersReducer = (state = initialState, action) => {
	switch (action.type) {
		case GET_USER_DATA: {
			return {
				...state,
				userData: action.payload,
			};
		}
		case REMOVE_USER_DATA: {
			return {
				...state,
				userData: {},
			};
		}
		case GET_ALL_USERS_DATA: {
			return {
				...state,
				allUsersData: action.payload,
			};
		}
		case ADD_NEW_USER_DATA: {
			return {
				...state,
				newUserData: action.payload,
			};
		}
		case USER_DATA_FAIL: {
			return {
				...state,
				userDataError: action.payload,
			};
		}
		case START_SPINNER: {
			return {
				...state,
				isUserDataLoading: true,
			};
		}
		case STOP_SPINNER: {
			return {
				...state,
				isUserDataLoading: false,
			};
		}
		default: {
			return state;
		}
	}
};

export default UsersReducer;
