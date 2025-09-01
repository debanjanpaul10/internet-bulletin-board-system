namespace IBBS.Domain.DrivingPorts;

using IBBS.Domain.DomainEntities.AI;

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
	/// Gets the application information data asynchronously.
	/// </summary>
	/// <returns>The about us details data <see cref="AboutUsAppInfoDataDTO"/></returns>
	Task<AboutUsAppInfoDataDomain> GetAboutUsDataAsync();
}
