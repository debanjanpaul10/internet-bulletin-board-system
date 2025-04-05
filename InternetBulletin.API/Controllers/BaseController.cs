// *********************************************************************************
//	<copyright file="BaseController.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Base Controller Class.</summary>
// *********************************************************************************

namespace InternetBulletin.API.Controllers
{
    using InternetBulletin.API.Helpers;
    using InternetBulletin.Shared.Constants;
    using InternetBulletin.Shared.DTOs;
    using Microsoft.AspNetCore.Mvc;
    using System.Globalization;
    using System.IdentityModel.Tokens.Jwt;
    using System.Net;
    using static InternetBulletin.Shared.Constants.ConfigurationConstants;

    /// <summary>
    /// The Base Controller Class.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    /// <param name="configuration">The Configuration.</param>
    [Authorize]
    public abstract class BaseController(IConfiguration configuration, ILogger<BaseController> logger) : ControllerBase
    {
        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration _configuration = configuration;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<BaseController> _logger = logger;

        /// <summary>
        /// Determines whether this instance is authorized.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is authorized; otherwise, <c>false</c>.
        /// </returns>
        protected bool IsAuthorized()
        {
            try
            {
                var authorizationHeader = HttpContext.Request.Headers.Authorization.ToString();
                if (string.IsNullOrWhiteSpace(authorizationHeader))
                {
                    this._logger.LogError(LoggingConstants.AuthorizationMissingMessage);
                    return false;
                }

                var tokenValue = authorizationHeader.Replace(BearerConstant, string.Empty, StringComparison.OrdinalIgnoreCase).Trim();
                if (string.IsNullOrEmpty(tokenValue))
                {
                    this._logger.LogError(LoggingConstants.TokenMissingMessage);
                    return false;
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(tokenValue);

                var applicationId = jwtToken.Claims.FirstOrDefault(c => c.Type == ApplicationIdTokenConstant)?.Value;
                var clientId = _configuration[ClientIdConstant];
                if (!string.Equals(applicationId, clientId, StringComparison.Ordinal))
                {
                    this._logger.LogError(LoggingConstants.ApplicationIdMismatchMessage);
                    return false;
                }

                var tokenExpiryTime = jwtToken.Claims.FirstOrDefault(c => c.Type == TokenExpiryConstant)?.Value;
                if (string.IsNullOrEmpty(tokenExpiryTime) || !double.TryParse(tokenExpiryTime, out var expiryTimestamp))
                {
                    this._logger.LogError(LoggingConstants.TokenExpiryMissingMessage);
                    return false;
                }

                var expiryTime = DateTime.UnixEpoch.AddSeconds(expiryTimestamp);
                if (DateTime.UtcNow > expiryTime)
                {
                    this._logger.LogError(LoggingConstants.TokenExpiredMessageConstant);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodFailed, nameof(IsAuthorized), DateTime.UtcNow, ex.Message));
                throw;
            }
        }

        /// <summary>
        /// Handles the bad request.
        /// </summary>
        /// <returns>The unauthorized object result</returns>
        protected UnauthorizedObjectResult HandleUnAuthorizedRequest()
        {
            var responseData = new ResponseDTO()
            {
                Data = ExceptionConstants.UserUnauthorizedMessageConstant,
                StatusCode = (int)HttpStatusCode.Unauthorized,
                IsSuccess = false,
            };
            return this.Unauthorized(responseData);
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
