using IBBS.API.Adapters.Contracts;
using IBBS.API.Adapters.Models.Posts;
using IBBS.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using static IBBS.API.Helpers.APIConstants;
using static IBBS.API.Helpers.SwaggerConstants.PostsController;

namespace IBBS.API.Controllers.v1;

/// <summary>
/// The Posts Controller Class.
/// </summary>
/// <seealso cref="BaseController" />
/// <param name="httpContextAccessor">The http context accessor.</param>
/// <param name="postsHandler">The Posts api adapter handler.</param>
/// <param name="configuration">The configuration service.</param>
[ApiController]
[Route(RouteConstants.PostsController.BaseRoute)]
public sealed class PostsController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IPostsHandler postsHandler) : BaseController(httpContextAccessor, configuration)
{
    /// <summary>
    /// Gets all posts data asynchronous.
    /// </summary>
    /// <returns>The action result.</returns>
    [HttpGet(RouteConstants.PostsController.GetAllPosts_Route)]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<PostWithRatingsDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = GetAllPostsDataAction.Summary, Description = GetAllPostsDataAction.Description, OperationId = GetAllPostsDataAction.OperationId)]
    public async Task<IActionResult> GetAllPostsDataAsync()
    {
        var result = await postsHandler.GetAllPostsAsync(UserEmail ?? string.Empty);
        if (result is not null) return HandleSuccessResult(result);
        else return HandleBadRequest(ExceptionConstants.SomethingWentWrongMessageConstant);
    }

    /// <summary>
    /// Gets the post asynchronous.
    /// </summary>
    /// <param name="postId">The post identifier.</param>
    /// <returns>The action result.</returns>
    [HttpGet(RouteConstants.PostsController.GetPost_Route)]
    [ProducesResponseType(typeof(PostDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = GetPostAction.Summary, Description = GetPostAction.Description, OperationId = GetPostAction.OperationId)]
    public async Task<IActionResult> GetPostAsync(string postId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(postId);
        if (base.IsAuthorized(AuthorizationTypes.UserBased))
        {
            var result = await postsHandler.GetPostAsync(postId, UserEmail);
            if (result is not null && !Equals(result.PostId, Guid.Empty)) return HandleSuccessResult(result);
            else return this.HandleBadRequest(ExceptionConstants.PostNotFoundMessageConstant);
        }

        return HandleUnAuthorizedRequest();
    }

    /// <summary>
    /// Adds the new post asynchronous.
    /// </summary>
    /// <param name="newPost">The new post.</param>
    /// <returns>The action result of JSON response.</returns>
    [HttpPost(RouteConstants.PostsController.NewPost_Route)]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = AddNewPostAction.Summary, Description = AddNewPostAction.Description, OperationId = AddNewPostAction.OperationId)]
    public async Task<IActionResult> AddNewPostAsync(AddPostDTO newPost)
    {
        ArgumentNullException.ThrowIfNull(newPost);
        if (base.IsAuthorized(AuthorizationTypes.UserBased))
        {
            var result = await postsHandler.AddNewPostAsync(newPost, UserEmail);
            if (result) return this.HandleSuccessResult(result);
            else return this.HandleBadRequest(ExceptionConstants.SomethingWentWrongMessageConstant);
        }

        return HandleUnAuthorizedRequest();
    }

    /// <summary>
    /// Updates the post asynchronous.
    /// </summary>
    /// <param name="updatePost">The update post.</param>
    /// <returns>The action result of the JSON response.</returns>
    [HttpPost(RouteConstants.PostsController.UpdatePost_Route)]
    [ProducesResponseType(typeof(PostDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = UpdatePostAction.Summary, Description = UpdatePostAction.Description, OperationId = UpdatePostAction.OperationId)]
    public async Task<IActionResult> UpdatePostAsync(UpdatePostDTO updatePost)
    {
        ArgumentNullException.ThrowIfNull(updatePost);
        if (base.IsAuthorized(AuthorizationTypes.UserBased))
        {
            var result = await postsHandler.UpdatePostAsync(updatePost, UserEmail);
            if (result is not null && result.PostId != Guid.Empty) return this.HandleSuccessResult(result);
            else return this.HandleBadRequest(ExceptionConstants.SomethingWentWrongMessageConstant);
        }

        return HandleUnAuthorizedRequest();
    }

    /// <summary>
    /// Deletes the post asynchronous.
    /// </summary>
    /// <param name="postId">The post identifier.</param>
    /// <returns>The action result of the JSON response.</returns>
    [HttpPost(RouteConstants.PostsController.DeletePost_Route)]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = DeletePostAction.Summary, Description = DeletePostAction.Description, OperationId = DeletePostAction.OperationId)]
    public async Task<IActionResult> DeletePostAsync(string postId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(postId);
        if (base.IsAuthorized(AuthorizationTypes.UserBased))
        {
            var result = await postsHandler.DeletePostAsync(postId, UserEmail);
            if (result) return HandleSuccessResult(result);
            else return this.HandleBadRequest(ExceptionConstants.SomethingWentWrongMessageConstant);
        }

        return HandleUnAuthorizedRequest();
    }
}
