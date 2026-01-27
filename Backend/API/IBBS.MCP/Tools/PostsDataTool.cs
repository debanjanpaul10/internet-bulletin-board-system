using System.ComponentModel;
using IBBS.API.Adapters.Contracts;
using InternetBulletin.Shared.DTOs;
using ModelContextProtocol.Server;
using static IBBS.MCP.Helpers.MCPConstants;
using static IBBS.MCP.Helpers.ToolsConstants.PostsDataTool;

namespace IBBS.MCP.Tools;

/// <summary>
/// Provides server-side tools for retrieving and managing post data within the current user context.
/// </summary>
/// <remarks>This tool is intended for use in server environments where post data must be accessed or managed in
/// the context of an authenticated user. All operations performed by this tool are scoped to the current user and leverage dependency-injected services for context, logging, and data access.</remarks>
/// <param name="postsHandler">Handles post-related data operations, such as retrieving and managing post information.</param>
/// <param name="configuration">The configuration service.</param>
/// <param name="httpContextAccessor">The http context accessor service.</param>
/// <seealso cref="BaseTool"/>
[McpServerToolType]
public sealed class PostsDataTool(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IPostsHandler postsHandler) : BaseTool(httpContextAccessor, configuration)
{
    /// <summary>
    /// Asynchronously retrieves data for all available posts.
    /// </summary>
    /// <remarks>This method performs an asynchronous operation to fetch all posts associated with the current
    /// user context. The returned response includes both the result data and any relevant status or error information.
    /// This method does not throw exceptions for missing data; errors are reported in the response object.</remarks>
    /// <returns>A <see cref="ResponseDTO"/> containing the data for all posts. The response includes status information and the collection of post data. If no posts are available, the collection will be empty.</returns>
    [McpServerTool]
    [Description(GetAllPostsDataAction.Description)]
    public async Task<ResponseDTO> GetAllPostsDataAsync()
    {
        if (base.IsAuthorized())
        {
            var result = await postsHandler.GetAllPostsAsync(string.Empty).ConfigureAwait(false);
            if (result is not null) return HandleSuccessResult(result);
            else return HandleBadRequest(ExceptionConstants.SomethingWentWrongMessageConstant);
        }

        return HandleUnAuthorizedRequest();
    }

    /// <summary>
    /// Retrieves the data for a single post identified by the specified post ID.
    /// </summary>
    /// <remarks>This method requires the caller to be authorized. If the post does not exist or the caller is not authorized, the response will indicate the appropriate error condition.</remarks>
    /// <param name="postId">The unique identifier of the post to retrieve. Cannot be null or empty.</param>
    /// <returns>A <see cref="ResponseDTO"/> containing the post data if found and authorized; otherwise, a response indicating an error or unauthorized access.</returns>
    [McpServerTool]
    [Description(GetPostDataAction.Description)]
    public async Task<ResponseDTO> GetPostAsync([Description(GetPostDataAction.InputDescription)] string postId)
    {
        if (base.IsAuthorized())
        {
            var result = await postsHandler.GetPostAsync(postId, string.Empty).ConfigureAwait(false);
            if (result is not null && !Equals(result.PostId, Guid.Empty)) return HandleSuccessResult(result);
            else return HandleBadRequest(ExceptionConstants.PostNotFoundMessageConstant);
        }

        return HandleUnAuthorizedRequest();
    }
}
