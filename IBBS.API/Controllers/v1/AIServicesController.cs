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
/// <param name="httpContextAccessor">The http context accessor.</param>
/// <seealso cref="BaseController" />
[ApiController]
[Route(RouteConstants.AiServicesController.BaseRoute)]
public class AIServicesController(IHttpContextAccessor httpContextAccessor, IAiServicesHandler aiServicesHandler) : BaseController(httpContextAccessor)
{
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
		if (IsAuthorized())
		{
			ArgumentNullException.ThrowIfNull(requestDto);
			var rewrittenStory = await aiServicesHandler.RewriteWithAIAsync(UserEmail, requestDto).ConfigureAwait(false);
			if (!string.IsNullOrEmpty(rewrittenStory))
			{
				return HandleSuccessResult(rewrittenStory);
			}

			return HandleBadRequest(ExceptionConstants.SomethingWentWrongMessageConstant);
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
		if (IsAuthorized())
		{
			ArgumentNullException.ThrowIfNull(requestDto);
			var tagForStory = await aiServicesHandler.GenerateTagForStoryAsync(UserEmail, requestDto).ConfigureAwait(false);
			if (!string.IsNullOrEmpty(tagForStory))
			{
				return HandleSuccessResult(tagForStory);
			}

			return HandleBadRequest(ExceptionConstants.SomethingWentWrongMessageConstant);
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
		if (IsAuthorized())
		{
			ArgumentNullException.ThrowIfNull(requestDto);
			var tagForStory = await aiServicesHandler.ModerateContentDataAsync(UserEmail, requestDto).ConfigureAwait(false);
			if (!string.IsNullOrEmpty(tagForStory))
			{
				return HandleSuccessResult(tagForStory);
			}

			return HandleBadRequest(ExceptionConstants.SomethingWentWrongMessageConstant);
		}

		return HandleUnAuthorizedRequest();
	}

	/// <summary>
	/// Gets the chatbot response asynchronous.
	/// </summary>
	/// <param name="chatMessage">The chat message.</param>
	/// <returns>The ai chatbot response dto model.</returns>
	[HttpPost(RouteConstants.AiServicesController.ChatbotRespond_Route)]
	[ProducesResponseType(typeof(AIChatbotResponseDTO), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[SwaggerOperation(Summary = GetChatbotResponseAction.Summary, Description = GetChatbotResponseAction.Description, OperationId = GetChatbotResponseAction.OperationId)]
	public async Task<IActionResult> GetChatbotResponseAsync([FromBody] UserQueryRequestDTO chatMessage)
	{
		if (IsAuthorized())
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(chatMessage.UserQuery);
			var result = await aiServicesHandler.GetChatbotResponseAsync(chatMessage).ConfigureAwait(false);
			if (result is not null)
			{
				return HandleSuccessResult(result);
			}

			return HandleBadRequest(ExceptionConstants.SomethingWentWrongMessageConstant);
		}

		return HandleUnAuthorizedRequest();
	}

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
		if (IsAuthorized())
		{
			ArgumentNullException.ThrowIfNull(aiResponseFeedback);
			var result = await aiServicesHandler.PostAiResultFeedbackAsync(aiResponseFeedback, UserEmail).ConfigureAwait(false);
			if (result)
			{
				return HandleSuccessResult(result);
			}

			return HandleBadRequest(ExceptionConstants.SomethingWentWrongMessageConstant);
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
		if (IsAuthorized())
		{
			var result = await aiServicesHandler.GetSamplePromptsForChatbotAsync().ConfigureAwait(false);
			if (result is not null)
			{
				return HandleSuccessResult(result);
			}

			return HandleBadRequest(ExceptionConstants.SomethingWentWrongMessageConstant);
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
		if (IsAuthorized())
		{
			var result = await aiServicesHandler.GenerateBugSeverityAsync(bugSeverityInput).ConfigureAwait(false);
			if (result is not null)
			{
				return HandleSuccessResult(result);
			}

			return HandleBadRequest(ExceptionConstants.SomethingWentWrongMessageConstant);
		}

		return HandleUnAuthorizedRequest();
	}
}
