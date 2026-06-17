using IBBS.Domain.Contracts;
using IBBS.Domain.DomainEntities.Posts;
using IBBS.Domain.DrivenPorts;
using IBBS.Domain.DrivingPorts;
using IBBS.Domain.Helpers;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static IBBS.Domain.Helpers.DomainConstants;

namespace IBBS.Domain.UseCases;

/// <summary>
/// The Posts BusinessManager Class.
/// </summary>
/// <param name="logger">The logger.</param>
/// <param name="correlationContext">The correlation context.</param>
/// <param name="postsDataService">The Posts Data Service.</param>
/// <param name="postRatingsDataService">The post ratings data service.</param>
/// <seealso cref="IPostsService"/>
public sealed class PostsService(
    ILogger<PostsService> logger,
    ICorrelationContext correlationContext,
    IPostsDataService postsDataService,
    IPostRatingsDataService postRatingsDataService) : IPostsService
{
    /// <inheritdoc />
    public async Task<PostDomain> GetPostAsync(
        string postId,
        string userName,
        CancellationToken cancellationToken = default
    )
    {
        PostDomain response = new();
        try
        {
            logger.LogAppInformation(
                LoggingConstants.MethodStartedMessageConstant,
                nameof(GetPostAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, postId, userName })
            );

            var postGuid = DomainUtilities.ValidateAndParsePostId(
                postId,
                logger
            );

            var result = await postsDataService.GetPostAsync(
                postId: postGuid,
                userName,
                isForCurrentUser: true,
                cancellationToken
            ).ConfigureAwait(false);

            response = DomainUtilities.ThrowIfNull(
                obj: result,
                message: ExceptionConstants.PostNotFoundMessageConstant,
                commonLogger: logger
            );
            return response;
        }
        catch (Exception ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.MethodFailedWithMessageConstant,
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
                LoggingConstants.MethodEndedMessageConstant,
                nameof(GetPostAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, postId, userName, response })
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
                LoggingConstants.MethodStartedMessageConstant,
                nameof(AddNewPostAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, newPost, userName }));

            DomainUtilities.ThrowIfNull(
                obj: newPost,
                message: ExceptionConstants.NullPostMessageConstant,
                commonLogger: logger
            );

            response = await postsDataService.AddNewPostAsync(
                newPost,
                userName,
                cancellationToken
            ).ConfigureAwait(false);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.MethodFailedWithMessageConstant,
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
                LoggingConstants.MethodEndedMessageConstant,
                nameof(AddNewPostAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, newPost, userName, response })
            );
        }
    }

    /// <inheritdoc />
    public async Task<PostDomain> UpdatePostAsync(
        UpdatePostDomain updatedPost,
        string userName,
        CancellationToken cancellationToken = default
    )
    {
        PostDomain response = new();
        try
        {
            logger.LogAppInformation(
                LoggingConstants.MethodStartedMessageConstant,
                nameof(UpdatePostAsync), DateTime.UtcNow,
                    JsonConvert.SerializeObject(new { correlationContext.CorrelationId, updatedPost, userName })
            );

            DomainUtilities.ThrowIfNull(
                obj: updatedPost,
                message: ExceptionConstants.NullPostMessageConstant,
                commonLogger: logger
            );

            var result = await postsDataService.UpdatePostAsync(
                updatedPost,
                userName,
                isRatingUpdate: false,
                cancellationToken
            ).ConfigureAwait(false);

            response = DomainUtilities.ThrowIfNull(
                obj: result,
                message: ExceptionConstants.PostNotFoundMessageConstant,
                commonLogger: logger
            );
            return response;
        }
        catch (Exception ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.MethodFailedWithMessageConstant,
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
                LoggingConstants.MethodEndedMessageConstant,
                nameof(UpdatePostAsync), DateTime.UtcNow,
                    JsonConvert.SerializeObject(new { correlationContext.CorrelationId, updatedPost, userName, response })
            );
        }
    }

    /// <inheritdoc />
    public async Task<bool> DeletePostAsync(
        string postId,
        string userName,
        CancellationToken cancellationToken = default
    )
    {
        bool response = false;
        try
        {
            logger.LogAppInformation(
                LoggingConstants.MethodStartedMessageConstant,
                nameof(DeletePostAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, postId, userName })
            );

            var postGuid = DomainUtilities.ValidateAndParsePostId(
                postId,
                logger
            );

            response = await postsDataService.DeletePostAsync(
                postId: postGuid,
                userName,
                cancellationToken
            ).ConfigureAwait(false);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.MethodFailedWithMessageConstant,
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
                LoggingConstants.MethodEndedMessageConstant,
                nameof(DeletePostAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, postId, userName, response })
            );
        }
    }

    /// <inheritdoc />
    public async Task<List<PostWithRatingsDomain>> GetAllPostsAsync(
        string userName,
        CancellationToken cancellationToken = default
    )
    {
        List<PostWithRatingsDomain> response = [];
        try
        {
            logger.LogAppInformation(
                LoggingConstants.MethodStartedMessageConstant,
                nameof(GetAllPostsAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, userName })
            );

            if (string.IsNullOrWhiteSpace(userName))
            {
                var result = await postsDataService.GetAllPostsAsync(
                    cancellationToken
                ).ConfigureAwait(false);

                var postsData = result.Select(post => new PostWithRatingsDomain
                {
                    PostId = post.PostId,
                    PostTitle = post.PostTitle,
                    PostContent = post.PostContent,
                    PostCreatedDate = post.PostCreatedDate,
                    PostOwnerUserName = post.PostOwnerUserName,
                    Ratings = post.Ratings,
                    IsActive = post.IsActive,
                }).ToList();

                response = postsData;
            }
            else
            {
                response = await postRatingsDataService.GetAllPostsWithRatingsAsync(
                    userName
                ).ConfigureAwait(false);
            }

            return response;
        }
        catch (Exception ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.MethodFailedWithMessageConstant,
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
                LoggingConstants.MethodEndedMessageConstant,
                nameof(GetAllPostsAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, userName, response })
            );
        }
    }
}
