export const PageConstants = {
  DarkModeConstant: "dark-mode",
  DarkConstant: "dark",
  LightConstant: "light",
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
  },
  ButtonTitles: {
    HomeButton: "Where the fun starts!",
    Logout: "Bye bye bye!",
    Register: "This place is a fit for me",
    TurnOnLight: "Turn on the light!",
    TurnOnDark: "I am the dark!",
    Login: "I am back!",
    Create: "I have something to share ...",
    MyProfile: "I wanna check myself out...",
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
    RegisterNewUser: "Register yourself, and join the bulletin board's wealth!",
    AddButton: "Register",
    CancelButton: "Cancel",
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
    CancelButton: "Cancel",
  },
  LoginSuccess: "User logged in successfully!",
  LogoutSuccess: "User logged out successfully!",
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
    Header:
      "Share your story, no need for glory, just a bit of wit, and maybe some allegory.",
    TitleBarPlaceholder: "How can you summarize your adventure?",
    ContentBoxPlaceholder: "Go on. Tell us. Stop the suspense now.",
  },
  validations: {
    TitleRequired: "Dude! You need a title for your adventure.",
    ContentRequired: "Elaborate your idea please!",
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
    WelcomeMessage: "Your Profile's so fly, it could make a crocodile smile!",
  },
};
