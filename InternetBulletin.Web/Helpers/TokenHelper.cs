
namespace InternetBulletin.Web.Helpers
{
    using System;
    using System.Globalization;
    using static InternetBulletin.Shared.Constants.ConfigurationConstants;
    using InternetBulletin.Shared.Constants;
    using Microsoft.Identity.Client;

    /// <summary>
    /// Token helper.
    /// </summary>
    /// <param name="configuration">The Configuration</param>
    /// <param name="logger">The Logger</param>
    public class TokenHelper(IConfiguration configuration, ILogger<TokenHelper> logger)
    {
        /// <summary>
        /// The configuration.
        /// </summary>
        private readonly IConfiguration _configuration = configuration;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<TokenHelper> _logger = logger;

        /// <summary>
        /// Gets azure ad token async.
        /// </summary>
        public async Task<string> GetAzureAdTokenAsync()
        {
            try
            {
                var tenantId = this._configuration[TenantIdConstant];
                var clientId = this._configuration[ClientIdConstant];
                var clientSecret = this._configuration[ClientSecretConstant];

                if (!string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(clientSecret) && !string.IsNullOrEmpty(tenantId))
                {
                    var defaultScopeUrl = string.Format(CultureInfo.CurrentCulture, ResourceConstant, clientId);
                    var scopes = new string[] { defaultScopeUrl };
                    var msalClient = ConfidentialClientApplicationBuilder
                        .Create(clientId: clientId).WithClientSecret(clientSecret: clientSecret).WithAuthority(AadAuthorityAudience.AzureAdMyOrg, validateAuthority: true)
                        .WithTenantId(tenantId: tenantId).Build();

                    var authResult = await msalClient.AcquireTokenForClient(scopes).ExecuteAsync();

                    var accessToken = authResult.AccessToken;
                    return accessToken;
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(GetAzureAdTokenAsync), DateTime.UtcNow, ex.Message));
                throw;
            }

        }
    }
}
