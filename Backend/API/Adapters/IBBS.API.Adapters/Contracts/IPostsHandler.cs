using IBBS.API.Adapters.Models.Posts;

namespace IBBS.API.Adapters.Contracts;

/// <summary>
/// The posts api adapter handler interface.
/// </summary>
public interface IPostsHandler
{

    Task<PostDTO> GetPostAsync(
        string postId,
        string userName,
        CancellationToken cancellationToken = default
    );


    Task<bool> AddNewPostAsync(
        AddPostDTO newPost,
        string userName,
        CancellationToken cancellationToken = default
    );


    Task<PostDTO> UpdatePostAsync(
        UpdatePostDTO updatedPost,
        string userName,
        CancellationToken cancellationToken = default
    );

    Task<bool> DeletePostAsync(
        string postId,
        string userName,
        CancellationToken cancellationToken = default
    );

    Task<IEnumerable<PostWithRatingsDTO>> GetAllPostsAsync(
        string userName,
        CancellationToken cancellationToken = default
    );
}
