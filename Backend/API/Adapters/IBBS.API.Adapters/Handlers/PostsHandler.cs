using AutoMapper;
using IBBS.API.Adapters.Contracts;
using IBBS.API.Adapters.Models.Posts;
using IBBS.Domain.DomainEntities.Posts;
using IBBS.Domain.DrivingPorts;

namespace IBBS.API.Adapters.Handlers;

/// <summary>
/// The posts api adapter handler.
/// </summary>
/// <param name="mapper">The auto mapper.</param>
/// <param name="postsService">The posts service.</param>
/// <seealso cref="IBBS.API.Adapters.Contracts.IPostsHandler" />
public sealed class PostsHandler(
    IPostsService postsService,
    IMapper mapper) : IPostsHandler
{
    /// <inheritdoc />
    public async Task<bool> AddNewPostAsync(
        AddPostDTO newPost,
        string userName,
        CancellationToken cancellationToken = default
    )
    {
        var domainInput = mapper.Map<AddPostDomain>(newPost);
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
        return mapper.Map<IEnumerable<PostWithRatingsDTO>>(domainResult);
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
        return mapper.Map<PostDTO>(domainResult);
    }

    /// <inheritdoc />
    public async Task<PostDTO> UpdatePostAsync(
        UpdatePostDTO updatedPost,
        string userName,
        CancellationToken cancellationToken = default
    )
    {
        var domainInput = mapper.Map<UpdatePostDomain>(updatedPost);
        var domainResult = await postsService.UpdatePostAsync(
            updatedPost: domainInput,
            userName,
            cancellationToken
        ).ConfigureAwait(false);
        return mapper.Map<PostDTO>(domainResult);
    }
}
