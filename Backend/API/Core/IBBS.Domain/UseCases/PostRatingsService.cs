using IBBS.Domain.DomainEntities;
using IBBS.Domain.DomainEntities.Posts;
using IBBS.Domain.DrivenPorts;
using IBBS.Domain.DrivingPorts;
using IBBS.Domain.Helpers;
using InternetBulletin.Data.Contracts;
using Microsoft.Extensions.Logging;
using static IBBS.Domain.Helpers.DomainConstants;

namespace IBBS.Domain.UseCases;

/// <summary>
/// PostDomain ratings service.
/// </summary>
/// <param name="logger">The logger.</param>
/// <param name="postRatingsDataService">The post ratings data service.</param>
/// <param name="postsDataService">The posts data service.</param>
/// <seealso cref="IPostRatingsService"/>
public sealed class PostRatingsService(
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
            userName
        ).ConfigureAwait(false);

        DomainUtilities.ThrowIfNull(post, ExceptionConstants.PostNotFoundMessageConstant, logger);

        if (postRating is not null && postRating.PostId != Guid.Empty)
            return await this.HandleUpdateExistingPostRatingDataAsync(
                postRating,
                post,
                userName,
                cancellationToken
            ).ConfigureAwait(false);
        else
            return await this.HandleAddNewPostRatingDataAsync(
                postIdGuid,
                post,
                userName,
                cancellationToken
            ).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<List<PostRatingDomain>> GetAllUserPostRatingsAsync(
        string userName,
        CancellationToken cancellationToken = default
    ) =>
        await postRatingsDataService.GetAllUserPostRatingsAsync(
            userName
        ).ConfigureAwait(false);


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
            postRating
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
            postRating: newRating
        ).ConfigureAwait(false);
        return new UpdateRatingDomain { HasAlreadyUpdated = false, IsUpdateSuccess = true };
    }

    #endregion
}

