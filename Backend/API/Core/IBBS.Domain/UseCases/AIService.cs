using System.Globalization;
using IBBS.Domain.DomainEntities;
using IBBS.Domain.DomainEntities.AI;
using IBBS.Domain.DrivenPorts;
using IBBS.Domain.DrivingPorts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static IBBS.Domain.Helpers.DomainConstants;
using static IBBS.Domain.Helpers.DomainConstants.AiAgentsIdConstants;

namespace IBBS.Domain.UseCases;

/// <summary>
/// The AI Service.
/// </summary>
/// <param name="logger">The logger service.</param>
/// <param name="aiAgentsService">The ai agent service.</param>
/// <param name="commonDataManager">The common data manager.</param>
/// <param name="configuration">The configuration.</param>
/// <seealso cref="IBBS.Domain.DrivingPorts.IAIService" />
public sealed class AIService(ILogger<AIService> logger, IAiAgentsService aiAgentsService, ICommonDataManager commonDataManager, IConfiguration configuration) : IAIService
{

    /// <summary>
    /// Rewrites the provided story using AI processing.
    /// </summary>
    /// <param name="userName">The username of the user requesting the rewrite.</param>
    /// <param name="requestDTO">The story content to be rewritten.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the AI-rewritten story.</returns>
    public async Task<string> RewriteWithAIAsync(string userName, UserStoryRequestDomain requestDTO)
    {
        try
        {
            logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(RewriteWithAIAsync), DateTime.UtcNow, userName));

