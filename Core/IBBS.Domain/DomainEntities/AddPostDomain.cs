namespace IBBS.Domain.DomainEntities;

/// <summary>
/// The add post domain model.
/// </summary>
public class AddPostDomain
{
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
	/// Gets or sets the post created by.
	/// </summary>
	/// <value>
	/// The post created by.
	/// </value>
	public string PostCreatedBy { get; set; } = string.Empty;
}
