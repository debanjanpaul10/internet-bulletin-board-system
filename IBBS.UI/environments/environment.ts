export interface Environment {
    production: boolean;
    apiBaseUrl: string;
    auth0Config: {
        domain: string;
        clientId: string;
        redirectUri: string;
    };
}

export const environment: Environment = {
    production: true,
    apiBaseUrl: "https://your-production-api-url.com",
    auth0Config: {
        domain: "debanjans-lab.jp.auth0.com",
        clientId: "QMzTORGKI2KXC4SZQ5EsRWUK6DVaJySO",
        redirectUri: window.location.origin,
    },
};
