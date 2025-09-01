namespace IBBS.Domain.DomainEntities;

/// <summary>
/// The Update post domain model.
/// </summary>
public class UpdatePostDomain
{
	/// <summary>
	/// Gets or sets the post identifier.
	/// </summary>
	/// <value>
	/// The post identifier.
	/// </value>
	public Guid PostId { get; set; }

	/// <summary>
	/// Gets or sets the post title.
	/// </summary>
	/// <value>
	/// The post title.
	/// </value>
	public string PostTitle { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the content of the post.
	/// </summary>
	/// <value>
	/// The content of the post.
	/// </value>
	public string PostContent { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the post rating.
	/// </summary>
	/// <value>
	/// The post rating.
	/// </value>
	public int? PostRating { get; set; }
}
