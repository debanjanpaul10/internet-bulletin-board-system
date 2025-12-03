using System.Net;
using InternetBulletin.Shared.DTOs;
using static IBBS.MCP.Helpers.MCPConstants;

namespace IBBS.MCP.Tools;

/// <summary>
/// Provides a base class for tools that require access to the current HTTP context and authenticated user information, such as the user's email address.
/// </summary>
/// <remarks>BaseTool is intended to be inherited by classes that operate within an ASP.NET Core environment and
/// need to access user-specific data from the HTTP context. Upon instantiation, the class attempts to extract the
/// user's email address from the claims in the current HTTP context, making it available to derived classes. This
/// facilitates authorization checks and user-specific operations. The class also provides protected helper methods for
/// handling common API response scenarios, including success, bad request, and unauthorized responses. Thread safety is
/// not guaranteed; instances should be scoped appropriately in web applications.</remarks>
public abstract class BaseTool
{
    /// <summary>
    /// Handles the success result.
    /// </summary>
    /// <param name="response">The response.</param>
    /// <returns>The ok object result</returns>
    protected ResponseDTO HandleSuccessResult(object response) =>
        new()
        {
            Data = response,
            IsSuccess = true,
            StatusCode = (int)HttpStatusCode.OK
        };

    /// <summary>
    /// Handles the bad request.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <returns>The bad request result.</returns>
    protected ResponseDTO HandleBadRequest(string message) =>
        new()
        {
            Data = message,
            IsSuccess = false,
            StatusCode = (int)HttpStatusCode.BadRequest
        };

    /// <summary>
    /// Handles the bad request.
    /// </summary>
    /// <returns>The unauthorized object result</returns>
    protected ResponseDTO HandleUnAuthorizedRequest() =>
        new()
        {
            Data = ExceptionConstants.UserUnauthorizedMessageConstant,
            StatusCode = (int)HttpStatusCode.Unauthorized,
            IsSuccess = false,
        };
}
