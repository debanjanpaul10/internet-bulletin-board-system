import { GetConfigurationAsync } from "@helpers/InternetBulletinService";
import { GET_CONFIGURATION_VALUE } from "@store/Common/ActionTypes";

/**
 * Gets the configuration data from api.
 * @param {string} keyName The key name
 * @returns {Promise} The promise from the api response.
 */
export const GetConfigurationValueAsync = (keyName) => {
	return async (dispatch) => {
		try {
			const response = await GetConfigurationAsync(keyName);
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
