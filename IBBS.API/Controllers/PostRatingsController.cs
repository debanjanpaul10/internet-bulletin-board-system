using IBBS.API.Adapters.Contracts;
using IBBS.API.Adapters.Models.Posts;
using IBBS.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using static IBBS.API.Helpers.APIConstants;
using static IBBS.API.Helpers.SwaggerConstants.PostRatingsController;

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
	[HttpGet(RouteConstants.PostRatingsController.GetAllUserRatings_Route)]
	[ProducesResponseType(typeof(IEnumerable<PostRatingDTO>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[SwaggerOperation(Summary = GetAllUserRatingsAction.Summary, Description = GetAllUserRatingsAction.Description, OperationId = GetAllUserRatingsAction.OperationId)]
	public async Task<IActionResult> GetAllUserRatingsAsync()
	{
		if (IsAuthorized())
		{
			var result = await postRatingsHandler.GetAllUserPostRatingsAsync(UserEmail).ConfigureAwait(false);
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
	[HttpPost(RouteConstants.PostRatingsController.UpdateRating_Route)]
	[ProducesResponseType(typeof(UpdateRatingDTO), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[SwaggerOperation(Summary = UpdateRatingAction.Summary, Description = UpdateRatingAction.Description, OperationId = UpdateRatingAction.OperationId)]
	public async Task<IActionResult> UpdateRatingAsync(PostRatingDTO postRating)
	{
		if (IsAuthorized())
		{
			var result = await postRatingsHandler.UpdateRatingAsync(postRating, UserEmail).ConfigureAwait(false);
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

