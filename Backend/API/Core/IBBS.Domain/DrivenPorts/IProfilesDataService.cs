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
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The user posts and ratings data.</returns>
	Task<IEnumerable<PostDomain>> GetUserPostsAsync(
		string userEmail,
		CancellationToken cancellationToken = default
	);

	/// <summary>
	/// Gets user ratings async.
	/// </summary>
	/// <param name="userEmail">The user email.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The list of user post ratings data</returns>
	Task<IEnumerable<UserPostRatingDomain>> GetUserRatingsAsync(
		string userEmail,
		CancellationToken cancellationToken = default
	);
}
