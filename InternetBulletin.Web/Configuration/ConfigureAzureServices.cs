// *********************************************************************************
//	<copyright file="ConfigureAzureServices.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Configures Azure Services.</summary>
// *********************************************************************************

namespace InternetBulletin.Web.Configuration
{
    using Azure.Identity;
    using InternetBulletin.Shared.Constants;
    using Microsoft.Extensions.Configuration.AzureAppConfiguration;
    using static InternetBulletin.Shared.Constants.ConfigurationConstants;

    /// <summary>
    /// Configures Azure Services.
    /// </summary>
    public static class ConfigureAzureServices
    {
        /// <summary>
        /// Configures azure app config.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="credentials">The credentials.</param>
        /// <exception cref="InvalidOperationException">InvalidOperationException error.</exception>
        public static void ConfigureAzureAppConfig(this WebApplicationBuilder builder, DefaultAzureCredential credentials)
        {
            var appConfigurationEndpoint = builder.Configuration[AppConfigurationEndpointKeyConstant];
            if (string.IsNullOrEmpty(appConfigurationEndpoint))
            {
                throw new InvalidOperationException(ExceptionConstants.ConfigurationValueIsEmptyMessageConstant);
            }

            builder.Configuration.AddAzureAppConfiguration(options =>
            {
                options.Connect(new Uri(appConfigurationEndpoint), credentials)
                .Select(KeyFilter.Any).Select(KeyFilter.Any, BaseConfigurationAppConfigKeyConstant)
                .Select(KeyFilter.Any, IbbsAPIAppConfigKeyConstant).Select(KeyFilter.Any, IBBSAIAppConfigKeyConstant)
                .ConfigureKeyVault(configure =>
                {
                    configure.SetCredential(credentials);
                });
            });
        }
    }
}
