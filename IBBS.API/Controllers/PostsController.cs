using IBBS.API.Adapters.Contracts;
using IBBS.API.Adapters.Models.Posts;
using IBBS.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static IBBS.API.Helpers.APIConstants;

namespace IBBS.API.Controllers;

/// <summary>
/// The Posts Controller Class.
/// </summary>
/// <seealso cref="BaseController" />
/// <param name="httpContextAccessor">The http context accessor.</param>
/// <param name="logger">The Logger.</param>
/// <param name="postsHandler">The Posts api adapter handler.</param>
[ApiController]
[Route(RouteConstants.PostsController.BaseRoute)]
public class PostsController(ILogger<PostsController> logger, IHttpContextAccessor httpContextAccessor, IPostsHandler postsHandler) : BaseController(httpContextAccessor)
{
	/// <summary>
	/// Gets all posts data asynchronous.
	/// </summary>
	/// <returns>The action result.</returns>
	[HttpGet]
	[AllowAnonymous]
	[Route(RouteConstants.PostsController.GetAllPosts_Route)]
	public async Task<IActionResult> GetAllPostsDataAsync()
	{
		try
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(GetPostAsync), DateTime.UtcNow, string.Empty));
			var result = await postsHandler.GetAllPostsAsync(UserName ?? string.Empty);
			if (result is not null && result.Any())
			{
				return HandleSuccessResult(result);
			}
			else
			{
				return this.HandleBadRequest(ExceptionConstants.PostsNotPresentMessageConstant);
			}
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
	/// Gets the post asynchronous.
	/// </summary>
	/// <param name="postId">The post identifier.</param>
	/// <returns>The action result.</returns>
	[HttpGet]
	[Route(RouteConstants.PostsController.GetPost_Route)]
	public async Task<IActionResult> GetPostAsync(string postId)
	{
		try
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(GetPostAsync), DateTime.UtcNow, postId));
			if (IsAuthorized())
			{
				var result = await postsHandler.GetPostAsync(postId, UserName);
				if (result is not null && !Equals(result.PostId, Guid.Empty))
				{
					return HandleSuccessResult(result);
				}
				else
				{
					return this.HandleBadRequest(ExceptionConstants.PostNotFoundMessageConstant);
				}
			}

			return HandleUnAuthorizedRequest();

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

	/// <summary>
	/// Adds the new post asynchronous.
	/// </summary>
	/// <param name="newPost">The new post.</param>
	/// <returns>The action result of JSON response.</returns>
	[HttpPost]
	[Route(RouteConstants.PostsController.NewPost_Route)]
	public async Task<IActionResult> AddNewPostAsync(AddPostDTO newPost)
	{
		try
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(AddNewPostAsync), DateTime.UtcNow, newPost.PostTitle));
			if (IsAuthorized())
			{
				var result = await postsHandler.AddNewPostAsync(newPost, UserName);
				if (result)
				{
					return this.HandleSuccessResult(result);
				}
				else
				{
					return this.HandleBadRequest(ExceptionConstants.SomethingWentWrongMessageConstant);
				}
			}

			return HandleUnAuthorizedRequest();
		}
		catch (Exception ex)
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodFailed, nameof(AddNewPostAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(AddNewPostAsync), DateTime.UtcNow, newPost.PostTitle));
		}
	}

	/// <summary>
	/// Updates the post asynchronous.
	/// </summary>
	/// <param name="updatePost">The update post.</param>
	/// <returns>The action result of the JSON response.</returns>
	[HttpPost]
	[Route(RouteConstants.PostsController.UpdatePost_Route)]
	public async Task<IActionResult> UpdatePostAsync(UpdatePostDTO updatePost)
	{
		try
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(UpdatePostAsync), DateTime.UtcNow, updatePost.PostId));
			if (IsAuthorized())
			{
				var result = await postsHandler.UpdatePostAsync(updatePost, UserName);
				if (result is not null && result.PostId != Guid.Empty)
				{
					return this.HandleSuccessResult(result);
				}
				else
				{
					return this.HandleBadRequest(ExceptionConstants.SomethingWentWrongMessageConstant);
				}
			}

			return HandleUnAuthorizedRequest();
		}
		catch (Exception ex)
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodFailed, nameof(UpdatePostAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(UpdatePostAsync), DateTime.UtcNow, updatePost.PostId));
		}
	}

	/// <summary>
	/// Deletes the post asynchronous.
	/// </summary>
	/// <param name="postId">The post identifier.</param>
	/// <returns>The action result of the JSON response.</returns>
	[HttpPost]
	[Route(RouteConstants.PostsController.DeletePost_Route)]
	public async Task<IActionResult> DeletePostAsync(string postId)
	{
		try
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(DeletePostAsync), DateTime.UtcNow, postId));
			if (IsAuthorized())
			{
				var result = await postsHandler.DeletePostAsync(postId, UserName);
				if (result)
				{
					return HandleSuccessResult(result);
				}
				else
				{
					return this.HandleBadRequest(ExceptionConstants.SomethingWentWrongMessageConstant);
				}
			}

			return HandleUnAuthorizedRequest();
		}
		catch (Exception ex)
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodFailed, nameof(DeletePostAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(DeletePostAsync), DateTime.UtcNow, postId));
		}
	}
}
