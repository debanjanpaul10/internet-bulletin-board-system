using IBBS.Domain.DomainEntities;
using IBBS.Domain.DomainEntities.AI;

namespace IBBS.Domain.DrivingPorts;

/// <summary>
/// AI Services interface.
/// </summary>
public interface IAIService
{
    /// <summary>
    /// Rewrites with AI asynchronously.
    /// </summary>
    /// <param name="userName">The current user name.</param>
    /// <param name="requestDTO">The story.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The AI response data</returns>
    Task<string> RewriteWithAIAsync(
        string userName,
        UserStoryRequestDomain requestDTO,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Generates the tag for story asynchronous.
    /// </summary>
    /// <param name="userName">The current user name.</param>
    /// <param name="requestDTO">The story.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The genre tag response.</returns>
    Task<string> GenerateTagForStoryAsync(
        string userName,
        UserStoryRequestDomain requestDTO,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Moderates the content data asynchronous.
    /// </summary>
    /// <param name="userName">The current user name.</param>
    /// <param name="requestDTO">The story.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The moderation content response.</returns>
    Task<string> ModerateContentDataAsync(
        string userName,
        UserStoryRequestDomain requestDTO,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Posts the ai result feedback asynchronous.
    /// </summary>
    /// <param name="aiResponseFeedback">The ai response feedback.</param>
    /// <param name="userEmail">The user email address.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The boolean for success/failure.</returns>
    Task<bool> PostAiResultFeedbackAsync(
        AIResponseFeedbackDomain aiResponseFeedback,
        string userEmail,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Gets the sample prompts for chatbot asynchronous.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token used to cancel the operation.</param>
    /// <returns>The list of <see cref="LookupMasterDomain"/></returns>
    Task<IEnumerable<LookupMasterDomain>> GetSamplePromptsForChatbotAsync(
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Generate the bug severity using LLM.
    /// </summary>
    /// <param name="bugSeverityAiRequest">The bug severity AI request model.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The bug severity.</returns>
    Task<string> GenerateBugSeverityAsync(
        BugSeverityAIRequestDomain bugSeverityAiRequest,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Gets the chatbot response using LLM.
    /// </summary>
    /// <param name="userQueryRequest">The user query request domain model.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The AI response string.</returns>
    Task<string> GetChatbotResponseAsync(
        UserQueryRequestDomain userQueryRequest,
        CancellationToken cancellationToken = default
    );
}
