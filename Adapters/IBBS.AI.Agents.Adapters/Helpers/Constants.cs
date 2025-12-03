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
        internal const string BearerConstant = "Bearer";

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

        /// <summary>
        /// The ai agents web issuer constant
        /// </summary>
        internal const string AiAgentsWebIssuerConstant = "AiAgentsLab:WebIssuer";

        /// <summary>
        /// The ai agents ad client identifier
        /// </summary>
        internal const string AiAgentsAdClientId = "AiAgentsLab:ClientId";

        /// <summary>
        /// The ai agents ad client secret
        /// </summary>
        internal const string AiAgentsAdClientSecret = "AiAgentsLab:ClientSecret";

        /// <summary>
        /// The ai agents lab tenant identifier
        /// </summary>
        internal const string AiAgentsLabTenantId = "AiAgentsLab:TenantId";

        /// <summary>
        /// The token scope format
        /// </summary>
        internal const string TokenScopeFormat = "{0}/.default";
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
        /// The detect user intent API route
        /// </summary>
        internal const string DetectUserIntent_ApiRoute = "v1/skills/intentdetectionskill";

        /// <summary>
        /// The get user greeting response API route
        /// </summary>
        internal const string GetUserGreetingResponse_ApiRoute = "v1/skills/usergreetingskill";

        /// <summary>
        /// The get rag text response API route
        /// </summary>
        internal const string GetRAGTextResponse_ApiRoute = "v1/skills/ragtextresponseskill";

        /// <summary>
        /// The get nl to SQL response API route
        /// </summary>
        internal const string GetNlToSqlResponse_ApiRoute = "v1/skills/nltosqlskill";

        /// <summary>
        /// The get SQL query markdown response API route
        /// </summary>
        internal const string GetSQLQueryMarkdownResponse_ApiRoute = "v1/skills/getsqlquerymarkdownresponse";

        /// <summary>
        /// The get followup questions response API route.
        /// </summary>
        internal const string GetFollowupQuestionsResponse_ApiRoute = "v1/skills/getfollowupquestionsresponse";

        /// <summary>
        /// The invoke agent API route.
        /// </summary>
        internal const string InvokeAgent_ApiRoute = "v2/chat/invokeagent";
    }
}
