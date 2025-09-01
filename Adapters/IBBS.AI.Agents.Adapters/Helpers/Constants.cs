namespace IBBS.AI.Agents.Adapters.Helpers;

/// <summary>
/// The constants class.
/// </summary>
internal static class Constants
{
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

		/// <summary>
		/// The cache key found message constant.
		/// </summary>
		internal const string CacheKeyFoundMessageConstant = "Cache service found the existing key: {0}";

		/// <summary>
		/// The cache key not found message constant.
		/// </summary>
		internal const string CacheKeyNotFoundMessageConstant = "Cache service could not find the key: {0}";
	}

	/// <summary>
	/// The configuration constants class.
	/// </summary>
	internal static class ConfigurationConstants
	{
		/// <summary>
		/// The application json constant
		/// </summary>
		internal const string ApplicationJsonConstant = "application/json";

		/// <summary>
		/// The bearer constant
		/// </summary>
		internal const string BearerConstant = "Bearer ";

		/// <summary>
		/// The ai agents HTTP client
		/// </summary>
		internal const string AiAgentsHttpClient = "aiagentsclient";

		/// <summary>
		/// The ai agents API base URL
		/// </summary>
		internal const string AiAgentsApiBaseUrl = "AiAgentsLab:ApiBaseUrl";

		/// <summary>
		/// The local ai agents base URL
		/// </summary>
		internal const string LocalAiAgentsBaseUrl = "LocalAiAgentsBaseUrl";
	}

	/// <summary>
	/// The exception constants class.
	/// </summary>
	internal static class ExceptionConstants
	{
		/// <summary>
		/// The ai services cannot be availed exception constant.
		/// </summary>
		public const string AiServicesCannotBeAvailedExceptionConstant = "Oops! It seems our AI Services are down as of this moment. Please try again after sometime.";
	}

	/// <summary>
	/// The AI Agents Routes constants.
	/// </summary>
	internal static class AIAgentsRoutesConstants
	{
		/// <summary>
		/// The generate tag API route
		/// </summary>
		internal const string GenerateTag_ApiRoute = "plugins/generatetag";

		/// <summary>
		/// The moderate content API route
		/// </summary>
		internal const string ModerateContent_ApiRoute = "plugins/moderatecontent";

		/// <summary>
		/// The rewrite text API route
		/// </summary>
		internal const string RewriteText_ApiRoute = "plugins/rewritetext";
	}
}
