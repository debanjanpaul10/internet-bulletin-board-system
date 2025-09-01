using IBBS.Domain.DomainEntities.AI;

namespace IBBS.Domain.DrivenPorts;

/// <summary>
/// The AI agents service interface.
/// </summary>
public interface IAiAgentsService
{

	/// <summary>
	/// Rewrites with AI asynchronously.
	/// </summary>
	/// <param name="requestDTO">The story.</param>
	/// <returns>The AI response data</returns>
	Task<string> RewriteWithAIAsync(UserStoryRequestDomain requestDTO);

	/// <summary>
	/// Generates the tag for story asynchronous.
	/// </summary>
	/// <param name="requestDTO">The story.</param>
	/// <returns>The genre tag response.</returns>
	Task<string> GenerateTagForStoryAsync(UserStoryRequestDomain requestDTO);

	/// <summary>
	/// Moderates the content data asynchronous.
	/// </summary>
	/// <param name="requestDTO">The story.</param>
	/// <returns>The moderation content response.</returns>
	Task<string> ModerateContentDataAsync(UserStoryRequestDomain requestDTO);
}
