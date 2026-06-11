using IBBS.Domain.DomainEntities.AI;
using IBBS.Domain.DomainEntities.Posts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static IBBS.Domain.Helpers.DomainConstants;
using static IBBS.Domain.Helpers.DomainConstants.AiAgentsIdConstants;

namespace IBBS.Domain.Helpers;

/// <summary>
/// The Domain utilities.
/// </summary>
internal static class DomainUtilities
{
    /// <summary>
    /// Throws if null.
    /// </summary>
    /// <param name="obj">The obj.</param>
    /// <param name="message">The message.</param>
    /// <param name="commonLogger">The common logger.</param>
    /// <typeparam name="T">The null variable type</typeparam>
    /// <typeparam name="L">The logger type.</typeparam>
    internal static T ThrowIfNull<T, L>(
        T obj,
        string message,
        ILogger<L> commonLogger
    )
    {
        if (obj is null)
            ThrowLoggedException(
                message,
                commonLogger
            );

        return obj;
    }

    /// <summary>
    /// Throws logged exception.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="commonLogger">The common logger.</param>
    /// <typeparam name="T"></typeparam>
    internal static void ThrowLoggedException<T>(
        string message,
        ILogger<T> commonLogger
    )
    {
        var exception = new Exception(message);
        commonLogger.LogAppError(
            exception,
            message: exception.Message
        );
        throw exception;
    }

    /// <summary>
    /// Validates and parse post id.
    /// </summary>
    /// <param name="postId">The post id.</param>
    /// <param name="logger">The logger.</param>
    /// <typeparam name="T">The logger type.</typeparam>
    internal static Guid ValidateAndParsePostId<T>(string postId, ILogger<T> logger)
    {
        if (string.IsNullOrWhiteSpace(postId))
            ThrowLoggedException(
                message: ExceptionConstants.PostIdNotPresentMessageConstant,
                commonLogger: logger
            );

        if (!Guid.TryParse(postId, out var postGuid))
            ThrowLoggedException(
                message: ExceptionConstants.PostGuidNotValidMessageConstant,
                commonLogger: logger
            );

        return postGuid;
    }

    /// <summary>
    /// Creates update post dto.
    /// </summary>
    /// <param name="post">The post.</param>
    internal static UpdatePostDomain CreateUpdatePostDTO(
        PostDomain post
    ) =>
        new()
        {
            PostContent = post.PostContent,
            PostRating = post.Ratings,
            PostId = post.PostId,
            PostTitle = post.PostTitle
        };

    /// <summary>
    /// Prepares the agent chatbot reponse.
    /// </summary>
    /// <param name="aiAgentResponse">The ai agent response.</param>
    /// <param name="userIntent">The user intent.</param>
    /// <param name="input">The input.</param>
    /// <param name="aiResponse">The ai response.</param>
    /// <returns>The populated agent response domain.</returns>
    internal static AIChatbotResponseDomain PrepareAgentChatbotReponse(
        this AIChatbotResponseDomain aiAgentResponse,
        string userIntent,
        string input,
        string aiResponse
    )
    {
        aiAgentResponse.UserIntent = userIntent.Trim();
        aiAgentResponse.UserQuery = input.Trim();
        aiAgentResponse.AIResponseData = aiResponse.Trim();

        return aiAgentResponse;
    }

    /// <summary>
    /// Prepares the workspace agent chat request domain.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    /// <param name="agentId">The agent id.</param>
    /// <param name="requestDTO">The request DTO.</param>
    /// <returns>The populated workspace agent chat request domain.</returns>
    internal static WorkspaceAgentChatRequestDomain PrepareWorkspaceAgentChatRequestDomain(
        IConfiguration configuration,
        string? agentId,
        UserStoryRequestDomain requestDTO
    ) => new()
    {
        AgentId = agentId
            ?? throw new Exception(string.Format(ExceptionConstants.AgentNotFoundMessageConstant, RewriteTextAgent.Id)),
        ApplicationName = configuration[IbbsPluginsWorkspace.WorkspaceName]
            ?? throw new Exception(string.Format(ExceptionConstants.WorkspaceNotFoundMessageConstant, IbbsPluginsWorkspace.WorkspaceName)),
        WorkspaceId = configuration[IbbsPluginsWorkspace.WorkspaceId]
            ?? throw new Exception(string.Format(ExceptionConstants.WorkspaceNotFoundMessageConstant, IbbsPluginsWorkspace.WorkspaceId)),
        ConversationId = Guid.NewGuid().ToString(),
        UserMessage = requestDTO.Story
    };

    /// <summary>
    /// Prepares the workspace agent chat request domain.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    /// <param name="agentId">The agent id.</param>
    /// <param name="requestDTO">The request DTO.</param>
    /// <returns>The populated workspace agent chat request domain.</returns>
    internal static WorkspaceAgentChatRequestDomain PrepareWorkspaceAgentChatRequestDomain(
        IConfiguration configuration,
        string agentId,
        BugSeverityAIRequestDomain requestDTO
    ) => new()
    {
        AgentId = agentId
            ?? throw new Exception(string.Format(ExceptionConstants.AgentNotFoundMessageConstant, GenerateBugSeverityAgent.Id)),
        ApplicationName = configuration[IbbsPluginsWorkspace.WorkspaceName]
            ?? throw new Exception(string.Format(ExceptionConstants.WorkspaceNotFoundMessageConstant, IbbsPluginsWorkspace.WorkspaceName)),
        WorkspaceId = configuration[IbbsPluginsWorkspace.WorkspaceId]
            ?? throw new Exception(string.Format(ExceptionConstants.WorkspaceNotFoundMessageConstant, IbbsPluginsWorkspace.WorkspaceId)),
        ConversationId = Guid.NewGuid().ToString(),
        UserMessage = JsonConvert.SerializeObject(new { Title = requestDTO.BugTitle, Description = requestDTO.BugDescription })
    };

    /// <summary>
    /// Prepares the workspace agent chat request domain.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    /// <param name="requestDTO">The request DTO.</param>
    /// <returns>The populated workspace agent chat request domain.</returns>
    internal static WorkspaceAgentChatRequestDomain PrepareWorkspaceAgentChatRequestDomain(
        IConfiguration configuration,
        UserQueryRequestDomain requestDTO
    ) => new()
    {
        ApplicationName = configuration[IbbsGroupchatWorkspace.WorkspaceName]
            ?? throw new Exception(string.Format(ExceptionConstants.WorkspaceNotFoundMessageConstant, IbbsGroupchatWorkspace.WorkspaceName)),
        WorkspaceId = configuration[IbbsGroupchatWorkspace.WorkspaceId]
            ?? throw new Exception(string.Format(ExceptionConstants.WorkspaceNotFoundMessageConstant, IbbsGroupchatWorkspace.WorkspaceId)),
        ConversationId = Guid.NewGuid().ToString(),
        UserMessage = requestDTO.UserQuery
    };
}
