using IBBS.Domain.Contracts;
using IBBS.Domain.DomainEntities.Posts;
using IBBS.Domain.DrivenPorts;
using IBBS.Domain.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static IBBS.Infrastructure.Persistence.Adapters.Helpers.Constants;

namespace IBBS.Infrastructure.Persistence.Adapters.DataManager;

/// <summary>
/// The posts data service.
/// </summary>
/// <param name="correlationContext">The correlation context.</param>
/// <param name="dbContext">The database context.</param>
/// <param name="logger">The logger service.</param>
/// <seealso cref="IPostsDataService" />
public sealed class PostsDataService(
    ICorrelationContext correlationContext,
    ILogger<PostsDataService> logger,
    SqlDbContext dbContext) : IPostsDataService
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

            var query = dbContext.Posts.Where(p => p.PostId == postId && p.IsActive);
            if (string.IsNullOrEmpty(userName))
                return await query.FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false) ?? new();

            query = isForCurrentUser ? query.Where(p => p.PostOwnerUserName == userName) : query.Where(p => p.PostOwnerUserName != userName);
            response = await query.FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false) ?? new PostDomain();
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

            var postId = Guid.NewGuid();
            var existingPost = await dbContext.Posts.AnyAsync(
                predicate: x => x.PostId == postId && x.IsActive,
                cancellationToken
            ).ConfigureAwait(false);
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
                await dbContext.Posts.AddAsync(
                    entity: dbPostData,
                    cancellationToken
                ).ConfigureAwait(false);
                await dbContext.SaveChangesAsync(
                    cancellationToken
                ).ConfigureAwait(false);
                response = true;
            }
            else
            {
                var exception = new Exception(ExceptionConstants.PostExistsMessageConstant);
                logger.LogAppError(
                    exception,
                    exception.Message
                );

                response = false;
            }

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

            if (isRatingUpdate)
            {
                return await HandleRatingUpdateForPostAsync(
                    updatedPost,
                    cancellationToken
                ).ConfigureAwait(false);
            }
            else
            {
                var dbPostData = await dbContext.Posts.FirstOrDefaultAsync(
                    predicate: x => x.PostId == updatedPost.PostId && x.IsActive && x.PostOwnerUserName == userName,
                    cancellationToken
                ).ConfigureAwait(false);
                if (dbPostData is not null)
                {
                    dbPostData.PostTitle = updatedPost.PostTitle;
                    dbPostData.PostContent = updatedPost.PostContent;

                    await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                    response = dbPostData;
                }
                else
                {
                    var exception = new Exception(ExceptionConstants.PostNotFoundMessageConstant);
                    logger.LogAppError(exception, exception.Message);
                    response = default!;
                }
            }

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

    /// <inheritdoc />
    public async Task<List<PostDomain>> GetAllPostsAsync(
        CancellationToken cancellationToken = default
    )
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
    /// Handles the rating update for post asynchronous.
    /// </summary>
    /// <param name="updatedPost">The updated post.</param>
    /// <returns>The updated post domain.</returns>
    private async Task<PostDomain> HandleRatingUpdateForPostAsync(
        UpdatePostDomain updatedPost,
        CancellationToken cancellationToken
    )
    {
        var dbPostData = await dbContext.Posts.FirstOrDefaultAsync(x => x.PostId == updatedPost.PostId && x.IsActive, cancellationToken);
        if (dbPostData is not null)
        {
            if (updatedPost.PostRating.HasValue)
                dbPostData.Ratings = updatedPost.PostRating.Value;

            await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return dbPostData;
        }
        else
        {
            var exception = new Exception(ExceptionConstants.PostNotFoundMessageConstant);
            logger.LogAppError(exception, exception.Message);
            throw exception;
        }
    }

    #endregion

}
