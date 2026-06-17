using IBBS.Infrastructure.Persistence.Adapters.Contracts;
using IBBS.Infrastructure.Persistence.Adapters.Models;
using Microsoft.EntityFrameworkCore;

namespace IBBS.Infrastructure.Persistence.Adapters.Repositories;

/// <summary>
/// The implementation of the profiles repository.
/// </summary>
/// <remarks>This class implements methods for getting user posts and ratings from the database.</remarks>
/// <param name="unitOfWork">The unit of work.</param>
/// <param name="dbContext">The database context.</param>
/// <seealso cref="IProfilesRepository"/>
public sealed class ProfilesRepository(
    IUnitOfWork unitOfWork,
    SqlDbContext dbContext) : IProfilesRepository
{
    /// <inheritdoc />
    public async Task<IEnumerable<PostEntity>> GetUserPostsAsync(
        string userEmail,
        CancellationToken cancellationToken = default
    ) =>
        await unitOfWork.Repository<PostEntity>().GetAllAsync(
            filter: p => p.PostOwnerUserName == userEmail && p.IsActive,
            cancellationToken: cancellationToken
        ).ConfigureAwait(false);


    /// <inheritdoc />
    public async Task<IEnumerable<UserPostRating>> GetUserRatingsAsync(
        string userEmail,
        CancellationToken cancellationToken = default
    ) =>
        await dbContext.PostRatings
            .Where(pr => pr.UserName == userEmail && pr.IsActive && pr.RatingValue == 1)
            .Join(
                dbContext.Posts.Where(p => p.IsActive),
                pr => pr.PostId,
                p => p.PostId,
                (pr, p) => new UserPostRating
                {
                    PostName = p.PostTitle,
                    RatedOn = pr.RatedOn,
                    CurrentRatingValue = pr.RatingValue
                })
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

}