using IBBS.Domain.DomainEntities.Posts;
using IBBS.Infrastructure.Persistence.Adapters.Contracts;
using IBBS.Infrastructure.Persistence.Adapters.Models;

namespace IBBS.Infrastructure.Persistence.Adapters.Repositories;

/// <summary>
/// The implementation of the posts repository.
/// </summary>
/// <remarks>This class implements methods for getting posts and updating posts from the database.</remarks>
/// <param name="unitOfWork">The unit of work.</param>
/// <seealso cref="IPostsRepository"/>
public sealed class PostsRepository(IUnitOfWork unitOfWork) : IPostsRepository
{
    /// <inheritdoc />
    public async Task<bool> AddNewPostAsync(
        PostEntity newPost,
        string userName,
        CancellationToken cancellationToken = default
    )
    {
        var postId = Guid.NewGuid();
        var existingPost = await unitOfWork.Repository<PostEntity>().GetAsync(
            filter: x => x.PostId == postId && x.IsActive,
            cancellationToken: cancellationToken
        ).ConfigureAwait(false);
        if (existingPost is not null)
        {
            var dbPostData = new PostEntity()
            {
                PostId = postId,
                PostContent = newPost.PostContent,
                PostTitle = newPost.PostTitle,
                IsActive = true,
                PostCreatedDate = DateTime.UtcNow,
                PostOwnerUserName = userName,
                Ratings = 0
            };

            await unitOfWork.Repository<PostEntity>().AddAsync(
                entity: dbPostData,
                cancellationToken
            ).ConfigureAwait(false);

            await unitOfWork.SaveChangesAsync(
                cancellationToken
            ).ConfigureAwait(false);
            return true;
        }

        return false;
    }

    /// <inheritdoc />
    public async Task<bool> DeletePostAsync(
        Guid postId,
        string userName,
        CancellationToken cancellationToken = default
    )
    {
        var dbPostData = await unitOfWork.Repository<PostEntity>().FirstOrDefaultAsync(
            predicate: post => post.PostId == postId && post.IsActive && post.PostOwnerUserName == userName,
            cancellationToken
        ).ConfigureAwait(false);
        if (dbPostData is not null)
        {
            dbPostData.IsActive = false;
            await unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return true;
        }

        return false;
    }

    /// <inheritdoc />
    public async Task<List<PostEntity>> GetAllPostsAsync(
        CancellationToken cancellationToken = default
    ) =>
        await unitOfWork.Repository<PostEntity>().GetAllAsync(
            filter: x => x.IsActive,
            cancellationToken: cancellationToken
        ).ConfigureAwait(false);


    /// <inheritdoc />
    public async Task<PostEntity> GetPostAsync(
        Guid postId,
        string userName,
        bool isForCurrentUser,
        CancellationToken cancellationToken = default
    )
    {
        PostEntity? dbResponse = new();
        var postEntity = await unitOfWork.Repository<PostEntity>().GetAllAsync(
            filter: p => p.PostId == postId && p.IsActive,
            cancellationToken: cancellationToken
        ).ConfigureAwait(false);
        if (string.IsNullOrEmpty(userName))
        {
            dbResponse = postEntity.FirstOrDefault() ?? new();
        }
        else
        {
            postEntity = isForCurrentUser
                ? [.. postEntity.Where(p => p.PostOwnerUserName == userName)]
                : [.. postEntity.Where(p => p.PostOwnerUserName != userName)];

            dbResponse = postEntity.FirstOrDefault() ?? new();
        }

        return dbResponse;
    }

    /// <inheritdoc />
    public async Task<PostEntity> UpdatePostAsync(
        UpdatePostDomain updatedPost,
        string userName,
        bool isRatingUpdate,
        CancellationToken cancellationToken = default
    )
    {
        if (isRatingUpdate)
        {
            return await this.HandleRatingUpdateForPostAsync(
                updatedPost,
                cancellationToken
            ).ConfigureAwait(false);
        }
        else
        {
            var dbPostData = await unitOfWork.Repository<PostEntity>().FirstOrDefaultAsync(
                predicate: x => x.PostId == updatedPost.PostId && x.IsActive && x.PostOwnerUserName == userName,
                cancellationToken
            ).ConfigureAwait(false);
            if (dbPostData is not null)
            {
                dbPostData.PostTitle = updatedPost.PostTitle;
                dbPostData.PostContent = updatedPost.PostContent;

                await unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                return dbPostData;
            }
            else
            {
                return default!;
            }
        }
    }

    /// <summary>
    /// Handles the rating update for post asynchronous.
    /// </summary>
    /// <param name="updatedPost">The updated post.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated post domain.</returns>
    private async Task<PostEntity> HandleRatingUpdateForPostAsync(
        UpdatePostDomain updatedPost,
        CancellationToken cancellationToken
    )
    {
        var dbPostData = await unitOfWork.Repository<PostEntity>().FirstOrDefaultAsync(
            predicate: x => x.PostId == updatedPost.PostId && x.IsActive,
            cancellationToken
        ).ConfigureAwait(false);
        if (dbPostData is not null)
        {
            if (updatedPost.PostRating.HasValue)
                dbPostData.Ratings = updatedPost.PostRating.Value;

            await unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return dbPostData;
        }

        return default!;
    }
}