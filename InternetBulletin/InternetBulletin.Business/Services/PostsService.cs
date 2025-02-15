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
	using Microsoft.Extensions.Logging;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	/// <summary>
	/// The Posts BusinessManager Class.
	/// </summary>
	/// <param name="postsDataService">The Posts Data Service.</param>
	/// <param name="logger">The logger.</param>
	public class PostsService(IPostsDataService postsDataService, ILogger<PostsService> logger) : IPostsService
	{
		/// <summary>
		/// The posts data service
		/// </summary>
		private readonly IPostsDataService _postsDataService = postsDataService;

		/// <summary>
		/// The logger
		/// </summary>
		private readonly ILogger<PostsService> _logger = logger;

		/// <summary>
		/// Gets the post asynchronous.
		/// </summary>
		/// <param name="postId">The post identifier.</param>
		/// <returns>
		/// The specific post.
		/// </returns>
		public async Task<Post> GetPostAsync(string postId)
		{
			if (string.IsNullOrWhiteSpace(postId))
			{
				var exception = new ArgumentNullException(ExceptionConstants.PostIdNotPresentMessageConstant);
				this._logger.LogError(exception, exception.Message);

				throw exception;
			}

			if (!Guid.TryParse(postId, out var postGuid))
			{
				var exception = new FormatException(ExceptionConstants.PostGuidNotValidMessageConstant);
				this._logger.LogError(exception, exception.Message);

				throw exception;
			}

			var result = await this._postsDataService.GetPostAsync(postGuid);
			if (result is not null && result.PostId != Guid.Empty)
			{
				return result;
			}
			else
			{
				var exception = new FileNotFoundException(ExceptionConstants.PostNotFoundMessageConstant);
				this._logger.LogError(exception, exception.Message);

				throw exception;
			}
		}

		/// <summary>
		/// Adds the new post asynchronous.
		/// </summary>
		/// <param name="newPost">The new post.</param>
		/// <returns>
		/// The boolean for success or failure.
		/// </returns>
		public async Task<bool> AddNewPostAsync(Post newPost)
		{
			if (newPost is null)
			{
				var exception = new ArgumentNullException(ExceptionConstants.NullPostMessageConstant);
				this._logger.LogError(exception, exception.Message);

				throw exception;
			}

			var result = await this._postsDataService.AddNewPostAsync(newPost);
			return result;
		}

		/// <summary>
		/// Updates the post asynchronous.
		/// </summary>
		/// <param name="updatedPost">The updated post.</param>
		/// <returns>The updated post data.</returns>
		public async Task<Post> UpdatePostAsync(Post updatedPost)
		{
			if (updatedPost is null)
			{
				var exception = new ArgumentNullException(ExceptionConstants.NullPostMessageConstant);
				this._logger.LogError(exception, exception.Message);

				throw exception;
			}

			var result = await this._postsDataService.UpdatePostAsync(updatedPost);
			return result;
		}

		/// <summary>
		/// Deletes the post asynchronous.
		/// </summary>
		/// <param name="postId">The post identifier.</param>
		/// <returns>The boolean for success / failure</returns>
		public async Task<bool> DeletePostAsync(string postId)
		{
			if (string.IsNullOrWhiteSpace(postId))
			{
				var exception = new ArgumentNullException(ExceptionConstants.PostIdNotPresentMessageConstant);
				this._logger.LogError(exception, exception.Message);

				throw exception;
			}

			if (!Guid.TryParse(postId, out var postIdGuid))
			{
				var exception = new FormatException(ExceptionConstants.PostGuidNotValidMessageConstant);
				this._logger.LogError(exception, exception.Message);

				throw exception;
			}

			var result = await this._postsDataService.DeletePostAsync(postIdGuid);
			return result;
		}

		/// <summary>
		/// Gets all posts asynchronous.
		/// </summary>
		/// <returns>The list of <see cref="Post"/></returns>
		public async Task<List<Post>> GetAllPostsAsync()
		{
			var result = await this._postsDataService.GetAllPostsAsync();
			return result;
		}
	}
}
