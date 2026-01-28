using IBBS.AI.Agents.Adapters.Contracts;
using IBBS.Domain.DomainEntities.AI;
using IBBS.Domain.DrivenPorts;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static IBBS.AI.Agents.Adapters.Helpers.Constants;

namespace IBBS.AI.Agents.Adapters.AgentManagers;

/// <summary>
/// The AI Agents Service.
/// </summary>
/// <param name="httpClientHelper">The http client helper service.</param>
/// <param name="logger">The logger service.</param>
/// <seealso cref="IBBS.Domain.DrivenPorts.IAiAgentsService" />
public sealed class AiAgentsService(IHttpClientHelper httpClientHelper, ILogger<AiAgentsService> logger) : IAiAgentsService
{
    /// <summary>
    /// Invoke the workspace agent with user message and get the response.
    /// </summary>
    /// <param name="chatRequestModel">The chat request model.</param>
    /// <returns>The string response from AI.</returns>
    public async Task<string> InvokeWorkspaceAgentAsync(WorkspaceAgentChatRequestDomain chatRequestModel)
    {
        try
        {
            logger.LogInformation(LoggingConstants.LogHelperMethodStart, nameof(InvokeWorkspaceAgentAsync), DateTime.UtcNow, JsonConvert.SerializeObject(chatRequestModel));

            var response = await httpClientHelper.GetAIResponseAsync(
                data: chatRequestModel,
                apiUrl: AIAgentsRoutesConstants.InvokeWorkspaceAgent_ApiRoute).ConfigureAwait(false);
            var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            var aiResponse = JsonConvert.DeserializeObject<AIAgentResponse>(responseString) ?? new AIAgentResponse();
            return aiResponse.ResponseData.ToString() ?? throw new Exception(ExceptionConstants.AiServicesCannotBeAvailedExceptionConstant);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, LoggingConstants.LogHelperMethodFailed, nameof(InvokeWorkspaceAgentAsync), DateTime.UtcNow, ex.Message);
            throw;
        }
        finally
        {
            logger.LogInformation(LoggingConstants.LogHelperMethodEnded, nameof(InvokeWorkspaceAgentAsync), DateTime.UtcNow, JsonConvert.SerializeObject(chatRequestModel));
        }
    }

    /// <summary>
    /// Gets the workspace group chat response.
    /// </summary>
    /// <param name="chatRequestModel">The workspace agent chat request dto model.</param>
    /// <returns>The group chat response.</returns>
    public async Task<string> GetWorkspaceGroupChatResponseAsync(WorkspaceAgentChatRequestDomain chatRequestModel)
    {
        try
        {
            logger.LogInformation(LoggingConstants.LogHelperMethodStart, nameof(GetWorkspaceGroupChatResponseAsync), DateTime.UtcNow, JsonConvert.SerializeObject(chatRequestModel));
            var response = await httpClientHelper.GetAIResponseAsync(
                data: chatRequestModel,
                apiUrl: AIAgentsRoutesConstants.GetWorkspaceGroupChatResponse_ApiRoute).ConfigureAwait(false);
            var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            var aiResponse = JsonConvert.DeserializeObject<AIAgentResponse>(responseString) ?? new AIAgentResponse();
            return aiResponse.ResponseData.ToString() ?? throw new Exception(ExceptionConstants.AiServicesCannotBeAvailedExceptionConstant);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, LoggingConstants.LogHelperMethodFailed, nameof(GetWorkspaceGroupChatResponseAsync), DateTime.UtcNow, ex.Message);
            throw;
        }
        finally
        {
            logger.LogInformation(LoggingConstants.LogHelperMethodEnded, nameof(GetWorkspaceGroupChatResponseAsync), DateTime.UtcNow, JsonConvert.SerializeObject(chatRequestModel));
        }

    }
}
