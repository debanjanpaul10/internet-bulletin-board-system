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
/// the context of an authenticated user. All operations performed by this tool are scoped to the current user and
/// leverage dependency-injected services for context, logging, and data access.</remarks>
/// <param name="logger">The logger used to record diagnostic and operational information for this tool.</param>
/// <param name="postsHandler">Handles post-related data operations, such as retrieving and managing post information.</param>
/// <seealso cref="BaseTool"/>
[McpServerToolType]
public class PostsDataTool(ILogger<PostsDataTool> logger, IPostsHandler postsHandler) : BaseTool
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
        try
        {
            logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(GetPostAsync), DateTime.UtcNow, string.Empty));

            var result = await postsHandler.GetAllPostsAsync(string.Empty).ConfigureAwait(false);
            return this.HandleSuccessResult(result);
        }
        catch (Exception ex)
        {
            logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodFailed, nameof(GetAllPostsDataAsync), DateTime.UtcNow, ex.Message));
            throw;
        }
        finally
        {
            logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetAllPostsDataAsync), DateTime.UtcNow, string.Empty));
        }
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
        try
        {
            logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(GetPostAsync), DateTime.UtcNow, postId));

            var result = await postsHandler.GetPostAsync(postId, string.Empty).ConfigureAwait(false);
            if (result is not null && !Equals(result.PostId, Guid.Empty))
                return this.HandleSuccessResult(result);
            else
                return this.HandleBadRequest(ExceptionConstants.PostNotFoundMessageConstant);
        }

        catch (Exception ex)
        {
            logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodFailed, nameof(GetPostAsync), DateTime.UtcNow, ex.Message));
            throw;
        }
        finally
        {
            logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetPostAsync), DateTime.UtcNow, postId));
        }
    }
}
