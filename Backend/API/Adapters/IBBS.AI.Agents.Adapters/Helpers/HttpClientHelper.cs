using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using System.Text;
using IBBS.AI.Agents.Adapters.Contracts;
using IBBS.Domain.Contracts;
using IBBS.Domain.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static IBBS.AI.Agents.Adapters.Helpers.Constants;

namespace IBBS.AI.Agents.Adapters.Helpers;

/// <summary>
/// Http client helper service.
/// </summary>
/// <param name="correlationContext">The correlation context.</param>
/// <param name="logger">The Logger</param>
/// <param name="configuration">The configuration.</param>
/// <param name="httpClientFactory">The http client factory.</param>
/// <seealso cref="IHttpClientHelper"/>
[ExcludeFromCodeCoverage]
public sealed class HttpClientHelper(
    ICorrelationContext correlationContext,
    ILogger<HttpClientHelper> logger,
    IConfiguration configuration,
    IHttpClientFactory httpClientFactory) : IHttpClientHelper
{
    /// <inheritdoc/>
    public async Task<HttpResponseMessage> GetAIResponseAsync<T>(
        T data,
        string apiUrl,
        CancellationToken cancellationToken = default
    )
    {
        HttpResponseMessage response = new();
        try
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodStart,
                nameof(GetAIResponseAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, data, apiUrl })
            );

            ArgumentException.ThrowIfNullOrEmpty(apiUrl);

            var client = httpClientFactory.CreateClient(name: ConfigurationConstants.AiAgentsHttpClient);
            var authenticationToken = await TokenHelper.GetAiAgentsLabTokenAsync(
                configuration,
                logger,
                correlationId: correlationContext.CorrelationId,
                cancellationToken
            ).ConfigureAwait(false);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                scheme: ConfigurationConstants.BearerConstant,
                parameter: authenticationToken
            );

            var inputJson = JsonConvert.SerializeObject(data);
            var contentData = new StringContent(
                content: inputJson,
                encoding: Encoding.UTF8,
                mediaType: ConfigurationConstants.ApplicationJsonConstant
            );

            response = await client.PostAsync(
                requestUri: apiUrl,
                content: contentData,
                cancellationToken
            ).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
                return response.EnsureSuccessStatusCode();

            return response;
        }
        catch (Exception ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.LogHelperMethodFailed,
                nameof(GetAIResponseAsync), DateTime.UtcNow, ex.Message
            );
            throw new IBBSBusinessException(
                message: ex.Message,
                correlationId: correlationContext.CorrelationId
            );
        }
        finally
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodEnded,
                nameof(GetAIResponseAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, data, apiUrl, response })
            );
        }
    }
}
