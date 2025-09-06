using IBBS.Domain.DomainEntities;
using IBBS.Domain.DomainEntities.Posts;
using IBBS.Domain.DrivenPorts;
using IBBS.Domain.DrivingPorts;
using Microsoft.Extensions.Logging;
using static IBBS.Domain.Helpers.DomainConstants;

namespace IBBS.Domain.UseCases;

/// <summary>
/// The Profiles Service class.
/// </summary>
/// <seealso cref="IBBS.Domain.DrivingPorts.IProfilesService" />
public class ProfilesService(ILogger<ProfilesService> logger, IProfilesDataService profilesDataService) : IProfilesService
{
	/// <summary>
	/// Gets the user profile data asynchronous.
	/// </summary>
	/// <param name="userEmail">The user email.</param>
	/// <returns>
	/// The user profile domain.
	/// </returns>
	public async Task<UserProfileDomain> GetUserProfileDataAsync(string userEmail)
	{
		if (string.IsNullOrEmpty(userEmail))
		{
			var exception = new Exception(ExceptionConstants.UserIdCannotBeNullMessageConstant);
			logger.LogError(exception, exception.Message);
			throw exception;
		}

		var userPosts = await profilesDataService.GetUserPostsAsync(userEmail).ConfigureAwait(false);
		var userRatings = await profilesDataService.GetUserRatingsAsync(userEmail).ConfigureAwait(false);
		var userProfileData = new UserProfileDomain()
		{
			EmailAddress = userEmail,
			UserPosts = [.. userPosts.Select(p => new UserPostDomain
				{
					PostTitle = p.PostTitle,
					PostCreatedDate = p.PostCreatedDate,
					PostId = p.PostId,
					PostOwnerUserName = p.PostOwnerUserName,
					Ratings = p.Ratings
				})],
			UserPostRatings = userRatings
		};

		return userProfileData;
	}
}
