
namespace IBBS.Domain.DrivenPorts;

using IBBS.Domain.DomainEntities.Posts;

/// <summary>
/// The Posts DataManager Interface Class.
/// </summary>
public interface IPostsDataService
{
    /// <summary>
    /// Gets the post asynchronous.
    /// </summary>
    /// <param name="postId">The post identifier.</param>
    /// <param name="userName">Name of the user.</param>
    /// <param name="isForCurrentUser">if set to <c>true</c> [is for current user].</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The post domain.</returns>
    Task<PostDomain> GetPostAsync(
        Guid postId,
        string userName,
        bool isForCurrentUser,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Adds the new post asynchronous.
    /// </summary>
    /// <param name="newPost">The new post.</param>
    /// <param name="userName">Name of the user.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A value indicating whether the post was added successfully.</returns>
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
    /// <param name="isRatingUpdate">if set to <c>true</c> [is rating update].</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated post domain.</returns>
    Task<PostDomain> UpdatePostAsync(
        UpdatePostDomain updatedPost,
        string userName,
        bool isRatingUpdate,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Deletes the post asynchronous.
    /// </summary>
    /// <param name="postId">The post identifier.</param>
    /// <param name="userName">Name of the user.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A value indicating whether the post was deleted successfully.</returns>
    Task<bool> DeletePostAsync(
        Guid postId,
        string userName,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Gets all posts asynchronous.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The list of post domains.</returns>
    Task<List<PostDomain>> GetAllPostsAsync(
        CancellationToken cancellationToken = default
    );
}
