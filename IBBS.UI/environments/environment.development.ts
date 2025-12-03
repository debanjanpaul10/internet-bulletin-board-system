import { Environment } from "@/app/Types/environment";

export const environment: Environment = {
	production: false,
	apiBaseUrl: "https://localhost:44352/",
	auth0Config: {
		domain: "debanjans-lab.jp.auth0.com",
		clientId: "QMzTORGKI2KXC4SZQ5EsRWUK6DVaJySO",
		redirectUri: "https://localhost:5173",
		audience: "5484cb2b-9b41-4e2d-b668-5924bb7e702f",
	},
};
