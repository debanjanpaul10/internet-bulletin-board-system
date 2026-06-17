using IBBS.AI.Agents.Adapters.Contracts;
using IBBS.Domain.Contracts;
using IBBS.Domain.DomainEntities.AI;
using IBBS.Domain.DrivenPorts;
using IBBS.Domain.Helpers;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static IBBS.AI.Agents.Adapters.Helpers.Constants;

namespace IBBS.AI.Agents.Adapters.AgentManagers;

/// <summary>
/// The AI Agents Service.
/// </summary>
/// <param name="correlationContext">The correlation context.</param>
/// <param name="httpClientHelper">The http client helper service.</param>
/// <param name="logger">The logger service.</param>
/// <seealso cref="IAiAgentsService" />
public sealed class AiAgentsService(
    ICorrelationContext correlationContext,
    IHttpClientHelper httpClientHelper,
    ILogger<AiAgentsService> logger) : IAiAgentsService
{
    /// <inheritdoc/>
    public async Task<string> InvokeWorkspaceAgentAsync(
        WorkspaceAgentChatRequestDomain chatRequestModel,
        CancellationToken cancellationToken = default
    )
    {
        string response = string.Empty;
        try
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodStart,
                nameof(InvokeWorkspaceAgentAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, chatRequestModel })
            );

            var httpResponse = await httpClientHelper.GetAIResponseAsync(
                data: chatRequestModel,
                apiUrl: AIAgentsRoutesConstants.InvokeWorkspaceAgent_ApiRoute,
                cancellationToken
            ).ConfigureAwait(false);
            var responseString = await httpResponse.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            var aiResponse = JsonConvert.DeserializeObject<AIAgentResponse>(responseString) ?? new AIAgentResponse();
            response = aiResponse.ResponseData.ToString() ?? throw new Exception(ExceptionConstants.AiServicesCannotBeAvailedExceptionConstant);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.LogHelperMethodFailed,
                nameof(InvokeWorkspaceAgentAsync), DateTime.UtcNow, ex.Message
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
                nameof(InvokeWorkspaceAgentAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, chatRequestModel, response }));
        }
    }

    /// <inheritdoc/>
    public async Task<string> GetWorkspaceGroupChatResponseAsync(
        WorkspaceAgentChatRequestDomain chatRequestModel,
        CancellationToken cancellationToken = default
    )
    {
        string response = string.Empty;
        try
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodStart,
                nameof(GetWorkspaceGroupChatResponseAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, chatRequestModel })
            );

            var httpResponse = await httpClientHelper.GetAIResponseAsync(
                data: chatRequestModel,
                apiUrl: AIAgentsRoutesConstants.GetWorkspaceGroupChatResponse_ApiRoute,
                cancellationToken
            ).ConfigureAwait(false);
            var responseString = await httpResponse.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            var aiResponse = JsonConvert.DeserializeObject<AIAgentResponse>(responseString) ?? new AIAgentResponse();
            response = aiResponse.ResponseData.ToString() ?? throw new Exception(ExceptionConstants.AiServicesCannotBeAvailedExceptionConstant);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.LogHelperMethodFailed,
                nameof(GetWorkspaceGroupChatResponseAsync), DateTime.UtcNow, ex.Message
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
                nameof(GetWorkspaceGroupChatResponseAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, chatRequestModel, response })
            );
        }

    }
}
