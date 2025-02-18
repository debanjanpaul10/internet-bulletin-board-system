// *********************************************************************************
//	<copyright file="PostsDataService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Posts Data Manager Class.</summary>
// *********************************************************************************

namespace InternetBulletin.Data.DataServices
{
	using InternetBulletin.Data.Contracts;
	using InternetBulletin.Data.Entities;
	using InternetBulletin.Shared.Constants;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Logging;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	/// <summary>
	/// The Posts DataManager Class.
	/// </summary>
	/// <param name="cosmosDbContext">The Cosmos DB Context.</param>
	/// <param name="logger">The Logger.</param>
	public class PostsDataService(CosmosDbContext cosmosDbContext, ILogger<PostsDataService> logger) : IPostsDataService
	{
		/// <summary>
		/// The Cosmos database context
		/// </summary>
		private readonly CosmosDbContext _cosmosDbContext = cosmosDbContext;

		/// <summary>
		/// The logger
		/// </summary>
		private readonly ILogger<PostsDataService> _logger = logger;

		/// <summary>
		/// Gets the post asynchronous.
		/// </summary>
		/// <param name="postId">The post identifier.</param>
		/// <returns>
		/// The specific post.
		/// </returns>
		public async Task<Post> GetPostAsync(Guid postId)
		{
			try
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(GetPostAsync), DateTime.UtcNow, postId));

				var post = await this._cosmosDbContext.Posts.FirstOrDefaultAsync(p => p.PostId == postId && p.IsActive);
				return post ?? new Post();
			}
			catch (Exception ex)
			{
				this._logger.LogError(ex, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(GetPostAsync), DateTime.UtcNow, ex.Message));
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
		/// <returns>
		/// The boolean for success or failure.
		/// </returns>
		public async Task<bool> AddNewPostAsync(Post newPost)
		{
			try
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(AddNewPostAsync), DateTime.UtcNow, newPost.PostId));

				var postId = Guid.NewGuid();
				var existingPost = await this._cosmosDbContext.Posts.AnyAsync(x => x.PostId == postId && x.IsActive);
				if (!existingPost)
				{
					newPost.PostId = postId;
					await this._cosmosDbContext.Posts.AddAsync(newPost);
					await this._cosmosDbContext.SaveChangesAsync();
					return true;
				}
				else
				{
					var exception = new Exception(ExceptionConstants.PostExistsMessageConstant);
					this._logger.LogError(exception, exception.Message);

					throw exception;
				}
			}
			catch (DbUpdateException dbEx)
			{
				this._logger.LogError(dbEx, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(AddNewPostAsync), DateTime.UtcNow, dbEx.Message));
				return false;
			}
			catch (Exception ex)
			{
				this._logger.LogError(ex, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(AddNewPostAsync), DateTime.UtcNow, ex.Message));
				return false;
			}
			finally
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(AddNewPostAsync), DateTime.UtcNow, newPost.PostId));
			}
		}

		/// <summary>
		/// Updates the post asynchronous.
		/// </summary>
		/// <param name="updatedPost">The updated post.</param>
		/// <returns></returns>
		public async Task<Post> UpdatePostAsync(Post updatedPost)
		{
			try
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(AddNewPostAsync), DateTime.UtcNow, updatedPost.PostId));

				var dbPostData = await this._cosmosDbContext.Posts.FirstOrDefaultAsync(x => x.PostId == updatedPost.PostId && x.IsActive);
				if (dbPostData is not null)
				{
					dbPostData.PostTitle = updatedPost.PostTitle;
					dbPostData.PostContent = updatedPost.PostContent;
					dbPostData.PostModifiedDate = DateTime.UtcNow;
					dbPostData.PostModifiedBy = updatedPost.PostModifiedBy;

					await this._cosmosDbContext.SaveChangesAsync();

					return dbPostData;
				}
				else
				{
					var exception = new Exception(ExceptionConstants.PostNotFoundMessageConstant);
					this._logger.LogError(exception, exception.Message);
					throw exception;
				}
			}
			catch (DbUpdateException dbEx)
			{
				this._logger.LogError(dbEx, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(UpdatePostAsync), DateTime.UtcNow, dbEx.Message));
				return new Post();
			}
			catch (Exception ex)
			{
				this._logger.LogError(ex, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(UpdatePostAsync), DateTime.UtcNow, ex.Message));
				return new Post();
			}
			finally
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(UpdatePostAsync), DateTime.UtcNow, updatedPost.PostId));
			}
		}

		/// <summary>
		/// Deletes the post asynchronous.
		/// </summary>
		/// <param name="postId">The post identifier.</param>
		/// <returns>
		/// The boolean for success / failure
		/// </returns>
		public async Task<bool> DeletePostAsync(Guid postId)
		{
			try
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(DeletePostAsync), DateTime.UtcNow, postId));
				var dbPostData = await this._cosmosDbContext.Posts.FirstOrDefaultAsync(post => post.PostId == postId && post.IsActive);
				if (dbPostData is not null)
				{
					dbPostData.IsActive = false;
					await this._cosmosDbContext.SaveChangesAsync();

					return true;
				}
				else
				{
					var exception = new Exception(ExceptionConstants.PostNotFoundMessageConstant);
					this._logger.LogError(exception, exception.Message);
					throw exception;
				}

			}
			catch (DbUpdateException dbEx)
			{
				this._logger.LogError(dbEx, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(DeletePostAsync), DateTime.UtcNow, dbEx.Message));
				return false;
			}
			catch (Exception ex)
			{
				this._logger.LogError(ex, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(DeletePostAsync), DateTime.UtcNow, ex.Message));
				return false;
			}
			finally
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(DeletePostAsync), DateTime.UtcNow, postId));
			}
		}

		/// <summary>
		/// Gets all posts asynchronous.
		/// </summary>
		/// <returns>The list of <see cref="Post"/></returns>
		public async Task<List<Post>> GetAllPostsAsync()
		{
			try
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(GetAllPostsAsync), DateTime.UtcNow, string.Empty));
				
				var result = await this._cosmosDbContext.Posts.Where(x => x.IsActive).ToListAsync();
				return result;
			}
			catch (Exception ex)
			{
				this._logger.LogError(ex, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(GetAllPostsAsync), DateTime.UtcNow, ex.Message));
				throw;
			}
			finally
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetAllPostsAsync), DateTime.UtcNow, string.Empty));
			}
		}
	}
}
