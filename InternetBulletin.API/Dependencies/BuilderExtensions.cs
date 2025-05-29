// *********************************************************************************
//	<copyright file="BuilderExtensions.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Builder extensions.</summary>
// *********************************************************************************

namespace InternetBulletin.API.Dependencies
{
    using System.Security.Claims;
    using Azure.Identity;
    using InternetBulletin.API.Controllers;
    using InternetBulletin.Shared.Constants;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.Configuration.AzureAppConfiguration;
    using Microsoft.Identity.Web;
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
        public static void ConfigureAzureAppConfiguration(this WebApplicationBuilder builder, DefaultAzureCredential credentials)
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
                .Select(KeyFilter.Any).Select(KeyFilter.Any, BaseConfigurationAppConfigKeyConstant).Select(KeyFilter.Any, IbbsAPIAppConfigKeyConstant)
                .ConfigureKeyVault(configure =>
                {
                    configure.SetCredential(credentials);
                });
            });
        }

        /// <summary>
        /// Configures api services.
        /// </summary>
        /// <summary>
        /// Configures core API services for the application, including memory caching, authentication, HTTP client factory, Azure SQL Server, and dependency injection for business and data managers.
        /// </summary>
        public static void ConfigureApiServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddMemoryCache();
            builder.ConfigureAuthenticationServices();
            builder.ConfigureHttpClientFactory();
            builder.ConfigureAzureSqlServer();
            builder.ConfigureBusinessManagerDependencies();
            builder.ConfigureDataManagerDependencies();
        }

        #region PRIVATE Methods

        /// <summary>
        /// Configures http client factory.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <summary>
        /// Registers an HTTP client with a base address from configuration and a 3-minute timeout.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the API base address configuration value is missing or empty.
        /// </exception>
        private static void ConfigureHttpClientFactory(this WebApplicationBuilder builder)
        {
            builder.Services.AddHttpClient(IbbsConstants.IbbsAIConstant, client =>
            {
                var apiBaseAddress = builder.Configuration[IbbsAiApiBaseUrlConstant];
                if (string.IsNullOrEmpty(apiBaseAddress))
                {
                    throw new ArgumentNullException(apiBaseAddress);
                }

                client.BaseAddress = new Uri(apiBaseAddress);
                client.Timeout = TimeSpan.FromMinutes(3);
            });
        }

        /// <summary>
        /// Configures authentication services.
        /// </summary>
        /// <summary>
        /// Configures JWT Bearer authentication using Microsoft Identity Web API with token validation parameters and event handlers.
        /// </summary>
        private static void ConfigureAuthenticationServices(this WebApplicationBuilder builder)
        {
            var configuration = builder.Configuration;
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidAudience = configuration[IBBSApiClientIdConstant],
                        ValidateLifetime = true,
                        ValidateIssuer = true,
                        ValidIssuer = configuration[IBBSApiIssuerConstant]
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = HandleAuthTokenValidationSuccessAsync,
                        OnAuthenticationFailed = HandleAuthTokenValidationFailedAsync
                    };
                },
                options =>
                {
                    configuration.Bind(AzureAdB2CConstant, options);
                });

        }

        /// <summary>
        /// Handles auth token validation success async.
        /// </summary>
        /// <param name="context">The token validation context.</param>
        private static async Task HandleAuthTokenValidationSuccessAsync(this TokenValidatedContext context)
        {
            var claimsPrincipal = context.Principal;
            if (claimsPrincipal?.Identity is not ClaimsIdentity claimsIdentity || !claimsIdentity.IsAuthenticated)
            {
                context.Fail(ExceptionConstants.InvalidTokenExceptionConstant);
                return;
            }

            context.HttpContext.User = new ClaimsPrincipal(claimsIdentity);
            await Task.CompletedTask;
        }

        /// <summary>
        /// Handles auth token validation failed async.
        /// </summary>
        /// <param name="context">The auth failed context.</param>
        private static async Task HandleAuthTokenValidationFailedAsync(this AuthenticationFailedContext context)
        {
            var authenticationFailedException = new UnauthorizedAccessException(context.Exception.Message);
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<BaseController>>();
            logger.LogError(authenticationFailedException, context.Exception.Message);

            context.Fail(context.Exception.Message);
            await Task.CompletedTask;
        }

        #endregion
    }
}
