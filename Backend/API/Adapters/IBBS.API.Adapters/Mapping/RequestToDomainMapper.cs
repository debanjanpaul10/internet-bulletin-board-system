using IBBS.API.Adapters.Models;
using IBBS.API.Adapters.Models.AI;
using IBBS.API.Adapters.Models.Posts;
using IBBS.Domain.DomainEntities;
using IBBS.Domain.DomainEntities.AI;
using IBBS.Domain.DomainEntities.Posts;

namespace IBBS.API.Adapters.Mapping;

/// <summary>
/// The request to domain mapper for AI services.
/// </summary>
public static class RequestToDomainMapper
{
    /// <summary>
    /// Maps the user story request DTO to domain model.
    /// </summary>
    /// <param name="requestDTO">The user story request DTO.</param>
    /// <returns>The user story request domain model.</returns>
    internal static UserStoryRequestDomain MapToDomain(
        UserStoryRequestDTO requestDto
    ) => new()
    {
        Story = requestDto.Story
    };

    /// <summary>
    /// Maps the bug severity AI request DTO to domain model.
    /// </summary>
    /// <param name="requestDto">The bug severity AI request DTO.</param>
    /// <returns>The bug severity AI request domain model.</returns>
    internal static BugSeverityAIRequestDomain MapToDomain(
        BugSeverityAIRequestDTO requestDto
    ) => new()
    {
        BugDescription = requestDto.BugDescription,
        BugTitle = requestDto.BugTitle,
    };

    /// <summary>
    /// Maps the user query request DTO to domain model.
    /// </summary>
    /// <param name="requestDto">The user query request DTO.</param>
    /// <returns>The user query request domain model.</returns>
    internal static UserQueryRequestDomain MapToDomain(
        UserQueryRequestDTO requestDto
    ) => new()
    {
        UserQuery = requestDto.UserQuery
    };

    /// <summary>
    /// Maps the AI response feedback DTO to domain model.
    /// </summary>
    /// <param name="requestDto">The AI response feedback DTO.</param>
    /// <returns>The AI response feedback domain model.</returns>
    internal static AIResponseFeedbackDomain MapToDomain(
        AIResponseFeedbackDTO requestDto
    ) => new()
    {
        AIResponse = requestDto.AIResponse,
        FeedbackComments = requestDto.FeedbackComments,
        IsNegativeFeedback = requestDto.IsNegativeFeedback,
        IsPositiveFeedback = requestDto.IsPositiveFeedback,
        UserQuery = requestDto.UserQuery
    };

    /// <summary>
    /// Maps the bug report DTO to domain model.
    /// </summary>
    /// <param name="requestDto">The bug report request dto.</param>
    /// <returns>The bug report domain model.</returns>
    internal static BugReportDomain MapToDomain(
        BugReportDTO requestDto
    ) => new()
    {
        BugSeverity = requestDto.BugSeverity,
        BugStatus = requestDto.BugStatus,
        CreatedBy = requestDto.CreatedBy,
        Description = requestDto.BugDescription,
        Title = requestDto.BugTitle,
        PageUrl = requestDto.PageUrl
    };

    /// <summary>
    /// Maps the <see cref="PostRatingDTO"/> to <see cref="PostRatingDomain"/>.
    /// </summary>
    /// <param name="requestDto">The request dto.</param>
    /// <returns>The post rating domain model.</returns>
    internal static PostRatingDomain MapToDomain(
        PostRatingDTO requestDto
    ) => new()
    {
        PostId = Guid.Parse(requestDto.PostId)
    };

    /// <summary>
    /// Maps the <see cref="AddPostDTO"/> to <see cref="AddPostDomain"/>
    /// </summary>
    /// <param name="requestDto">The request dto.</param>
    /// <returns>The add post domain model.</returns>
    internal static AddPostDomain MapToDomain(
        AddPostDTO requestDto
    ) => new()
    {
        PostContent = requestDto.PostContent,
        PostCreatedBy = requestDto.PostCreatedBy,
        PostTitle = requestDto.PostTitle,
    };

    /// <summary>
    /// Maps the <see cref="UpdatePostDTO"/> to <see cref="UpdatePostDomain"/>.
    /// </summary>
    /// <param name="requestDto">The request dto.</param>
    /// <returns>The update post domain model.</returns>
    internal static UpdatePostDomain MapToDomain(
        UpdatePostDTO requestDto
    ) => new()
    {
        PostContent = requestDto.PostContent,
        PostId = requestDto.PostId,
        PostRating = requestDto.PostRating,
        PostTitle = requestDto.PostTitle
    };
}