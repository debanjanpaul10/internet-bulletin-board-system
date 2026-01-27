using IBBS.Domain.DomainEntities.Posts;
using IBBS.Domain.DrivenPorts;
using IBBS.Domain.DrivingPorts;
using IBBS.Domain.Helpers;
using InternetBulletin.Data.Contracts;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static IBBS.Domain.Helpers.DomainConstants;

namespace IBBS.Domain.UseCases;

/// <summary>
/// The Posts BusinessManager Class.
/// </summary>
/// <param name="logger">The logger.</param>
/// <param name="postsDataService">The Posts Data Service.</param>
/// <param name="postRatingsDataService">The post ratings data service.</param>
/// <seealso cref="IPostsService"/>
public sealed class PostsService(ILogger<PostsService> logger, IPostsDataService postsDataService, IPostRatingsDataService postRatingsDataService) : IPostsService
{
    /// <summary>
    /// Gets the post asynchronous.
    /// </summary>
    /// <param name="postId">The post identifier.</param>
    /// <param name="userName">The user name.</param>
    /// <returns>
    /// The specific post.
    /// </returns>
    public async Task<PostDomain> GetPostAsync(string postId, string userName)
    {
        try
        {
            logger.LogInformation(LoggingConstants.MethodStartedMessageConstant, nameof(GetPostAsync), DateTime.UtcNow, postId);

            var postGuid = DomainUtilities.ValidateAndParsePostId(postId, logger);
            var result = await postsDataService.GetPostAsync(postGuid, userName, true).ConfigureAwait(false);
            return DomainUtilities.ThrowIfNull(result, ExceptionConstants.PostNotFoundMessageConstant, logger);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetPostAsync), DateTime.UtcNow, ex.Message);
            throw;
        }
        finally
        {
            logger.LogInformation(LoggingConstants.MethodEndedMessageConstant, nameof(GetPostAsync), DateTime.UtcNow, postId);
        }
    }

    /// <summary>
    /// Adds the new post asynchronous.
    /// </summary>
    /// <param name="newPost">The new post.</param>
    /// <returns>
    /// The boolean for success or failure.
    /// </returns>
    public async Task<bool> AddNewPostAsync(AddPostDomain newPost, string userName)
    {
        try
        {
            logger.LogInformation(LoggingConstants.MethodStartedMessageConstant, nameof(AddNewPostAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { newPost, userName }));

            DomainUtilities.ThrowIfNull(newPost, ExceptionConstants.NullPostMessageConstant, logger);
            return await postsDataService.AddNewPostAsync(newPost, userName).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, LoggingConstants.MethodFailedWithMessageConstant, nameof(AddNewPostAsync), DateTime.UtcNow, ex.Message);
            throw;
        }
        finally
        {
            logger.LogInformation(LoggingConstants.MethodEndedMessageConstant, nameof(AddNewPostAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { newPost, userName }));
        }
    }

    /// <summary>
    /// Updates the post asynchronous.
    /// </summary>
    /// <param name="updatedPost">The updated post.</param>
    /// <returns>The updated post data.</returns>
    public async Task<PostDomain> UpdatePostAsync(UpdatePostDomain updatedPost, string userName)
    {
        try
        {
            logger.LogInformation(LoggingConstants.MethodStartedMessageConstant, nameof(UpdatePostAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { updatedPost, userName }));

            DomainUtilities.ThrowIfNull(updatedPost, ExceptionConstants.NullPostMessageConstant, logger);
            var result = await postsDataService.UpdatePostAsync(updatedPost, userName, false).ConfigureAwait(false);
            return DomainUtilities.ThrowIfNull(result, ExceptionConstants.PostNotFoundMessageConstant, logger);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, LoggingConstants.MethodFailedWithMessageConstant, nameof(UpdatePostAsync), DateTime.UtcNow, ex.Message);
            throw;
        }
        finally
        {
            logger.LogInformation(LoggingConstants.MethodEndedMessageConstant, nameof(UpdatePostAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { updatedPost, userName }));
        }

    }

    /// <summary>
    /// Deletes the post asynchronous.
    /// </summary>
    /// <param name="postId">The post identifier.</param>
    /// <param name="userName">The user name.</param>
    /// <returns>The boolean for success / failure</returns>
    public async Task<bool> DeletePostAsync(string postId, string userName)
    {
        try
        {
            logger.LogInformation(LoggingConstants.MethodStartedMessageConstant, nameof(DeletePostAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { postId, userName }));

            var postGuid = DomainUtilities.ValidateAndParsePostId(postId, logger);
            return await postsDataService.DeletePostAsync(postGuid, userName).ConfigureAwait(false);
        }

        catch (Exception ex)
        {
            logger.LogError(ex, LoggingConstants.MethodFailedWithMessageConstant, nameof(DeletePostAsync), DateTime.UtcNow, ex.Message);
            throw;
        }
        finally
        {
            logger.LogInformation(LoggingConstants.MethodEndedMessageConstant, nameof(DeletePostAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { postId, userName }));
        }
    }

    /// <summary>
    /// Gets all posts asynchronous.
    /// </summary>
    /// <param name="userName">The user name</param>
    /// <returns>The list of <see cref="PostWithRatingsDTO"/></returns>
    public async Task<List<PostWithRatingsDomain>> GetAllPostsAsync(string userName)
    {
        try
        {
            logger.LogInformation(LoggingConstants.MethodStartedMessageConstant, nameof(GetAllPostsAsync), DateTime.UtcNow, userName ?? string.Empty);

            if (string.IsNullOrWhiteSpace(userName))
            {
                var result = await postsDataService.GetAllPostsAsync().ConfigureAwait(false);
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

                return postsData;
            }
            else
            {
                return await postRatingsDataService.GetAllPostsWithRatingsAsync(userName).ConfigureAwait(false);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetAllPostsAsync), DateTime.UtcNow, ex.Message);
            throw;
        }
        finally
        {
            logger.LogInformation(LoggingConstants.MethodEndedMessageConstant, nameof(GetAllPostsAsync), DateTime.UtcNow, userName ?? string.Empty);
        }
    }
}
