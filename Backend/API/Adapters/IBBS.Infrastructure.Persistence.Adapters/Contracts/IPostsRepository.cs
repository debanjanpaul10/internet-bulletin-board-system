using IBBS.Domain.DomainEntities.Posts;
using IBBS.Infrastructure.Persistence.Adapters.Models;

namespace IBBS.Infrastructure.Persistence.Adapters.Contracts;

/// <summary>
/// The posts repository interface.
/// </summary>
/// <remarks>This interface defines methods for getting posts and updating posts from the database.</remarks>
public interface IPostsRepository
{
    /// <summary>
    /// Gets the post asynchronous.
    /// </summary>
    /// <param name="postId">The post identifier.</param>
    /// <param name="userName">Name of the user.</param>
    /// <param name="isForCurrentUser">if set to <c>true</c> [is for current user].</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The post domain.</returns>
    Task<PostEntity> GetPostAsync(
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
        PostEntity newPost,
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
    Task<PostEntity> UpdatePostAsync(
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
    Task<List<PostEntity>> GetAllPostsAsync(
        CancellationToken cancellationToken = default
    );
}