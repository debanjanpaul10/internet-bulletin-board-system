import axios from "axios";
import { environment } from "@environment";

/**
 * @class
 * The Http Utility class.
 */
class HttpUtility {
	/**
	 * The Web api base url endpoint.
	 */
	static readonly WebApiEndpoint = environment.apiBaseUrl;

	/**
	 * The Get API data function.
	 * @param {string} apiUrl The api url.
	 * @param {string} accessToken The access token.
	 *
	 * @returns {Promise} The promise of the api response.
	 */
	static readonly GetAsync = async (
		apiUrl: string,
		accessToken: string,
	): Promise<any> => {
		try {
			const url = this.WebApiEndpoint + apiUrl;
			const response = await axios.get(url, {
				headers: {
					Authorization: `Bearer ${accessToken}`,
				},
			});

			if (response?.data) {
				return response.data;
			}

			return "";
		} catch (error: any) {
			console.error(error);
			throw error.response ? error.response.data : error.message;
		}
	};

	/**
	 * The Post API data function.
	 * @param {string} apiUrl The api url.
	 * @param {Object} data The post data object.
	 * @param {string} accessToken The access token.
	 *
	 * @returns {Promise} The promise of the api response.
	 */
	static readonly PostAsync = async (
		apiUrl: string,
		data: any,
		accessToken: string,
	): Promise<any> => {
		try {
			const url = this.WebApiEndpoint + apiUrl;

			const response = await axios.post(url, data, {
				headers: {
					Authorization: `Bearer ${accessToken}`,
				},
			});

			if (response?.data) {
				return response.data;
			}
		} catch (error: any) {
			console.error(error);
			throw error.response ? error.response.data : error.message;
		}
	};
}

export default HttpUtility;
