// *********************************************************************************
//	<copyright file="BulletinServicesController.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Bulletin Services Controller Class.</summary>
// *********************************************************************************

namespace InternetBulletin.API.Controllers
{
    using InternetBulletin.Business.Contracts;
    using InternetBulletin.Shared.Constants;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Bulletin services class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="httpContextAccessor">The http context accessor.</param>
    /// <param name="usersService">The user services.</param>
    /// <seealso cref="BaseController"/>
    [ApiController]
    [Route(RouteConstants.BulletinServicesBase_RoutePrefix)]
    public class BulletinServicesController(
        ILogger<BulletinServicesController> logger, IHttpContextAccessor httpContextAccessor, IUsersService usersService) : BaseController(httpContextAccessor)
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<BulletinServicesController> _logger = logger;

        /// <summary>
        /// The user services.
        /// </summary>
        private readonly IUsersService _usersService = usersService;

        /// <summary>
        /// Gets graph user data async.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <returns>The graph user data dto.</returns>
        [HttpGet]
        [Route(RouteConstants.GetUsersDataFromGraph_Route)]
        public async Task<IActionResult> GetUsersDataFromGraphApiAsync(string userName)
        {
            try
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(GetUsersDataFromGraphApiAsync), DateTime.UtcNow, this.UserName));
                if (this.IsAuthorized())
                {
                    var result = await this._usersService.GetGraphUserDataAsync(userName);
                    if (result is not null)
                    {
                        return this.HandleSuccessResult(result);
                    }
                    else
                    {
                        return this.HandleBadRequest(ExceptionConstants.UserIdNotPresentExceptionConstant);
                    }
                }

                return this.HandleUnAuthorizedRequest();

            }
            catch (Exception ex)
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodFailed, nameof(GetUsersDataFromGraphApiAsync), DateTime.UtcNow, ex.Message));
                throw;
            }
            finally
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetUsersDataFromGraphApiAsync), DateTime.UtcNow, this.UserName));
            }
        }
    }

}

