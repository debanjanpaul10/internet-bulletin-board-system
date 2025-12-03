using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using System.Text;
using IBBS.AI.Agents.Adapters.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static IBBS.AI.Agents.Adapters.Helpers.Constants;

namespace IBBS.AI.Agents.Adapters.Helpers;

/// <summary>
/// Http client helper service.
/// </summary>
/// <param name="logger">The Logger</param>
/// <param name="configuration">The configuration.</param>
/// <param name="httpClientFactory">The http client factory.</param>
/// <seealso cref="IHttpClientHelper"/>
[ExcludeFromCodeCoverage]
public class HttpClientHelper(ILogger<HttpClientHelper> logger, IConfiguration configuration, IHttpClientFactory httpClientFactory) : IHttpClientHelper
{
	/// <summary>
	/// Gets the ai response asynchronous.
	/// </summary>
	/// <typeparam name="T">The input data.</typeparam>
	/// <param name="data">The data.</param>
	/// <param name="apiUrl">The api url.</param>
	/// <returns>
	/// The response from AI
	/// </returns>
	public async Task<HttpResponseMessage> GetAIResponseAsync<T>(T data, string apiUrl)
	{
		try
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(GetAIResponseAsync), DateTime.UtcNow, data?.GetType().Name ?? string.Empty));
			ArgumentException.ThrowIfNullOrEmpty(apiUrl);

			var client = httpClientFactory.CreateClient(ConfigurationConstants.AiAgentsHttpClient);
			await PrepareHttpClientFactoryAsync(client, TokenHelper.GetAiAgentsLabTokenAsync(configuration, logger));

			var inputJson = JsonConvert.SerializeObject(data);
			var contentData = new StringContent(content: inputJson, encoding: Encoding.UTF8, ConfigurationConstants.ApplicationJsonConstant);

			var response = await client.PostAsync(apiUrl, contentData).ConfigureAwait(false);
			if (!response.IsSuccessStatusCode)
				return response.EnsureSuccessStatusCode();

			return response;
		}
		catch (Exception ex)
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodFailed, nameof(GetAIResponseAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetAIResponseAsync), DateTime.UtcNow, data?.GetType().Name ?? string.Empty));
		}
	}

	#region PRIVATE Methods

	/// <summary>
	/// Prepares http client factory async.
	/// </summary>
	/// <param name="client">The client.</param>
	/// <param name="tokenTask">The task to get the token.</param>
	private static async Task PrepareHttpClientFactoryAsync(HttpClient client, Task<string> tokenTask)
	{
		var token = await tokenTask.ConfigureAwait(false);
		client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(ConfigurationConstants.BearerConstant, token);
	}

	#endregion
}
