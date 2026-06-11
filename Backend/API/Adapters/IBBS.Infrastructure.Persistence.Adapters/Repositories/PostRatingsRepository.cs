using IBBS.Infrastructure.Persistence.Adapters.Contracts;
using IBBS.Infrastructure.Persistence.Adapters.Models;
using Microsoft.EntityFrameworkCore;

namespace IBBS.Infrastructure.Persistence.Adapters.Repositories;

/// <summary>
/// The implementation of the post ratings repository.
/// </summary>
/// <remarks>This class implements methods for getting post ratings from the database.</remarks>
/// <param name="unitOfWork">The unit of work.</param>
/// <param name="dbContext">The database context.</param>
/// <seealso cref="IPostRatingsRepository"/>
public sealed class PostRatingsRepository(
    IUnitOfWork unitOfWork,
    SqlDbContext dbContext) : IPostRatingsRepository
{
    /// <inheritdoc />W
    public async Task<bool> AddPostRatingAsync(
        PostRatingEntity postRating,
        CancellationToken cancellationToken = default
    )
    {
        await unitOfWork.Repository<PostRatingEntity>()
            .AddAsync(entity: postRating, cancellationToken)
            .ConfigureAwait(false);

        var savedCount = await unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return savedCount > 0;
    }

    /// <inheritdoc />
    public async Task<List<PostWithRatings>> GetAllPostsWithRatingsAsync(
        string userName,
        CancellationToken cancellationToken = default
    )
    {
        var query = from post in dbContext.Posts
                    where post.IsActive
                    join rating in dbContext.PostRatings.Where(r => r.UserName == userName && r.IsActive)
                    on post.PostId equals rating.PostId into ratings
                    from rating in ratings.DefaultIfEmpty()
                    select new PostWithRatings
                    {
                        PostId = post.PostId,
                        PostTitle = post.PostTitle,
                        PostContent = post.PostContent,
                        PostCreatedDate = post.PostCreatedDate,
                        PostOwnerUserName = post.PostOwnerUserName,
                        Ratings = post.Ratings,
                        IsActive = post.IsActive,
                        RatingValue = rating != null ? rating.RatingValue : 0
                    };

        var result = await query.ToListAsync(cancellationToken).ConfigureAwait(false);
        return result;
    }

    /// <inheritdoc />
    public async Task<List<PostRatingEntity>> GetAllUserPostRatingsAsync(
        string userName,
        CancellationToken cancellationToken = default
    ) =>
        await unitOfWork.Repository<PostRatingEntity>()
            .GetAllAsync(filter: r => r.UserName == userName && r.IsActive, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

    /// <inheritdoc />
    public async Task<PostRatingEntity> GetPostRatingAsync(
        Guid postId,
        string userName,
        CancellationToken cancellationToken = default
    ) =>
        await unitOfWork.Repository<PostRatingEntity>()
            .GetAsync(filter: r => r.PostId == postId && r.UserName == userName && r.IsActive, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

    /// <inheritdoc />
    public async Task<bool> UpdatePostRatingAsync(
        PostRatingEntity postRating,
        CancellationToken cancellationToken = default
    )
    {
        var existingPostRating = await unitOfWork.Repository<PostRatingEntity>()
            .FirstOrDefaultAsync(predicate: r => r.PostId == postRating.PostId && r.UserName == postRating.UserName, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (existingPostRating is not null)
        {
            existingPostRating.RatedOn = DateTime.UtcNow;
            var savedCounts = await unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return savedCounts > 0;
        }
        else
        {
            return false;
        }
    }
}