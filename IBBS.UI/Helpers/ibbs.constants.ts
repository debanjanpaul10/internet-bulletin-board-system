export const PageConstants = {
	DarkModeConstant: "dark-mode",
	DarkConstant: "dark",
	LightConstant: "light",
	DefaultLoaderMessage: "Loading amazing content ...",
};

export const HeaderPageConstants = {
	Headings: {
		Login: {
			Name: "Join our cult!",
		},
		Home: {
			Name: "Home",
			Link: "/",
		},
		Logout: {
			Name: "Logout",
		},
		CreatePost: {
			Name: "Create a new post!",
			Link: "/create",
		},
		MyProfile: {
			Name: "My Profile",
			Link: "/myprofile",
		},
		AboutUs: {
			Name: "About us!",
			Link: "/aboutus",
		},
	},
	ButtonTitles: {
		HomeButton: "Where the fun starts!",
		Logout: "Bye bye bye!",
		Register: "This place is a fit for me",
		BugButton: "Did you find a bug? Report it back to us!",
		Login: "I am back!",
		Create: "I have something to share ...",
		MyProfile: "I wanna check myself out...",
		SideDrawer: "Check out what features I have ...",
		AboutUs: "Wanna know about the application itself ?",
	},
};

export const LandingPageConstants = {
	PostsContainerConstants: {
		PostsHeader: "Latest Posts",
		DiscoverMessageConstant: "Discover what the community is talking about",
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
		SubText:
			"Connect, share, and discover amazing stories from our vibrant community",
	},
};

export const PostBodyConstants = {
	ButtonText: {
		RatingsButtonTooltipText: "Vote as good one!",
		AlreadyRatedButtonTooltipText: "Thanks for your vote!",
		EditButtonTooltipText: "Edit the post!",
		DeleteButtonTooltipText: "Delete the post!",
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
		TitleBarPlaceholder:
			"How can you summarize your adventure? (No more than 50 characters boss!)",
		ContentBoxPlaceholder: "Go on. Tell us. Stop the suspense now.",
		RewriteAIButtonTexts: {
			ButtonText: "Rewrite with GenAI",
			TooltipText:
				"You can fix the minor mistakes in your story with GenAI!",
		},
		ModerateWithAIButtonTexts: {
			ButtonText: "Review Content & Get Genre",
			TooltipText:
				"AI will review your content for NSFW material and suggest the genre",
		},
	},
	validations: {
		TitleRequired: "Dude! You need a title for your adventure.",
		ContentRequired: "Elaborate your idea please!",
		MaxTitleLength: "The title must not exceed 50 characters, dude!",
	},
	DialogContentConstants: {
		HeadingMessage: "Confirm Cancellation",
		ConfirmationMessage:
			"Are you sure you want to cancel? Any unsaved changes will be lost.",
		YesButtonText: "Yes, cancel",
		NoButtonText: "No, stay here",
	},
	AiContentConstants: {
		DialogHeader: "AI Response",
		CancelButton: "Cancel changes",
		AcceptButton: "Accept Changes",
	},
};

export const NoPostsMessageConstant = {
	Heading: "Oops! We do not have anything for you right now.",
};

export const ConsoleMessage = "Developed from scratch by Debanjan Paul!";

export const modalStyle = {
	position: "absolute",
	top: "50%",
	left: "50%",
	transform: "translate(-50%, -50%)",
	width: "auto",
	border: "2px solid #000",
	boxShadow: 24,
	p: 4,
};

export const MyProfilePageConstants = {
	Headings: {
		Header: "My Profile",
		WelcomeMessage:
			"Your Profile's so fly, it could make a crocodile smile!",
		AsPerMyKnowledge: "As per my knowledge, these are your details!",
		YourPostsMessage: "Your stories",
		YourRatings: "Your ratings",
		AboutUsMessage: "This is my story!",
	},
	PostTableHeaders: ["Post Name", "Date Created", "Ratings"],
	RatingsTableHeaders: ["Post Name", "Rated on"],
};

export const AboutUsPageConstants = {
	ButtonTexts: {
		WebsiteNav: "Visit the official website",
	},
	Subtitle: "Made with ❤ and",
};

export const FooterConstants = {
	EmailLink: "mailto:debanjanpaul10@gmail.com",
	FaceBookLink: "https://www.facebook.com/debanjan.paul.69",
	GithubLink: "https://github.com/debanjanpaul10",
	LinkedinLink: "https://www.linkedin.com/in/debanjan-paul/",
};

export const NSFWConstant = "NSFW";
export const BlankTextErrorMessageConstant =
	"Atleast enter something in the text box boss!";

export const ChatbotConstants = {
	ChatbotFloatingIconTooltip: "Chat with IBBS AI Assistant",
	ChatbotWindow: {
		AgentName: "IBBS AI Assistant",
		AiAssistantGreetingMessage:
			"Hello! I'm IBBS AI assistant. How can I help you today?",
		WelcomeMessage: "Hello! How can I help you today?",
		CopyRequestTooltip: "Copy message",
		CopyTooltip: "Copy response",
		LikeTooltip: "Good response",
		DislikeTooltip: "Bad response",
		TypingPlaceholder: "Type your query",
		ThinkingMessage: "AI is thinking ...",
		WarningMessage:
			"AI features are still in Alpha phase, please expect incorrect responses or bugs",
	},
};

export const BugReportConstants = {
	Header: "We accept that we screwed up, so please help us and submit a bug report",
	LoaderMessage: "Submitting the bug report ...",
	GettingBugSeverity: "Getting the bug severity based on your input ...",
};
