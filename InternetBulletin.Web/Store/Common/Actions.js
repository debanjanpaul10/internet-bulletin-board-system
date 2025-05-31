import { GetConfigurationApiAsync } from "@services/ibbs.apiservice";
import {
	GET_CONFIGURATION_VALUE,
	TOGGLE_ERROR_TOASTER,
	TOGGLE_SIDE_BAR_STATUS,
	TOGGLE_SUCCESS_TOASTER,
} from "@store/Common/ActionTypes";

/**
 * Gets the configuration data from api.
 * @param {string} keyName The key name
 * @returns {Promise} The promise from the api response.
 */
export const GetConfigurationValueAsync = (keyName) => {
	return async (dispatch) => {
		try {
			const response = await GetConfigurationApiAsync(keyName);
			if (response?.statusCode === 200) {
				dispatch(GetConfigurationValueSuccess(response.data));
			}
		} catch (error) {
			console.error(error);
		}
	};
};

/**
 * Saves the configuration key to redux store.
 * @param {Object} data The api response.
 * @returns {Object} The action type and payload data.
 */
const GetConfigurationValueSuccess = (data) => {
	return {
		type: GET_CONFIGURATION_VALUE,
		payload: data,
	};
};

/**
 * Saves the toggle of success toaster to redux store.
 * @param {Object} data The data object
 * @returns {Object} The action type and payload data.
 */
export const ToggleSuccessToaster = (data) => {
	return {
		type: TOGGLE_SUCCESS_TOASTER,
		payload: {
			shouldShow: data.shouldShow,
			successMessage: data.successMessage,
		},
	};
};

/**
 * Saves the toggle of error toaster to redux store.
 * @param {Object} data The data object
 * @returns {Object} The action type and payload data.
 */
export const ToggleErrorToaster = (data) => {
	return {
		type: TOGGLE_ERROR_TOASTER,
		payload: {
			shouldShow: data.shouldShow,
			errorMessage: data.errorMessage,
		},
	};
};

/**
 * Saves the toggle of side bar status to redux store.
 * @param {boolean} isOpen The is open boolean flag.
 * @returns {Object} The action type and payload data.
 */
export const ToggleSideBar = (isOpen) => {
	return {
		type: TOGGLE_SIDE_BAR_STATUS,
		payload: isOpen,
	};
};
