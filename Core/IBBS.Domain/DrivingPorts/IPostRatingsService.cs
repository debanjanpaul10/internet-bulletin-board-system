namespace IBBS.Domain.DrivingPorts;

using IBBS.Domain.DomainEntities;

/// <summary>
/// Post ratings service interface.
/// </summary>
public interface IPostRatingsService
{
	/// <summary>
	/// Updates the rating asynchronous.
	/// </summary>
	/// <param name="postRating">The post rating.</param>
	/// <param name="userName">The user name.</param>
	/// <returns>The updated rating domain data.</returns>
	Task<UpdateRatingDomain> UpdateRatingAsync(PostRatingDomain postRating, string userName);

	/// <summary>
	/// Gets all user post ratings async.
	/// </summary>
	/// <param name="userName">The user name.</param>
	/// <returns>The list of post ratings</returns>
	Task<List<PostRatingDomain>> GetAllUserPostRatingsAsync(string userName);
}
