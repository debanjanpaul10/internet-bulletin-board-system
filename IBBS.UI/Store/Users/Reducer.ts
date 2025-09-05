import {
    GET_USER_PROFILE_DATA,
    TOGGLE_USER_PROFILE_DATA_SPINNER,
} from "./ActionTypes";

const initialState: any = {
    userProfileData: {},
    isUserProfileDataLoading: true,
};

/**
 * The User Reducer.
 * @param state The state.
 * @param action The action.
 * @returns The updated state.
 */
const UserReducer = (state = initialState, action: any) => {
    switch (action.type) {
        case GET_USER_PROFILE_DATA: {
            return {
                ...state,
                userProfileData: action.payload,
            };
        }
        case TOGGLE_USER_PROFILE_DATA_SPINNER: {
            return {
                ...state,
                isUserProfileDataLoading: action.payload,
            };
        }
        default: {
            return state;
        }
    }
};

export { UserReducer };
