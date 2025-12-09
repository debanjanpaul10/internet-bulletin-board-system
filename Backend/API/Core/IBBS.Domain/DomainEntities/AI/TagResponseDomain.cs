namespace IBBS.Domain.DomainEntities.AI;

/// <summary>
/// The user story tag response domain model.
/// </summary>
public class TagResponseDomain
{
    /// <summary>
    /// The user story genre.
    /// </summary>
    public string UserStoryTag { get; set; } = string.Empty;

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

