// *********************************************************************************
//	<copyright file="HttpClientHelper.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The HTTP Client Helper Class.</summary>
// *********************************************************************************

namespace InternetBulletin.Web.Helpers
{
    using InternetBulletin.Shared.Constants;
    using Newtonsoft.Json;
    using System.Text;

    /// <summary>
    /// The HTTP Client Helper Interface.
    /// </summary>
    public interface IHttpClientHelper
    {
        /// <summary>
        /// Gets the API response asynchronous.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>The response message</returns>
        Task<HttpResponseMessage> GetAsync(string url);

        /// <summary>
        /// Posts the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">The URL.</param>
        /// <param name="data">The data.</param>
        /// <returns>The response message.</returns>
        Task<HttpResponseMessage> PostAsync(string url, string data);

        /// <summary>
        /// Posts ai async.
        /// </summary>
        /// <param name="url">The url.</param>
        /// <param name="data">The data.</param>
        Task<HttpResponseMessage> PostAiAsync<T>(string url, T data);
    }

    /// <summary>
    /// The HTTP Client Helper Class.
    /// </summary>
    /// <seealso cref="InternetBulletin.Web.Helpers.IHttpClientHelper" />
    /// <param name="httpClientFactory">The HTTP client factory.</param>
    /// <param name="logger">The Logger.</param>
    public class HttpClientHelper(IHttpClientFactory httpClientFactory, ILogger<HttpClientHelper> logger) : IHttpClientHelper
    {
        /// <summary>
        /// The HTTP client factory
        /// </summary>
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<HttpClientHelper> _logger = logger;

        /// <summary>
        /// Gets the API response asynchronous.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>The response message</returns>
        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            using var httpClient = this._httpClientFactory.CreateClient(ConfigurationConstants.BulletinHttpClientConstant);
            try
            {
                var response = await httpClient.GetAsync(url).ConfigureAwait(false);
                return response;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(GetAsync), DateTime.Now, url));
                throw;
            }
        }

        /// <summary>
        /// Posts the data to API asynchronously.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="data">The data.</param>
        /// <returns>The http response message.</returns>
        public async Task<HttpResponseMessage> PostAsync(string url, string data)
        {
            using var httpClient = this._httpClientFactory.CreateClient(ConfigurationConstants.BulletinHttpClientConstant);
            try
            {
                var content = new StringContent(data, Encoding.UTF8, ConfigurationConstants.ApplicationJsonConstant);
                var response = await httpClient.PostAsync(url, content).ConfigureAwait(false);
                return response;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(PostAsync), DateTime.Now, url));
                throw;
            }
        }

        /// <summary>
        /// Posts ai async.
        /// </summary>
        /// <param name="url">The url.</param>
        /// <param name="data">The data.</param>
        public async Task<HttpResponseMessage> PostAiAsync<T>(string url, T data)
        {
            using var httpClient = this._httpClientFactory.CreateClient(ConfigurationConstants.BulletinAiHttpClientConstant);
            try
            {
                var jsonData = JsonConvert.SerializeObject(data);
                var content = new StringContent(jsonData, Encoding.UTF8, ConfigurationConstants.ApplicationJsonConstant);
                var response = await httpClient.PostAsync(url, content).ConfigureAwait(false);
                return response;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(PostAsync), DateTime.Now, url));
                throw;
            }
        }
    }
}
