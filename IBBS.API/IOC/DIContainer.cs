﻿using Azure.Identity;
using IBBS.API.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using static IBBS.API.Helpers.APIConstants;

namespace IBBS.API.IOC;

/// <summary>
/// The Dependency Injection Container Class.
/// </summary>
public static class DIContainer
{
	/// <summary>
	/// Configures the API services.
	/// </summary>
	/// <param name="builder">The builder.</param>
	public static void ConfigureApiServices(this WebApplicationBuilder builder)
	{
		builder.ConfigureAuthenticationServices();
		builder.Services.AddMemoryCache();
	}

	/// <summary>
	/// Configures azure app configuration.
	/// </summary>
	/// <param name="builder">The builder.</param>
	/// <param name="credentials">The credentials.</param>
	/// <exception cref="InvalidOperationException">InvalidOperationException error.</exception>
	public static void ConfigureAzureAppConfiguration(this WebApplicationBuilder builder, DefaultAzureCredential credentials)
	{
		var configuration = builder.Configuration;
		var appConfigurationEndpoint = configuration[ConfigurationConstants.AppConfigurationEndpointKeyConstant];
		if (string.IsNullOrEmpty(appConfigurationEndpoint))
		{
			throw new InvalidOperationException(ExceptionConstants.ConfigurationValueIsEmptyMessageConstant);
		}

		configuration.AddAzureAppConfiguration(options =>
		{
			options.Connect(new Uri(appConfigurationEndpoint), credentials)
				.Select(KeyFilter.Any).Select(KeyFilter.Any, ConfigurationConstants.BaseConfigurationAppConfigKeyConstant)
				.Select(KeyFilter.Any, ConfigurationConstants.IbbsAPIAppConfigKeyConstant)
				.ConfigureKeyVault((options) =>
				{
					options.SetCredential(credentials);
				});

		});
	}

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
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateLifetime = true,
				ValidateIssuer = true,
				ValidateAudience = true,
				RequireExpirationTime = true,
				RequireSignedTokens = true,
				ValidAudience = configuration[ConfigurationConstants.IbbsWebClientIdConstant],
				ValidIssuer = configuration[ConfigurationConstants.IBBSWebIssuerConstant],
				SignatureValidator = (token, _) => new Microsoft.IdentityModel.JsonWebTokens.JsonWebToken(token)
			};
			options.Events = new JwtBearerEvents
			{
				OnTokenValidated = HandleAuthTokenValidationSuccessAsync,
				OnAuthenticationFailed = HandleAuthTokenValidationFailedAsync
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
		var authenticationFailedException = new UnauthorizedAccessException(context.Exception.Message);
		var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<BaseController>>();
		logger.LogError(authenticationFailedException, context.Exception.Message);

		context.Fail(context.Exception.Message);
		await Task.CompletedTask;
	}

}
