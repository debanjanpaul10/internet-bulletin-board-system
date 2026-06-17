using System.ComponentModel;
using IBBS.API.Adapters.Contracts;
using IBBS.API.Adapters.Models.Posts;
using IBBS.Domain.Contracts;
using IBBS.Domain.Helpers;
using InternetBulletin.Shared.DTOs;
using ModelContextProtocol.Server;
using Newtonsoft.Json;
using static IBBS.MCP.Helpers.MCPConstants;
using static IBBS.MCP.Helpers.ToolsConstants.PostsDataTool;

namespace IBBS.MCP.Tools;

/// <summary>
/// Provides functionality to retrieve and manage post ratings for users.
/// </summary>
/// <remarks>This tool is intended for server-side operations involving user post ratings. It relies on dependency
/// injection for logging and data access, and is designed to be used within the context of the application's tool infrastructure.</remarks>
/// <param name="logger">The logger.</param>
/// <param name="correlationContext">The correlation context.</param>
/// <param name="configuration">The configuration service.</param>
/// <param name="httpContextAccessor">The http context accessor service.</param>
/// <param name="postRatingsHandler">The handler responsible for accessing and managing post ratings data. Cannot be null.</param>
/// <seealso cref="BaseTool"/>
[McpServerToolType]
public sealed class PostRatingsTool(
    ILogger<PostRatingsTool> logger,
    ICorrelationContext correlationContext,
    IConfiguration configuration,
    IHttpContextAccessor httpContextAccessor,
    IPostRatingsHandler postRatingsHandler) : BaseTool(httpContextAccessor, configuration)
{
    /// <summary>
    /// Gets all user ratings asynchronous.
    /// </summary>
    /// <param name="userEmail">The user email.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="ResponseDTO"/> containing the user's post ratings if found; otherwise, a response indicating a bad request.</returns>
    [McpServerTool]
    [Description(GetAllUserRatingsAction.Description)]
    public async Task<ResponseDTO> GetAllUserRatingsAsync(
        [Description(GetAllUserRatingsAction.InputDescription)] string userEmail,
        CancellationToken cancellationToken = default
    )
    {
        IEnumerable<PostRatingDTO> response = [];
        try
        {
            logger.LogAppInformation(
               LoggingConstants.LogHelperMethodStart,
               nameof(GetAllUserRatingsAsync), DateTime.UtcNow,
                   JsonConvert.SerializeObject(new { correlationContext.CorrelationId, userEmail })
            );

            if (base.IsAuthorized())
            {
                response = await postRatingsHandler.GetAllUserPostRatingsAsync(
                    userName: userEmail,
                    cancellationToken
                ).ConfigureAwait(false);

                return HandleSuccessResult(response);
            }

            return HandleUnAuthorizedRequest();
        }
        catch (TaskCanceledException ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.LogHelperMethodFailed,
                nameof(GetAllUserRatingsAsync), DateTime.UtcNow,
                    JsonConvert.SerializeObject(new { correlationContext.CorrelationId, ex.Message })
            );
            return HandleTaskCancelledResponse(message: ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.LogHelperMethodFailed,
                nameof(GetAllUserRatingsAsync), DateTime.UtcNow,
                    JsonConvert.SerializeObject(new { correlationContext.CorrelationId, ex.Message })
            );
            return HandleBadRequest(message: ex.Message);
        }
        finally
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodEnded,
                nameof(GetAllUserRatingsAsync), DateTime.UtcNow,
                    JsonConvert.SerializeObject(new { correlationContext.CorrelationId, userEmail, response })
            );
        }
    }
}
