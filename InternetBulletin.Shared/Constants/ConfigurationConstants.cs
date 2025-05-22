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
        /// The user name claim constant.
        /// </summary>
        public const string UserNameClaimConstant = "extension_UserName";

        /// <summary>
        /// The user display name constant.
        /// </summary>
        public const string UserDisplayNameConstant = "name";

        /// <summary>
        /// The azure ad b2 c constant.
        /// </summary>
        public const string AzureAdB2CConstant = "AzureAdB2C";

        /// <summary>
        /// The IBBS api client id constant.
        /// </summary>
        public const string IBBSApiClientIdConstant = "AzureAdB2C:ClientId";

        /// <summary>
        /// The IBBS api issuer constant.
        /// </summary>
        public const string IBBSApiIssuerConstant = "AzureAdB2C:Issuer";

        /// <summary>
        /// The tenant id constant.
        /// </summary>
        public const string TenantIdConstant = "AzureAD:TenantId";

        #region Graph API

        /// <summary>
        /// The graph api default scope constant.
        /// </summary>
        public const string GraphAPIDefaultScopeConstant = "AzureAD:GraphAPIDefaultScope";

        /// <summary>
        /// The graph a p i client id constant.
        /// </summary>
        public const string GraphAPIClientIdConstant = "AzureAD:GraphAPIClientId";

        /// <summary>
        /// The graph a p i client secret constant.
        /// </summary>
        public const string GraphAPIClientSecretConstant = "AzureAD:GraphAPIClientSecret";

        #endregion
    }
}
