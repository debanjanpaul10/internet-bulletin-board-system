// *********************************************************************************
//	<copyright file="TokenHelper.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Token helper class.</summary>
// *********************************************************************************

namespace IBBS.AI.Agents.Adapters.Helpers;

using Azure.Core;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using static IBBS.AI.Agents.Adapters.Helpers.Constants;

/// <summary>
/// Token helper.
/// </summary>
[ExcludeFromCodeCoverage]
internal class TokenHelper
{
	/// <summary>
	/// Gets ibbs ai token async.
	/// </summary>
	/// <param name="configuration">The configuration.</param>
	/// <param name="logger">The logger.</param>
	/// <exception cref="Exception">Exception error.</exception>
	public static async Task<string> GetAiAgentsLabTokenAsync(IConfiguration configuration, ILogger logger)
	{
		try
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetAiAgentsLabTokenAsync), DateTime.UtcNow, string.Empty));

			var tenantId = configuration[ConfigurationConstants.AiAgentsLabTenantId];
			var clientId = configuration[ConfigurationConstants.AiAgentsAdClientId];
			var clientSecret = configuration[ConfigurationConstants.AiAgentsAdClientSecret];
			var scopes = new[] { string.Format(CultureInfo.CurrentCulture, ConfigurationConstants.TokenScopeFormat), clientId };

			var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);
			var accessToken = await credential.GetTokenAsync(new TokenRequestContext(scopes!), CancellationToken.None);

			ArgumentException.ThrowIfNullOrWhiteSpace(accessToken.Token);
			return accessToken.Token;
		}
		catch (Exception ex)
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodFailed, nameof(GetAiAgentsLabTokenAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetAiAgentsLabTokenAsync), DateTime.UtcNow, string.Empty));
		}
	}
}

