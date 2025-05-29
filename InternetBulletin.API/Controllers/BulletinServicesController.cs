// *********************************************************************************
//	<copyright file="BulletinServicesController.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Bulletin Services Controller Class.</summary>
// *********************************************************************************

namespace InternetBulletin.API.Controllers
{
	using InternetBulletin.Business.Contracts;
	using InternetBulletin.Shared.Constants;
	using Microsoft.AspNetCore.Mvc;

	/// <summary>
	/// Bulletin services class.
	/// </summary>
	/// <param name="logger">The logger.</param>
	/// <param name="httpContextAccessor">The http context accessor.</param>
	/// <param name="usersService">The user services.</param>
	/// <param name="postsService">The posts services.</param>
	/// <seealso cref="BaseController"/>
	[ApiController]
	[Route(RouteConstants.BulletinServicesBase_RoutePrefix)]
	public class BulletinServicesController(ILogger<BulletinServicesController> logger, IHttpContextAccessor httpContextAccessor, IUsersService usersService, IPostsService postsService) : BaseController(httpContextAccessor)
	{
		/// <summary>
		/// The logger.
		/// </summary>
		private readonly ILogger<BulletinServicesController> _logger = logger;

		/// <summary>
		/// The user services.
		/// </summary>
		private readonly IUsersService _usersService = usersService;

		/// <summary>
		/// The posts service.
		/// </summary>
		private readonly IPostsService _postsService = postsService;

		/// <summary>
		/// Gets graph user data async.
		/// </summary>
		/// <param name="userName">The user name.</param>
		/// <summary>
		/// Retrieves user data from the Graph API for the specified username if the request is authorized.
		/// </summary>
		/// <param name="userName">The username for which to retrieve Graph API user data.</param>
		/// <returns>An <see cref="IActionResult"/> containing the user data if found and authorized; otherwise, a bad request or unauthorized response.</returns>
		[HttpGet]
		[Route(RouteConstants.GetUsersDataFromGraph_Route)]
		public async Task<IActionResult> GetUsersDataFromGraphApiAsync(string userName)
		{
			try
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(GetUsersDataFromGraphApiAsync), DateTime.UtcNow, this.UserName));
				if (this.IsAuthorized())
				{
					var result = await this._usersService.GetGraphUserDataAsync(userName);
					if (result is not null)
					{
						return this.HandleSuccessResult(result);
					}
					else
					{
						return this.HandleBadRequest(ExceptionConstants.UserIdNotPresentExceptionConstant);
					}
				}

				return this.HandleUnAuthorizedRequest();

			}
			catch (Exception ex)
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodFailed, nameof(GetUsersDataFromGraphApiAsync), DateTime.UtcNow, ex.Message));
				throw;
			}
			finally
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetUsersDataFromGraphApiAsync), DateTime.UtcNow, this.UserName));
			}
		}

		/// <summary>
		/// Rewrites the with ai asynchronous.
		/// </summary>
		/// <param name="story">The story.</param>
		/// <summary>
		/// Rewrites the provided story using AI and returns the rewritten version.
		/// </summary>
		/// <param name="story">The original story to be rewritten.</param>
		/// <returns>An HTTP response containing the AI-rewritten story if successful; otherwise, a bad request or unauthorized response.</returns>
		[HttpPost]
		[Route(RouteConstants.RewriteWithAI_Route)]
		public async Task<IActionResult> RewriteWithAIAsync([FromBody] string story)
		{
			try
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(RewriteWithAIAsync), DateTime.UtcNow, this.UserName));
				if (this.IsAuthorized())
				{
					var rewrittenStory = await this._postsService.RewriteWithAIAsync(story);
					if (!string.IsNullOrEmpty(rewrittenStory))
					{
						return this.HandleSuccessResult(rewrittenStory);
					}

					return this.HandleBadRequest(ExceptionConstants.SomethingWentWrongMessageConstant);
				}

				return this.HandleUnAuthorizedRequest();

			}
			catch (Exception ex)
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodFailed, nameof(RewriteWithAIAsync), DateTime.UtcNow, ex.Message));
				throw;
			}
			finally
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(RewriteWithAIAsync), DateTime.UtcNow, this.UserName));
			}
		}

	}

}

