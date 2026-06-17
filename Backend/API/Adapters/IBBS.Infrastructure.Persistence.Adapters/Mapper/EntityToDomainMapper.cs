using IBBS.Domain.DomainEntities;
using IBBS.Domain.DomainEntities.Posts;
using IBBS.Infrastructure.Persistence.Adapters.Models;

namespace IBBS.Infrastructure.Persistence.Adapters.Mapper;

/// <summary>
/// The entity to domain mapper.
/// </summary>
/// <remarks>This class provides methods for mapping entities to domain objects.</remarks>
internal static class EntityToDomainMapper
{
    /// <summary>
    /// Maps the <see cref="AiUsageEntity"/> to <see cref="AiUsageDomain"/>
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>The AI usage domain.</returns>
    internal static LookupMasterDomain MapToDomain(
        LookupMasterEntity entity
    ) => new()
    {
        CreatedBy = entity.CreatedBy,
        DateCreated = entity.DateCreated,
        DateModified = entity.DateModified,
        Id = entity.Id,
        IsActive = entity.IsActive,
        KeyName = entity.KeyName,
        KeyValue = entity.KeyValue,
        ModifiedBy = entity.ModifiedBy,
        Type = entity.Type
    };

    /// <summary>
    /// Maps the <see cref="PostEntity"/> to <see cref="PostDomain"/>
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>The post domain.</returns>
    internal static PostRatingDomain MapToDomain(
        PostRatingEntity entity
    ) => new()
    {
        IsActive = entity.IsActive,
        PostId = entity.PostId,
        RatingValue = entity.RatingValue,
        UserName = entity.UserName,
        PostRatingId = entity.PostRatingId,
        RatedOn = entity.RatedOn
    };

    /// <summary>
    /// Maps the <see cref="PostWithRatings"/> to <see cref="PostWithRatingsDomain"/>
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>The post domain.</returns>
    internal static PostWithRatingsDomain MapToDomain(
        PostWithRatings entity
    ) => new()
    {
        PostId = entity.PostId,
        PostTitle = entity.PostTitle,
        PostContent = entity.PostContent,
        PostCreatedDate = entity.PostCreatedDate,
        PostOwnerUserName = entity.PostOwnerUserName,
        Ratings = entity.Ratings,
        IsActive = entity.IsActive,
        RatingValue = entity.RatingValue
    };

    /// <summary>
    /// Maps the <see cref="UserPostRating"/> to <see cref="UserPostRatingDomain"/>.
    /// </summary>
    /// <param name="entity">The user post rating entity.</param>
    /// <returns>The user post rating domain.</returns>
    internal static UserPostRatingDomain MapToDomain(
        UserPostRating entity
    ) => new()
    {
        CurrentRatingValue = entity.CurrentRatingValue,
        PostName = entity.PostName,
        RatedOn = entity.RatedOn
    };

    /// <summary>
    /// Maps the <see cref="PostEntity"/> to <see cref="PostDomain"/>
    /// </summary>
    /// <param name="entity">The post entity.</param>
    /// <returns>The post domain.</returns>
    internal static PostDomain MapToDomain(
        PostEntity entity
    ) => new()
    {
        IsActive = entity.IsActive,
        PostContent = entity.PostContent,
        PostCreatedDate = entity.PostCreatedDate,
        PostId = entity.PostId,
        PostOwnerUserName = entity.PostOwnerUserName,
        PostTitle = entity.PostTitle,
        Ratings = entity.Ratings
    };
}
