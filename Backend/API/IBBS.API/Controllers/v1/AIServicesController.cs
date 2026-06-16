using IBBS.API.Adapters.Contracts;
using IBBS.API.Adapters.Models;
using IBBS.API.Adapters.Models.AI;
using IBBS.API.Helpers;
using IBBS.Domain.Contracts;
using IBBS.Domain.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using static IBBS.API.Helpers.APIConstants;
using static IBBS.API.Helpers.SwaggerConstants.AIServicesController;

namespace IBBS.API.Controllers.v1;

/// <summary>
/// The AI Services Controller.
/// </summary>
/// <param name="aiServicesHandler">The AI Services adapter Handler.</param>
/// <param name="configuration">The configuration service.</param>
/// <param name="httpContextAccessor">The http context accessor.</param>
/// <param name="correlationContext">The correlation context.</param>
/// <param name="logger">The logger.</param>
/// <seealso cref="BaseController" />
[ApiController]
[Route(RouteConstants.AiServicesController.BaseRoute)]
public sealed class AIServicesController(
    ILogger<AIServicesController> logger,
    ICorrelationContext correlationContext,
    IHttpContextAccessor httpContextAccessor,
    IConfiguration configuration,
    IAiServicesHandler aiServicesHandler) : BaseController(httpContextAccessor, configuration)
{
    #region PLUGINS

    /// <summary>
    /// Rewrites the with ai asynchronous.
    /// </summary>
    /// <param name="requestDto">The request dto.</param>
    /// <returns>The ai rewritten response.</returns>
    [HttpPost(RouteConstants.AiServicesController.RewriteWithAI_Route)]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(
        Summary = RewriteWithAIAction.Summary,
        Description = RewriteWithAIAction.Description,
        OperationId = RewriteWithAIAction.OperationId)]
    public async Task<IActionResult> RewriteWithAIAsync(
        UserStoryRequestDTO requestDto
    )
    {
        string response = string.Empty;
        try
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodStart,
                nameof(RewriteWithAIAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, base.UserEmail, requestDto })
            );

            ArgumentNullException.ThrowIfNull(requestDto);
            if (base.IsAuthorized(AuthorizationTypes.UserBased))
            {
                response = await aiServicesHandler.RewriteWithAIAsync(
                    userName: UserEmail,
                    requestDTO: requestDto,
                    cancellationToken: HttpContext.RequestAborted
                ).ConfigureAwait(false);
                return base.HandleSuccessResult(response);
            }

            return base.HandleUnAuthorizedRequest();
        }
        catch (TaskCanceledException ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.LogHelperMethodFailed,
                nameof(RewriteWithAIAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, base.UserEmail, ex.Message })
            );
            return base.HandleTaskCancelledResponse(message: ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.LogHelperMethodFailed,
                nameof(RewriteWithAIAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, base.UserEmail, ex.Message })
            );
            return base.HandleBadRequest(message: ex.Message);
        }
        finally
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodEnded,
                nameof(RewriteWithAIAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, requestDto, base.UserEmail, response })
            );
        }
    }

    /// <summary>
    /// Generates the tag for story asynchronous.
    /// </summary>
    /// <param name="requestDto">The request dto.</param>
    /// <returns>The tag response dto.</returns>
    [HttpPost(RouteConstants.AiServicesController.GenerateGenreTag_Route)]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(
        Summary = GenerateTagForStoryAction.Summary,
        Description = GenerateTagForStoryAction.Description,
        OperationId = GenerateTagForStoryAction.OperationId)]
    public async Task<IActionResult> GenerateTagForStoryAsync(
        UserStoryRequestDTO requestDto
    )
    {
        string response = string.Empty;
        try
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodStart,
                nameof(GenerateTagForStoryAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, base.UserEmail, requestDto })
            );

            ArgumentNullException.ThrowIfNull(requestDto);
            if (base.IsAuthorized(AuthorizationTypes.UserBased))
            {
                response = await aiServicesHandler.GenerateTagForStoryAsync(
                    userName: UserEmail,
                    requestDTO: requestDto,
                    cancellationToken: HttpContext.RequestAborted
                ).ConfigureAwait(false);

                return base.HandleSuccessResult(response);
            }

            return base.HandleUnAuthorizedRequest();
        }
        catch (TaskCanceledException ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.LogHelperMethodFailed,
                nameof(GenerateTagForStoryAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, base.UserEmail, ex.Message })
            );
            return base.HandleTaskCancelledResponse(message: ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.LogHelperMethodFailed,
                nameof(GenerateTagForStoryAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, base.UserEmail, ex.Message })
            );
            return base.HandleBadRequest(message: ex.Message);
        }
        finally
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodEnded,
                nameof(GenerateTagForStoryAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, base.UserEmail, requestDto, response })
            );
        }
    }

    /// <summary>
    /// Moderates the content data asynchronous.
    /// </summary>
    /// <param name="requestDto">The request dto.</param>
    /// <returns>The moderation content response.</returns>
    [HttpPost(RouteConstants.AiServicesController.ModerateContent_Route)]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(
        Summary = ModerateContentDataAction.Summary,
        Description = ModerateContentDataAction.Description,
        OperationId = ModerateContentDataAction.OperationId)]
    public async Task<IActionResult> ModerateContentDataAsync(
        UserStoryRequestDTO requestDto
    )
    {
        string response = string.Empty;
        try
        {
            logger.LogAppInformation(
               LoggingConstants.LogHelperMethodStart,
               nameof(ModerateContentDataAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, base.UserEmail, requestDto })
            );

            ArgumentNullException.ThrowIfNull(requestDto);
            if (base.IsAuthorized(authorizationTypes: AuthorizationTypes.UserBased))
            {
                response = await aiServicesHandler.ModerateContentDataAsync(
                    userName: UserEmail,
                    requestDTO: requestDto,
                    cancellationToken: HttpContext.RequestAborted
                ).ConfigureAwait(false);

                return base.HandleSuccessResult(response);
            }

            return base.HandleUnAuthorizedRequest();
        }
        catch (TaskCanceledException ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.LogHelperMethodFailed,
                nameof(ModerateContentDataAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, base.UserEmail, ex.Message })
            );
            return base.HandleTaskCancelledResponse(message: ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.LogHelperMethodFailed,
                nameof(ModerateContentDataAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, base.UserEmail, ex.Message })
            );
            return base.HandleBadRequest(message: ex.Message);
        }
        finally
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodEnded,
                nameof(ModerateContentDataAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, base.UserEmail, requestDto, response })
            );
        }
    }

    /// <summary>
    /// Gets the bug severity status asynchronous.
    /// </summary>
    /// <param name="bugSeverityInput">The bug severity input.</param>
    /// <returns>The bug severity response dto.</returns>
    [HttpPost(RouteConstants.AiServicesController.GetBugSeverityStatus_ApiRoute)]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(
        Summary = GetBugSeverityStatusAction.Summary,
        Description = GetBugSeverityStatusAction.Description,
        OperationId = GetBugSeverityStatusAction.OperationId)]
    public async Task<IActionResult> GetBugSeverityStatusAsync(
        [FromBody] BugSeverityAIRequestDTO bugSeverityInput
    )
    {
        string response = string.Empty;
        try
        {
            logger.LogAppInformation(
               LoggingConstants.LogHelperMethodStart,
               nameof(GetBugSeverityStatusAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, base.UserEmail, bugSeverityInput })
            );

            ArgumentNullException.ThrowIfNull(bugSeverityInput);
            if (base.IsAuthorized(authorizationTypes: AuthorizationTypes.UserBased))
            {
                response = await aiServicesHandler.GenerateBugSeverityAsync(
                    bugSeverityAiRequest: bugSeverityInput,
                    cancellationToken: HttpContext.RequestAborted
                ).ConfigureAwait(false);

                return base.HandleSuccessResult(response);
            }

            return HandleUnAuthorizedRequest();
        }
        catch (TaskCanceledException ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.LogHelperMethodFailed,
                nameof(GetBugSeverityStatusAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, base.UserEmail, ex.Message })
            );
            return base.HandleTaskCancelledResponse(message: ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.LogHelperMethodFailed,
                nameof(GetBugSeverityStatusAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, base.UserEmail, ex.Message })
            );
            return base.HandleBadRequest(message: ex.Message);
        }
        finally
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodEnded,
                nameof(GetBugSeverityStatusAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, base.UserEmail, bugSeverityInput, response })
            );
        }
    }

    #endregion

    #region CHATBOT

    /// <summary>
    /// Posts the ai result feedback asynchronous.
    /// </summary>
    /// <param name="aiResponseFeedback">The ai response feedback.</param>
    /// <returns>The boolean for success/failure.</returns>
    /// <exception cref="System.ArgumentNullException"></exception>
    [HttpPost(RouteConstants.AiServicesController.HandleAIFeedback_Route)]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(
        Summary = PostAiResultFeedbackAction.Summary,
        Description = PostAiResultFeedbackAction.Description,
        OperationId = PostAiResultFeedbackAction.OperationId)]
    public async Task<IActionResult> PostAiResultFeedbackAsync(
        [FromBody] AIResponseFeedbackDTO aiResponseFeedback
    )
    {
        bool response = false;
        try
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodStart,
                nameof(PostAiResultFeedbackAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, base.UserEmail, aiResponseFeedback })
            );

            ArgumentNullException.ThrowIfNull(aiResponseFeedback);
            if (base.IsAuthorized(AuthorizationTypes.UserBased))
            {
                response = await aiServicesHandler.PostAiResultFeedbackAsync(
                    aiResponseFeedback,
                    userEmail: UserEmail,
                    cancellationToken: HttpContext.RequestAborted
                ).ConfigureAwait(false);
                return base.HandleSuccessResult(response);
            }

            return base.HandleUnAuthorizedRequest();
        }
        catch (TaskCanceledException ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.LogHelperMethodFailed,
                nameof(PostAiResultFeedbackAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, base.UserEmail, ex.Message })
            );
            return base.HandleTaskCancelledResponse(message: ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.LogHelperMethodFailed,
                nameof(PostAiResultFeedbackAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, base.UserEmail, ex.Message })
            );
            return base.HandleBadRequest(message: ex.Message);
        }
        finally
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodEnded,
                nameof(PostAiResultFeedbackAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, base.UserEmail, aiResponseFeedback, response })
            );
        }
    }

    /// <summary>
    /// Gets the sample prompts for chatbot asynchronous.
    /// </summary>
    /// <returns>The list of <see cref="LookupMasterDTO"/></returns>
    [HttpGet(RouteConstants.AiServicesController.GetSamplePrompts_Route)]
    [ProducesResponseType(typeof(IEnumerable<LookupMasterDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(
        Summary = GetSamplePromptsForChatbotAction.Summary,
        Description = GetSamplePromptsForChatbotAction.Description,
        OperationId = GetSamplePromptsForChatbotAction.OperationId)]
    public async Task<IActionResult> GetSamplePromptsForChatbotAsync()
    {
        IEnumerable<LookupMasterDTO> response = [];
        try
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodStart,
                nameof(GetSamplePromptsForChatbotAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, base.UserEmail })
            );

            if (base.IsAuthorized(authorizationTypes: AuthorizationTypes.UserBased))
            {
                response = await aiServicesHandler.GetSamplePromptsForChatbotAsync(
                    cancellationToken: HttpContext.RequestAborted
                ).ConfigureAwait(false);
                return base.HandleSuccessResult(response);
            }

            return base.HandleUnAuthorizedRequest();
        }
        catch (TaskCanceledException ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.LogHelperMethodFailed,
                nameof(GetSamplePromptsForChatbotAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, base.UserEmail, ex.Message })
            );
            return base.HandleTaskCancelledResponse(message: ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.LogHelperMethodFailed,
                nameof(GetSamplePromptsForChatbotAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, base.UserEmail, ex.Message })
            );
            return base.HandleBadRequest(message: ex.Message);
        }
        finally
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodEnded,
                nameof(GetSamplePromptsForChatbotAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, base.UserEmail, response })
            );
        }
    }

    /// <summary>
    /// Gets the chatbot response using LLM.
    /// </summary>
    /// <param name="userQueryRequest">The user query request dto model.</param>
    /// <returns>The AI response string.</returns>
    [HttpPost(RouteConstants.AiServicesController.ChatbotRespond_Route)]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = GetChatbotResponseAction.Summary, Description = GetChatbotResponseAction.Description, OperationId = GetChatbotResponseAction.OperationId)]
    public async Task<IActionResult> GetChatbotResponseAsync(
        [FromBody] UserQueryRequestDTO userQueryRequest
    )
    {
        string response = string.Empty;
        try
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodStart,
                nameof(GetChatbotResponseAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, base.UserEmail, userQueryRequest })
            );

            ArgumentNullException.ThrowIfNull(userQueryRequest);
            if (base.IsAuthorized(authorizationTypes: AuthorizationTypes.UserBased))
            {
                response = await aiServicesHandler.GetChatbotResponseAsync(
                    userQueryRequest,
                    cancellationToken: HttpContext.RequestAborted
                ).ConfigureAwait(false);
                return base.HandleSuccessResult(response);
            }

            return base.HandleUnAuthorizedRequest();
        }
        catch (TaskCanceledException ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.LogHelperMethodFailed,
                nameof(GetChatbotResponseAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, ex.Message })
            );
            return base.HandleTaskCancelledResponse(message: ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.LogHelperMethodFailed,
                nameof(GetChatbotResponseAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, ex.Message })
            );
            return base.HandleBadRequest(message: ex.Message);
        }
        finally
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodEnded,
                nameof(GetChatbotResponseAsync), DateTime.UtcNow,
                    JsonConvert.SerializeObject(new { correlationContext.CorrelationId, base.UserEmail, userQueryRequest, response })
            );
        }
    }

    #endregion
}
