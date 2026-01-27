using System.ComponentModel;
using IBBS.API.Adapters.Contracts;
using InternetBulletin.Shared.DTOs;
using ModelContextProtocol.Server;
using static IBBS.MCP.Helpers.MCPConstants;
using static IBBS.MCP.Helpers.ToolsConstants.PostsDataTool;

namespace IBBS.MCP.Tools;

/// <summary>
/// Provides functionality to retrieve and manage post ratings for users.
/// </summary>
/// <remarks>This tool is intended for server-side operations involving user post ratings. It relies on dependency
/// injection for logging and data access, and is designed to be used within the context of the application's tool infrastructure.</remarks>
/// <param name="postRatingsHandler">The handler responsible for accessing and managing post ratings data. Cannot be null.</param>
/// <param name="httpContextAccessor">The http context accessor service.</param>
/// <param name="configuration">The configuration service.</param>
/// <seealso cref="BaseTool"/>
[McpServerToolType]
public sealed class PostRatingsTool(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IPostRatingsHandler postRatingsHandler) : BaseTool(httpContextAccessor, configuration)
{
    /// <summary>
    /// Retrieves all post ratings associated with the specified user email address asynchronously.
    /// </summary>
    /// <remarks>If no ratings are found for the specified user, the response will indicate a bad request.
    /// This method logs its execution and may throw exceptions encountered during processing.</remarks>
    /// <param name="userEmail">The email address of the user whose post ratings are to be retrieved. Cannot be null or empty.</param>
    /// <returns>A <see cref="ResponseDTO"/> containing the user's post ratings if found; otherwise, a response indicating a bad
    /// request.</returns>
    [McpServerTool]
    [Description(GetAllUserRatingsAction.Description)]
    public async Task<ResponseDTO> GetAllUserRatingsAsync([Description(GetAllUserRatingsAction.InputDescription)] string userEmail)
    {
        if (base.IsAuthorized())
        {
            var result = await postRatingsHandler.GetAllUserPostRatingsAsync(userEmail).ConfigureAwait(false);
            if (result is not null) return HandleSuccessResult(result);
            else return HandleBadRequest(ExceptionConstants.UnableToGetUserPostRatingsMessageConstant);
        }

        return HandleUnAuthorizedRequest();
    }
}
