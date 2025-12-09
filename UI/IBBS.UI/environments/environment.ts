import { Environment } from "@/app/Types/environment";

export const environment: Environment = {
	production: true,
	apiBaseUrl: "https://app-webapi-ibbs.azurewebsites.net/",
	auth0Config: {
		domain: "debanjans-lab.jp.auth0.com",
		clientId: "QMzTORGKI2KXC4SZQ5EsRWUK6DVaJySO",
		redirectUri: window.location.origin,
		audience: "5484cb2b-9b41-4e2d-b668-5924bb7e702f",
	},
};
