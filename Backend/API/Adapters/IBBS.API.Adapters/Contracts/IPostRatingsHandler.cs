using IBBS.API.Adapters.Models.Posts;

namespace IBBS.API.Adapters.Contracts;

/// <summary>
/// The post ratings handler interface.
/// </summary>
public interface IPostRatingsHandler
{
    /// <summary>
    /// Updates the rating asynchronous.
    /// </summary>
    /// <param name="postRating">The post rating.</param>
    /// <param name="userName">Name of the user.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated rating.</returns>
    Task<UpdateRatingDTO> UpdateRatingAsync(
        PostRatingDTO postRating,
        string userName,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Gets all user post ratings asynchronous.
    /// </summary>
    /// <param name="userName">Name of the user.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The collection of post ratings.</returns>
    Task<IEnumerable<PostRatingDTO>> GetAllUserPostRatingsAsync(
        string userName,
        CancellationToken cancellationToken = default
    );
}
