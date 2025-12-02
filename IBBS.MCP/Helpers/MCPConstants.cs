namespace IBBS.MCP.Helpers;

/// <summary>
/// The MCP Constants Class.
/// </summary>
internal static class MCPConstants
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
        /// The auth0 domain
        /// </summary>
        internal const string Auth0Domain = "Auth0:Domain";

        /// <summary>
        /// The auth0 API audience
        /// </summary>
        internal const string Auth0ApiAudience = "Auth0:ApiAudience";

        /// <summary>
        /// The auth0 web client identifier
        /// </summary>
        internal const string Auth0WebClientId = "Auth0:WebClientId";

        /// <summary>
        /// The auth0 API client identifier
        /// </summary>
        internal const string Auth0ApiClientId = "Auth0:ApiClientId";

        /// <summary>
        /// The application json constant
        /// </summary>
        internal const string ApplicationJsonConstant = "application/json";

        /// <summary>
        /// The user display name constant.
        /// </summary>
        internal const string UserEmailClaimConstant = "name";

        /// <summary>
        /// The ibbs API application configuration key constant
        /// </summary>
        internal const string IbbsAPIAppConfigKeyConstant = "IBBS.API";

        /// <summary>
        /// The ai agents configuration key constant
        /// </summary>
        internal const string AiAgentsConfigurationKeyConstant = "AiAgentsConfiguration";

        /// <summary>
        /// Represents the name of the default authorization policy used when no specific policy is specified.
        /// </summary>
        internal const string DefaultAuthorizationPolicy = "DefaultPolicy";
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
    }

    /// <summary>
    /// The Swagger UI Constants
    /// </summary>
    internal static class SwaggerUIConstants
    {
        /// The api version for Swagger documentation.
        /// </summary>
        internal const string ApiVersion = "v1";

        /// <summary>
        /// The swagger endpoint for the API documentation.
        /// </summary>
        internal const string SwaggerEndpointUrl = "/swagger/v1/swagger.json";

        /// <summary>
        /// The swagger ui endpoint prefix.
        /// </summary>
        internal const string SwaggerUiPrefix = "swaggerui";

        /// <summary>
        /// The description for the Swagger documentation.
        /// </summary>
        internal const string SwaggerDescription = "MCP Documentation for IBBS";

        /// <summary>
        /// The Author Details class contains information about the author of the API.
        /// </summary>
        internal static class AuthorDetails
        {
            /// <summary>
            /// The author's name.
            /// </summary>
            internal static readonly string Name = "Debanjan Paul";

            /// <summary>
            /// The author's email address.
            /// </summary>
            internal static readonly string Email = "debanjanpaul10@gmail.com";
        }

        /// <summary>
        /// The API name for Swagger documentation.
        /// </summary>
        internal const string ApplicationAPIName = "IBBS.MCP.Server";
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
