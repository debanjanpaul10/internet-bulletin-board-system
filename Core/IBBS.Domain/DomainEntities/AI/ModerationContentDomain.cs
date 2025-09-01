namespace IBBS.Domain.DomainEntities.AI;

/// <summary>
/// The moderation content response domain model.
/// </summary>
public class ModerationContentDomain
{
    /// <summary>
    /// The content rating for the user story.
    /// </summary>
    public string ContentRating { get; set; } = string.Empty;

    /// <summary>
    /// The total tokens consumed.
    /// </summary>
    public int TotalTokensConsumed { get; set; }

    /// <summary>
    /// The candidates token count.
    /// </summary>
    public int CandidatesTokenCount { get; set; }

    /// <summary>
    /// The prompt token count.
    /// </summary>
    public int PromptTokenCount { get; set; }
}
