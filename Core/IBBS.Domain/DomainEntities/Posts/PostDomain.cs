namespace IBBS.Domain.DomainEntities.Posts;

/// <summary>
/// The Post Domain model Class.
/// </summary>
public class PostDomain : UserPostDomain
{
	/// <summary>
	/// Gets or sets the content of the post.
	/// </summary>
	/// <value>
	/// The content of the post.
	/// </value>
	public string PostContent { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets a value indicating whether this instance is active.
	/// </summary>
	/// <value>
	///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
	/// </value>
	public bool IsActive { get; set; }

}