            var chatRequestModel = new WorkspaceAgentChatRequestDomain()
            {
                AgentId = configuration[RewriteTextAgent.Id] ?? throw new Exception(string.Format(ExceptionConstants.AgentNotFoundMessageConstant, RewriteTextAgent.Id)),
                ApplicationName = configuration[IbbsPluginsWorkspace.WorkspaceName] ?? throw new Exception(string.Format(ExceptionConstants.WorkspaceNotFoundMessageConstant, IbbsPluginsWorkspace.WorkspaceName)),
                WorkspaceId = configuration[IbbsPluginsWorkspace.WorkspaceId] ?? throw new Exception(string.Format(ExceptionConstants.WorkspaceNotFoundMessageConstant, IbbsPluginsWorkspace.WorkspaceId)),
                ConversationId = Guid.NewGuid().ToString(),
                UserMessage = requestDTO.Story
            };
            return await aiAgentsService.InvokeWorkspaceAgentAsync(chatRequestModel).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(RewriteWithAIAsync), DateTime.UtcNow, ex.Message));
            throw;
        }
        finally
        {
            logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(RewriteWithAIAsync), DateTime.UtcNow, userName));
        }
    }

    /// <summary>
    /// Generates a genre tag for the provided story using AI processing.
    /// </summary>
    /// <param name="userName">The username of the user requesting the tag generation.</param>
    /// <param name="requestDTO">The story content for which to generate a tag.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the generated genre tag.</returns>
    public async Task<string> GenerateTagForStoryAsync(string userName, UserStoryRequestDomain requestDTO)
    {
        try
        {
            logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GenerateTagForStoryAsync), DateTime.UtcNow, userName));

            var chatRequestModel = new WorkspaceAgentChatRequestDomain()
            {
                AgentId = configuration[GenerateTagAgent.Id] ?? throw new Exception(string.Format(ExceptionConstants.AgentNotFoundMessageConstant, GenerateTagAgent.Id)),
                ApplicationName = configuration[IbbsPluginsWorkspace.WorkspaceName] ?? throw new Exception(string.Format(ExceptionConstants.WorkspaceNotFoundMessageConstant, IbbsPluginsWorkspace.WorkspaceName)),
                WorkspaceId = configuration[IbbsPluginsWorkspace.WorkspaceId] ?? throw new Exception(string.Format(ExceptionConstants.WorkspaceNotFoundMessageConstant, IbbsPluginsWorkspace.WorkspaceId)),
                ConversationId = Guid.NewGuid().ToString(),
                UserMessage = requestDTO.Story
            };
            return await aiAgentsService.InvokeWorkspaceAgentAsync(chatRequestModel).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GenerateTagForStoryAsync), DateTime.UtcNow, ex.Message));
            throw;
        }
        finally
        {
            logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GenerateTagForStoryAsync), DateTime.UtcNow, userName));
        }
    }

    /// <summary>
    /// Moderates the content of the provided story using AI processing.
    /// </summary>
    /// <param name="userName">The username of the user requesting content moderation.</param>
    /// <param name="requestDTO">The story content to be moderated.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the content rating.</returns>
    public async Task<string> ModerateContentDataAsync(string userName, UserStoryRequestDomain requestDTO)
    {
        try
        {
            logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(ModerateContentDataAsync), DateTime.UtcNow, userName));

            var chatRequestModel = new WorkspaceAgentChatRequestDomain()
            {
                AgentId = configuration[ModerateContentAgent.Id] ?? throw new Exception(string.Format(ExceptionConstants.AgentNotFoundMessageConstant, ModerateContentAgent.Id)),
                ApplicationName = configuration[IbbsPluginsWorkspace.WorkspaceName] ?? throw new Exception(string.Format(ExceptionConstants.WorkspaceNotFoundMessageConstant, IbbsPluginsWorkspace.WorkspaceName)),
                WorkspaceId = configuration[IbbsPluginsWorkspace.WorkspaceId] ?? throw new Exception(string.Format(ExceptionConstants.WorkspaceNotFoundMessageConstant, IbbsPluginsWorkspace.WorkspaceId)),
                ConversationId = Guid.NewGuid().ToString(),
                UserMessage = requestDTO.Story
            };
            return await aiAgentsService.InvokeWorkspaceAgentAsync(chatRequestModel).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(ModerateContentDataAsync), DateTime.UtcNow, ex.Message));
            throw;
        }
        finally
        {
            logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(ModerateContentDataAsync), DateTime.UtcNow, userName));
        }
    }

    /// <summary>
    /// Generate the bug severity using LLM.
    /// </summary>
    /// <param name="bugSeverityAiRequest">The bug severity AI request model.</param>
    /// <returns>The bug severity.</returns>
    public async Task<string> GenerateBugSeverityAsync(BugSeverityAIRequestDomain bugSeverityAiRequest)
    {
        try
        {
            logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GenerateBugSeverityAsync), DateTime.UtcNow, bugSeverityAiRequest.BugTitle));

            var chatRequestModel = new WorkspaceAgentChatRequestDomain()
            {
                AgentId = configuration[GenerateBugSeverityAgent.Id] ?? throw new Exception(string.Format(ExceptionConstants.AgentNotFoundMessageConstant, GenerateBugSeverityAgent.Id)),
                ApplicationName = configuration[IbbsPluginsWorkspace.WorkspaceName] ?? throw new Exception(string.Format(ExceptionConstants.WorkspaceNotFoundMessageConstant, IbbsPluginsWorkspace.WorkspaceName)),
                WorkspaceId = configuration[IbbsPluginsWorkspace.WorkspaceId] ?? throw new Exception(string.Format(ExceptionConstants.WorkspaceNotFoundMessageConstant, IbbsPluginsWorkspace.WorkspaceId)),
                ConversationId = Guid.NewGuid().ToString(),
                UserMessage = JsonConvert.SerializeObject(new { Title = bugSeverityAiRequest.BugTitle, Description = bugSeverityAiRequest.BugDescription })
            };
            return await aiAgentsService.InvokeWorkspaceAgentAsync(chatRequestModel).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GenerateBugSeverityAsync), DateTime.UtcNow, ex.Message));
            throw;
        }
        finally
        {
            logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GenerateBugSeverityAsync), DateTime.UtcNow, bugSeverityAiRequest.BugTitle));
        }
    }

    /// <summary>
    /// Posts the ai result feedback asynchronous.
    /// </summary>
    /// <param name="aiResponseFeedback">The ai response feedback.</param>
    /// <param name="userEmail">The user email address.</param>
    /// <returns>
    /// The boolean for success/failure.
    /// </returns>
    public async Task<bool> PostAiResultFeedbackAsync(AIResponseFeedbackDomain aiResponseFeedback, string userEmail)
    {
        try
        {
            logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(PostAiResultFeedbackAsync), DateTime.UtcNow, userEmail));
            await Task.Delay(300);
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(PostAiResultFeedbackAsync), DateTime.UtcNow, ex.Message));
            throw;
        }
        finally
        {
            logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(PostAiResultFeedbackAsync), DateTime.UtcNow, userEmail));
        }
    }

    /// <summary>
    /// Gets the sample prompts for chatbot asynchronous.
    /// </summary>
    /// <returns>
    /// The list of <see cref="LookupMasterDomain" />
    /// </returns>
    public async Task<IEnumerable<LookupMasterDomain>> GetSamplePromptsForChatbotAsync()
    {
        try
        {
            logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetSamplePromptsForChatbotAsync), DateTime.UtcNow, string.Empty));
            return await commonDataManager.GetSamplePromptsForChatbotAsync().ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetSamplePromptsForChatbotAsync), DateTime.UtcNow, ex.Message));
            throw;
        }
        finally
        {
            logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetSamplePromptsForChatbotAsync), DateTime.UtcNow, string.Empty));
        }
    }

}
