// *********************************************************************************
//	<copyright file="ProfilesController.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Posts Controller Class.</summary>
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
    /// <param name="profilesService">The Profiles Service.</param>
    /// <param name="logger">The Logger.</param>
    /// <param name="httpContextAccessor">The http context accessor.</param>
    [ApiController]
    [Route(RouteConstants.ProfilesBase_RoutePrefix)]
    public class ProfilesController(IProfilesService profilesService, ILogger<ProfilesController> logger, IHttpContextAccessor httpContextAccessor) : BaseController(httpContextAccessor)
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
        /// Gets the post asynchronous.
        /// </summary>
        /// <param name="postId">The post identifier.</param>
        /// <returns>The action result.</returns>
        [HttpGet]
        [Route(RouteConstants.GetUserProfileData_Route)]
        public async Task<IActionResult> GetUserProfilesDataAsync()
        {
            try
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(GetUserProfilesDataAsync), DateTime.UtcNow, this.UserName));
                if (this.IsAuthorized())
                {
                    var result = await this._profilesService.GetUserProfileDataAsync(this.UserName);
                    if (result is not null && !string.IsNullOrEmpty(result.UserName))
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
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodFailed, nameof(GetUserProfilesDataAsync), DateTime.UtcNow, ex.Message));
                throw;
            }
            finally
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetUserProfilesDataAsync), DateTime.UtcNow, this.UserName));
            }
        }
    }
}


