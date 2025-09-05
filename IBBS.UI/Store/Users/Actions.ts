import { Action, Dispatch } from "redux";

import { GetUserProfilesDataApiAsync } from "@/Services/ibbs.apiservice";
import {
    GET_USER_PROFILE_DATA,
    TOGGLE_USER_PROFILE_DATA_SPINNER,
} from "./ActionTypes";

/**
 * Stores the profile data spinner toggle state to redux store.
 * @param isLoading The is loading boolean flag.
 * @returns The action type and payload data.
 */
export const ToggleProfileDataSpinner = (isLoading: boolean) => {
    return {
        type: TOGGLE_USER_PROFILE_DATA_SPINNER,
        paylad: isLoading,
    };
};

/**
 * Gets the user profile data from api.
 * @param accessToken The access token
 * @returns The promise of the api response.
 */
export const GetUserProfileDataAsync = (accessToken: string) => {
    return async (dispatch: Dispatch<Action>) => {
        try {
            dispatch(ToggleProfileDataSpinner(true));
            const response = await GetUserProfilesDataApiAsync(accessToken);
            if (response?.statusCode === 200) {
                dispatch(GetUserProfileDataSuccess(response?.data));
            }
        } catch (error) {
            console.error(error);
        } finally {
            dispatch(ToggleProfileDataSpinner(false));
        }
    };
};

/**
 * Stores the user profile data to redux store.
 * @param data The user profile data.
 * @returns The action type and payload data.
 */
const GetUserProfileDataSuccess = (data: any) => {
    return {
        type: GET_USER_PROFILE_DATA,
        payload: data,
    };
};
