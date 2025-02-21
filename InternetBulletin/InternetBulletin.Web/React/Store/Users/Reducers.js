import { GET_ALL_USERS_DATA, GET_USER_DATA } from "@store/Users/ActionTypes";

const initialState = {
	userData: null,
	allUsersData: [],
};

const UsersReducer = (state = initialState, action) => {
	switch (action.type) {
		case GET_USER_DATA: {
			return {
				...state,
				userData: action.payload,
			};
		}
		case GET_ALL_USERS_DATA: {
			return {
				...state,
				allUsersData: action.payload,
			};
		}
		default: {
			return state;
		}
	}
};

export default UsersReducer;
