using IBBS.Domain.Contracts;
using IBBS.Domain.DomainEntities;
using IBBS.Domain.DomainEntities.Posts;
using IBBS.Domain.DrivenPorts;
using IBBS.Domain.DrivingPorts;
using IBBS.Domain.Helpers;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static IBBS.Domain.Helpers.DomainConstants;

namespace IBBS.Domain.UseCases;

/// <summary>
/// The Profiles BusinessManager Class.
/// </summary>
/// <remarks>This class implements methods for getting user posts and ratings from the database.</remarks>
/// <param name="logger">The logger.</param>
/// <param name="correlationContext">The correlation context.</param>
/// <param name="profilesDataService">The Profiles Data Service.</param>
/// <seealso cref="IProfilesService"/>
public sealed class ProfilesService(
    ILogger<ProfilesService> logger,
    ICorrelationContext correlationContext,
    IProfilesDataService profilesDataService) : IProfilesService
{
    /// <inheritdoc />
    public async Task<UserProfileDomain> GetUserProfileDataAsync(
        string userEmail,
        CancellationToken cancellationToken = default
    )
    {
        UserProfileDomain response = new();
        try
        {
            logger.LogAppInformation(
                LoggingConstants.MethodStartedMessageConstant,
                nameof(GetUserProfileDataAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, userEmail })
            );

            ArgumentException.ThrowIfNullOrWhiteSpace(userEmail);

            var userPostsTask = profilesDataService.GetUserPostsAsync(userEmail, cancellationToken);
            var userRatingsTask = profilesDataService.GetUserRatingsAsync(userEmail, cancellationToken);
            await Task.WhenAll(userPostsTask, userPostsTask)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

            var userPosts = userPostsTask.Result;
            var userRatings = userRatingsTask.Result;
            response = new UserProfileDomain()
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

            return response;
        }
        catch (Exception ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.MethodFailedWithMessageConstant,
                nameof(GetUserProfileDataAsync), DateTime.UtcNow, ex.Message
            );
            throw new IBBSBusinessException(
                message: ex.Message,
                correlationId: correlationContext.CorrelationId
            );
        }
        finally
        {
            logger.LogAppInformation(
                LoggingConstants.MethodEndedMessageConstant,
                nameof(GetUserProfileDataAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, userEmail, response })
            );
        }
    }
}
