using IBBS.Domain.Contracts;
using IBBS.Domain.DomainEntities.Posts;
using IBBS.Domain.DrivenPorts;
using IBBS.Domain.Helpers;
using IBBS.Infrastructure.Persistence.Adapters.Contracts;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static IBBS.Infrastructure.Persistence.Adapters.Helpers.Constants;
using static IBBS.Infrastructure.Persistence.Adapters.Mapper.EntityToDomainMapper;

namespace IBBS.Infrastructure.Persistence.Adapters.DataManager;

/// <summary>
/// The Profiles data service class.
/// </summary>
/// <param name="logger">The logger service.</param>
/// <param name="correlationContext">The correlation context.</param>
/// <param name="profilesRepository">The profiles repository.</param>
/// <seealso cref="IProfilesDataService" />
public sealed class ProfilesDataService(
    ILogger<ProfilesDataService> logger,
    ICorrelationContext correlationContext,
    IProfilesRepository profilesRepository) : IProfilesDataService
{
    /// <inheritdoc />
    public async Task<IEnumerable<PostDomain>> GetUserPostsAsync(
        string userEmail,
        CancellationToken cancellationToken = default
    )
    {
        List<PostDomain> response = [];
        try
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodStart,
                nameof(GetUserPostsAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, userEmail })
            );

            var dbResponse = await profilesRepository.GetUserPostsAsync(
                userEmail,
                cancellationToken
            ).ConfigureAwait(false);
            response = [.. dbResponse.Select(MapToDomain)];
            return response;
        }
        catch (Exception ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.LogHelperMethodFailed,
                nameof(GetUserPostsAsync), DateTime.UtcNow, ex.Message
            );
            throw new IBBSBusinessException(
                message: ex.Message,
                correlationId: correlationContext.CorrelationId
            );
        }
        finally
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodEnded,
                nameof(GetUserPostsAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, userEmail, response })
            );
        }
    }

    /// <inheritdoc />
    public async Task<IEnumerable<UserPostRatingDomain>> GetUserRatingsAsync(
        string userEmail,
        CancellationToken cancellationToken = default
    )
    {
        IEnumerable<UserPostRatingDomain> response = [];
        try
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodStart,
                nameof(GetUserRatingsAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, userEmail })
            );

            var dbResponse = await profilesRepository.GetUserRatingsAsync(
                userEmail,
                cancellationToken
            ).ConfigureAwait(false);
            response = [.. dbResponse.Select(MapToDomain)];
            return response;
        }
        catch (Exception ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.LogHelperMethodFailed,
                nameof(GetUserRatingsAsync), DateTime.UtcNow, ex.Message
            );
            throw new IBBSBusinessException(
                message: ex.Message,
                correlationId: correlationContext.CorrelationId
            );
        }
        finally
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodEnded,
                nameof(GetUserRatingsAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, userEmail, response })
            );
        }
    }
}
