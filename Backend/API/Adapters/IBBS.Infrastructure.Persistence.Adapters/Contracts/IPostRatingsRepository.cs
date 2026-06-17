using IBBS.Infrastructure.Persistence.Adapters.Models;

namespace IBBS.Infrastructure.Persistence.Adapters.Contracts;

/// <summary>
/// The post ratings repository interface.
/// </summary>
/// <remarks>This interface defines methods for getting post ratings from the database.</remarks>
public interface IPostRatingsRepository
{
    /// <summary>
	/// Gets post rating async.
	/// </summary>
	/// <param name="postId">The post id.</param>
	/// <param name="userName">The user name.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The post rating data</returns>
	Task<PostRatingEntity> GetPostRatingAsync(
        Guid postId,
        string userName,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Adds a new post rating async.
    /// </summary>
    /// <param name="postRating">The post rating data dto.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A boolean value for success/failure.</returns>
    Task<bool> AddPostRatingAsync(
        PostRatingEntity postRating,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Updates an existing rating async.
    /// </summary>
    /// <param name="postRating">The post rating.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A boolean value for success/failure.</returns>
    Task<bool> UpdatePostRatingAsync(
        PostRatingEntity postRating,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Gets all user post ratings async.
    /// </summary>
    /// <param name="userName">The user name.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The list of post ratings</returns>
    Task<List<PostRatingEntity>> GetAllUserPostRatingsAsync(
        string userName,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Gets all posts with ratings async.
    /// </summary>
    /// <param name="userName">The user name.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The post with ratings dto.</returns>
    Task<List<PostWithRatings>> GetAllPostsWithRatingsAsync(
        string userName,
        CancellationToken cancellationToken = default
    );
}