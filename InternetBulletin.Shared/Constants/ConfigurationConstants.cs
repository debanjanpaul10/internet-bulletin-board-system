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
		/// The local sql connection string constant.
		/// </summary>
		public const string LocalSqlConnectionStringConstant = "LocalSqlServerConnection";

		/// <summary>
		/// The mongo db connection string constant.
		/// </summary>
		public const string MongoDbConnectionStringConstant = "MongoDbConnectionString";

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
		/// The ibbs functin app config key constant.
		/// </summary>
		public const string IbbsFunctinAppConfigKeyConstant = "IBBS.Function";

		/// <summary>
		/// The local appsettings file constant.
		/// </summary>
		public const string LocalAppsettingsFileConstant = "appsettings.development.json";

		/// <summary>
		/// The user name claim constant.
		/// </summary>
		public const string UserNameClaimConstant = "User Name";

		/// <summary>
		/// The user display name constant.
		/// </summary>
		public const string UserDisplayNameConstant = "name";

		/// <summary>
		/// The authorization constant.
		/// </summary>
		public const string AuthorizationConstant = "Authorization";

		/// <summary>
		/// The bearer constant.
		/// </summary>
		public const string BearerConstant = "Bearer";

		/// <summary>
		/// The application json constant.
		/// </summary>
		public const string ApplicationJsonConstant = "application/json";

		/// <summary>
		/// The null string constant.
		/// </summary>
		public const string NullStringConstant = "null";

		/// <summary>
		/// The token scope format.
		/// </summary>
		public const string TokenScopeFormat = "{0}/.default";

		/// <summary>
		/// The mongo db database name constant.
		/// </summary>
		public const string MongoDatabaseNameConstant = "MongoDatabaseName";

		#region Azure AD Constants

		/// <summary>
		/// The IBBS web api client id constant.
		/// </summary>
		public const string IbbsWebApiClientIdConstant = "IbbsWebApiClientId";

		/// <summary>
		/// The IBBS Web client id constant.
		/// </summary>
		public const string IbbsWebClientIdConstant = "IbbsWebClientId";

		/// <summary>
		/// The IBBS web token issuer constant.
		/// </summary>
		public const string IBBSWebIssuerConstant = "IbbsWebIssuer";

		/// <summary>
		/// The tenant id constant.
		/// </summary>
		public const string TenantIdConstant = "TenantId";

		/// <summary>
		/// The ibbs ai FICC token audience.
		/// </summary>
		public const string IbbsAiFICCTokenAudience = "api://AzureADTokenExchange";

		#endregion

		#region Graph API

		/// <summary>
		/// The graph api default scope constant.
		/// </summary>
		public const string GraphAPIDefaultScopeConstant = "GraphAPIDefaultScope";

		/// <summary>
		/// The graph a p i client id constant.
		/// </summary>
		public const string GraphAPIClientIdConstant = "GraphAPIClientId";

		/// <summary>
		/// The graph a p i client secret constant.
		/// </summary>
		public const string GraphAPIClientSecretConstant = "GraphAPIClientSecret";

		#endregion

		#region IBBS.AI

		/// <summary>
		/// The ibbs ai API URL constant
		/// </summary>
		public const string IbbsAiApiBaseUrlConstant = "IbbsAiApiBaseUrl";

		/// <summary>
		/// The ibbs ai ad client id.
		/// </summary>
		public const string IbbsAiAdClientId = "IbbsAiApiClientId";

		/// <summary>
		/// The ibbs ai ad client secret.
		/// </summary>
		public const string IbbsAiAdClientSecret = "IbbsAiClientSecret";

		#endregion
	}
}
