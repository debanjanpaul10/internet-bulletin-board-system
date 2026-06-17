namespace IBBS.Infrastructure.Persistence.Adapters.Models;

/// <summary>
/// The domain entity for Bug Reports.
/// </summary>
/// <seealso cref="System.IEquatable&lt;IBBS.Infrastructure.Persistence.Adapters.Models.BugReportEntity&gt;" />
public sealed record BugReportEntity
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>
    /// The identifier.
    /// </value>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the title.
    /// </summary>
    /// <value>
    /// The title.
    /// </value>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    /// <value>
    /// The description.
    /// </value>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the bug severity identifier.
    /// </summary>
    /// <value>
    /// The bug severity identifier.
    /// </value>
    public int BugSeverity { get; set; }

    /// <summary>
    /// Gets or sets the bug status identifier.
    /// </summary>
    /// <value>
    /// The bug status identifier.
    /// </value>
    public int BugStatus { get; set; }

    /// <summary>
    /// Gets or sets the page URL.
    /// </summary>
    /// <value>
    /// The page URL.
    /// </value>
    public string PageUrl { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether this instance is active.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
    /// </value>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Gets or sets the date created.
    /// </summary>
    /// <value>
    /// The date created.
    /// </value>
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the created by.
    /// </summary>
    /// <value>
    /// The created by.
    /// </value>
    public string CreatedBy { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date modified.
    /// </summary>
    /// <value>
    /// The date modified.
    /// </value>
    public DateTime DateModified { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the modified by.
    /// </summary>
    /// <value>
    /// The modified by.
    /// </value>
    public string ModifiedBy { get; set; } = string.Empty;
}
