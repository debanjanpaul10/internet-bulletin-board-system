using IBBS.API.Adapters.Models;
using IBBS.API.Adapters.Models.AI;

namespace IBBS.API.Adapters.Contracts;

/// <summary>
/// The AI Services Handler interface.
/// </summary>
public interface IAiServicesHandler
{
    /// <summary>
    /// Rewrites with AI asynchronously.
    /// </summary>
    /// <param name="userName">The current user name.</param>
    /// <param name="requestDTO">The story.</param>
    /// <param name="cancellationToken">A cancellation token used to cancel the operation.</param>
    /// <returns>The AI response data</returns>
    Task<string> RewriteWithAIAsync(
        string userName,
        UserStoryRequestDTO requestDTO,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Generates the tag for story asynchronous.
    /// </summary>
    /// <param name="userName">The current user name.</param>
    /// <param name="requestDTO">The story.</param>
    /// <param name="cancellationToken">A cancellation token used to cancel the operation.</param>
    /// <returns>The genre tag response.</returns>
    Task<string> GenerateTagForStoryAsync(
        string userName,
        UserStoryRequestDTO requestDTO,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Moderates the content data asynchronous.
    /// </summary>
    /// <param name="userName">The current user name.</param>
    /// <param name="requestDTO">The story.</param>
    /// <param name="cancellationToken">A cancellation token used to cancel the operation.</param>
    /// <returns>The moderation content response.</returns>
    Task<string> ModerateContentDataAsync(
        string userName,
        UserStoryRequestDTO requestDTO,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Posts the ai result feedback asynchronous.
    /// </summary>
    /// <param name="aiResponseFeedback">The ai response feedback.</param>
    /// <param name="userEmail">The user email.</param>
    /// <param name="cancellationToken">A cancellation token used to cancel the operation.</param>
    /// <returns>The boolean for success/failure.</returns>
    Task<bool> PostAiResultFeedbackAsync(
        AIResponseFeedbackDTO aiResponseFeedback,
        string userEmail,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Gets the sample prompts for chatbot asynchronous.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token used to cancel the operation.</param>
    /// <returns>The list of <see cref="LookupMasterDTO"/></returns>
    Task<IEnumerable<LookupMasterDTO>> GetSamplePromptsForChatbotAsync(
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Generate the bug severity using LLM.
    /// </summary>
    /// <param name="bugSeverityAiRequest">The bug severity AI request model.</param>
    /// <param name="cancellationToken">A cancellation token used to cancel the operation.</param>
    /// <returns>The bug severity.</returns>
    Task<string> GenerateBugSeverityAsync(
        BugSeverityAIRequestDTO bugSeverityAiRequest,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Gets the chatbot response using LLM.
    /// </summary>
    /// <param name="userQueryRequest">The user query request dto model.</param>
    /// <param name="cancellationToken">A cancellation token used to cancel the operation.</param>
    /// <returns>The AI response string.</returns>
    Task<string> GetChatbotResponseAsync(
        UserQueryRequestDTO userQueryRequest,
        CancellationToken cancellationToken = default
    );
}
