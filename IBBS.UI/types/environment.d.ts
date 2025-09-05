export interface Environment {
	production: boolean;
	apiBaseUrl: string;
	auth0Config: {
		domain: string;
		clientId: string;
		redirectUri: string;
		audience?: string;
		scope?: string;
	};
}
