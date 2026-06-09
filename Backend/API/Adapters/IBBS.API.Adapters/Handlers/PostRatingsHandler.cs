using AutoMapper;
using IBBS.API.Adapters.Contracts;
using IBBS.API.Adapters.Models.Posts;
using IBBS.Domain.DomainEntities.Posts;
using IBBS.Domain.DrivingPorts;

namespace IBBS.API.Adapters.Handlers;

/// <summary>
/// The posts ratings handler.
/// </summary>
/// <param name="mapper">The auto mapper service.</param>
/// <param name="postRatingsService">The post ratings service.</param>
/// <seealso cref="IBBS.API.Adapters.Contracts.IPostRatingsHandler" />
public sealed class PostRatingsHandler(
    IMapper mapper,
    IPostRatingsService postRatingsService) : IPostRatingsHandler
{
    /// <inheritdoc />
    public async Task<IEnumerable<PostRatingDTO>> GetAllUserPostRatingsAsync(
        string userName,
        CancellationToken cancellationToken = default
    )
    {
        var domainResult = await postRatingsService.GetAllUserPostRatingsAsync(
            userName,
            cancellationToken
        ).ConfigureAwait(false);

        return mapper.Map<IEnumerable<PostRatingDTO>>(domainResult);
    }

    /// <inheritdoc />
    public async Task<UpdateRatingDTO> UpdateRatingAsync(
        PostRatingDTO postRating,
        string userName,
        CancellationToken cancellationToken = default
    )
    {
        var domainInput = mapper.Map<PostRatingDomain>(postRating);
        var domainResult = await postRatingsService.UpdateRatingAsync(
            postRating: domainInput,
            userName,
            cancellationToken
        ).ConfigureAwait(false);

        return mapper.Map<UpdateRatingDTO>(domainResult);
    }
}
