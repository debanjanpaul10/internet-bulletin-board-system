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
			var appConfigEndpoint = configuration.GetValue<string>(AppConfigEndpointConstant);
			var userAssignedClientId = configuration.GetValue<string>(UserAssignedClientIdConstant);
			if (!string.IsNullOrEmpty(appConfigEndpoint) && !string.IsNullOrEmpty(userAssignedClientId))
			{
				if (configuration.GetValue<bool>(IsDevelopmentModeConstant))
				{
					var connectionString = configuration.GetValue<string>(AppConfigConnectionStringConstant);
					configuration.AddAzureAppConfiguration(connectionString);
				}
				else
				{
					var tokenCredentials = new DefaultAzureCredential(
										new DefaultAzureCredentialOptions
										{
											ManagedIdentityClientId = userAssignedClientId
										});
					configuration.AddAzureAppConfiguration(options =>
					{
						options.Connect(new Uri(appConfigEndpoint), tokenCredentials);
						options.ConfigureKeyVault(options =>
						{
							options.SetCredential(tokenCredentials);
						});
					});
				}
			}
		}

		/// <summary>
		/// Configures the azure key vault.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		public static void ConfigureAzureKeyVault(ConfigurationManager configuration)
		{
			var keyVaultEndpoint = configuration.GetValue<string>(KeyVaultEndpointConstant);
			var userAssignedClientId = configuration.GetValue<string>(UserAssignedClientIdConstant);
			if (!string.IsNullOrEmpty(keyVaultEndpoint) && !string.IsNullOrEmpty(userAssignedClientId))
			{
				configuration.AddAzureKeyVault(new Uri(keyVaultEndpoint), new DefaultAzureCredential(
					new DefaultAzureCredentialOptions()
					{
						ManagedIdentityClientId = userAssignedClientId
					}));
			}
		}

		/// <summary>
		/// Configures the azure application insights.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		/// <param name="services">The services.</param>
		public static void ConfigureAzureApplicationInsights(ConfigurationManager configuration, IServiceCollection services)
		{
			var appInsightsTelemetryConnection = KeyVaultHelper.GetKeyValueAsync(configuration, AppInsightsInstrumentationKeykv);
			var options = new ApplicationInsightsServiceOptions { ConnectionString = appInsightsTelemetryConnection };
			services.AddApplicationInsightsTelemetry(options);
		}
	}
}
