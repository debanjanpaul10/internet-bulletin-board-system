using System.Security.Claims;
using Azure.Identity;
using IBBS.AI.Agents.Adapters.IOC;
using IBBS.API.Adapters.IOC;
using IBBS.Domain.IOC;
using IBBS.Infrastructure.MongoDB.Adapters.IOC;
using IBBS.Infrastructure.Persistence.Adapters.IOC;
using IBBS.MCP.Tools;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.IdentityModel.Tokens;
using static IBBS.MCP.Helpers.MCPConstants;

namespace IBBS.MCP.IOC;

/// <summary>
/// Provides extension methods for configuring authentication services in a dependency injection container.
/// </summary>
/// <remarks>This static class contains helper methods for registering authentication schemes and related event
/// handlers with an <see cref="IServiceCollection"/>. The methods are intended to simplify the setup of JWT bearer
/// authentication and related token validation logic in ASP.NET Core applications. These extensions are typically used
/// during application startup to configure authentication behavior based on environment and configuration
/// settings.</remarks>
internal static class DIContainer
{
    /// <summary>
    /// Configures MCP-related services, including authentication, caching, API handlers, data dependencies, domain
    /// services, AI agent services, and MongoDB adapter dependencies, for the specified web application builder.
    /// </summary>
    /// <remarks>This method should be called during application startup to ensure all required MCP services are
    /// registered in the dependency injection container. The configuration and environment settings of the builder are
    /// used to customize service registration as appropriate.</remarks>
    /// <param name="builder">The <see cref="WebApplicationBuilder"/> instance to which MCP services will be added. Must not be null.</param>
    internal static void ConfigureMcpServices(this WebApplicationBuilder builder)
    {
        builder.ConfigureAuthenticationServices().AddAuthorizationPolicyBuilder();
        builder.Services.AddMemoryCache().AddAPIHandlers()
            .AddDataDependencies(builder.Configuration, builder.Environment.IsDevelopment())
            .AddDomainServices().AddAiAgentsServices(builder.Configuration)
            .AddMongoDbAdapterDependencies(builder.Configuration);
    }

    /// <summary>
    /// Adds a default authorization policy to the specified web application builder, configuring it to use JWT bearer authentication.
    /// </summary>
    /// <remarks>The added policy is named "policy" and is configured to use the JWT bearer authentication
    /// scheme. This method should be called before building the application to ensure the policy is available for
    /// authorization.</remarks>
    /// <param name="builder">The web application builder to which the authorization policy will be added. Cannot be null.</param>
    /// <returns>The same <see cref="WebApplicationBuilder"/> instance, enabling method chaining.</returns>
    internal static WebApplicationBuilder AddAuthorizationPolicyBuilder(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthorizationBuilder()
          .AddPolicy(ConfigurationConstants.DefaultAuthorizationPolicy, policy =>
          {
              policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
              policy.RequireAuthenticatedUser();
          });

        return builder;
    }

    /// <summary>
    /// Configures Azure App Configuration and Azure Key Vault integration for the specified web application builder
    /// using the provided credentials.
    /// </summary>
    /// <remarks>This method adds Azure App Configuration as a configuration source and enables Key Vault
    /// integration for secret management. The App Configuration endpoint must be set in the application's configuration
    /// using the key defined by ConfigurationConstants.AppConfigurationEndpointKeyConstant. All keys are selected from
    /// the configuration, including those under specific namespaces for base configuration, API settings, and AI agent
    /// settings.</remarks>
    /// <param name="builder">The web application builder to configure with Azure App Configuration and Key Vault integration.</param>
    /// <param name="credentials">The credentials used to authenticate requests to Azure App Configuration and Azure Key Vault. Must be a valid instance of DefaultAzureCredential.</param>
    /// <exception cref="InvalidOperationException">Thrown if the App Configuration endpoint is not specified in the application's configuration.</exception>
    internal static void ConfigureAzureAppConfiguration(this WebApplicationBuilder builder, DefaultAzureCredential credentials)
    {
        var configuration = builder.Configuration;
        var appConfigurationEndpoint = configuration[ConfigurationConstants.AppConfigurationEndpointKeyConstant];
        if (string.IsNullOrEmpty(appConfigurationEndpoint))
            throw new InvalidOperationException(ExceptionConstants.ConfigurationValueIsEmptyMessageConstant);

        configuration.AddAzureAppConfiguration(options =>
            options.Connect(new Uri(appConfigurationEndpoint), credentials)
                .Select(KeyFilter.Any).Select(KeyFilter.Any, ConfigurationConstants.BaseConfigurationAppConfigKeyConstant)
                .Select(KeyFilter.Any, ConfigurationConstants.IbbsAPIAppConfigKeyConstant).Select(KeyFilter.Any, ConfigurationConstants.AiAgentsConfigurationKeyConstant)
                .ConfigureKeyVault((options) => options.SetCredential(credentials))
        );
    }

    /// <summary>
    /// Configures JWT bearer authentication services for the specified web application builder using Auth0 settings.
    /// </summary>
    /// <remarks>This method sets up authentication schemes and token validation parameters based on
    /// configuration values for Auth0. In development environments, HTTPS metadata requirements are disabled to facilitate local testing.</remarks>
    /// <param name="builder">The web application builder to which authentication services will be added. Must not be null.</param>
    internal static WebApplicationBuilder ConfigureAuthenticationServices(this WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateLifetime = false,
                ValidateIssuer = true,
                ValidateAudience = true,
                RequireExpirationTime = false,
                RequireSignedTokens = true,
                ValidAudience = configuration[ConfigurationConstants.Auth0ApiAudience],
                ValidIssuer = configuration[ConfigurationConstants.Auth0Domain],
                SignatureValidator = (token, _) => new Microsoft.IdentityModel.JsonWebTokens.JsonWebToken(token)
            };

            // For development: disable HTTPS requirement
            if (builder.Environment.IsDevelopment())
                options.RequireHttpsMetadata = false;

            options.Events = new JwtBearerEvents
            {
                OnTokenValidated = HandleAuthTokenValidationSuccessAsync,
                OnAuthenticationFailed = HandleAuthTokenValidationFailedAsync
            };
        });

        return builder;
    }

    /// <summary>
    /// Handles successful validation of an authentication token by updating the HTTP context's user principal.
    /// </summary>
    /// <remarks>This method sets the authenticated user principal on the HTTP context after token validation.
    /// If the principal is not authenticated, the context is marked as failed.</remarks>
    /// <param name="context">The token validation context containing authentication information and the HTTP context to update. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
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
    /// Handles an authentication token validation failure by logging the error and marking the authentication attempt as failed.
    /// </summary>
    /// <remarks>This method should be called when token validation fails during authentication. It logs the
    /// failure and ensures the authentication process is terminated with the appropriate error message.</remarks>
    /// <param name="context">The context for the authentication failure event. Provides access to the exception details and HTTP context required for logging and failure handling.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    private static async Task HandleAuthTokenValidationFailedAsync(this AuthenticationFailedContext context)
    {
        var authenticationFailedException = new UnauthorizedAccessException(context.Exception.Message);
        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<BaseTool>>();
        logger.LogError(authenticationFailedException, context.Exception.Message);

        context.Fail(context.Exception.Message);
        await Task.CompletedTask;
    }
}
