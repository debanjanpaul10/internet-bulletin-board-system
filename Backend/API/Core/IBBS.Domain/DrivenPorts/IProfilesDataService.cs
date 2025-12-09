using IBBS.Domain.DomainEntities.Posts;

namespace IBBS.Domain.DrivenPorts;

/// <summary>
/// The Profiles Data Service interface.
/// </summary>
public interface IProfilesDataService
{
	/// <summary>
	/// Gets all posts and ratings for user async.
	/// </summary>
	/// <param name="userEmail">The user email.</param>
	/// <returns>The user posts and ratings data.</returns>
	Task<List<PostDomain>> GetUserPostsAsync(string userEmail);

	/// <summary>
	/// Gets user ratings async.
	/// </summary>
	/// <param name="userEmail">The user email.</param>
	/// <returns>The list of user post ratings data</returns>
	Task<List<UserPostRatingDomain>> GetUserRatingsAsync(string userEmail);
}
