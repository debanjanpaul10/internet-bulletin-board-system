import { UrlConstants } from "../Helpers/config.constants";

const msalConfig = {
    auth: {
        clientId: "7f668472-562f-4489-9725-b75308ce1e3f",
        authority:
            "https://login.microsoftonline.com/499b9f66-f4dd-4c09-ab36-163bbc38a326",
        redirectUri: UrlConstants.WebUrls.LocalDevUrl,
    },
};

const loginRequests = {
    scopes: [
        "api://b238fa82-27e1-4f22-9005-b8e0fba668b0/Users.Read",
        "api://b238fa82-27e1-4f22-9005-b8e0fba668b0/Users.Write",
    ],
};

export { msalConfig, loginRequests };
