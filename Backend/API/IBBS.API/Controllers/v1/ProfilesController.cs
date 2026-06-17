using IBBS.API.Adapters.Contracts;
using IBBS.API.Adapters.Models.Users;
using IBBS.API.Helpers;
using IBBS.Domain.Contracts;
using IBBS.Domain.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using static IBBS.API.Helpers.APIConstants;
using static IBBS.API.Helpers.SwaggerConstants.ProfilesController;

namespace IBBS.API.Controllers.v1;

/// <summary>
/// The Profiles API Controller.
/// </summary>
/// <param name="httpContextAccessor">The http context accessor.</param>
/// <param name="profilesHandler">The profiles api adapter handler.</param>
/// <param name="configuration">The configuration service.</param>
/// <param name="correlationContext">The correlation context used for logging requests.</param>
/// <param name="logger">The logger service.</param>
/// <seealso cref="BaseController" />
[ApiController]
[Route(RouteConstants.ProfilesController.BaseRoute)]
public sealed class ProfilesController(
    IHttpContextAccessor httpContextAccessor,
    IConfiguration configuration,
    ILogger<ProfilesController> logger,
    ICorrelationContext correlationContext,
    IProfilesHandler profilesHandler) : BaseController(httpContextAccessor, configuration)
{
    /// <summary>
    /// Gets the user profiles data asynchronous.
    /// </summary>
    /// <returns>The user profile dto.</returns>
    [HttpGet(RouteConstants.ProfilesController.GetUserProfileData_Route)]
    [ProducesResponseType(typeof(UserProfileDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(
        Summary = GetUserProfilesDataAction.Summary,
        Description = GetUserProfilesDataAction.Description,
        OperationId = GetUserProfilesDataAction.OperationId)]
    public async Task<IActionResult> GetUserProfilesDataAsync()
    {
        UserProfileDto response = new();
        try
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodStart,
                nameof(GetUserProfilesDataAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, base.UserEmail })
            );

            if (base.IsAuthorized(authorizationTypes: AuthorizationTypes.UserBased))
            {
                response = await profilesHandler.GetUserProfileDataAsync(
                    userEmail: base.UserEmail,
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
                nameof(GetUserProfilesDataAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, base.UserEmail, ex.Message })
            );
            return base.HandleTaskCancelledResponse(message: ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.LogHelperMethodFailed,
                nameof(GetUserProfilesDataAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, base.UserEmail, ex.Message })
            );
            return base.HandleBadRequest(message: ex.Message);
        }
        finally
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodEnded,
                nameof(GetUserProfilesDataAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, base.UserEmail, response })
            );
        }
    }
}
