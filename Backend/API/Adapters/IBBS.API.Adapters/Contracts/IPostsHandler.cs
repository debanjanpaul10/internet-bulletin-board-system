using IBBS.API.Adapters.Models.Posts;

namespace IBBS.API.Adapters.Contracts;

/// <summary>
/// The posts api adapter handler interface.
/// </summary>
public interface IPostsHandler
{

    /// <summary>
    /// Gets the post asynchronous.
    /// </summary>
    /// <param name="postId">The post identifier.</param>
    /// <param name="userName">The user name.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The post data response dto.</returns>
    Task<PostDTO> GetPostAsync(
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
    /// <returns>A boolean for success/failure.</returns>
    Task<bool> AddNewPostAsync(
        AddPostDTO newPost,
        string userName,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Updates the post asynchronous.
    /// </summary>
    /// <param name="updatedPost">The updated post.</param>
    /// <param name="userName">Name of the user.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated post data response dto.</returns>
    Task<PostDTO> UpdatePostAsync(
        UpdatePostDTO updatedPost,
        string userName,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Deletes the post asynchronous.
    /// </summary>
    /// <param name="postId">The post identifier.</param>
    /// <param name="userName">Name of the user.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A boolean for success/failure.</returns>
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
    /// <returns>The list of <see cref="PostWithRatingsDTO"/></returns>
    Task<IEnumerable<PostWithRatingsDTO>> GetAllPostsAsync(
        string userName,
        CancellationToken cancellationToken = default
    );
}
