import { Environment } from "@/app/Types/environment";

export const environment: Environment = {
	production: true,
	apiBaseUrl: "https://app-webapi-ibbs.azurewebsites.net/",
	auth0Config: {
		domain: "debanjans-lab.jp.auth0.com",
		clientId: "QMzTORGKI2KXC4SZQ5EsRWUK6DVaJySO",
		redirectUri: window.location.origin,
	},
};
