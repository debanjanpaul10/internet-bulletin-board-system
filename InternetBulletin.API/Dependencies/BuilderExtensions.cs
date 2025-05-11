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
        /// <param name="builder">The builder.</param>
        public static void ConfigureApiServices(this WebApplicationBuilder builder)
        {
            builder.ConfigureAuthenticationServices();
            builder.ConfigureAzureSqlServer();
            builder.ConfigureBusinessManagerDependencies();
            builder.ConfigureDataManagerDependencies();
        }

        #region PRIVATE Methods

        /// <summary>
        /// Configures authentication services.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private static void ConfigureAuthenticationServices(this WebApplicationBuilder builder)
        {
            var configuration = builder.Configuration;
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = $"https://{configuration[Auth0DomainConstant]}";
                options.Audience = configuration[Auth0AudienceConstant];
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async context =>
                    {
                        await context.HandleAuthTokenValidationSuccessAsync();
                    },
                    OnAuthenticationFailed = async context =>
                    {
                        await context.HandleAuthTokenValidationFailedAsync();
                    }
                };
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
            var authenticationFailedException = new UnauthorizedAccessException(ExceptionConstants.InvalidTokenExceptionConstant);
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<BaseController>>();
            logger.LogError(authenticationFailedException, authenticationFailedException.Message);
            await Task.CompletedTask;
        }

        #endregion
    }
}
