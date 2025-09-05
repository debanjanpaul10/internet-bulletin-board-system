namespace IBBS.Domain.DomainEntities.Posts;

/// <summary>
/// The PostDomain Ratings Entity Class.
/// </summary>
public class PostRatingDomain
{
    /// <summary>
    /// Gets or sets the post rating id.
    /// </summary>
    /// <value>
    /// The post rating id.
    /// </value>
    public int PostRatingId { get; set; }

    /// <summary>
    /// Gets or sets the post id.
    /// </summary>
    /// <value>
    /// The post id.
    /// </value>
    public Guid PostId { get; set; }

    /// <summary>
    /// Gets or sets the user name.
    /// </summary>
    /// <value>
    /// The user name.
    /// </value>
    public string UserName { get; set; } = string.Empty;

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
    /// The previous rating value.
    /// </value>
    public int RatingValue { get; set; }

    /// <summary>
    /// Gets or sets the is active.
    /// </summary>
    /// <value>
    /// The is active.
    /// </value>
    public bool IsActive { get; set; }
}