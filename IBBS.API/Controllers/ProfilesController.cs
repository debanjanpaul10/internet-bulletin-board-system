using IBBS.API.Adapters.Contracts;
using IBBS.API.Adapters.Models.Users;
using IBBS.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using static IBBS.API.Helpers.APIConstants;
using static IBBS.API.Helpers.SwaggerConstants.ProfilesController;

namespace IBBS.API.Controllers;

/// <summary>
/// The Profiles API Controller.
/// </summary>
/// <param name="httpContextAccessor">The http context accessor.</param>
/// <param name="logger">The logger service.</param>
/// <param name="profilesHandler">The profiles api adapter handler.</param>
/// <seealso cref="IBBS.API.Controllers.BaseController" />
[ApiController]
[Route(RouteConstants.ProfilesController.BaseRoute)]
public class ProfilesController(ILogger<ProfilesController> logger, IHttpContextAccessor httpContextAccessor, IProfilesHandler profilesHandler) : BaseController(httpContextAccessor)
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
		try
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(GetUserProfilesDataAsync), DateTime.UtcNow, this.UserEmail));
			if (this.IsAuthorized())
			{
				var result = await profilesHandler.GetUserProfileDataAsync(this.UserEmail).ConfigureAwait(false);
				if (result is not null && !string.IsNullOrEmpty(result.EmailAddress))
				{
					return this.HandleSuccessResult(result);
				}
				else
				{
					return this.HandleBadRequest(ExceptionConstants.PostsNotPresentMessageConstant);
				}
			}

			return this.HandleUnAuthorizedRequest();

		}
		catch (Exception ex)
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodFailed, nameof(GetUserProfilesDataAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetUserProfilesDataAsync), DateTime.UtcNow, this.UserEmail));
		}
	}
}
