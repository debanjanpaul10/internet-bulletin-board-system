using IBBS.API.Adapters.Contracts;
using IBBS.API.Adapters.Models;
using IBBS.API.Helpers;
using InternetBulletin.Shared.DTOs.AI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static IBBS.API.Helpers.APIConstants;

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
	[HttpGet]
	[Route(RouteConstants.AiServicesController.GetAboutUsData_Route)]
	[AllowAnonymous]
	public async Task<AboutUsAppInfoDataDTO> GetAboutUsDataAsync()
	{
		var result = await aiServicesHandler.GetAboutUsDataAsync().ConfigureAwait(false);
		return result;
	}

	/// <summary>
	/// Rewrites the with ai asynchronous.
	/// </summary>
	/// <param name="requestDto">The request dto.</param>
	/// <returns>The ai rewritten response.</returns>
	[HttpPost]
	[Route(RouteConstants.AiServicesController.RewriteWithAI_Route)]
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
	[HttpPost]
	[Route(RouteConstants.AiServicesController.GenerateGenreTag_Route)]
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
	[HttpPost]
	[Route(RouteConstants.AiServicesController.ModerateContent_Route)]
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
