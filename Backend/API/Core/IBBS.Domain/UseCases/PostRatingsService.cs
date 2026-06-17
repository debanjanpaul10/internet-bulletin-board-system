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
/// PostDomain ratings service.
/// </summary>
/// <param name="correlationContext">The correlation context.</param>
/// <param name="logger">The logger.</param>
/// <param name="postRatingsDataService">The post ratings data service.</param>
/// <param name="postsDataService">The posts data service.</param>
/// <seealso cref="IPostRatingsService"/>
public sealed class PostRatingsService(
    ICorrelationContext correlationContext,
    ILogger<PostRatingsService> logger,
    IPostRatingsDataService postRatingsDataService,
    IPostsDataService postsDataService) : IPostRatingsService
{
    /// <inheritdoc />
    public async Task<UpdateRatingDomain> UpdateRatingAsync(
        PostRatingDomain postRatingDomain,
        string userName,
        CancellationToken cancellationToken = default
    )
    {
        UpdateRatingDomain response = new();
        try
        {
            logger.LogAppInformation(
                LoggingConstants.MethodStartedMessageConstant,
                nameof(UpdateRatingAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, userName })
            );

            var postIdGuid = DomainUtilities.ValidateAndParsePostId(
                postId: Convert.ToString(postRatingDomain.PostId)!,
                logger
            );

            var post = await postsDataService.GetPostAsync(
                postId: postIdGuid,
                userName,
                isForCurrentUser: false,
                cancellationToken
            ).ConfigureAwait(false);
            var postRating = await postRatingsDataService.GetPostRatingAsync(
                postId: postIdGuid,
                userName,
                cancellationToken
            ).ConfigureAwait(false);

            DomainUtilities.ThrowIfNull(
                obj: post,
                message: ExceptionConstants.PostNotFoundMessageConstant,
                commonLogger: logger
            );

            if (postRating is not null && postRating.PostId != Guid.Empty)
                response = await this.HandleUpdateExistingPostRatingDataAsync(
                    postRating,
                    post,
                    userName,
                    cancellationToken
                ).ConfigureAwait(false);
            else
                response = await this.HandleAddNewPostRatingDataAsync(
                    postIdGuid,
                    post,
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
                nameof(UpdateRatingAsync), DateTime.UtcNow, ex.Message
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
                nameof(UpdateRatingAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, userName, response })
            );
        }
    }

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
                LoggingConstants.MethodStartedMessageConstant,
                nameof(GetAllUserPostRatingsAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, userName })
            );

            response = await postRatingsDataService.GetAllUserPostRatingsAsync(
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
                LoggingConstants.MethodEndedMessageConstant,
                nameof(GetAllUserPostRatingsAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, userName, response })
            );
        }
    }

    #region PRIVATE METHODS

    /// <summary>
    /// Handles the update existing post rating data asynchronous.
    /// </summary>
    /// <param name="postRating">The post rating.</param>
    /// <param name="post">The post.</param>
    /// <param name="userName">Name of the user.</param>
    /// <returns>The updated rating domain data.</returns>
    private async Task<UpdateRatingDomain> HandleUpdateExistingPostRatingDataAsync(
        PostRatingDomain postRating,
        PostDomain post,
        string userName,
        CancellationToken cancellationToken = default
    )
    {
        if (postRating.RatingValue == 0)
        {
            post.Ratings += 1;
            postRating.RatingValue = 1;
        }
        else
        {
            post.Ratings = Math.Max(0, post.Ratings - 1);
            postRating.RatingValue = 0;
        }

        await postsDataService.UpdatePostAsync(
            updatedPost: DomainUtilities.CreateUpdatePostDTO(post),
            userName,
            isRatingUpdate: true,
            cancellationToken
        ).ConfigureAwait(false);
        await postRatingsDataService.UpdatePostRatingAsync(
            postRating,
            cancellationToken
        ).ConfigureAwait(false);
        return new UpdateRatingDomain { HasAlreadyUpdated = true, IsUpdateSuccess = true };
    }

    /// <summary>
    /// Handles the add new post rating data asynchronous.
    /// </summary>
    /// <param name="postIdGuid">The post identifier unique identifier.</param>
    /// <param name="post">The post.</param>
    /// <param name="userName">Name of the user.</param>
    /// <returns>The update rating domain data.</returns>
    private async Task<UpdateRatingDomain> HandleAddNewPostRatingDataAsync(
        Guid postIdGuid,
        PostDomain post,
        string userName,
        CancellationToken cancellationToken = default
    )
    {
        post.Ratings += 1;
        var newRating = new PostRatingDomain
        {
            PostId = postIdGuid,
            UserName = userName,
            RatedOn = DateTime.UtcNow,
            IsActive = true,
            RatingValue = 1,
        };

        await postsDataService.UpdatePostAsync(
            updatedPost: DomainUtilities.CreateUpdatePostDTO(post),
            userName,
            isRatingUpdate: true,
            cancellationToken
        ).ConfigureAwait(false);
        await postRatingsDataService.AddPostRatingAsync(
            postRating: newRating,
            cancellationToken
        ).ConfigureAwait(false);
        return new UpdateRatingDomain { HasAlreadyUpdated = false, IsUpdateSuccess = true };
    }

    #endregion
}

