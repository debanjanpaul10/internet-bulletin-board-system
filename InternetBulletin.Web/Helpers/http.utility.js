import axios from "axios";
import {
	UrlConstants,
} from "@helpers/config.constants";

/**
 * The Http Utility Functions.
 * @returns {function} The object of functions
 */
function HttpUtility() {
	/**
	 * The Get API data function.
	 * @param {string} apiUrl The api url.
	 * @param {Promise} getIdTokenClaims The token function.
	 *
	 * @returns {Promise} The promise of the api response.
	 */
	const GetAsync = async (apiUrl, getIdTokenClaims) => {
		try {
			const token = await getIdTokenClaims();
			var webEndpoint = UrlConstants.LocalHostUrl; //: UrlConstants.AppWebUrl;
			const url = webEndpoint + apiUrl;
			let bearerToken = "";

			if (token !== null && token !== undefined) {
				bearerToken = `Bearer ${token.__raw}`;
			} 

			const response = await axios.get(url, {
				headers: {
					Authorization: bearerToken,
				},
			});

			if (response !== null || response.data !== "") {
				return response.data;
			}

			return "";
		} catch (error) {
			console.error(error);
			return Promise.reject(
				error.response ? error.response.data : error.message
			);
		}
	};

	/**
	 * The Post API data function.
	 * @param {string} apiUrl The api url.
	 * @param {Object} data The post data object.
	 * @param {Promise} getIdTokenClaims The token function.
	 * @returns {Promise} The promise of the api response.
	 */
	const PostAsync = async (apiUrl, data, getIdTokenClaims) => {
		try {
			const token = await getIdTokenClaims();
			var webEndpoint = UrlConstants.LocalHostUrl; //: UrlConstants.AppWebUrl;
			const url = webEndpoint + apiUrl;

			const response = await axios.post(url, data, {
				headers: {
					Authorization: `Bearer ${token.__raw}`,
				},
			});

			if (response !== null || response.data !== "") {
				return response.data;
			}
		} catch (error) {
			console.error(error);
			return Promise.reject(
				error.response ? error.response.data : error.message
			);
		}
	};

	/**
	 * The Post AI data function.
	 * @param {Object} data The post data object.
	 * @returns {Promise} The promise of the api response.
	 */
	const PostAIAsync = async (data) => {
		try {
			var webEndpoint =
				process.env.NODE_ENV == "development"
					? UrlConstants.LocalHostUrl
					: UrlConstants.AppWebUrl;

			const url = webEndpoint + UrlConstants.AiRewriteUrl;

			const response = await axios.post(url, data);
			if (response !== null || response.data !== "") {
				return response.data;
			}
		} catch (error) {
			console.error(error);
			return Promise.reject(
				error.response ? error.response.data : error.message
			);
		}
	};

	return { GetAsync, PostAsync, PostAIAsync };
}

export const { GetAsync, PostAsync, PostAIAsync } = HttpUtility();
