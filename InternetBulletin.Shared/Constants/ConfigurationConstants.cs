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
        /// The web antiforgery token value
        /// </summary>
        public const string WebAntiforgeryTokenConstant = "x-antiforgery-token-ibbs-web";

        /// <summary>
        /// The web antiforgery token value
        /// </summary>
        public const string WebAntiforgeryTokenValue = "WebAntiforgeryToken";

        /// <summary>
        /// The API antiforgery token value
        /// </summary>
        public const string APIAntiforgeryTokenConstant = "x-antiforgery-token-ibbs-api";

        /// <summary>
        /// The ai antiforgery token constant.
        /// </summary>
        public const string AIAntiforgeryTokenConstant = "x-antiforgery-token-bulletin-ai";

        /// <summary>
        /// The api antiforgery token value.
        /// </summary>
        public const string APIAntiforgeryTokenValue = "APIAntiforgeryToken";

        /// <summary>
        /// The application insights instrumentation keykv
        /// </summary>
        public const string AppInsightsInstrumentationKeykv = "AppInsights-InstrumentationKey";

        /// <summary>
        /// The cosmos db connection string constant
        /// </summary>
        public const string CosmosConnectionStringConstant = "CosmosConnectionString";

        /// <summary>
        /// The SQL connection string constant
        /// </summary>
        public const string SqlConnectionStringConstant = "SqlConnectionString";

        /// <summary>
        /// The base address constant
        /// </summary>
        public const string WebApiBaseAddressConstant = "WebApiBaseAddress";

        /// <summary>
        /// The local web api base address constant.
        /// </summary>
        public const string LocalWebApiBaseAddressConstant = "LocalWebApiBaseAddress";

        /// <summary>
        /// The bulletin HTTP client constant
        /// </summary>
        public const string BulletinHttpClientConstant = "bulletinClient";

        /// <summary>
        /// The bulletin ai http client constant.
        /// </summary>
        public const string BulletinAiHttpClientConstant = "bulletinAiClient";

        /// <summary>
        /// The user assigned client identifier constant
        /// </summary>
        public const string ManagedIdentityClientIdConstant = "ManagedIdentityClientId";

        /// <summary>
        /// The is development mode constant
        /// </summary>
        public const string IsDevelopmentModeConstant = "IsDevelopmentMode";

        /// <summary>
        /// The application json constant
        /// </summary>
        public const string ApplicationJsonConstant = "application/json";

        /// <summary>
        /// The cosmos database name constant.
        /// </summary>
        public const string CosmosDatabaseNameConstant = "CosmosDatabaseName";

        /// <summary>
        /// The posts container name constant.
        /// </summary>
        public const string PostsContainerConstant = "posts";

        /// <summary>
        /// The base configuration app config key constant.
        /// </summary>
        public const string BaseConfigurationAppConfigKeyConstant = "BaseConfiguration";

        /// <summary>
        /// The ibbs api app config key constant.
        /// </summary>
        public const string IbbsAPIAppConfigKeyConstant = "IBBS.API";

        /// <summary>
        /// The bulletin ai api endpoint constant.
        /// </summary>
        public const string BulletinAIApiEndpointConstant = "BulletinAiApiUrl";

        /// <summary>
        /// The bulletin ai token constant.
        /// </summary>
        public const string BulletinAiTokenConstant = "BulletinAiToken";

        /// <summary>
        /// The ibbs ai app config key constant.
        /// </summary>
        public const string IBBSAIAppConfigKeyConstant = "IBBS.AI";
    }
}
