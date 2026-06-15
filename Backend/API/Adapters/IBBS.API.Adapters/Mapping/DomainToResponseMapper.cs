using IBBS.API.Adapters.Models;
using IBBS.API.Adapters.Models.Users;
using IBBS.Domain.DomainEntities;
using IBBS.Domain.DomainEntities.Posts;

namespace IBBS.API.Adapters.Mapping;

/// <summary>
/// The domain to response mapper for AI services.
/// </summary>
public static class DomainToResponseMapper
{
    /// <summary>
    /// Maps the <see cref="LookupMasterDomain"/> to <see cref="LookupMasterDTO"/>.
    /// </summary>
    /// <param name="domain">The lookup master domain model.</param>
    /// <returns>The lookup master DTO.</returns>
    internal static LookupMasterDTO MapToResponse(
        LookupMasterDomain domain
    ) => new()
    {
        Id = domain.Id,
        KeyName = domain.KeyName,
        KeyValue = domain.KeyValue,
        Type = domain.Type
    };

    /// <summary>
    /// Maps the <see cref="UserProfileDomain"/> to <see cref="UserProfileDto"/>.
    /// </summary>
    /// <param name="domain">The user profile domain model.</param>
    /// <returns>The user profile dto.</returns>
    internal static UserProfileDto MapToResponse(
        UserProfileDomain domain
    ) => new()
    {
        EmailAddress = domain.EmailAddress,
        UserPostRatings = [.. domain.UserPostRatings.Select(MapToResponse)],
        UserPosts = [.. domain.UserPosts.Select(MapToResponse)]
    };

    /// <summary>
    /// Maps the <see cref="UserPostRatingDomain"/> to <see cref="UserPostRatingDTO"/>.
    /// </summary>
    /// <param name="domain">The user post rating domain model.</param>
    /// <returns>The user post rating dto.</returns>
    internal static UserPostRatingDTO MapToResponse(
        UserPostRatingDomain domain
    ) => new()
    {
        CurrentRatingValue = domain.CurrentRatingValue,
        PostName = domain.PostName,
        RatedOn = domain.RatedOn
    };

    /// <summary>
    /// Maps the <see cref="UserPostDomain"/> to <see cref="UserPostDTO"/>.
    /// </summary>
    /// <param name="domain">The user post domain model.</param>
    /// <returns>The user post dto.</returns>
    internal static UserPostDTO MapToResponse(
        UserPostDomain domain
    ) => new()
    {
        PostCreatedDate = domain.PostCreatedDate,
        PostId = domain.PostId,
        PostOwnerUserName = domain.PostOwnerUserName,
        PostTitle = domain.PostTitle,
        Ratings = domain.Ratings
    };
}