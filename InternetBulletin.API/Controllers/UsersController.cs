// *********************************************************************************
//	<copyright file="UsersController.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Users Controller Class.</summary>
// *********************************************************************************

namespace InternetBulletin.API.Controllers
{
    using InternetBulletin.Business.Contracts;
    using InternetBulletin.Data.Entities;
    using InternetBulletin.Shared.Constants;
    using InternetBulletin.Shared.DTOs;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The Users Controller Class.
    /// </summary>
    /// <seealso cref="InternetBulletin.API.Controllers.BaseController" />
    /// <param name="configuration">The Configuration.</param>
    /// <param name="usersService">The Users Service.</param>
    /// <param name="logger">The Logger</param>
    [ApiController]
    [Route(RouteConstants.UsersBase_RoutePrefx)]
    public class UsersController(IConfiguration configuration, IUsersService usersService, ILogger<UsersController> logger) : BaseController(configuration)
    {
        /// <summary>
        /// The users service
        /// </summary>
        private readonly IUsersService _usersService = usersService;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<UsersController> _logger = logger;

        /// <summary>
        /// Logs in for the passed user data
        /// </summary>
        /// <param name="userLogin">The user identifiers.</param>
        /// <returns>The action result of the JSON response.</returns>
        [HttpPost]
        [Route(RouteConstants.GetUser_Route)]
        public async Task<IActionResult> GetUserAsync(UserLoginDTO userLogin)
        {
            try
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(GetUserAsync), DateTime.UtcNow, userLogin.UserEmail));
                if (this.IsAuthorized())
                {
                    var result = await this._usersService.GetUserAsync(userLogin);
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
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodFailed, nameof(GetUserAsync), DateTime.UtcNow, ex.Message));
                return this.HandleBadRequest(ex.Message);
            }
            finally
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetUserAsync), DateTime.UtcNow, userLogin.UserEmail));
            }
        }

        /// <summary>
        /// Gets all users asynchronous.
        /// </summary>
        /// <returns>The action result of the JSON response.</returns>
        [HttpGet]
        [Route(RouteConstants.GetAllUsers_Route)]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            try
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(GetAllUsersAsync), DateTime.UtcNow, string.Empty));
                if (this.IsAuthorized())
                {
                    var result = await this._usersService.GetAllUsersAsync();
                    if (result is not null && result.Count > 0)
                    {
                        return this.HandleSuccessResult(result);
                    }
                    else
                    {
                        return this.HandleBadRequest(ExceptionConstants.UsersNotPresentMessageConstants);
                    }
                }

                return this.HandleUnAuthorizedRequest();
            }
            catch (Exception ex)
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodFailed, nameof(GetAllUsersAsync), DateTime.UtcNow, ex.Message));
                return this.HandleBadRequest(ex.Message);
            }
            finally
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetAllUsersAsync), DateTime.UtcNow, string.Empty));
            }
        }

        /// <summary>
        /// Adds the new user asynchronous.
        /// </summary>
        /// <param name="newUser">The new user.</param>
        /// <returns>The action result of the JSON response.</returns>
        [HttpPost]
        [Route(RouteConstants.NewUser_Route)]
        public async Task<IActionResult> AddNewUserAsync(NewUserDTO newUser)
        {
            try
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(AddNewUserAsync), DateTime.UtcNow, newUser.UserAlias));
                if (this.IsAuthorized())
                {
                    var result = await this._usersService.AddNewUserAsync(newUser);
                    if (result)
                    {
                        return this.HandleSuccessResult(result);
                    }
                    else
                    {
                        return this.HandleBadRequest(ExceptionConstants.SomethingWentWrongMessageConstant);
                    }
                }

                return this.HandleUnAuthorizedRequest();
            }
            catch (Exception ex)
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodFailed, nameof(AddNewUserAsync), DateTime.UtcNow, ex.Message));
                return this.HandleBadRequest(ex.Message);
            }
            finally
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(AddNewUserAsync), DateTime.UtcNow, newUser.UserAlias));
            }
        }

        /// <summary>
        /// Updates the user asynchronous.
        /// </summary>
        /// <param name="updateUser">The update user.</param>
        /// <returns>The action result of the JSON response.</returns>
        [HttpPost]
        [Route(RouteConstants.UpdateUser_Route)]
        public async Task<IActionResult> UpdateUserAsync(User updateUser)
        {
            try
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(UpdateUserAsync), DateTime.UtcNow, updateUser.UserAlias));
                if (this.IsAuthorized())
                {
                    var result = await this._usersService.UpdateUserAsync(updateUser);
                    if (result is not null && result.UserId > 0)
                    {
                        return this.HandleSuccessResult(result);
                    }
                    else
                    {
                        return this.HandleBadRequest(ExceptionConstants.SomethingWentWrongMessageConstant);
                    }
                }

                return this.HandleUnAuthorizedRequest();
            }
            catch (Exception ex)
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodFailed, nameof(UpdateUserAsync), DateTime.UtcNow, ex.Message));
                return this.HandleBadRequest(ex.Message);
            }
            finally
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(UpdateUserAsync), DateTime.UtcNow, updateUser.UserAlias));
            }
        }

        /// <summary>
        /// Deletes the user asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The action result of the JSON response.</returns>
        [HttpPost]
        [Route(RouteConstants.DeleteUser_Route)]
        public async Task<IActionResult> DeleteUserAsync(int userId)
        {
            try
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(DeleteUserAsync), DateTime.UtcNow, userId));
                if (this.IsAuthorized())
                {
                    var result = await this._usersService.DeleteUserAsync(userId);
                    if (result)
                    {
                        return this.HandleSuccessResult(result);
                    }
                    else
                    {
                        return this.HandleBadRequest(ExceptionConstants.SomethingWentWrongMessageConstant);
                    }
                }

                return this.HandleUnAuthorizedRequest();
            }
            catch (Exception ex)
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodFailed, nameof(DeleteUserAsync), DateTime.UtcNow, ex.Message));
                return this.HandleBadRequest(ex.Message);
            }
            finally
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(DeleteUserAsync), DateTime.UtcNow, userId));
            }
        }
    }
}
