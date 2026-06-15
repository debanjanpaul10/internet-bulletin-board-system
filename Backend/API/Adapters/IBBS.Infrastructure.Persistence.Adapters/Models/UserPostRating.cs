namespace IBBS.Infrastructure.Persistence.Adapters.Models;

/// <summary>
/// The user post rating model.
/// </summary>
public sealed record UserPostRating
{
    /// <summary>
    /// Gets or sets the post name.
    /// </summary>
    /// <value>
    /// The post name.
    /// </value>
    public string PostName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the rated on.
    /// </summary>
    /// <value>
    /// The rated on.
    /// </value>
    public DateTime RatedOn { get; set; }

    /// <summary>
    /// Gets or sets the current rating value.
    /// </summary>
    /// <value>
    /// The current rating value.
    /// </value>
    public int CurrentRatingValue { get; set; }
}