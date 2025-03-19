// *********************************************************************************
//	<copyright file="ProfilesController.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Profiles Controller.</summary>
// *********************************************************************************

namespace InternetBulletin.API.Controllers
{
    using InternetBulletin.Business.Contracts;
    using InternetBulletin.Shared.Constants;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The Profiles Controller Class.
    /// </summary>
    /// <seealso cref="InternetBulletin.API.Controllers.BaseController" />
    /// <param name="configuration">The Configuration.</param>
    /// <param name="profilesService">The Profiles Service.</param>
    /// <param name="logger">The Logger</param>
    [ApiController]
    [Route(RouteConstants.ProfilesBase_RoutePrefix)]
    public class ProfilesController(IConfiguration configuration, IProfilesService profilesService, ILogger<ProfilesController> logger) : BaseController(configuration)
    {
        /// <summary>
        /// The profiles service.
        /// </summary>
        private readonly IProfilesService _profilesService = profilesService;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<ProfilesController> _logger = logger;

        /// <summary>
        /// Gets the user profile data.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>The action result of the JSON response.</returns>
        [HttpGet]
        [Route(RouteConstants.GetUserProfileData_Route)]
        public async Task<IActionResult> GetUserProfileDataAsync(int userId)
        {
            try
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(GetUserProfileDataAsync), DateTime.UtcNow, userId));
                if (this.IsAuthorized())
                {
                    var result = await this._profilesService.GetUserProfileDataAsync(userId).ConfigureAwait(false);
                    if (result is not null && result.UserId > 0)
                    {
                        return this.HandleSuccessResult(result);
                    }
                    else
                    {
                        return this.HandleBadRequest(ExceptionConstants.UserDoesNotExistsMessageConstant);
                    }
                }

                return this.HandleUnAuthorizedRequest();
            }
            catch (Exception ex)
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodFailed, nameof(GetUserProfileDataAsync), DateTime.UtcNow, ex.Message));
                return this.HandleBadRequest(ex.Message);
            }
            finally
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetUserProfileDataAsync), DateTime.UtcNow, userId));
            }
        }
    }
}
