namespace IBBS.API.Adapters.Models.Posts;

/// <summary>
/// The PostDomain Ratings DTO.
/// </summary>
public class PostRatingDTO
{
    /// <summary>
    /// Gets or sets the post id.
    /// </summary>
    /// <value>
    /// The post id.
    /// </value>
    public string PostId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the incremented or decremented value
    /// </summary>
    /// <value>
    /// The incremented or decremented value.
    /// </value>
    public bool IsIncrement { get; set; }
}