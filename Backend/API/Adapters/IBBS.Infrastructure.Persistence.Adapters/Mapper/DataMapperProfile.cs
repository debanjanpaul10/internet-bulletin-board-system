using IBBS.Domain.DomainEntities;
using IBBS.Domain.DomainEntities.AI;
using IBBS.Domain.DomainEntities.Posts;
using IBBS.Infrastructure.Persistence.Adapters.Models;

namespace IBBS.Infrastructure.Persistence.Adapters.Mapper;

/// <summary>
/// The data mapper profile
/// </summary>
public static class DataMapperProfile
{
    #region Domain -> Entity

    /// <summary>
    /// Maps the <see cref="AiUsageDomain"/> to <see cref="AiUsageEntity"/>
    /// </summary>
    /// <param name="domain">The domain model.</param>
    /// <returns>The AI usage entity.</returns>
    internal static AiUsageEntity MapToEntity(
        AiUsageDomain domain
    ) => new()
    {
        CandidatesTokenCount = domain.CandidatesTokenCount,
        PromptTokenCount = domain.PromptTokenCount,
        TotalTokensConsumed = domain.TotalTokensConsumed,
        Usage = domain.Usage,
        UsageTime = domain.UsageTime,
        UserName = domain.UserName,
        Id = domain.Id,
        IsActive = domain.IsActive
    };

    /// <summary>
    /// Maps the <see cref="BugReportDomain"/> to <see cref="BugReportEntity"/>
    /// </summary>
    /// <param name="domain">The domain model.</param>
    /// <returns>The bug report entity.</returns>
    internal static BugReportEntity MapToEntity(
        BugReportDomain domain
    ) => new()
    {
        BugSeverity = domain.BugSeverity,
        BugStatus = domain.BugStatus,
        Title = domain.Title,
        Description = domain.Description,
        DateModified = domain.DateModified,
        Id = domain.Id,
        IsActive = domain.IsActive,
        ModifiedBy = domain.ModifiedBy,
        DateCreated = domain.DateCreated,
        CreatedBy = domain.CreatedBy,
        PageUrl = domain.PageUrl
    };

    /// <summary>
    /// Maps the <see cref="LookupMasterDomain"/> to <see cref="LookupMasterEntity"/>
    /// </summary>
    /// <param name="domain">The domain model</param>
    /// <returns>The lookup master entity</returns>
    internal static LookupMasterEntity MapToEntity(
        LookupMasterDomain domain
    ) => new()
    {
        CreatedBy = domain.CreatedBy,
        DateCreated = domain.DateCreated,
        DateModified = domain.DateModified,
        Id = domain.Id,
        IsActive = domain.IsActive,
        KeyName = domain.KeyName,
        KeyValue = domain.KeyValue,
        ModifiedBy = domain.ModifiedBy,
        Type = domain.Type
    };

    /// <summary>
    /// Maps the <see cref="PostDomain"/> to <see cref="PostEntity"/>
    /// </summary>
    /// <param name="domain">The domain model</param>
    /// <returns>The post entity</returns>
    internal static PostEntity MapToEntity(
        PostDomain domain
    ) => new()
    {
        IsActive = domain.IsActive,
        PostContent = domain.PostContent,
        PostCreatedDate = domain.PostCreatedDate,
        PostId = domain.PostId,
        PostOwnerUserName = domain.PostOwnerUserName,
        PostTitle = domain.PostTitle,
        Ratings = domain.Ratings
    };

    /// <summary>
    /// Maps the <see cref="PostRatingDomain"/> to <see cref="PostRatingEntity"/>
    /// </summary>
    /// <param name="domain">The domain model</param>
    /// <returns>The post rating entity</returns>
    internal static PostRatingEntity MapToEntity(
        PostRatingDomain domain
    ) => new()
    {
        IsActive = domain.IsActive,
        PostId = domain.PostId,
        RatingValue = domain.RatingValue,
        UserName = domain.UserName,
        PostRatingId = domain.PostRatingId,
        RatedOn = domain.RatedOn
    };

    /// <summary>
    /// Maps the <see cref="User"/> to <see cref="UserEntity"/>
    /// </summary>
    /// <param name="domain">The domain model</param>
    /// <returns>The user entity</returns>
    internal static UserEntity MapToEntity(
        User domain
    ) => new()
    {
        DateCreated = domain.DateCreated,
        DisplayName = domain.DisplayName,
        EmailAddress = domain.EmailAddress,
        Id = domain.Id,
        IsActive = domain.IsActive,
        UserName = domain.UserName,
        IsAdmin = domain.IsAdmin
    };

    #endregion

    #region Entity -> Domain

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
    /// Maps the <see cref="PostEntity"/> to <see cref="PostDomain"/>
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

    #endregion
}
