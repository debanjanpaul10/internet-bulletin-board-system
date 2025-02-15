// *********************************************************************************
//	<copyright file="ConfigureAzureServices.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Configures Azure Services.</summary>
// *********************************************************************************

namespace InternetBulletin.Web.Configuration
{
	using Azure.Identity;
	using InternetBulletin.Web.Helpers;
	using Microsoft.ApplicationInsights.AspNetCore.Extensions;
	using static InternetBulletin.Shared.Constants.ConfigurationConstants;

	/// <summary>
	/// Configures Azure Services.
	/// </summary>
	public static class ConfigureAzureServices
	{
		/// <summary>
		/// Configures the azure application configuration.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		public static void ConfigureAzureAppConfig(ConfigurationManager configuration)
		{
			var appConfigConnectionString = KeyVaultHelper.GetSecretDataAsync(configuration, AppConfigurationConnectionString);
			if (!string.IsNullOrEmpty(appConfigConnectionString))
			{
				var tokenCredential = new DefaultAzureCredential();
				configuration.AddAzureAppConfiguration(options =>
				{
					options.Connect(connectionString: appConfigConnectionString);
					options.ConfigureKeyVault(option =>
					{
						option.SetCredential(tokenCredential);
					});
				});
			}
		}

		/// <summary>
		/// Configures the azure application insights.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		/// <param name="services">The services.</param>
		public static void ConfigureAzureApplicationInsights(ConfigurationManager configuration, IServiceCollection services)
		{
			var appInsightsTelemetryConnection = KeyVaultHelper.GetSecretDataAsync(configuration, AppInsightsInstrumentationKeykv);
			var options = new ApplicationInsightsServiceOptions { ConnectionString = appInsightsTelemetryConnection };
			services.AddApplicationInsightsTelemetry(options);
		}
	}
}
