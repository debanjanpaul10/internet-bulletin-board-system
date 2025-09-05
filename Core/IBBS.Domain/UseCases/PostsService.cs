using IBBS.Domain.DomainEntities.Posts;
using IBBS.Domain.DrivenPorts;
using IBBS.Domain.DrivingPorts;
using IBBS.Domain.Helpers;
using InternetBulletin.Data.Contracts;
using Microsoft.Extensions.Logging;
using static IBBS.Domain.Helpers.DomainConstants;

namespace IBBS.Domain.UseCases;

/// <summary>
/// The Posts BusinessManager Class.
/// </summary>
/// <param name="logger">The logger.</param>
/// <param name="postsDataService">The Posts Data Service.</param>
/// <param name="postRatingsDataService">The post ratings data service.</param>
/// <seealso cref="IPostsService"/>
public class PostsService(ILogger<PostsService> logger, IPostsDataService postsDataService, IPostRatingsDataService postRatingsDataService) : IPostsService
{

	/// <summary>
	/// Gets the post asynchronous.
	/// </summary>
	/// <param name="postId">The post identifier.</param>
	/// <param name="userName">The user name.</param>
	/// <returns>
	/// The specific post.
	/// </returns>
	public async Task<PostDomain> GetPostAsync(string postId, string userName)
	{
		var postGuid = DomainUtilities.ValidateAndParsePostId(postId, logger);
		var result = await postsDataService.GetPostAsync(postGuid, userName, true);
		return DomainUtilities.ThrowIfNull(result, ExceptionConstants.PostNotFoundMessageConstant, logger);
	}

	/// <summary>
	/// Adds the new post asynchronous.
	/// </summary>
	/// <param name="newPost">The new post.</param>
	/// <returns>
	/// The boolean for success or failure.
	/// </returns>
	public async Task<bool> AddNewPostAsync(AddPostDomain newPost, string userName)
	{
		DomainUtilities.ThrowIfNull(newPost, ExceptionConstants.NullPostMessageConstant, logger);
		var result = await postsDataService.AddNewPostAsync(newPost, userName);
		return result;
	}

	/// <summary>
	/// Updates the post asynchronous.
	/// </summary>
	/// <param name="updatedPost">The updated post.</param>
	/// <returns>The updated post data.</returns>
	public async Task<PostDomain> UpdatePostAsync(UpdatePostDomain updatedPost, string userName)
	{
		DomainUtilities.ThrowIfNull(updatedPost, ExceptionConstants.NullPostMessageConstant, logger);
		var result = await postsDataService.UpdatePostAsync(updatedPost, userName, false);
		return DomainUtilities.ThrowIfNull(result, ExceptionConstants.PostNotFoundMessageConstant, logger);
	}

	/// <summary>
	/// Deletes the post asynchronous.
	/// </summary>
	/// <param name="postId">The post identifier.</param>
	/// <param name="userName">The user name.</param>
	/// <returns>The boolean for success / failure</returns>
	public async Task<bool> DeletePostAsync(string postId, string userName)
	{
		var postGuid = DomainUtilities.ValidateAndParsePostId(postId, logger);
		var response = await postsDataService.DeletePostAsync(postGuid, userName);
		return response;
	}

	/// <summary>
	/// Gets all posts asynchronous.
	/// </summary>
	/// <param name="userName">The user name</param>
	/// <returns>The list of <see cref="PostWithRatingsDTO"/></returns>
	public async Task<List<PostWithRatingsDomain>> GetAllPostsAsync(string userName)
	{
		if (string.IsNullOrWhiteSpace(userName)) // Consider IsNullOrWhiteSpace for robustness
		{
			var result = await postsDataService.GetAllPostsAsync();
			var postsData = result.Select(post => new PostWithRatingsDomain
			{
				PostId = post.PostId,
				PostTitle = post.PostTitle,
				PostContent = post.PostContent,
				PostCreatedDate = post.PostCreatedDate,
				PostOwnerUserName = post.PostOwnerUserName,
				Ratings = post.Ratings,
				IsActive = post.IsActive,
			}).ToList();

			return postsData;
		}
		else
		{
			var postsData = await postRatingsDataService.GetAllPostsWithRatingsAsync(userName);
			return postsData;
		}
	}

}
