namespace IBBS.API.Adapters.Models.AI;

/// <summary>
/// The Bug Severity AI Request DTO model.
/// </summary>
public class BugSeverityAIRequestDTO
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
