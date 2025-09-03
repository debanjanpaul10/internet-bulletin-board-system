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
	/// <returns>The AI response data</returns>
	Task<string> RewriteWithAIAsync(string userName, UserStoryRequestDomain requestDTO);

	/// <summary>
	/// Generates the tag for story asynchronous.
	/// </summary>
	/// <param name="userName">The current user name.</param>
	/// <param name="requestDTO">The story.</param>
	/// <returns>The genre tag response.</returns>
	Task<string> GenerateTagForStoryAsync(string userName, UserStoryRequestDomain requestDTO);

	/// <summary>
	/// Moderates the content data asynchronous.
	/// </summary>
	/// <param name="userName">The current user name.</param>
	/// <param name="requestDTO">The story.</param>
	/// <returns>The moderation content response.</returns>
	Task<string> ModerateContentDataAsync(string userName, UserStoryRequestDomain requestDTO);

	/// <summary>
	/// Gets the chatbot response asynchronous.
	/// </summary>
	/// <param name="userQueryRequest">The user query request.</param>
	/// <param name="areFollowupQuestionsEnabled">The boolean flag for followup questions.</param>
	/// <returns>The ai agent response.</returns>
	Task<AIChatbotResponseDomain> GetChatbotResponseAsync(UserQueryRequestDomain userQueryRequest, bool areFollowupQuestionsEnabled);
}
