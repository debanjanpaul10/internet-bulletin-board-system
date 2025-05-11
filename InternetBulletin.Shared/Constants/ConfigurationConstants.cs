// *********************************************************************************
//	<copyright file="ConfigurationConstants.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Configuration Constants Class.</summary>
// *********************************************************************************

namespace InternetBulletin.Shared.Constants
{
    /// <summary>
    /// The Configuration Constants Class.
    /// </summary>
    public static class ConfigurationConstants
    {
        /// <summary>
        /// The app configuration endpoint key constant.
        /// </summary>
        public const string AppConfigurationEndpointKeyConstant = "AppConfigurationEndpoint";

        /// <summary>
        /// The SQL connection string constant
        /// </summary>
        public const string SqlConnectionStringConstant = "SqlConnectionString";

        /// <summary>
        /// The user assigned client identifier constant
        /// </summary>
        public const string ManagedIdentityClientIdConstant = "ManagedIdentityClientId";

        /// <summary>
        /// The base configuration app config key constant.
        /// </summary>
        public const string BaseConfigurationAppConfigKeyConstant = "BaseConfiguration";

        /// <summary>
        /// The ibbs api app config key constant.
        /// </summary>
        public const string IbbsAPIAppConfigKeyConstant = "IBBS.API";

        /// <summary>
        /// The local appsettings file constant.
        /// </summary>
        public const string LocalAppsettingsFileConstant = "appsettings.development.json";

        /// <summary>
        /// The auth0 domain constant.
        /// </summary>
        public const string Auth0DomainConstant = "Auth0:Domain";

        /// <summary>
        /// The audience constant.
        /// </summary>
        public const string Auth0AudienceConstant = "Auth0:Audience";

        /// <summary>
        /// The user name claim constant.
        /// </summary>
        public const string UserNameClaimConstant = "username";
    }
}
