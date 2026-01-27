using IBBS.API.Adapters.Contracts;
using IBBS.API.Adapters.Models;
using IBBS.API.Adapters.Models.AI;
using IBBS.API.Helpers;
using Microsoft.AspNetCore.Mvc;
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
/// <seealso cref="BaseController" />
[ApiController]
[Route(RouteConstants.AiServicesController.BaseRoute)]
public sealed class AIServicesController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IAiServicesHandler aiServicesHandler) : BaseController(httpContextAccessor, configuration)
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
    [SwaggerOperation(Summary = RewriteWithAIAction.Summary, Description = RewriteWithAIAction.Description, OperationId = RewriteWithAIAction.OperationId)]
    public async Task<IActionResult> RewriteWithAIAsync(UserStoryRequestDTO requestDto)
    {
        ArgumentNullException.ThrowIfNull(requestDto);
        if (base.IsAuthorized(AuthorizationTypes.UserBased))
        {
            var result = await aiServicesHandler.RewriteWithAIAsync(UserEmail, requestDto).ConfigureAwait(false);
            if (!string.IsNullOrEmpty(result)) return HandleSuccessResult(result);
            else return HandleBadRequest(ExceptionConstants.SomethingWentWrongMessageConstant);
        }

        return HandleUnAuthorizedRequest();

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
    [SwaggerOperation(Summary = GenerateTagForStoryAction.Summary, Description = GenerateTagForStoryAction.Description, OperationId = GenerateTagForStoryAction.OperationId)]
    public async Task<IActionResult> GenerateTagForStoryAsync(UserStoryRequestDTO requestDto)
    {
        ArgumentNullException.ThrowIfNull(requestDto);
        if (base.IsAuthorized(AuthorizationTypes.UserBased))
        {
            var result = await aiServicesHandler.GenerateTagForStoryAsync(UserEmail, requestDto).ConfigureAwait(false);
            if (!string.IsNullOrEmpty(result)) return HandleSuccessResult(result);
            else return HandleBadRequest(ExceptionConstants.SomethingWentWrongMessageConstant);
        }

        return HandleUnAuthorizedRequest();
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
    [SwaggerOperation(Summary = ModerateContentDataAction.Summary, Description = ModerateContentDataAction.Description, OperationId = ModerateContentDataAction.OperationId)]
    public async Task<IActionResult> ModerateContentDataAsync(UserStoryRequestDTO requestDto)
    {
        ArgumentNullException.ThrowIfNull(requestDto);
        if (base.IsAuthorized(AuthorizationTypes.UserBased))
        {
            var result = await aiServicesHandler.ModerateContentDataAsync(UserEmail, requestDto).ConfigureAwait(false);
            if (!string.IsNullOrEmpty(result)) return HandleSuccessResult(result);
            else return HandleBadRequest(ExceptionConstants.SomethingWentWrongMessageConstant);
        }

        return HandleUnAuthorizedRequest();
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
    [SwaggerOperation(Summary = GetBugSeverityStatusAction.Summary, Description = GetBugSeverityStatusAction.Description, OperationId = GetBugSeverityStatusAction.OperationId)]
    public async Task<IActionResult> GetBugSeverityStatusAsync([FromBody] BugSeverityAIRequestDTO bugSeverityInput)
    {
        ArgumentNullException.ThrowIfNull(bugSeverityInput);
        if (base.IsAuthorized(AuthorizationTypes.UserBased))
        {
            var result = await aiServicesHandler.GenerateBugSeverityAsync(bugSeverityInput).ConfigureAwait(false);
            if (result is not null) return HandleSuccessResult(result);
            else return HandleBadRequest(ExceptionConstants.SomethingWentWrongMessageConstant);
        }

        return HandleUnAuthorizedRequest();
    }

    #endregion

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
    [SwaggerOperation(Summary = PostAiResultFeedbackAction.Summary, Description = PostAiResultFeedbackAction.Description, OperationId = PostAiResultFeedbackAction.OperationId)]
    public async Task<IActionResult> PostAiResultFeedbackAsync([FromBody] AIResponseFeedbackDTO aiResponseFeedback)
    {
        ArgumentNullException.ThrowIfNull(aiResponseFeedback);
        if (base.IsAuthorized(AuthorizationTypes.UserBased))
        {
            var result = await aiServicesHandler.PostAiResultFeedbackAsync(aiResponseFeedback, UserEmail).ConfigureAwait(false);
            if (result) return HandleSuccessResult(result);
            else return HandleBadRequest(ExceptionConstants.SomethingWentWrongMessageConstant);
        }

        return HandleUnAuthorizedRequest();
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
    [SwaggerOperation(Summary = GetSamplePromptsForChatbotAction.Summary, Description = GetSamplePromptsForChatbotAction.Description, OperationId = GetSamplePromptsForChatbotAction.OperationId)]
    public async Task<IActionResult> GetSamplePromptsForChatbotAsync()
    {
        if (base.IsAuthorized(AuthorizationTypes.UserBased))
        {
            var result = await aiServicesHandler.GetSamplePromptsForChatbotAsync().ConfigureAwait(false);
            if (result is not null) return HandleSuccessResult(result);
            else return HandleBadRequest(ExceptionConstants.SomethingWentWrongMessageConstant);
        }

        return HandleUnAuthorizedRequest();
    }

}
