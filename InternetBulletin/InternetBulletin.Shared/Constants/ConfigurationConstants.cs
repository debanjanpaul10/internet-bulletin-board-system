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
		/// The application configuration endpoint constant
		/// </summary>
		public static readonly string AppConfigEndpointConstant = "AppConfigEndpoint";

		/// <summary>
		/// The application configuration connection string constant
		/// </summary>
		public static readonly string AppConfigConnectionStringConstant = "AppConfigConnectionString";

		/// <summary>
		/// The base address constant
		/// </summary>
		public static readonly string WebApiBaseAddressConstant = "WebApiBaseAddress";

		/// <summary>
		/// The bulletin HTTP client constant
		/// </summary>
		public static readonly string BulletinHttpClientConstant = "bulletinClient";

		/// <summary>
		/// The web antiforgery token constant
		/// </summary>
		public static readonly string WebAntiforgeryTokenConstant = "x-antiforgery-token-web";

		/// <summary>
		/// The web antiforgery token value
		/// </summary>
		public static readonly string WebAntiforgeryTokenValue = "x-antiforgery-token-web-kv";

		/// <summary>
		/// The key vault endpoint constant
		/// </summary>
		public static readonly string KeyVaultEndpointConstant = "KeyVaultUrl";

		/// <summary>
		/// The user assigned client identifier constant
		/// </summary>
		public static readonly string UserAssignedClientIdConstant = "UserAssignedClientId";

		/// <summary>
		/// The is development mode constant
		/// </summary>
		public static readonly string IsDevelopmentModeConstant = "IsDevelopmentMode";

		/// <summary>
		/// The application insights instrumentation keykv
		/// </summary>
		public static readonly string AppInsightsInstrumentationKeykv = "AppInsights-InstrumentationKey-kv";

		/// <summary>
		/// The application json constant
		/// </summary>
		public static readonly string ApplicationJsonConstant = "application/json";
	}
}
