namespace IBBS.API.Adapters.Models.Users;

/// <summary>
/// The User Profile DTO.
/// </summary>
public class UserProfileDto
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
	public List<UserPostDTO> UserPosts { get; set; } = [];

	/// <summary>
	/// Gets or sets the user post ratings.
	/// </summary>
	/// <value>
	/// The user post ratings.
	/// </value>
	public List<UserPostRatingDTO> UserPostRatings { get; set; } = [];
}
