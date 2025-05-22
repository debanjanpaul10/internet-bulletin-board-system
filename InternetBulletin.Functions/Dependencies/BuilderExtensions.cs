// *********************************************************************************
//	<copyright file="BuilderExtensions.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Builder extensions.</summary>
// *********************************************************************************

namespace InternetBulletin.Functions.Dependencies
{
    using Azure.Identity;
    using InternetBulletin.Business.Contracts;
    using InternetBulletin.Business.Services;
    using InternetBulletin.Data;
    using InternetBulletin.Data.Contracts;
    using InternetBulletin.Data.DataServices;
    using InternetBulletin.Shared.Constants;
    using InternetBulletin.Shared.Helpers;
    using Microsoft.Azure.Functions.Worker.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Configuration.AzureAppConfiguration;
    using Microsoft.Extensions.DependencyInjection;
    using static InternetBulletin.Shared.Constants.ConfigurationConstants;

    /// <summary>
    /// Builder extensions.
    /// </summary>
    public static class BuilderExtensions
    {
        /// <summary>
        /// Configures azure app configuration.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="credentials">The credentials.</param>
        /// <exception cref="InvalidOperationException">InvalidOperationException error.</exception>
        public static void ConfigureAzureAppConfiguration(this FunctionsApplicationBuilder builder, DefaultAzureCredential credentials)
        {
            var configuration = builder.Configuration;
            var appConfigurationEndpoint = configuration[AppConfigurationEndpointKeyConstant];
            if (string.IsNullOrEmpty(appConfigurationEndpoint))
            {
                throw new InvalidOperationException(ExceptionConstants.ConfigurationValueIsEmptyMessageConstant);
            }

            configuration.AddAzureAppConfiguration(options =>
            {
                options.Connect(new Uri(appConfigurationEndpoint), credentials)
                .Select(KeyFilter.Any).Select(KeyFilter.Any, BaseConfigurationAppConfigKeyConstant)
                .Select(KeyFilter.Any, IbbsFunctinAppConfigKeyConstant)
                .ConfigureKeyVault(configure =>
                {
                    configure.SetCredential(credentials);
                });
            });
        }

        /// <summary>
        /// Configures the application dependencies.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="services">The services.</param>
        public static void ConfigureAzureSqlServer(this FunctionsApplicationBuilder builder)
        {
            var sqlConnectionString = builder.Configuration[SqlConnectionStringConstant];
            if (!string.IsNullOrEmpty(sqlConnectionString))
            {
                builder.Services.AddDbContext<SqlDbContext>(options =>
                {
                    options.UseSqlServer
                    (
                        connectionString: sqlConnectionString,
                        options => options.EnableRetryOnFailure
                        (
                            maxRetryCount: 3,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null
                        )
                    );
                });
            }
        }

        /// <summary>
        /// Configures function services.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public static void ConfigureFunctionServices(this FunctionsApplicationBuilder builder)
        {
            builder.Services.AddHttpClient<IHttpClientHelper, HttpClientHelper>();
            builder.ConfigureBusinessManagerDependencies();
            builder.ConfigureDataManagerDependencies();
        }

        #region PRIVATE Methods

        /// <summary>
        /// Configures business manager dependencies.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public static void ConfigureBusinessManagerDependencies(this FunctionsApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUsersService, UsersService>();
            builder.Services.AddScoped<IHttpClientHelper, HttpClientHelper>();
        }

        /// <summary>
        /// Configures data manager dependencies.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public static void ConfigureDataManagerDependencies(this FunctionsApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUsersDataService, UsersDataService>();
        }

        #endregion
    }

}

