// *********************************************************************************
//	<copyright file="PostsService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Posts BusinessManager Class.</summary>
// *********************************************************************************

namespace InternetBulletin.Business.Services
{
	using InternetBulletin.Business.Contracts;
	using InternetBulletin.Data.Contracts;
	using InternetBulletin.Data.Entities;
	using InternetBulletin.Shared.Constants;
	using InternetBulletin.Shared.DTOs;
	using InternetBulletin.Shared.DTOs.Posts;
	using InternetBulletin.Shared.Helpers;
	using Microsoft.Extensions.Logging;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	/// <summary>
	/// The Posts BusinessManager Class.
	/// </summary>
	/// <param name="postsDataService">The Posts Data Service.</param>
	/// <param name="logger">The logger.</param>
	public class PostsService(IPostsDataService postsDataService, IPostRatingsDataService postRatingsDataService, ILogger<PostsService> logger) : IPostsService
	{
		/// <summary>
		/// The posts data service
		/// </summary>
		private readonly IPostsDataService _postsDataService = postsDataService;

		/// <summary>
		/// The post ratings data service.
		/// </summary>
		private readonly IPostRatingsDataService _postRatingsDataService = postRatingsDataService;

		/// <summary>
		/// The logger
		/// </summary>
		private readonly ILogger<PostsService> _logger = logger;

		/// <summary>
		/// Gets the post asynchronous.
		/// </summary>
		/// <param name="postId">The post identifier.</param>
		/// <param name="userName">The user name.</param>
		/// <returns>
		/// The specific post.
		/// </returns>
		public async Task<Post> GetPostAsync(string postId, string userName)
		{
			var postGuid = ValidateAndParsePostId(postId);
			var result = await _postsDataService.GetPostAsync(postGuid, userName);
			return CommonUtilities.ThrowIfNull(result, ExceptionConstants.PostNotFoundMessageConstant, this._logger);
		}

		/// <summary>
		/// Adds the new post asynchronous.
		/// </summary>
		/// <param name="newPost">The new post.</param>
		/// <returns>
		/// The boolean for success or failure.
		/// </returns>
		public async Task<bool> AddNewPostAsync(AddPostDTO newPost, string userName)
		{
			CommonUtilities.ThrowIfNull(newPost, ExceptionConstants.NullPostMessageConstant, this._logger);
			var result = await _postsDataService.AddNewPostAsync(newPost, userName);
			return result;
		}

		/// <summary>
		/// Updates the post asynchronous.
		/// </summary>
		/// <param name="updatedPost">The updated post.</param>
		/// <returns>The updated post data.</returns>
		public async Task<Post> UpdatePostAsync(UpdatePostDTO updatedPost, string userName)
		{
			CommonUtilities.ThrowIfNull(updatedPost, ExceptionConstants.NullPostMessageConstant, this._logger);
			var result = await _postsDataService.UpdatePostAsync(updatedPost, userName);
			return CommonUtilities.ThrowIfNull(result, ExceptionConstants.PostNotFoundMessageConstant, this._logger);
		}

		/// <summary>
		/// Deletes the post asynchronous.
		/// </summary>
		/// <param name="postId">The post identifier.</param>
		/// <param name="userName">The user name.</param>
		/// <returns>The boolean for success / failure</returns>
		public async Task<bool> DeletePostAsync(string postId, string userName)
		{
			var postGuid = ValidateAndParsePostId(postId);
			var response = await _postsDataService.DeletePostAsync(postGuid, userName);
			return response;
		}

		/// <summary>
		/// Gets all posts asynchronous.
		/// </summary>
		/// <returns>The list of <see cref="Post"/></returns>
		public async Task<List<Post>> GetAllPostsAsync()
		{
			var result = await _postsDataService.GetAllPostsAsync();
			return result;
		}

		/// <summary>
		/// Updates rating async.
		/// </summary>
		/// <param name="postId">The post id.</param>
		/// <param name="isIncrement">If the rating is increased.</param>
		/// <param name="userName">The current user name</param>
		/// <returns>The update rating data dto.</returns>
		public async Task<UpdateRatingDto> UpdateRatingAsync(string postId, bool isIncrement, string userName)
		{
			var postIdGuid = this.ValidateAndParsePostId(postId);
			var (post, postRating) = await this.GetPostAndRatingAsync(postIdGuid, userName);
			CommonUtilities.ThrowIfNull(post, ExceptionConstants.PostNotFoundMessageConstant, this._logger);

			if (postRating is null)
			{
				return await this.HandleRatingAsync(post, postIdGuid, userName, isFirstTime: true);
			}
			else
			{
				return await this.HandleRatingAsync(post, postIdGuid, userName, isFirstTime: false, postRating);
			}
		}

		#region PRIVATE Methods

		/// <summary>
		/// Gets post and rating async.
		/// </summary>
		/// <param name="postIdGuid">The post id guid.</param>
		/// <param name="userName">The user name.</param>
		/// <returns>The tupple containing post and post rating.</returns>
		private async Task<(Post post, PostRating postRating)> GetPostAndRatingAsync(Guid postIdGuid, string userName)
		{
			var postTask = this._postsDataService.GetPostAsync(postIdGuid, userName);
			var postRatingTask = this._postRatingsDataService.GetPostRatingAsync(postIdGuid, userName);
			await Task.WhenAll(postTask, postRatingTask).ConfigureAwait(false);
			return (postTask.Result, postRatingTask.Result);
		}

		/// <summary>
		/// Handles both first time and already rated logic.
		/// </summary>
		private async Task<UpdateRatingDto> HandleRatingAsync(Post post, Guid postIdGuid, string userName, bool isFirstTime, PostRating postRating = null)
		{
			if (isFirstTime)
			{
				post.Ratings += 1;
				var newRating = new PostRating
				{
					PostId = postIdGuid,
					UserName = userName,
					RatedOn = DateTime.UtcNow
				};
				await this._postsDataService.UpdatePostAsync(CreateUpdatePostDTO(post), userName);
				await this._postRatingsDataService.AddPostRatingAsync(newRating);
				return new UpdateRatingDto { HasAlreadyUpdated = false, IsUpdateSuccess = true };
			}
			else
			{
				post.Ratings = post.Ratings > 0 ? post.Ratings - 1 : 0;
				await this._postsDataService.UpdatePostAsync(CreateUpdatePostDTO(post), userName);
				await this._postRatingsDataService.UpdatePostRatingAsync(postRating);
				return new UpdateRatingDto { HasAlreadyUpdated = true, IsUpdateSuccess = true };
			}
		}

		/// <summary>
		/// Creates update post dto.
		/// </summary>
		/// <param name="post">The post.</param>
		private static UpdatePostDTO CreateUpdatePostDTO(Post post)
		{
			return new UpdatePostDTO
			{
				PostContent = post.PostContent,
				PostRating = post.Ratings,
				PostId = post.PostId,
				PostTitle = post.PostTitle
			};
		}

		/// <summary>
		/// Validates and parse post id.
		/// </summary>
		/// <param name="postId">The post id.</param>
		private Guid ValidateAndParsePostId(string postId)
		{
			if (string.IsNullOrWhiteSpace(postId))
			{
				CommonUtilities.ThrowLoggedException(ExceptionConstants.PostIdNotPresentMessageConstant, this._logger);
			}

			if (!Guid.TryParse(postId, out var postGuid))
			{
				CommonUtilities.ThrowLoggedException(ExceptionConstants.PostGuidNotValidMessageConstant, this._logger);
			}

			return postGuid;
		}

		#endregion
	}
}
