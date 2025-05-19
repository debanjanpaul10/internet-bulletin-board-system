// *********************************************************************************
//	<copyright file="HttpClientHelper.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Http Client Helper Services Class.</summary>
// *********************************************************************************

namespace InternetBulletin.Shared.Helpers
{
    using Azure.Identity;
    using InternetBulletin.Shared.Constants;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Microsoft.Graph;
    using Microsoft.Graph.Models;
    using static InternetBulletin.Shared.Constants.ConfigurationConstants;

    /// <summary>
    /// Http client helper interface.
    /// </summary>
    public interface IHttpClientHelper
    {
        /// <summary>
        /// Gets graph api data async.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <returns>The user collection response data.</returns>
        Task<UserCollectionResponse> GetGraphApiDataAsync(string userName);
    }

    /// <summary>
    /// Http client helper service.
    /// </summary>
    /// <param name="logger">The Logger</param>
    /// <param name="configuration">The configuration.</param>
    /// <seealso cref="IHttpClientHelper"/>
    public class HttpClientHelper(ILogger<HttpClientHelper> logger, IConfiguration configuration) : IHttpClientHelper
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<HttpClientHelper> _logger = logger;

        /// <summary>
        /// The configuration.
        /// </summary>
        private readonly IConfiguration _configuration = configuration;

        /// <summary>
        /// Gets graph api data async.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <returns>The user collection response data.</returns>
        public async Task<UserCollectionResponse> GetGraphApiDataAsync(string userName)
        {
            try
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetGraphApiDataAsync), DateTime.UtcNow, userName));

                var scopes = new[] { this._configuration[GraphAPIDefaultScopeConstant] };
                var tenantId = this._configuration[TenantIdConstant];
                var clientId = this._configuration[GraphAPIClientIdConstant];
                var clientSecret = this._configuration[GraphAPIClientSecretConstant];

                var clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);
                var graphClient = new GraphServiceClient(clientSecretCredential, scopes);

                // Get all users and filter by the custom username field
                var users = await graphClient.Users
                    .GetAsync(requestConfiguration =>
                    {
                        requestConfiguration.QueryParameters.Select = [
                            IbbsConstants.IdConstant,
                            IbbsConstants.DisplayNameConstant,
                            IbbsConstants.IdentitiesConstant,
                            IbbsConstants.UserNameExtensionConstant
                        ];
                    });

                if (users is not null)
                {
                    return users;
                }

                throw new Exception(ExceptionConstants.UserDoesNotExistsMessageConstant);

            }
            catch (Exception ex)
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodFailed, nameof(GetGraphApiDataAsync), DateTime.UtcNow, ex.Message));
                throw;
            }
            finally
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetGraphApiDataAsync), DateTime.UtcNow, userName));
            }
        }
    }

}

