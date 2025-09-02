using IBBS.Domain.DomainEntities.Posts;
using IBBS.Domain.DrivenPorts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static IBBS.Infrastructure.Persistence.Adapters.Helpers.Constants;

namespace IBBS.Infrastructure.Persistence.Adapters.DataServices;

/// <summary>
/// The posts data service.
/// </summary>
/// <param name="dbContext">The database context.</param>
/// <param name="logger">The logger service.</param>
/// <seealso cref="IBBS.Domain.DrivenPorts.IPostsDataService" />
public class PostsDataService(SqlDbContext dbContext, ILogger<PostsDataService> logger) : IPostsDataService
{
	/// <summary>
	/// Gets the post asynchronous.
	/// </summary>
	/// <param name="postId">The post identifier.</param>
	/// <param name="userName">The user name.</param>
	/// <param name="isForCurrentUser">Checks if requested for the current user</param>
	/// <returns>
	/// The specific post.
	/// </returns>
	public async Task<PostDomain> GetPostAsync(Guid postId, string userName, bool isForCurrentUser)
	{
		try
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(GetPostAsync), DateTime.UtcNow, postId));

			var query = dbContext.Posts.Where(p => p.PostId == postId && p.IsActive);
			query = isForCurrentUser
				? query.Where(p => p.PostOwnerUserName == userName)
				: query.Where(p => p.PostOwnerUserName != userName);

			var result = await query.FirstOrDefaultAsync() ?? new PostDomain();
			return result;
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(GetPostAsync), DateTime.UtcNow, ex.Message));
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
	/// <returns>
	/// The boolean for success or failure.
	/// </returns>
	public async Task<bool> AddNewPostAsync(AddPostDomain newPost, string userName)
	{
		try
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(AddNewPostAsync), DateTime.UtcNow, newPost.PostTitle));

			var postId = Guid.NewGuid();
			var existingPost = await dbContext.Posts.AnyAsync(x => x.PostId == postId && x.IsActive);
			if (!existingPost)
			{
				var dbPostData = new PostDomain()
				{
					PostId = postId,
					PostContent = newPost.PostContent,
					PostTitle = newPost.PostTitle,
					IsActive = true,
					PostCreatedDate = DateTime.UtcNow,
					PostOwnerUserName = userName,
					Ratings = 0
				};
				await dbContext.Posts.AddAsync(dbPostData);
				await dbContext.SaveChangesAsync();
				return true;
			}
			else
			{
				var exception = new Exception(ExceptionConstants.PostExistsMessageConstant);
				logger.LogError(exception, exception.Message);

				throw exception;
			}
		}
		catch (DbUpdateException dbEx)
		{
			logger.LogError(dbEx, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(AddNewPostAsync), DateTime.UtcNow, dbEx.Message));
			throw;
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(AddNewPostAsync), DateTime.UtcNow, ex.Message));
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
	/// <param name="updatedPost">The updated post.</param>
	/// <param name="userName">The user name</param>
	/// <param name="isRatingUpdate">The boolean flag to signify rating update.</param>
	/// <returns>The updated post data.</returns>
	public async Task<PostDomain> UpdatePostAsync(UpdatePostDomain updatedPost, string userName, bool isRatingUpdate)
	{
		try
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(AddNewPostAsync), DateTime.UtcNow, updatedPost.PostId));

			if (isRatingUpdate)
			{
				return await HandleRatingUpdateForPostAsync(updatedPost);
			}
			else
			{
				var dbPostData = await dbContext.Posts.FirstOrDefaultAsync(x => x.PostId == updatedPost.PostId && x.IsActive && x.PostOwnerUserName == userName);
				if (dbPostData is not null)
				{
					dbPostData.PostTitle = updatedPost.PostTitle;
					dbPostData.PostContent = updatedPost.PostContent;

					await dbContext.SaveChangesAsync();
					return dbPostData;
				}
				else
				{
					var exception = new Exception(ExceptionConstants.PostNotFoundMessageConstant);
					logger.LogError(exception, exception.Message);
					throw exception;
				}
			}

		}
		catch (DbUpdateException dbEx)
		{
			logger.LogError(dbEx, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(UpdatePostAsync), DateTime.UtcNow, dbEx.Message));
			throw;
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(UpdatePostAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(UpdatePostAsync), DateTime.UtcNow, updatedPost.PostId));
		}
	}

	/// <summary>
	/// Deletes the post asynchronous.
	/// </summary>
	/// <param name="postId">The post identifier.</param>
	/// <param name="userName">The user name.</param>
	/// <returns>
	/// The boolean for success / failure
	/// </returns>
	public async Task<bool> DeletePostAsync(Guid postId, string userName)
	{
		try
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(DeletePostAsync), DateTime.UtcNow, postId));
			var dbPostData = await dbContext.Posts.FirstOrDefaultAsync(post => post.PostId == postId && post.IsActive && post.PostOwnerUserName == userName);
			if (dbPostData is not null)
			{
				dbPostData.IsActive = false;
				await dbContext.SaveChangesAsync();

				return true;
			}
			else
			{
				var exception = new Exception(ExceptionConstants.PostNotFoundMessageConstant);
				logger.LogError(exception, exception.Message);
				throw exception;
			}

		}
		catch (DbUpdateException dbEx)
		{
			logger.LogError(dbEx, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(DeletePostAsync), DateTime.UtcNow, dbEx.Message));
			throw;
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(DeletePostAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(DeletePostAsync), DateTime.UtcNow, postId));
		}
	}

	/// <summary>
	/// Gets all posts async.
	/// </summary>
	/// <returns>The list of posts</returns>
	public async Task<List<PostDomain>> GetAllPostsAsync()
	{
		try
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(GetAllPostsAsync), DateTime.UtcNow, string.Empty));

			var result = await dbContext.Posts.Where(x => x.IsActive).ToListAsync();
			return result;
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(GetAllPostsAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetAllPostsAsync), DateTime.UtcNow, string.Empty));
		}
	}

	#region PRIVATE Methods

	/// <summary>
	/// Handles rating update for post async.
	/// </summary>
	/// <param name="updatedPost">The updated post.</param>
	/// <returns>The updated post</returns>
	private async Task<PostDomain> HandleRatingUpdateForPostAsync(UpdatePostDomain updatedPost)
	{
		var dbPostData = await dbContext.Posts.FirstOrDefaultAsync(x => x.PostId == updatedPost.PostId && x.IsActive);
		if (dbPostData is not null)
		{
			if (updatedPost.PostRating.HasValue)
			{
				dbPostData.Ratings = updatedPost.PostRating.Value;
			}

			await dbContext.SaveChangesAsync();
			return dbPostData;
		}
		else
		{
			var exception = new Exception(ExceptionConstants.PostNotFoundMessageConstant);
			logger.LogError(exception, exception.Message);
			throw exception;
		}
	}

	#endregion

}
