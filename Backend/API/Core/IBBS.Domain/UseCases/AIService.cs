using IBBS.Domain.Contracts;
using IBBS.Domain.DomainEntities;
using IBBS.Domain.DomainEntities.AI;
using IBBS.Domain.DrivenPorts;
using IBBS.Domain.DrivingPorts;
using IBBS.Domain.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static IBBS.Domain.Helpers.DomainConstants;
using static IBBS.Domain.Helpers.DomainConstants.AiAgentsIdConstants;

namespace IBBS.Domain.UseCases;

/// <summary>
/// The AI Service.
/// </summary>
/// <param name="correlationContext">The correlation context used to track requests.</param>
/// <param name="logger">The logger service.</param>
/// <param name="aiAgentsService">The ai agent service.</param>
/// <param name="commonDataManager">The common data manager.</param>
/// <param name="configuration">The configuration.</param>
/// <seealso cref="IBBS.Domain.DrivingPorts.IAIService" />
public sealed class AIService(
    ICorrelationContext correlationContext,
    ILogger<AIService> logger,
    IAiAgentsService aiAgentsService,
    ICommonDataManager commonDataManager,
    IConfiguration configuration) : IAIService
{
    /// <inheritdoc/>
    public async Task<string> RewriteWithAIAsync(
        string userName,
        UserStoryRequestDomain requestDTO,
        CancellationToken cancellationToken = default
    )
    {
        string response = string.Empty;
        try
        {
            logger.LogAppInformation(
                LoggingConstants.MethodStartedMessageConstant,
                nameof(RewriteWithAIAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, requestDTO, userName })
            );

            var agentId = configuration[RewriteTextAgent.Id] ?? throw new Exception(string.Format(ExceptionConstants.AgentNotFoundMessageConstant, RewriteTextAgent.Id));
            var chatRequestModel = DomainUtilities.PrepareWorkspaceAgentChatRequestDomain(
                configuration,
                agentId,
                requestDTO
            );

            response = await aiAgentsService.InvokeWorkspaceAgentAsync(
                chatRequestModel,
                cancellationToken
            ).ConfigureAwait(false);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.MethodFailedWithMessageConstant,
                nameof(RewriteWithAIAsync), DateTime.UtcNow, ex.Message
            );
            throw new IBBSBusinessException(
                message: ex.Message,
                correlationId: correlationContext.CorrelationId
            );
        }
        finally
        {
            logger.LogAppInformation(
                LoggingConstants.MethodEndedMessageConstant,
                nameof(RewriteWithAIAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, requestDTO, userName, response })
            );
        }
    }

    /// <inheritdoc/>
    public async Task<string> GenerateTagForStoryAsync(
        string userName,
        UserStoryRequestDomain requestDTO,
        CancellationToken cancellationToken = default
    )
    {
        string response = string.Empty;
        try
        {
            logger.LogAppInformation(
                LoggingConstants.MethodStartedMessageConstant,
                nameof(GenerateTagForStoryAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, requestDTO, userName })
            );

            var agentId = configuration[GenerateTagAgent.Id] ?? throw new Exception(string.Format(ExceptionConstants.AgentNotFoundMessageConstant, GenerateTagAgent.Id));
            var chatRequestModel = DomainUtilities.PrepareWorkspaceAgentChatRequestDomain(
                configuration,
                agentId,
                requestDTO
            );

            response = await aiAgentsService.InvokeWorkspaceAgentAsync(
                chatRequestModel,
                cancellationToken
            ).ConfigureAwait(false);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.MethodFailedWithMessageConstant,
                nameof(GenerateTagForStoryAsync), DateTime.UtcNow, ex.Message
            );
            throw new IBBSBusinessException(
                message: ex.Message,
                correlationId: correlationContext.CorrelationId
            );
        }
        finally
        {
            logger.LogAppInformation(
                LoggingConstants.MethodEndedMessageConstant,
                nameof(GenerateTagForStoryAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, requestDTO, userName, response })
            );
        }
    }

    /// <inheritdoc/>
    public async Task<string> ModerateContentDataAsync(
        string userName,
        UserStoryRequestDomain requestDTO,
        CancellationToken cancellationToken = default
    )
    {
        string response = string.Empty;
        try
        {
            logger.LogAppInformation(
                LoggingConstants.MethodStartedMessageConstant,
                nameof(ModerateContentDataAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, requestDTO, userName })
            );

            var agentId = configuration[ModerateContentAgent.Id] ?? throw new Exception(string.Format(ExceptionConstants.AgentNotFoundMessageConstant, ModerateContentAgent.Id));
            var chatRequestModel = DomainUtilities.PrepareWorkspaceAgentChatRequestDomain(
                configuration,
                agentId,
                requestDTO
            );

            response = await aiAgentsService.InvokeWorkspaceAgentAsync(
                chatRequestModel,
                cancellationToken
            ).ConfigureAwait(false);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.MethodFailedWithMessageConstant,
                nameof(ModerateContentDataAsync), DateTime.UtcNow, ex.Message
            );
            throw new IBBSBusinessException(
                message: ex.Message,
                correlationId: correlationContext.CorrelationId
            );
        }
        finally
        {
            logger.LogAppInformation(
                LoggingConstants.MethodEndedMessageConstant,
                nameof(ModerateContentDataAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, requestDTO, userName, response })
            );
        }
    }

    /// <inheritdoc/>
    public async Task<string> GenerateBugSeverityAsync(
        BugSeverityAIRequestDomain bugSeverityAiRequest,
        CancellationToken cancellationToken = default
    )
    {
        string response = string.Empty;
        try
        {
            logger.LogAppInformation(
                LoggingConstants.MethodStartedMessageConstant,
                nameof(GenerateBugSeverityAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, bugSeverityAiRequest })
            );

            var agentId = configuration[GenerateBugSeverityAgent.Id] ?? throw new Exception(string.Format(ExceptionConstants.AgentNotFoundMessageConstant, GenerateBugSeverityAgent.Id));
            var chatRequestModel = DomainUtilities.PrepareWorkspaceAgentChatRequestDomain(
                configuration,
                agentId,
                requestDTO: bugSeverityAiRequest
            );

            response = await aiAgentsService.InvokeWorkspaceAgentAsync(
                chatRequestModel,
                cancellationToken
            ).ConfigureAwait(false);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.MethodFailedWithMessageConstant,
                nameof(GenerateBugSeverityAsync), DateTime.UtcNow, ex.Message
            );
            throw new IBBSBusinessException(
                message: ex.Message,
                correlationId: correlationContext.CorrelationId
            );
        }
        finally
        {
            logger.LogAppInformation(
                LoggingConstants.MethodEndedMessageConstant,
                nameof(GenerateBugSeverityAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, bugSeverityAiRequest, response })
            );
        }
    }

    /// <inheritdoc/>
    public async Task<bool> PostAiResultFeedbackAsync(
        AIResponseFeedbackDomain aiResponseFeedback,
        string userEmail,
        CancellationToken cancellationToken = default
    )
    {
        // TODO: Needs to be implemented
        bool response = false;
        try
        {
            logger.LogAppInformation(
                LoggingConstants.MethodStartedMessageConstant,
                nameof(PostAiResultFeedbackAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, aiResponseFeedback, userEmail })
            );
            await Task.Delay(
                millisecondsDelay: 300,
                cancellationToken
            ).ConfigureAwait(false);
            response = true;
            return response;
        }
        catch (Exception ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.MethodFailedWithMessageConstant,
                nameof(PostAiResultFeedbackAsync), DateTime.UtcNow, ex.Message
            );
            throw new IBBSBusinessException(
                message: ex.Message,
                correlationId: correlationContext.CorrelationId
            );
        }
        finally
        {
            logger.LogAppInformation(
                LoggingConstants.MethodEndedMessageConstant,
                nameof(PostAiResultFeedbackAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, aiResponseFeedback, userEmail, response })
            );
        }
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<LookupMasterDomain>> GetSamplePromptsForChatbotAsync(
        CancellationToken cancellationToken = default
    )
    {
        IEnumerable<LookupMasterDomain> response = [];
        try
        {
            logger.LogAppInformation(
                LoggingConstants.MethodStartedMessageConstant,
                nameof(GetSamplePromptsForChatbotAsync), DateTime.UtcNow, correlationContext.CorrelationId
            );

            response = await commonDataManager.GetSamplePromptsForChatbotAsync(
                cancellationToken
            ).ConfigureAwait(false);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.MethodFailedWithMessageConstant,
                nameof(GetSamplePromptsForChatbotAsync), DateTime.UtcNow, ex.Message
            );
            throw new IBBSBusinessException(
                message: ex.Message,
                correlationId: correlationContext.CorrelationId
            );
        }
        finally
        {
            logger.LogAppInformation(
                LoggingConstants.MethodEndedMessageConstant,
                nameof(GetSamplePromptsForChatbotAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, response })
            );
        }
    }

    /// <inheritdoc/>
    public async Task<string> GetChatbotResponseAsync(
        UserQueryRequestDomain userQueryRequest,
        CancellationToken cancellationToken = default
    )
    {
        string response = string.Empty;
        try
        {
            logger.LogAppInformation(
                LoggingConstants.MethodStartedMessageConstant,
                nameof(GetChatbotResponseAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, userQueryRequest })
            );

            var chatRequestModel = DomainUtilities.PrepareWorkspaceAgentChatRequestDomain(
                configuration,
                requestDTO: userQueryRequest
            );

            response = await aiAgentsService.GetWorkspaceGroupChatResponseAsync(
                chatRequestModel,
                cancellationToken
            ).ConfigureAwait(false);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.MethodFailedWithMessageConstant,
                nameof(GetChatbotResponseAsync), DateTime.UtcNow, ex.Message
            );
            throw new IBBSBusinessException(
                message: ex.Message,
                correlationId: correlationContext.CorrelationId
            );
        }
        finally
        {
            logger.LogAppInformation(
                LoggingConstants.MethodEndedMessageConstant,
                nameof(GetChatbotResponseAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, userQueryRequest, response })
            );
        }
    }
}
