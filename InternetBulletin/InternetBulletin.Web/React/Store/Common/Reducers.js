import { GET_CONFIGURATION_VALUE } from "@store/Common/ActionTypes";

const initialState = {
	configurationKey: "",
};

const CommonReducer = (state = initialState, action) => {
	switch (action.type) {
		case GET_CONFIGURATION_VALUE: {
			return {
				...state,
				configurationKey: action.payload,
			};
		}
		default: {
			return state;
		}
	}
};

export default CommonReducer;
