using IBBS.API.Adapters.Contracts;
using IBBS.API.Adapters.Models.Posts;
using IBBS.Domain.DrivingPorts;
using static IBBS.API.Adapters.Mapping.DomainToResponseMapper;
using static IBBS.API.Adapters.Mapping.RequestToDomainMapper;

namespace IBBS.API.Adapters.Handlers;

/// <summary>
/// The posts ratings handler.
/// </summary>
/// <param name="postRatingsService">The post ratings service.</param>
/// <seealso cref="IPostRatingsHandler" />
public sealed class PostRatingsHandler(IPostRatingsService postRatingsService) : IPostRatingsHandler
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
        return [.. domainResult.Select(MapToResponse)];
    }

    /// <inheritdoc />
    public async Task<UpdateRatingDTO> UpdateRatingAsync(
        PostRatingDTO postRating,
        string userName,
        CancellationToken cancellationToken = default
    )
    {
        var domainInput = MapToDomain(requestDto: postRating);
        var domainResult = await postRatingsService.UpdateRatingAsync(
            postRating: domainInput,
            userName,
            cancellationToken
        ).ConfigureAwait(false);
        return MapToResponse(domain: domainResult);
    }
}
