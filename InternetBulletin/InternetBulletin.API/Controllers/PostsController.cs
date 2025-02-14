// *********************************************************************************
//	<copyright file="PostsController.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Posts Controller Class.</summary>
// *********************************************************************************

namespace InternetBulletin.API.Controllers
{
	using InternetBulletin.Business.Contracts;
	using InternetBulletin.Data.Entities;
	using InternetBulletin.Shared.Constants;
	using Microsoft.AspNetCore.Mvc;

	/// <summary>
	/// The Posts Controller Class.
	/// </summary>
	/// <seealso cref="InternetBulletin.API.Controllers.BaseController" />
	/// <param name="configuration">The Configuration.</param>
	/// <param name="logger">The Logger.</param>
	/// <param name="postsService">The Posts Service.</param>
	[ApiController]
	[Route(RouteConstants.PostsBase_RoutePrefix)]
	public class PostsController(IConfiguration configuration, IPostsService postsService, ILogger<PostsController> logger) : BaseController(configuration)
	{
		/// <summary>
		/// The posts service
		/// </summary>
		private readonly IPostsService _postsService = postsService;

		/// <summary>
		/// The logger
		/// </summary>
		private readonly ILogger<PostsController> _logger = logger;

		/// <summary>
		/// Gets the post asynchronous.
		/// </summary>
		/// <param name="postId">The post identifier.</param>
		/// <returns>The action result.</returns>
		[HttpGet]
		[Route(RouteConstants.GetPost_Route)]
		public async Task<IActionResult> GetPostAsync(string postId)
		{
			try
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(GetPostAsync), DateTime.UtcNow, postId));
				if (await this.IsAuthorized())
				{
					var result = await this._postsService.GetPostAsync(postId);
					if (result is not null && !(Equals(result.PostId, Guid.Empty)))
					{
						return this.HandleSuccessResult(result);
					}
					else
					{
						return this.HandleBadRequest(ExceptionConstants.PostNotFoundMessageConstant);
					}
				}

				return this.HandleUnAuthorizedRequest();
			}
			catch (Exception ex)
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodFailed, nameof(GetPostAsync), DateTime.UtcNow, ex.Message));
				throw;
			}
			finally
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetPostAsync), DateTime.UtcNow, postId));
			}
		}

		/// <summary>
		/// Adds the new post asynchronous.
		/// </summary>
		/// <param name="newPost">The new post.</param>
		/// <returns>The action result of JSON response.</returns>
		[HttpPost]
		[Route(RouteConstants.NewPost_Route)]
		public async Task<IActionResult> AddNewPostAsync(Post newPost)
		{
			try
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(AddNewPostAsync), DateTime.UtcNow, newPost.PostId));
				if (await this.IsAuthorized())
				{
					var result = await this._postsService.AddNewPostAsync(newPost);
					if (result)
					{
						return this.HandleSuccessResult(result);
					}
					else
					{
						return this.HandleBadRequest(ExceptionConstants.SomethingWentWrongMessageConstant);
					}
				}

				return this.HandleUnAuthorizedRequest();
			}
			catch (Exception ex)
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodFailed, nameof(AddNewPostAsync), DateTime.UtcNow, ex.Message));
				throw;
			}
			finally
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(AddNewPostAsync), DateTime.UtcNow, newPost.PostId));
			}
		}
	}
}
