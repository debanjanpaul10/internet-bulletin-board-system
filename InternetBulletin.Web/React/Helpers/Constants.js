export const ConfigurationConstants = {
	PostBaseUrl:
		"https://localhost:44315/InternetBulletinWeb/PostResourceUrl?resourceUrl=",
	GetBaseUrl:
		"https://localhost:44315/InternetBulletinWeb/GetResourceUrl?resourceUrl=",
	WebAntiForgeryTokenValue:
		"eyJhbGciOiJIUzI1NiJ9.eyJSb2xlIjoiVXNlciIsIklzc3VlciI6IkludGVybmV0LUJ1bGxldGluLVdlYiJ9.l7ZFAR6EIk0fFk2j3tH5ZP0w8BRmSRDQ9A6_O3kHgEo",
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
		Logout: {
			Name: "Logout",
		},
		CreatePost: {
			Name: "Create",
			Link: "/create",
		},
	},
	ButtonTitles: {
		HomeButton: "Where the fun starts!",
		Logout: "Bye bye bye!",
		Register: "This place is a fit for me",
		TurnOnLight: "Turn on the light!",
		TurnOnDark: "I am the dark!",
		Login: "I am back!",
		Create: "I have something to share ...",
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
			"Welcome to the Internet Bulletin Board, where sharing ideas is your reward.",
		IBBS: "Internet Bulletin Board",
	},
};

export const CookiesConstants = {
	DarkMode: {
		Name: "darkMode",
		Timeout: 30,
	},
	LoggedInUser: {
		Name: "currentLoggedIn",
		Timeout: 30,
	},
};

export const CreatePostPageConstants = {
	Headings: {
		Header: "Share your story, no need for glory, just a bit of wit, and maybe some allegory.",
		TitleBarPlaceholder: "How can you summarize your adventure?",
		ContentBoxPlaceholder: "Go on. Tell us. Stop the suspense now.",
	},
	validations: {
		TitleRequired: "Dude! You need a title for your adventure.",
		ContentRequired: "Elaborate your idea please!",
	},
};

export const NoPostsMessageConstant = {
    Heading: "Oops! We do not have anything for you right now."
}