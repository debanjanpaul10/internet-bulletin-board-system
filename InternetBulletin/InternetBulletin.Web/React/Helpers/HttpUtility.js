import axios from "axios";
import { ConfigurationConstants } from "@helpers/Constants";
import { GetKeyVaultSecret } from "@helpers/KeyVaultHelper";

/**
 * The Http Utility Functions.
 * @returns {function} The object of functions
 */
function HttpUtility() {
	/**
	 * The Get API data function.
	 * @param {string} apiUrl The api url.
	 * @returns {Promise} The promise of the api response.
	 */
	const GetAsync = async (apiUrl) => {
		try {
			const url = ConfigurationConstants.GetBaseUrl + apiUrl;
			const webForgeryToken = GetKeyVaultSecret(
				ConfigurationConstants.WebAntiforgeryKeyVault
			);
            
			const response = await axios.get(url, {
				headers: {
					"x-antiforgery-token-web": webForgeryToken,
				},
			});

			if (response !== null || response.data !== "") {
				return response.data;
			}

			return "";
		} catch (error) {
			console.error(error);
		}
	};

	/**
	 * The Post API data function.
	 * @param {string} apiUrl The api url.
	 * @param {Object} data The post data object.
	 * @returns {Promise} The promise of the api response.
	 */
	const PostAsync = async (apiUrl, data) => {
		try {
			const url = ConfigurationConstants.PostBaseUrl + apiUrl;
			const webForgeryToken = GetKeyVaultSecret(
				ConfigurationConstants.WebAntiforgeryKeyVault
			);

			const response = await axios.post(url, data, {
				headers: {
					"x-antiforgery-token-web": webForgeryToken,
				},
			});

			if (response !== null || response.data !== "") {
				return response.data;
			}
		} catch (error) {
			console.error(error);
		}
	};

	return { GetAsync, PostAsync };
}

export const { GetAsync, PostAsync } = HttpUtility();
