using IBBS.Infrastructure.Persistence.Adapters.Models;

namespace IBBS.Infrastructure.Persistence.Adapters.Contracts;

/// <summary>
/// The profiles repository interface.
/// </summary>
/// <remarks>This interface defines methods for getting user posts and ratings from the database.</remarks>
public interface IProfilesRepository
{
    /// <summary>
	/// Gets all posts and ratings for user async.
	/// </summary>
	/// <param name="userEmail">The user email.</param>
	/// <returns>The user posts and ratings data.</returns>
	Task<IEnumerable<PostEntity>> GetUserPostsAsync(
        string userEmail,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Gets user ratings async.
    /// </summary>
    /// <param name="userEmail">The user email.</param>
    /// <returns>The list of user post ratings data</returns>
    Task<IEnumerable<UserPostRating>> GetUserRatingsAsync(
        string userEmail,
        CancellationToken cancellationToken = default
    );
}