namespace IBBS.Domain.DomainEntities.AI;

/// <summary>
/// The Bug Severity AI request domain model.
/// </summary>
public class BugSeverityAIRequestDomain
{
    /// <summary>
    /// Gets or sets the bug title.
    /// </summary>
    /// <value>
    /// The bug title.
    /// </value>
    public string BugTitle { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the bug description.
    /// </summary>
    /// <value>
    /// The bug description.
    /// </value>
    public string BugDescription { get; set; } = string.Empty;
}
