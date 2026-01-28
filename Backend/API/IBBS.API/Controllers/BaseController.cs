using System.Net;
using IBBS.API.Helpers;
using InternetBulletin.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static IBBS.API.Helpers.APIConstants;

namespace IBBS.API.Controllers;

/// <summary>
/// The base controller class.
/// </summary>
/// <param name="httpContextAccessor">The http context accessor.</param>
/// <param name="configuration">The configuration.</param>
/// <seealso cref="ControllerBase"/>
[Authorize]
public abstract class BaseController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : ControllerBase
{
    /// <summary>
    /// The user email
    /// </summary>
    protected string UserEmail => httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(claim => claim.Type.Equals(HeaderConstants.UserEmailClaimConstant))?.Value
        ?? HeaderConstants.NotApplicableStringConstant;

    /// <summary>
    /// Determines whether the request is authorized based on the authorization type.
    /// </summary>
    /// <param name="authorizationType">The authorization type.</param>
    /// <returns>The boolean <c>true</c> if the request is authorized, otherwise <c>false</c>.</returns>
    /// <exception cref="Exception">Thrown when the configuration is missing.</exception>
    protected bool IsAuthorized(AuthorizationTypes authorizationTypes)
    {
        if (httpContextAccessor.HttpContext is not null && httpContextAccessor.HttpContext?.User is not null)
        {
            // USER AUTHENTICATION
            if (authorizationTypes == AuthorizationTypes.UserBased)
                if (!string.IsNullOrEmpty(this.UserEmail) && !this.UserEmail.Equals(HeaderConstants.NotApplicableStringConstant, StringComparison.OrdinalIgnoreCase))
                    return true && this.CheckApplicationLevelAuthorization();


            // APPLICATION AUTHENTICATION
            if (authorizationTypes == AuthorizationTypes.ApplicationBased)
                return this.CheckApplicationLevelAuthorization();
        }

        return false;
    }

    /// <summary>
    /// Handles the success result.
    /// </summary>
    /// <param name="response">The response.</param>
    /// <returns>The ok object result</returns>
    protected OkObjectResult HandleSuccessResult(object response)
    {
        return this.Ok(new ResponseDTO()
        {
            Data = response,
            IsSuccess = true,
            StatusCode = (int)HttpStatusCode.OK
        });
    }

    /// <summary>
    /// Handles the bad request.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <returns>The bad request result.</returns>
    protected BadRequestObjectResult HandleBadRequest(string message)
    {
        return this.BadRequest(new ResponseDTO()
        {
            Data = message,
            IsSuccess = false,
            StatusCode = (int)HttpStatusCode.BadRequest
        });
    }

    /// <summary>
    /// Handles the bad request.
    /// </summary>
    /// <returns>The unauthorized object result</returns>
    protected UnauthorizedObjectResult HandleUnAuthorizedRequest()
    {
        return this.Unauthorized(new ResponseDTO()
        {
            Data = ExceptionConstants.UserUnauthorizedMessageConstant,
            StatusCode = (int)HttpStatusCode.Unauthorized,
            IsSuccess = false,
        });
    }

    #region PRIVATE METHODS

    /// <summary>
    /// Checks the application level authorization.
    /// </summary>
    /// <returns><c>true</c> if the application is authorized, otherwise <c>false</c>.</returns>
    /// <exception cref="Exception">Thrown when the configuration is missing.</exception>
    private bool CheckApplicationLevelAuthorization()
    {
        var currentClientId = this.User?.Claims?.FirstOrDefault(claim => claim.Type.Equals(HeaderConstants.ClientIdClaimConstant))?.Value;
        var ibbsClientId = configuration[ConfigurationConstants.Auth0WebClientId] ?? throw new Exception(ExceptionConstants.ConfigurationValueNotExistsMessageConstant);
        if (!string.IsNullOrEmpty(currentClientId) && !currentClientId.Equals(HeaderConstants.NotApplicableStringConstant, StringComparison.OrdinalIgnoreCase))
            return currentClientId.Equals(ibbsClientId, StringComparison.OrdinalIgnoreCase);

        return false;
    }

    #endregion
}
