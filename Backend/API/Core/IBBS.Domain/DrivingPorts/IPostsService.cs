namespace IBBS.Domain.DrivingPorts;

using IBBS.Domain.DomainEntities.Posts;

/// <summary>
/// The Posts BusinessManager Interface Class.
/// </summary>
public interface IPostsService
{
    /// <summary>
    /// Gets the post asynchronous.
    /// </summary>
    /// <param name="postId">The post identifier.</param>
    /// <param name="userName">Name of the user.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The post domain object.</returns>
    Task<PostDomain> GetPostAsync(
        string postId,
        string userName,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Adds the new post asynchronous.
    /// </summary>
    /// <param name="newPost">The new post.</param>
    /// <param name="userName">Name of the user.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A boolean indicating whether the post was added successfully.</returns>
    Task<bool> AddNewPostAsync(
        AddPostDomain newPost,
        string userName,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Updates the post asynchronous.
    /// </summary>
    /// <param name="updatedPost">The updated post.</param>
    /// <param name="userName">Name of the user.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated post domain object.</returns>
    Task<PostDomain> UpdatePostAsync(
        UpdatePostDomain updatedPost,
        string userName,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Deletes the post asynchronous.
    /// </summary>
    /// <param name="postId">The post identifier.</param>
    /// <param name="userName">Name of the user.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A boolean indicating whether the post was deleted successfully.</returns>
    Task<bool> DeletePostAsync(
        string postId,
        string userName,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Gets all posts asynchronous.
    /// </summary>
    /// <param name="userName">Name of the user.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The list of posts with ratings.</returns>
    Task<List<PostWithRatingsDomain>> GetAllPostsAsync(
        string userName,
        CancellationToken cancellationToken = default
    );
}
