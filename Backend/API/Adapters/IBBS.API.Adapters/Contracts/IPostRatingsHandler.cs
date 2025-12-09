using IBBS.API.Adapters.Models.Posts;

namespace IBBS.API.Adapters.Contracts;

/// <summary>
/// The post ratings handler interface.
/// </summary>
public interface IPostRatingsHandler
{
	/// <summary>
	/// Updates the rating asynchronous.
	/// </summary>
	/// <param name="postRating">The post rating.</param>
	/// <param name="userName">Name of the user.</param>
	/// <returns>The update rating dto object.</returns>
	Task<UpdateRatingDTO> UpdateRatingAsync(PostRatingDTO postRating, string userName);

	/// <summary>
	/// Gets all user post ratings async.
	/// </summary>
	/// <param name="userName">The user name.</param>
	/// <returns>The list of post ratings</returns>
	Task<IEnumerable<PostRatingDTO>> GetAllUserPostRatingsAsync(string userName);
}
