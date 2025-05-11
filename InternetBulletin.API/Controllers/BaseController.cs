// *********************************************************************************
//	<copyright file="BaseController.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Base Controller Class.</summary>
// *********************************************************************************

namespace InternetBulletin.API.Controllers
{
    using InternetBulletin.Shared.Constants;
    using InternetBulletin.Shared.DTOs;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Net;
    using static InternetBulletin.Shared.Constants.ConfigurationConstants;

    /// <summary>
    /// The Base Controller Class.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    /// <param name="configuration">The Configuration.</param>
    [Authorize]
    public abstract class BaseController : ControllerBase
    {
        /// <summary>
        /// The user name.
        /// </summary>
        protected string UserName = string.Empty;

        /// <summary>
        /// The http context accessor.
        /// </summary>
        private readonly IHttpContextAccessor? _httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">The http context accessor.</param>
        public BaseController(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
            if (this._httpContextAccessor.HttpContext is not null && this._httpContextAccessor.HttpContext?.User is not null)
            {
                var userName = this._httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(claim => claim.Type.Equals(UserNameClaimConstant))?.Value;
                if (!string.IsNullOrEmpty(userName))
                {
                    this.UserName = userName;
                }
                else
                {
                    var userIdNotFoundException = new InvalidOperationException(ExceptionConstants.UserIdNotPresentExceptionConstant);
                    throw userIdNotFoundException;
                }
            }
        }

        /// <summary>
        /// Handles the success result.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns>The ok object result</returns>
        protected OkObjectResult HandleSuccessResult(object response)
        {
            var responseData = new ResponseDTO()
            {
                Data = response,
                IsSuccess = true,
                StatusCode = (int)HttpStatusCode.OK
            };
            return this.Ok(responseData);
        }

        /// <summary>
        /// Handles the bad request.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The bad request result.</returns>
        protected BadRequestObjectResult HandleBadRequest(string message)
        {
            var responseData = new ResponseDTO()
            {
                Data = message,
                IsSuccess = false,
                StatusCode = (int)HttpStatusCode.BadRequest
            };
            return this.BadRequest(responseData);
        }
    }
}
