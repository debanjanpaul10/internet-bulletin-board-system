namespace IBBS.Infrastructure.Persistence.Adapters.DataServices;

using IBBS.Domain.DomainEntities.Posts;
using IBBS.Infrastructure.Persistence.Adapters;
using InternetBulletin.Data.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static IBBS.Infrastructure.Persistence.Adapters.Helpers.Constants;

/// <summary>
/// PostDomain ratings data service class.
/// </summary>
/// <param name="logger">The application logger</param>
/// <param name="sqlDbContext">The sql db context</param>
/// <seealso cref="IPostRatingsDataService"/>
public class PostRatingsDataService(SqlDbContext sqlDbContext, ILogger<PostRatingsDataService> logger) : IPostRatingsDataService
{
	/// <summary>
	/// Gets all user post ratings async.
	/// </summary>
	/// <param name="userName">The user name.</param>
	/// <returns>The list of post ratings</returns>
	public async Task<List<PostRatingDomain>> GetAllUserPostRatingsAsync(string userName)
	{
		try
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(GetAllUserPostRatingsAsync), DateTime.UtcNow, string.Empty));
			var result = await sqlDbContext.PostRatings.Where(r => r.UserName == userName && r.IsActive).ToListAsync();
			if (result is not null)
			{
				return result;
			}
			else
			{
				var exception = new Exception(ExceptionConstants.UnableToGetUserPostRatingsMessageConstant);
				logger.LogError(exception, exception.Message);
				throw exception;
			}
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(GetAllUserPostRatingsAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetAllUserPostRatingsAsync), DateTime.UtcNow, string.Empty));
		}
	}

	/// <summary>
	/// Gets post rating async.
	/// </summary>
	/// <param name="postId">The post id.</param>
	/// <param name="userName">The user name.</param>
	/// <returns>The post rating data.</returns>
	public async Task<PostRatingDomain> GetPostRatingAsync(Guid postId, string userName)
	{
		try
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(GetPostRatingAsync), DateTime.UtcNow, string.Empty));

			var result = await sqlDbContext.PostRatings.FirstOrDefaultAsync(r => r.PostId == postId && r.UserName == userName && r.IsActive);
			return result ?? new PostRatingDomain();
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(GetPostRatingAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetPostRatingAsync), DateTime.UtcNow, string.Empty));
		}
	}

	/// <summary>
	/// Adds a new rating async.
	/// </summary>
	/// <param name="postRating">The post rating.</param>
	public async Task AddPostRatingAsync(PostRatingDomain postRating)
	{
		try
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(AddPostRatingAsync), DateTime.UtcNow, postRating.PostId));
			if (postRating is not null)
			{
				await sqlDbContext.PostRatings.AddAsync(postRating);
				await sqlDbContext.SaveChangesAsync();
			}
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(AddPostRatingAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(AddPostRatingAsync), DateTime.UtcNow, postRating.PostId));
		}
	}

	/// <summary>
	/// Updates an existing rating async.
	/// </summary>
	/// <param name="postRating">The post rating.</param>
	public async Task UpdatePostRatingAsync(PostRatingDomain postRating)
	{
		try
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(UpdatePostRatingAsync), DateTime.UtcNow, postRating.PostId));
			var existingPostRating = await sqlDbContext.PostRatings.FirstOrDefaultAsync(r => r.PostId == postRating.PostId && r.UserName == postRating.UserName);
			if (existingPostRating is not null)
			{
				existingPostRating.RatedOn = DateTime.UtcNow;
				await sqlDbContext.SaveChangesAsync();
			}
			else
			{
				var exception = new Exception(ExceptionConstants.PostNotFoundMessageConstant);
				logger.LogError(exception, exception.Message);
				throw exception;
			}
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(UpdatePostRatingAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(UpdatePostRatingAsync), DateTime.UtcNow, postRating.PostId));
		}
	}

	/// <summary>
	/// Gets all posts with ratings async.
	/// </summary>
	/// <param name="userName">The user name.</param>
	public async Task<List<PostWithRatingsDomain>> GetAllPostsWithRatingsAsync(string userName)
	{
		try
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(GetAllPostsWithRatingsAsync), DateTime.UtcNow, string.Empty));

			var query = from post in sqlDbContext.Posts
						where post.IsActive
						join rating in sqlDbContext.PostRatings.Where(r => r.UserName == userName && r.IsActive)
						on post.PostId equals rating.PostId into ratings
						from rating in ratings.DefaultIfEmpty()
						select new PostWithRatingsDomain
						{
							PostId = post.PostId,
							PostTitle = post.PostTitle,
							PostContent = post.PostContent,
							PostCreatedDate = post.PostCreatedDate,
							PostOwnerUserName = post.PostOwnerUserName,
							Ratings = post.Ratings,
							IsActive = post.IsActive,
							RatingValue = rating != null ? rating.RatingValue : 0
						};

			var result = await query.ToListAsync();
			return result;
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(GetAllPostsWithRatingsAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetAllPostsWithRatingsAsync), DateTime.UtcNow, string.Empty));
		}
	}
}


