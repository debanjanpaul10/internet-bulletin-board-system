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
			if (!import.meta.env.VITE_WEB_ENDPOINT) {
				console.error("VITE_WEB_ENDPOINT is undefined");
			}

			if (!import.meta.env.VITE_ANTIFORGERY_TOKEN) {
				console.error("VITE_ANTIFORGERY_TOKEN is undefined");
			}

			const url =
				import.meta.env.VITE_WEB_ENDPOINT +
				ConfigurationConstants.GetBaseUrl +
				apiUrl;
			const webForgeryToken = import.meta.env.VITE_ANTIFORGERY_TOKEN;

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
			if (!import.meta.env.VITE_WEB_ENDPOINT) {
				console.error("VITE_WEB_ENDPOINT is undefined");
			}

			if (!import.meta.env.VITE_ANTIFORGERY_TOKEN) {
				console.error("VITE_ANTIFORGERY_TOKEN is undefined");
			}

			const url =
				import.meta.env.VITE_WEB_ENDPOINT +
				ConfigurationConstants.PostBaseUrl +
				apiUrl;
			const webForgeryToken = import.meta.env.VITE_ANTIFORGERY_TOKEN;

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
			if (!import.meta.env.VITE_WEB_ENDPOINT) {
				console.error("VITE_WEB_ENDPOINT is undefined");
			}

			if (!import.meta.env.VITE_ANTIFORGERY_TOKEN) {
				console.error("VITE_ANTIFORGERY_TOKEN is undefined");
			}

			const url =
				import.meta.env.VITE_WEB_ENDPOINT +
				ConfigurationConstants.AiRewriteUrl;
			const webForgeryToken = import.meta.env.VITE_ANTIFORGERY_TOKEN;

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
