using IBBS.API.Adapters.Contracts;
using IBBS.API.Adapters.Models.Posts;
using IBBS.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using static IBBS.API.Helpers.APIConstants;

namespace IBBS.API.Controllers;

/// <summary>
/// The PostDomain Ratings Controller Class.
/// </summary>
/// <seealso cref="BaseController" />
/// <param name="httpContextAccessor">The http context accessor.</param>
/// <param name="postRatingsHandler">The posts ratings service.</param>
[ApiController]
[Route(RouteConstants.PostRatingsController.BaseRoute)]
public class PostRatingsController(IHttpContextAccessor httpContextAccessor, IPostRatingsHandler postRatingsHandler) : BaseController(httpContextAccessor)
{
	/// <summary>
	/// Gets all the user ratings async
	/// </summary>
	/// <returns>The action result of the ratings data</returns>
	[HttpGet]
	[Route(RouteConstants.PostRatingsController.GetAllUserRatings_Route)]
	public async Task<IActionResult> GetAllUserRatingsAsync()
	{
		if (IsAuthorized())
		{
			var result = await postRatingsHandler.GetAllUserPostRatingsAsync(UserName).ConfigureAwait(false);
			if (result is not null)
			{
				return HandleSuccessResult(result);
			}
			else
			{
				return this.HandleBadRequest(ExceptionConstants.UnableToGetUserPostRatingsMessageConstant);
			}
		}

		return HandleUnAuthorizedRequest();
	}

	/// <summary>
	/// Updates the rating of the post asynchronously.
	/// </summary>
	/// <param name="postRating">The post rating.</param>
	/// <returns>The action result.</returns>
	[HttpPost]
	[Route(RouteConstants.PostRatingsController.UpdateRating_Route)]
	public async Task<IActionResult> UpdateRatingAsync(PostRatingDTO postRating)
	{
		if (IsAuthorized())
		{
			var result = await postRatingsHandler.UpdateRatingAsync(postRating, UserName).ConfigureAwait(false);
			if (result is not null)
			{
				return this.HandleSuccessResult(result);
			}
			else
			{
				return this.HandleBadRequest(ExceptionConstants.PostGuidNotValidMessageConstant);
			}
		}

		return HandleUnAuthorizedRequest();
	}
}

