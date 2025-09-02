using IBBS.API.Adapters.Contracts;
using IBBS.API.Adapters.Models;
using IBBS.API.Helpers;
using InternetBulletin.Shared.DTOs.AI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using static IBBS.API.Helpers.APIConstants;
using static IBBS.API.Helpers.SwaggerConstants.AIServicesController;

namespace IBBS.API.Controllers;

/// <summary>
/// The AI Services Controller.
/// </summary>
/// <param name="aiServicesHandler">The AI Services adapter Handler.</param>
/// <param name="httpContextAccessor">The http context accessor.</param>
/// <seealso cref="IBBS.API.Controllers.BaseController" />
[ApiController]
[Route(RouteConstants.AiServicesController.BaseRoute)]
public class AIServicesController(IHttpContextAccessor httpContextAccessor, IAiServicesHandler aiServicesHandler) : BaseController(httpContextAccessor)
{
	/// <summary>
	/// Gets the about us data asynchronously.
	/// </summary>
	/// <returns>The about us page data <see cref="AboutUsAppInfoDataDTO"/></returns>
	[HttpGet(RouteConstants.AiServicesController.GetAboutUsData_Route)]
	[ProducesResponseType(typeof(AboutUsAppInfoDataDTO), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[AllowAnonymous]
	[SwaggerOperation(Summary = GetAboutUsDataAction.Summary, Description = GetAboutUsDataAction.Description, OperationId = GetAboutUsDataAction.OperationId)]
	public async Task<IActionResult> GetAboutUsDataAsync()
	{
		var result = await aiServicesHandler.GetAboutUsDataAsync().ConfigureAwait(false);
		if (result is not null)
		{
			return this.HandleSuccessResult(result);
		}

		return this.HandleBadRequest(ExceptionConstants.SomethingWentWrongMessageConstant);
	}

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
			var rewrittenStory = await aiServicesHandler.RewriteWithAIAsync(UserName, requestDto).ConfigureAwait(false);
			if (!string.IsNullOrEmpty(rewrittenStory))
			{
				return this.HandleSuccessResult(rewrittenStory);
			}

			return this.HandleBadRequest(ExceptionConstants.SomethingWentWrongMessageConstant);
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
			var tagForStory = await aiServicesHandler.GenerateTagForStoryAsync(UserName, requestDto).ConfigureAwait(false);
			if (!string.IsNullOrEmpty(tagForStory))
			{
				return this.HandleSuccessResult(tagForStory);
			}

			return this.HandleBadRequest(ExceptionConstants.SomethingWentWrongMessageConstant);
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
			var tagForStory = await aiServicesHandler.ModerateContentDataAsync(UserName, requestDto).ConfigureAwait(false);
			if (!string.IsNullOrEmpty(tagForStory))
			{
				return this.HandleSuccessResult(tagForStory);
			}

			return this.HandleBadRequest(ExceptionConstants.SomethingWentWrongMessageConstant);
		}

		return HandleUnAuthorizedRequest();
	}
}
