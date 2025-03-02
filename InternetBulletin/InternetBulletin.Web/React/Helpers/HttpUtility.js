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
			const webForgeryToken = "eyJhbGciOiJIUzI1NiJ9.eyJSb2xlIjoiVXNlciIsIklzc3VlciI6IkludGVybmV0LUJ1bGxldGluLVdlYiJ9.1kR0l-Q5EUwmAykORxbMjGV9OTQ2UJKvDXMEQduW8o8"

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
			return Promise.reject(error.response ? error.response.data : error.message);
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
			const webForgeryToken = "eyJhbGciOiJIUzI1NiJ9.eyJSb2xlIjoiVXNlciIsIklzc3VlciI6IkludGVybmV0LUJ1bGxldGluLVdlYiJ9.1kR0l-Q5EUwmAykORxbMjGV9OTQ2UJKvDXMEQduW8o8"

			const response = await axios.post(url, data, {
				headers: {
					"x-antiforgery-token-web": webForgeryToken,
				},
			});

			if (response !== null || response.data !== "") {
				return response.data;
			}
		} catch (error) {
			return Promise.reject(error.response ? error.response.data : error.message);
		}
	};

	return { GetAsync, PostAsync };
}

export const { GetAsync, PostAsync } = HttpUtility();
