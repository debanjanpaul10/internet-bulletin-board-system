using InternetBulletin.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static IBBS.API.Helpers.APIConstants;

namespace IBBS.API.Controllers;

/// <summary>
/// The Base Controller Class.
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
[Authorize]
public abstract class BaseController : ControllerBase
{
	/// <summary>
	/// The user name.
	/// </summary>
	protected string UserName = string.Empty;

	/// <summary>
	/// The user full name.
	/// </summary>
	protected string UserFullName = string.Empty;

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
		_httpContextAccessor = httpContextAccessor;
		if (_httpContextAccessor.HttpContext is not null && _httpContextAccessor.HttpContext?.User is not null)
		{
			var userName = _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(claim => claim.Type.Equals(ConfigurationConstants.UserNameClaimConstant))?.Value;
			if (!string.IsNullOrEmpty(userName))
			{
				UserName = userName;
			}

			var userDisplayName = _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(claim => claim.Type.Equals(ConfigurationConstants.UserDisplayNameConstant))?.Value;
			if (!string.IsNullOrEmpty(userDisplayName))
			{
				UserFullName = userDisplayName;
			}
		}
	}

	/// <summary>
	/// Is user authorized.
	/// </summary>
	protected bool IsAuthorized()
	{
		if (string.IsNullOrEmpty(UserName))
		{
			throw new UnauthorizedAccessException();
		}

		return true;
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
		return Ok(responseData);
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
		return BadRequest(responseData);
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
		return Unauthorized(responseData);
	}
}
