using IBBS.Domain.Contracts;
using IBBS.Domain.DomainEntities.Posts;
using IBBS.Domain.DrivenPorts;
using IBBS.Domain.Helpers;
using IBBS.Infrastructure.Persistence.Adapters.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static IBBS.Infrastructure.Persistence.Adapters.Helpers.Constants;
using static IBBS.Infrastructure.Persistence.Adapters.Mapper.DomainToEntityMapper;
using static IBBS.Infrastructure.Persistence.Adapters.Mapper.EntityToDomainMapper;

namespace IBBS.Infrastructure.Persistence.Adapters.DataManager;

/// <summary>
/// The posts data service.
/// </summary>
/// <param name="correlationContext">The correlation context.</param>
/// <param name="logger">The logger service.</param>
/// <param name="postsRepository">The Posts repository.</param>
/// <seealso cref="IPostsDataService" />
public sealed class PostsDataService(
    ICorrelationContext correlationContext,
    ILogger<PostsDataService> logger,
    IPostsRepository postsRepository) : IPostsDataService
{
    /// <inheritdoc />
    public async Task<PostDomain> GetPostAsync(
        Guid postId,
        string userName,
        bool isForCurrentUser,
        CancellationToken cancellationToken = default
    )
    {
        PostDomain response = new();
        try
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodStart,
                nameof(GetPostAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, postId, userName, isForCurrentUser })
            );

            var dbResponse = await postsRepository.GetPostAsync(
                postId,
                userName,
                isForCurrentUser,
                cancellationToken
            ).ConfigureAwait(false);
            response = MapToDomain(entity: dbResponse);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.LogHelperMethodFailed,
                nameof(GetPostAsync), DateTime.UtcNow, ex.Message
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
                nameof(GetPostAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, postId, userName, isForCurrentUser, response })
            );
        }
    }

    /// <inheritdoc />
    public async Task<bool> AddNewPostAsync(
        AddPostDomain newPost,
        string userName,
        CancellationToken cancellationToken = default
    )
    {
        bool response = false;
        try
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodStart,
                nameof(AddNewPostAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, newPost, userName })
            );

            var dbEntity = MapToEntity(domain: newPost);
            response = await postsRepository.AddNewPostAsync(
                newPost: dbEntity,
                userName,
                cancellationToken
            ).ConfigureAwait(false);
            return response;
        }
        catch (DbUpdateException dbEx)
        {
            logger.LogAppError(
                dbEx,
                LoggingConstants.LogHelperMethodFailed,
                nameof(AddNewPostAsync), DateTime.UtcNow, dbEx.Message
            );
            throw new IBBSBusinessException(
                message: dbEx.Message,
                correlationId: correlationContext.CorrelationId
            );
        }
        catch (Exception ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.LogHelperMethodFailed,
                nameof(AddNewPostAsync), DateTime.UtcNow, ex.Message
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
                nameof(AddNewPostAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, newPost, userName })
            );
        }
    }

    /// <inheritdoc />
    public async Task<PostDomain> UpdatePostAsync(
        UpdatePostDomain updatedPost,
        string userName,
        bool isRatingUpdate,
        CancellationToken cancellationToken = default
    )
    {
        PostDomain response = new();
        try
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodStart,
                nameof(AddNewPostAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, updatedPost, userName, isRatingUpdate })
            );

            var dbResponse = await postsRepository.UpdatePostAsync(
                updatedPost,
                userName,
                isRatingUpdate,
                cancellationToken
            ).ConfigureAwait(false);
            response = MapToDomain(entity: dbResponse);
            return response;
        }
        catch (DbUpdateException dbEx)
        {
            logger.LogAppError(
                dbEx,
                LoggingConstants.LogHelperMethodFailed,
                nameof(UpdatePostAsync), DateTime.UtcNow, dbEx.Message
            );
            throw new IBBSBusinessException(
                message: dbEx.Message,
                correlationId: correlationContext.CorrelationId
            );
        }
        catch (Exception ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.LogHelperMethodFailed,
                nameof(UpdatePostAsync), DateTime.UtcNow, ex.Message
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
                nameof(UpdatePostAsync), DateTime.UtcNow,
                    JsonConvert.SerializeObject(new { correlationContext.CorrelationId, updatedPost, userName, isRatingUpdate, response })
            );
        }
    }

    /// <inheritdoc />
    public async Task<bool> DeletePostAsync(
        Guid postId,
        string userName,
        CancellationToken cancellationToken = default
    )
    {
        bool response = false;
        try
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodStart,
                nameof(DeletePostAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, postId, userName })
            );

            response = await postsRepository.DeletePostAsync(
                postId,
                userName,
                cancellationToken
            ).ConfigureAwait(false);
            return response;
        }
        catch (DbUpdateException dbEx)
        {
            logger.LogAppError(
                dbEx,
                LoggingConstants.LogHelperMethodFailed,
                nameof(DeletePostAsync), DateTime.UtcNow, dbEx.Message
            );
            throw new IBBSBusinessException(
                message: dbEx.Message,
                correlationId: correlationContext.CorrelationId
            );
        }
        catch (Exception ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.LogHelperMethodFailed,
                nameof(DeletePostAsync), DateTime.UtcNow, ex.Message
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
                nameof(DeletePostAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, postId, userName, response })
            );
        }
    }

    /// <inheritdoc />
    public async Task<List<PostDomain>> GetAllPostsAsync(
        CancellationToken cancellationToken = default
    )
    {
        List<PostDomain> response = [];
        try
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodStart,
                nameof(GetAllPostsAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId })
            );

            var dbResult = await postsRepository.GetAllPostsAsync(cancellationToken).ConfigureAwait(false);
            response = [.. dbResult.Select(MapToDomain)];
            return response;
        }
        catch (Exception ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.LogHelperMethodFailed,
                nameof(GetAllPostsAsync), DateTime.UtcNow, ex.Message
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
                nameof(GetAllPostsAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, response })
            );
        }
    }

}
