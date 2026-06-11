using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Azure.Core;
using Azure.Identity;
using IBBS.Domain.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using static IBBS.AI.Agents.Adapters.Helpers.Constants;

namespace IBBS.AI.Agents.Adapters.Helpers;

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
    /// <param name="correlationId">The correlation id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <exception cref="Exception">Exception error.</exception>
    public static async Task<string> GetAiAgentsLabTokenAsync(
        IConfiguration configuration,
        ILogger logger,
        string correlationId,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodStart,
                nameof(GetAiAgentsLabTokenAsync), DateTime.UtcNow, correlationId
            );

            var tenantId = configuration[ConfigurationConstants.AiAgentsLabTenantId];
            var clientId = configuration[ConfigurationConstants.AiAgentsAdClientId];
            var clientSecret = configuration[ConfigurationConstants.AiAgentsAdClientSecret];
            var scopes = new[] { string.Format(CultureInfo.CurrentCulture, ConfigurationConstants.TokenScopeFormat, clientId) };

            var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);
            var accessToken = await credential.GetTokenAsync(
                requestContext: new TokenRequestContext(scopes!),
                cancellationToken
            ).ConfigureAwait(false);

            ArgumentException.ThrowIfNullOrWhiteSpace(accessToken.Token);
            return accessToken.Token;
        }
        catch (Exception ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.LogHelperMethodFailed,
                nameof(GetAiAgentsLabTokenAsync), DateTime.UtcNow, ex.Message
            );
            throw new IBBSBusinessException(
                message: ex.Message,
                correlationId
            );
        }
        finally
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodEnded,
                nameof(GetAiAgentsLabTokenAsync), DateTime.UtcNow, correlationId
            );
        }
    }
}

