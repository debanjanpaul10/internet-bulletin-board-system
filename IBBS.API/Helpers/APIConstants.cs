namespace IBBS.API.Helpers;

/// <summary>
/// The API constants class.
/// </summary>
internal static class APIConstants
{
	/// <summary>
	/// The Configuration Constants class.
	/// </summary>
	internal static class ConfigurationConstants
	{
		/// <summary>
		/// The local appsettings file constant
		/// </summary>
		internal const string LocalAppsettingsFileConstant = "appsettings.development.json";

		/// <summary>
		/// The managed identity client identifier constant
		/// </summary>
		internal const string ManagedIdentityClientIdConstant = "ManagedIdentityClientId";

		/// <summary>
		/// The application configuration endpoint key constant
		/// </summary>
		internal const string AppConfigurationEndpointKeyConstant = "AppConfigurationEndpoint";

		/// <summary>
		/// The base configuration application configuration key constant
		/// </summary>
		internal const string BaseConfigurationAppConfigKeyConstant = "BaseConfiguration";

		/// <summary>
		/// The ibbs API application configuration key constant
		/// </summary>
		internal const string IbbsAPIAppConfigKeyConstant = "IBBS.API";

		/// <summary>
		/// The ibbs web client identifier constant
		/// </summary>
		internal const string IbbsWebClientIdConstant = "IBBSWebClientId";

		/// <summary>
		/// The ibbs web issuer constant
		/// </summary>
		internal const string IBBSWebIssuerConstant = "IBBSWebIssuer";

		/// <summary>
		/// The application json constant
		/// </summary>
		internal const string ApplicationJsonConstant = "application/json";

		/// <summary>
		/// The user name claim constant.
		/// </summary>
		internal const string UserNameClaimConstant = "User Name";

		/// <summary>
		/// The user display name constant.
		/// </summary>
		internal const string UserDisplayNameConstant = "name";
	}

	/// <summary>
	/// The Exception Constants Class.
	/// </summary>
	internal static class ExceptionConstants
	{
		/// <summary>
		/// The configuration value is empty message constant.
		/// </summary>
		internal const string ConfigurationValueIsEmptyMessageConstant = "The configuration key is empty!";

		/// <summary>
		/// The invalid token exception constant.
		/// </summary>
		internal const string InvalidTokenExceptionConstant = "Invalid token: Identity is not authenticated.";

		/// <summary>
		/// The user unauthorized message constant
		/// </summary>
		internal const string UserUnauthorizedMessageConstant = "User Not Authorized";

		/// <summary>
		/// Something went wrong message constant
		/// </summary>
		internal const string SomethingWentWrongMessageConstant = "Oops! Something went wrong while processing this request!";

		/// <summary>
		/// The post not found message constant
		/// </summary>
		public static readonly string PostNotFoundMessageConstant = "It seems the post you are looking for does not exists anymore!";

		/// <summary>
		/// The unable to get user post ratings message constant.
		/// </summary>
		public const string UnableToGetUserPostRatingsMessageConstant = "Unable to retrieve user's post ratings as of this moment!";

		/// <summary>
		/// The post exists message constant
		/// </summary>
		public static readonly string PostExistsMessageConstant = "A post with the generated ID already exists.";

		/// <summary>
		/// The null post message constant
		/// </summary>
		public static readonly string NullPostMessageConstant = "Attempted to add a null post.";

		/// <summary>
		/// The posts not present message constant
		/// </summary>
		public static readonly string PostsNotPresentMessageConstant = "There are no posts to be shown at the moment!";

		/// <summary>
		/// The user already exists message constant
		/// </summary>
		public static readonly string UserAlreadyExistsMessageConstant = "The given user alias and the user email is already being used!";

		/// <summary>
		/// The user does not exists message constant
		/// </summary>
		public static readonly string UserDoesNotExistsMessageConstant = "The user data does not exist with us anymore!";

		/// <summary>
		/// The user identifier not correct message constant
		/// </summary>
		public static readonly string UserIdNotCorrectMessageConstant = "The user id is not correct. Please enter a valid user id";

		/// <summary>
		/// The null user message constant
		/// </summary>
		public static readonly string NullUserMessageConstant = "Attempted to add null user data.";

		/// <summary>
		/// The users not present message constants
		/// </summary>
		public static readonly string UsersNotPresentMessageConstants = "There are no users registered.";

		/// <summary>
		/// The configuration value not exists message constant.
		/// </summary>
		public static readonly string ConfigurationValueNotExistsMessageConstant = "The configuration value is missing!";

		/// <summary>
		/// The user id cannot be null message constant.
		/// </summary>
		public const string UserIdCannotBeNullMessageConstant = "The user id passed must be a valid integer boss!";



		/// <summary>
		/// The un authorized constant
		/// </summary>
		public const string UnAuthorizedConstant = "Unauthorized";

		/// <summary>
		/// The user id not present exception constant.
		/// </summary>
		public const string UserIdNotPresentExceptionConstant = "User id is not present in the headers.";

		/// <summary>
		/// The post data cannot be empty exception constant
		/// </summary>
		public const string PostDataCannotBeEmptyExceptionConstant = "Oops! It seems the story that you have entered (or not yet entered) is blank!";

		/// <summary>
		/// The post unique identifier not valid message constant
		/// </summary>
		internal const string PostGuidNotValidMessageConstant = "The provided postId is not a valid GUID.";
	}

	/// <summary>
	/// The Logging Constants Class.
	/// </summary>
	internal static class LoggingConstants
	{
		/// <summary>
		/// The log helper method start
		/// </summary>
		internal const string LogHelperMethodStart = "{0} started at {1} for {2}";

		/// <summary>
		/// The log helper method ended
		/// </summary>
		internal const string LogHelperMethodEnded = "{0} ended at {1} for {2}";

		/// <summary>
		/// The log helper method failed
		/// </summary>
		internal const string LogHelperMethodFailed = "{0} failed at {1} with message {2}";
	}
}
