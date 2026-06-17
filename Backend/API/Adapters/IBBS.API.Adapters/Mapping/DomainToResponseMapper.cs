using IBBS.API.Adapters.Models;
using IBBS.API.Adapters.Models.Posts;
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

    /// <summary>
    /// Maps the <see cref="PostRatingDomain"/> to <see cref="PostRatingDTO"/>
    /// </summary>
    /// <param name="domain">The domain.</param>
    /// <returns>The updated post rating dto model.</returns>
    internal static PostRatingDTO MapToResponse(
        PostRatingDomain domain
    ) => new()
    {
        PostId = domain.PostId.ToString(),
    };

    /// <summary>
    /// Maps the <see cref="UpdateRatingDomain"/> to <see cref="UpdateRatingDTO"/>
    /// </summary>
    /// <param name="domain">The domain.</param>
    /// <returns>The update rating dto model.</returns>
    internal static UpdateRatingDTO MapToResponse(
        UpdateRatingDomain domain
    ) => new()
    {
        HasAlreadyUpdated = domain.HasAlreadyUpdated,
        IsUpdateSuccess = domain.IsUpdateSuccess,
        PostId = domain.PostId
    };

    /// <summary>
    /// Maps the <see cref="PostWithRatingsDomain"/> to <see cref="PostWithRatingsDTO"/>
    /// </summary>
    /// <param name="domain">The domain.</param>
    /// <returns>The post with ratings dto model.</returns>
    internal static PostWithRatingsDTO MapToResponse(
        PostWithRatingsDomain domain
    ) => new()
    {
        IsActive = domain.IsActive,
        PostContent = domain.PostContent,
        PostCreatedDate = domain.PostCreatedDate,
        PostId = domain.PostId,
        PostOwnerUserName = domain.PostOwnerUserName,
        PostTitle = domain.PostTitle,
        Ratings = domain.Ratings,
        RatingValue = domain.RatingValue
    };

    /// <summary>
    /// Maps the <see cref="PostDomain"/> to <see cref="PostDTO"/>
    /// </summary>
    /// <param name="domain">The domain.</param>
    /// <returns>The post dto model.</returns>
    internal static PostDTO MapToResponse(
        PostDomain domain
    ) => new()
    {
        PostTitle = domain.PostTitle,
        IsActive = domain.IsActive,
        PostContent = domain.PostContent,
        PostCreatedDate = domain.PostCreatedDate,
        PostId = domain.PostId,
        PostOwnerUserName = domain.PostOwnerUserName,
        Ratings = domain.Ratings
    };
}