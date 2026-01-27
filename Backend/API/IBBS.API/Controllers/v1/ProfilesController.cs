using IBBS.API.Adapters.Contracts;
using IBBS.API.Adapters.Models.Users;
using IBBS.API.Helpers;
using Microsoft.AspNetCore.Mvc;
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
/// <seealso cref="BaseController" />
[ApiController]
[Route(RouteConstants.ProfilesController.BaseRoute)]
public sealed class ProfilesController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IProfilesHandler profilesHandler) : BaseController(httpContextAccessor, configuration)
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
    [SwaggerOperation(Summary = GetUserProfilesDataAction.Summary, Description = GetUserProfilesDataAction.Description, OperationId = GetUserProfilesDataAction.OperationId)]
    public async Task<IActionResult> GetUserProfilesDataAsync()
    {
        if (base.IsAuthorized(AuthorizationTypes.UserBased))
        {
            var result = await profilesHandler.GetUserProfileDataAsync(this.UserEmail).ConfigureAwait(false);
            if (result is not null && !string.IsNullOrEmpty(result.EmailAddress)) return this.HandleSuccessResult(result);
            else return this.HandleBadRequest(ExceptionConstants.PostsNotPresentMessageConstant);
        }

        return this.HandleUnAuthorizedRequest();
    }
}
