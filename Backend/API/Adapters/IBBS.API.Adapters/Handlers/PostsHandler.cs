using IBBS.API.Adapters.Contracts;
using IBBS.API.Adapters.Models.Posts;
using IBBS.Domain.DrivingPorts;
using static IBBS.API.Adapters.Mapping.DomainToResponseMapper;
using static IBBS.API.Adapters.Mapping.RequestToDomainMapper;

namespace IBBS.API.Adapters.Handlers;

/// <summary>
/// The posts api adapter handler.
/// </summary>
/// <param name="postsService">The posts service.</param>
/// <seealso cref="IPostsHandler" />
public sealed class PostsHandler(IPostsService postsService) : IPostsHandler
{
    /// <inheritdoc />
    public async Task<bool> AddNewPostAsync(
        AddPostDTO newPost,
        string userName,
        CancellationToken cancellationToken = default
    )
    {
        var domainInput = MapToDomain(requestDto: newPost);
        return await postsService.AddNewPostAsync(
            newPost: domainInput,
            userName,
            cancellationToken
        ).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<bool> DeletePostAsync(
        string postId,
        string userName,
        CancellationToken cancellationToken = default
    ) =>
        await postsService.DeletePostAsync(
            postId,
            userName,
            cancellationToken
        ).ConfigureAwait(false);


    /// <inheritdoc />
    public async Task<IEnumerable<PostWithRatingsDTO>> GetAllPostsAsync(
        string userName,
        CancellationToken cancellationToken = default
    )
    {
        var domainResult = await postsService.GetAllPostsAsync(
            userName,
            cancellationToken
        ).ConfigureAwait(false);
        return [.. domainResult.Select(MapToResponse)];
    }

    /// <inheritdoc />
    public async Task<PostDTO> GetPostAsync(
        string postId,
        string userName,
        CancellationToken cancellationToken = default
    )
    {
        var domainResult = await postsService.GetPostAsync(
            postId,
            userName,
            cancellationToken
        ).ConfigureAwait(false);
        return MapToResponse(domain: domainResult);
    }

    /// <inheritdoc />
    public async Task<PostDTO> UpdatePostAsync(
        UpdatePostDTO updatedPost,
        string userName,
        CancellationToken cancellationToken = default
    )
    {
        var domainInput = MapToDomain(requestDto: updatedPost);
        var domainResult = await postsService.UpdatePostAsync(
            updatedPost: domainInput,
            userName,
            cancellationToken
        ).ConfigureAwait(false);
        return MapToResponse(domain: domainResult);
    }
}
