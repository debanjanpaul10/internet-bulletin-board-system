using IBBS.API.Adapters.Contracts;
using IBBS.API.Adapters.Models.Posts;
using IBBS.API.Helpers;
using IBBS.Domain.Contracts;
using IBBS.Domain.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using static IBBS.API.Helpers.APIConstants;
using static IBBS.API.Helpers.SwaggerConstants.PostRatingsController;

namespace IBBS.API.Controllers.v1;

/// <summary>
/// The Post Ratings Controller Class.
/// </summary>
/// <seealso cref="BaseController" />
/// <param name="httpContextAccessor">The http context accessor.</param>
/// <param name="postRatingsHandler">The posts ratings service.</param>
/// <param name="configuration">The configuration service.</param>
/// <param name="correlationContext">The correlation context used for logging requests.</param>
/// <param name="logger">The logger service.</param>
/// <seealso cref="BaseController"/>
[ApiController]
[Route(RouteConstants.PostRatingsController.BaseRoute)]
public sealed class PostRatingsController(
    IHttpContextAccessor httpContextAccessor,
    IConfiguration configuration,
    ICorrelationContext correlationContext,
    ILogger<PostRatingsController> logger,
    IPostRatingsHandler postRatingsHandler) : BaseController(httpContextAccessor, configuration)
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
    [SwaggerOperation(
        Summary = GetAllUserRatingsAction.Summary,
        Description = GetAllUserRatingsAction.Description,
        OperationId = GetAllUserRatingsAction.OperationId)]
    public async Task<IActionResult> GetAllUserRatingsAsync()
    {
        IEnumerable<PostRatingDTO> response = [];
        try
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodStart,
                nameof(GetAllUserRatingsAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, base.UserEmail })
            );

            if (base.IsAuthorized(authorizationTypes: AuthorizationTypes.UserBased))
            {
                response = await postRatingsHandler.GetAllUserPostRatingsAsync(
                    userName: base.UserEmail,
                    cancellationToken: HttpContext.RequestAborted
                ).ConfigureAwait(false);

                return base.HandleSuccessResult(response);
            }

            return base.HandleUnAuthorizedRequest();
        }
        catch (TaskCanceledException ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.LogHelperMethodFailed,
                nameof(GetAllUserRatingsAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, base.UserEmail, ex.Message })
            );
            return base.HandleTaskCancelledResponse(message: ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.LogHelperMethodFailed,
                nameof(GetAllUserRatingsAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, base.UserEmail, ex.Message })
            );
            return base.HandleBadRequest(message: ex.Message);
        }
        finally
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodEnded,
                nameof(GetAllUserRatingsAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, base.UserEmail, response })
            );
        }
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
    [SwaggerOperation(
        Summary = UpdateRatingAction.Summary,
        Description = UpdateRatingAction.Description,
        OperationId = UpdateRatingAction.OperationId)]
    public async Task<IActionResult> UpdateRatingAsync(
        PostRatingDTO postRating
    )
    {
        UpdateRatingDTO response = new();
        try
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodStart,
                nameof(UpdateRatingAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, base.UserEmail, postRating })
            );

            ArgumentNullException.ThrowIfNull(postRating);
            if (base.IsAuthorized(AuthorizationTypes.UserBased))
            {
                response = await postRatingsHandler.UpdateRatingAsync(
                    postRating,
                    userName: UserEmail,
                    cancellationToken: HttpContext.RequestAborted
                ).ConfigureAwait(false);

                return base.HandleSuccessResult(response);
            }

            return base.HandleUnAuthorizedRequest();
        }
        catch (TaskCanceledException ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.LogHelperMethodFailed,
                nameof(UpdateRatingAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, base.UserEmail, ex.Message })
            );
            return base.HandleTaskCancelledResponse(message: ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.LogHelperMethodFailed,
                nameof(UpdateRatingAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, base.UserEmail, ex.Message })
            );
            return base.HandleBadRequest(message: ex.Message);
        }
        finally
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodEnded,
                nameof(UpdateRatingAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, base.UserEmail, postRating, response })
            );
        }
    }
}

