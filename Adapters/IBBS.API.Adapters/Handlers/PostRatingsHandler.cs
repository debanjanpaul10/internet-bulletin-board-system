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
public class PostRatingsHandler(IMapper mapper, IPostRatingsService postRatingsService) : IPostRatingsHandler
{
	/// <summary>
	/// Gets all user post ratings async.
	/// </summary>
	/// <param name="userName">The user name.</param>
	/// <returns>
	/// The list of post ratings
	/// </returns>
	public async Task<IEnumerable<PostRatingDTO>> GetAllUserPostRatingsAsync(string userName)
	{
		var domainResult = await postRatingsService.GetAllUserPostRatingsAsync(userName).ConfigureAwait(false);
		return mapper.Map<IEnumerable<PostRatingDTO>>(domainResult);
	}

	/// <summary>
	/// Updates the rating asynchronous.
	/// </summary>
	/// <param name="postRating">The post rating.</param>
	/// <param name="userName">Name of the user.</param>
	/// <returns>
	/// The update rating dto object.
	/// </returns>
	public async Task<UpdateRatingDTO> UpdateRatingAsync(PostRatingDTO postRating, string userName)
	{
		var domainInput = mapper.Map<PostRatingDomain>(postRating);
		var domainResult = await postRatingsService.UpdateRatingAsync(domainInput, userName).ConfigureAwait(false);
		return mapper.Map<UpdateRatingDTO>(domainResult);
	}
}
