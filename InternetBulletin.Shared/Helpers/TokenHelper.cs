// *********************************************************************************
//	<copyright file="TokenHelper.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Token helper class.</summary>
// *********************************************************************************

namespace InternetBulletin.Shared.Helpers
{
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using Azure.Core;
    using Azure.Identity;
    using InternetBulletin.Shared.Constants;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Microsoft.Identity.Client;
    using static InternetBulletin.Shared.Constants.ConfigurationConstants;

    /// <summary>
    /// Token helper.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class TokenHelper
    {
        /// <summary>
        /// Gets ibbs ai token async.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="Exception">Exception error.</exception>
        public static async Task<string> GetIbbsAiTokenAsync(IConfiguration configuration, ILogger logger)
        {
            try
            {
                logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetIbbsAiTokenAsync), DateTime.UtcNow, IbbsConstants.IbbsAIConstant));

                var tenantId = configuration[TenantIdConstant];
                var clientId = configuration[IbbsAiAdClientId];
                var clientSecret = configuration[IbbsAiAdClientSecret];
                var scopes = new[] { string.Format(CultureInfo.CurrentCulture, TokenScopeFormat, clientId) };

                _ = bool.TryParse(Environment.GetEnvironmentVariable("IsDevelopmentMode"), out var isDevelopmentMode);
                if (isDevelopmentMode)
                {
                    var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);
                    var accessToken = await credential.GetTokenAsync(new TokenRequestContext(scopes), CancellationToken.None).ConfigureAwait(false);

                    if (string.IsNullOrEmpty(accessToken.Token))
                    {
                        throw new Exception(ExceptionConstants.SomethingWentWrongMessageConstant);
                    }

                    return accessToken.Token;
                }
                else
                {
                    var miClientId = configuration[ManagedIdentityClientIdConstant];
                    ArgumentException.ThrowIfNullOrEmpty(miClientId);

                    var msal = ConfidentialClientApplicationBuilder.Create(clientId)
                        .WithClientAssertion((AssertionRequestOptions options) => GetManagedIdentityToken(miClientId, IbbsAiFICCTokenAudience))
                        .WithAuthority(configuration[IBBSWebIssuerConstant]).Build();

                    var result = await msal.AcquireTokenForClient(scopes).ExecuteAsync();
                    return result.AccessToken;
                }
            }
            catch (Exception ex)
            {
                logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodFailed, nameof(GetIbbsAiTokenAsync), DateTime.UtcNow, ex.Message));
                throw;
            }
            finally
            {
                logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetIbbsAiTokenAsync), DateTime.UtcNow, IbbsConstants.IbbsAIConstant));
            }
        }


        /// <summary>
        /// Gets the managed identity token.
        /// </summary>
        /// <param name="msiClientId">The MSI Client Id.</param>
        /// <param name="audience">The FICC audience</param>
        /// <returns>The token data.</returns>
        public static async Task<string> GetManagedIdentityToken(string msiClientId, string audience)
        {
            var miIdentity = Microsoft.Identity.Client.AppConfig.ManagedIdentityId.WithUserAssignedClientId(msiClientId);
            var miApplication = ManagedIdentityApplicationBuilder.Create(miIdentity).Build();
            var miResult = await miApplication.AcquireTokenForManagedIdentity($"{audience}/.default").ExecuteAsync().ConfigureAwait(false);
            return miResult.AccessToken;
        }
    }

}

