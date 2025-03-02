export const ConfigurationConstants = {
	PostBaseUrl:
		"https://localhost:44315/InternetBulletinWeb/PostResourceUrl?resourceUrl=",
	GetBaseUrl:
		"https://localhost:44315/InternetBulletinWeb/GetResourceUrl?resourceUrl=",
	KeyVaultName: "https://kyv-internet-bulletin.vault.azure.net",
	WebAntiforgeryKeyVault: "x-antiforgery-token-web-kv",
	ClientId: "20b4f56e-754c-4788-b79f-829b8ee00c11",
};

export const HeaderPageConstants = {
	Headings: {
		Login: {
			Name: "Login",
			Link: "/login",
		},
		Register: {
			Name: "Register",
			Link: "/register",
		},
		Home: {
			Name: "Home",
			Link: "/",
		},
	},
	ButtonTitles: {
		HomeButton: "Where the fun starts!",
		Logout: "Bye bye bye!",
		Register: "This place is a fit for me",
		TurnOnLight: "Turn on the light!",
		TurnOnDark: "I am the dark!",
        Login: "I am back!"
	},
};

export const RegisterPageConstants = {
	validations: {
		NameRequired: "Please enter your name. This field cannot be blank.",
		UserAliasRequired:
			"Please enter a user alias. We will use this to identify you.",
		UserEmailRequired: "Please enter your email.",
		UserPasswordRequired:
			"The password field is mandatory. Else anyone can impersonate you.",
	},
	Headings: {
		RegisterNewUser:
			"Register yourself, and join the bulletin board's wealth!",
		AddButton: "Register",
	},
};

export const LoginPageConstants = {
	validations: {
		UserEmailRequired: "Please enter your email.",
		UserPasswordRequired:
			"The password field is mandatory. Else anyone can impersonate you.",
	},
	Headings: {
		LoginUser: "Log back in, let the fun begin !",
		LoginButton: "Login",
	},
};

export const ErrorPageConstants = {
	PageNotFoundMessage:
		"Oops! The page could not be found, but hey don't be down!",
};

export const HomePageConstants = {
	Headings: {
		WelcomeMessage:
			"Welcome to the Internet Bulletin Board System, where ideas blossom and wisdom's the rhythm!",
	},
};

export const CookiesConstants = {
	IsDarkModeCookie: "darkMode",
	CurrentLoggedInUserCookie: "currentLoggedIn",
};
