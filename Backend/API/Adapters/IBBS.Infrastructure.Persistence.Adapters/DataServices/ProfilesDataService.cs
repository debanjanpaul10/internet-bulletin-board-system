using IBBS.Domain.DomainEntities.Posts;
using IBBS.Domain.DrivenPorts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static IBBS.Infrastructure.Persistence.Adapters.Helpers.Constants;

namespace IBBS.Infrastructure.Persistence.Adapters.DataServices;

/// <summary>
/// The Profiles data service class.
/// </summary>
/// <param name="logger">The logger service.</param>
/// <param name="dbContext">The database context.</param>
/// <seealso cref="IBBS.Domain.DrivenPorts.IProfilesDataService" />
public class ProfilesDataService(ILogger<ProfilesDataService> logger, SqlDbContext dbContext) : IProfilesDataService
{
	/// <summary>
	/// Gets all posts and ratings for user async.
	/// </summary>
	/// <param name="userEmail">The user email.</param>
	/// <returns>
	/// The user posts and ratings data.
	/// </returns>
	public async Task<List<PostDomain>> GetUserPostsAsync(string userEmail)
	{
		try
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(GetUserPostsAsync), DateTime.UtcNow, userEmail));
			return await dbContext.Posts.Where(p => p.PostOwnerUserName == userEmail && p.IsActive).ToListAsync().ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(GetUserPostsAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetUserPostsAsync), DateTime.UtcNow, userEmail));
		}
	}

	/// <summary>
	/// Gets user ratings async.
	/// </summary>
	/// <param name="userEmail">The user email.</param>
	/// <returns>
	/// The list of user post ratings data
	/// </returns>
	public async Task<List<UserPostRatingDomain>> GetUserRatingsAsync(string userEmail)
	{
		try
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(GetUserRatingsAsync), DateTime.UtcNow, userEmail));
			var userRatings = await dbContext.PostRatings
				.Where(pr => pr.UserName == userEmail && pr.IsActive && pr.RatingValue == 1)
				.Join(
					dbContext.Posts.Where(p => p.IsActive),
					pr => pr.PostId,
					p => p.PostId,
					(pr, p) => new UserPostRatingDomain
					{
						PostName = p.PostTitle,
						RatedOn = pr.RatedOn,
						CurrentRatingValue = pr.RatingValue
					}
				).ToListAsync().ConfigureAwait(false);

			return userRatings;
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(GetUserRatingsAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetUserRatingsAsync), DateTime.UtcNow, userEmail));
		}
	}
}
