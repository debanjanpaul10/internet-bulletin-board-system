namespace IBBS.Domain.DomainEntities.Posts;

/// <summary>
/// The User Post domain model.
/// </summary>
public class UserPostDomain
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
	/// Gets or sets the post created date.
	/// </summary>
	/// <value>
	/// The post created date.
	/// </value>
	public DateTime PostCreatedDate { get; set; }

	/// <summary>
	/// Gets or sets the post owner user name.
	/// </summary>
	/// <value>
	/// The post owner user name.
	/// </value>
	public string PostOwnerUserName { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the rating.
	/// </summary>
	/// <value>
	/// The rating.
	/// </value>
	public int Ratings { get; set; }
}
