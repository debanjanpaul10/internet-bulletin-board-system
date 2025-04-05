import axios from "axios";
import { ConfigurationConstants } from "@helpers/Constants";

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
			var webEndpoint =
				process.env.NODE_ENV == "development"
					? ConfigurationConstants.LocalHostUrl
					: ConfigurationConstants.AppWebUrl;
			const url =
				webEndpoint + ConfigurationConstants.GetBaseUrl + apiUrl;
			const webForgeryToken = ConfigurationConstants.WebAntiforgeryToken;

			const response = await axios.get(url, {
				headers: {
					"x-antiforgery-token-ibbs-web": webForgeryToken,
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
	 * @returns {Promise} The promise of the api response.
	 */
	const PostAsync = async (apiUrl, data) => {
		try {
			var webEndpoint =
				process.env.NODE_ENV == "development"
					? ConfigurationConstants.LocalHostUrl
					: ConfigurationConstants.AppWebUrl;

			const url =
				webEndpoint + ConfigurationConstants.PostBaseUrl + apiUrl;
			const webForgeryToken = ConfigurationConstants.WebAntiforgeryToken;

			const response = await axios.post(url, data, {
				headers: {
					"x-antiforgery-token-ibbs-web": webForgeryToken,
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
					? ConfigurationConstants.LocalHostUrl
					: ConfigurationConstants.AppWebUrl;

			const url = webEndpoint + ConfigurationConstants.AiRewriteUrl;
			const webForgeryToken = ConfigurationConstants.WebAntiforgeryToken;

			const response = await axios.post(url, data, {
				headers: {
					"x-antiforgery-token-ibbs-web": webForgeryToken,
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

	return { GetAsync, PostAsync, PostAIAsync };
}

export const { GetAsync, PostAsync, PostAIAsync } = HttpUtility();
