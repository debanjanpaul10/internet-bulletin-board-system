using IBBS.Domain.DomainEntities.Posts;

namespace IBBS.Domain.DomainEntities;

/// <summary>
/// The user profile domain model.
/// </summary>
public class UserProfileDomain
{
	/// <summary>
	/// Gets or sets the email address.
	/// </summary>
	/// <value>
	/// The email address.
	/// </value>
	public string EmailAddress { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the user posts.
	/// </summary>
	/// <value>
	/// The user posts.
	/// </value>
	public List<UserPostDomain> UserPosts { get; set; } = [];

	/// <summary>
	/// Gets or sets the user post ratings.
	/// </summary>
	/// <value>
	/// The user post ratings.
	/// </value>
	public List<UserPostRatingDomain> UserPostRatings { get; set; } = [];
}
