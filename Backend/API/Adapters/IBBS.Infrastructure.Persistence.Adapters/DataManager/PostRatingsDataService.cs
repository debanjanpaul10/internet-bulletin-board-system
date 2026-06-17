using IBBS.Domain.Contracts;
using IBBS.Domain.DomainEntities.Posts;
using IBBS.Domain.DrivenPorts;
using IBBS.Domain.Helpers;
using IBBS.Infrastructure.Persistence.Adapters.Contracts;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static IBBS.Infrastructure.Persistence.Adapters.Helpers.Constants;
using static IBBS.Infrastructure.Persistence.Adapters.Mapper.DomainToEntityMapper;
using static IBBS.Infrastructure.Persistence.Adapters.Mapper.EntityToDomainMapper;

namespace IBBS.Infrastructure.Persistence.Adapters.DataManager;

/// <summary>
/// PostDomain ratings data service class.
/// </summary>
/// <param name="logger">The application logger</param>
/// <param name="correlationContext">The correlation context</param>
/// <param name="postRatingsRepository">The post ratings repository</param>
/// <seealso cref="IPostRatingsDataService"/>
public sealed class PostRatingsDataService(
    ICorrelationContext correlationContext,
    ILogger<PostRatingsDataService> logger,
    IPostRatingsRepository postRatingsRepository) : IPostRatingsDataService
{
    /// <inheritdoc />
    public async Task<List<PostRatingDomain>> GetAllUserPostRatingsAsync(
        string userName,
        CancellationToken cancellationToken = default
    )
    {
        List<PostRatingDomain> response = [];
        try
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodStart,
                nameof(GetAllUserPostRatingsAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, userName })
            );

            var dbResponse = await postRatingsRepository.GetAllUserPostRatingsAsync(
                userName,
                cancellationToken
            ).ConfigureAwait(false);

            response = [.. dbResponse.Select(MapToDomain)];
            return response;
        }
        catch (Exception ex)
        {
            logger.LogAppError(ex,
                LoggingConstants.LogHelperMethodFailed,
                nameof(GetAllUserPostRatingsAsync), DateTime.UtcNow, ex.Message
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
                nameof(GetAllUserPostRatingsAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, userName, response })
            );
        }
    }

    /// <inheritdoc />
    public async Task<PostRatingDomain> GetPostRatingAsync(
        Guid postId,
        string userName,
        CancellationToken cancellationToken = default
    )
    {
        PostRatingDomain response = new();
        try
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodStart,
                nameof(GetPostRatingAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, postId, userName })
            );

            var dbResult = await postRatingsRepository.GetPostRatingAsync(
                postId,
                userName,
                cancellationToken
            ).ConfigureAwait(false);
            response = MapToDomain(entity: dbResult);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.LogHelperMethodFailed, nameof(GetPostRatingAsync), DateTime.UtcNow, ex.Message
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
                nameof(GetPostRatingAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, postId, userName, response })
            );
        }
    }

    /// <inheritdoc />
    public async Task<bool> AddPostRatingAsync(
        PostRatingDomain postRating,
        CancellationToken cancellationToken = default
    )
    {
        bool response = false;
        try
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodStart,
                nameof(AddPostRatingAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, postRating })
            );

            var entityRequest = MapToEntity(domain: postRating);
            if (postRating is not null)
                response = await postRatingsRepository.AddPostRatingAsync(
                    postRating: entityRequest,
                    cancellationToken
                ).ConfigureAwait(false);

            return response;
        }
        catch (Exception ex)
        {
            logger.LogAppError(ex, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(AddPostRatingAsync), DateTime.UtcNow, ex.Message));
            throw;
        }
        finally
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodEnded,
                nameof(AddPostRatingAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, postRating, response })
            );
        }
    }

    /// <inheritdoc />
    public async Task<bool> UpdatePostRatingAsync(
        PostRatingDomain postRating,
        CancellationToken cancellationToken = default
    )
    {
        bool response = false;
        try
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodStart,
                nameof(UpdatePostRatingAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, postRating })
            );

            var dbEntity = MapToEntity(domain: postRating);
            response = await postRatingsRepository.UpdatePostRatingAsync(
                postRating: dbEntity,
                cancellationToken
            ).ConfigureAwait(false);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.LogHelperMethodFailed,
                nameof(UpdatePostRatingAsync), DateTime.UtcNow, ex.Message
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
                nameof(UpdatePostRatingAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, postRating, response })
            );
        }
    }

    /// <inheritdoc />
    public async Task<List<PostWithRatingsDomain>> GetAllPostsWithRatingsAsync(
        string userName,
        CancellationToken cancellationToken = default
    )
    {
        List<PostWithRatingsDomain> response = [];
        try
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodStart,
                nameof(GetAllPostsWithRatingsAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, userName })
            );

            var dbResult = await postRatingsRepository.GetAllPostsWithRatingsAsync(
                userName,
                cancellationToken
            ).ConfigureAwait(false);
            response = [.. dbResult.Select(MapToDomain)];
            return response;
        }
        catch (Exception ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.LogHelperMethodFailed,
                nameof(GetAllPostsWithRatingsAsync), DateTime.UtcNow, ex.Message
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
                nameof(GetAllPostsWithRatingsAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, userName, response })
            );
        }
    }
}


