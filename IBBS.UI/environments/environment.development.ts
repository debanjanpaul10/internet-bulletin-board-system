import { Environment } from "@/types/environment";

export const environment: Environment = {
	production: false,
	apiBaseUrl: "https://localhost:44352",
	auth0Config: {
		domain: "debanjans-lab.jp.auth0.com",
		clientId: "QMzTORGKI2KXC4SZQ5EsRWUK6DVaJySO",
		redirectUri: "https://localhost:5173",
	},
};
