// *********************************************************************************
//	<copyright file="UserProfileDto.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The User Profile DTO.</summary>
// *********************************************************************************

namespace InternetBulletin.Shared.DTOs
{
	/// <summary>
	/// The User Profile DTO.
	/// </summary>
	public class UserProfileDto
	{
		/// <summary>
		/// Gets or sets the user identifier.
		/// </summary>
		/// <value>
		/// The user identifier.
		/// </value>
		public int UserId { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the user email.
		/// </summary>
		/// <value>
		/// The user email.
		/// </value>
		public string UserEmail { get; set; }

		/// <summary>
		/// Gets or sets the user alias.
		/// </summary>
		/// <value>
		/// The user alias.
		/// </value>
		public string UserAlias { get; set; }

		/// <summary>
		/// Gets or sets the user password.
		/// </summary>
		/// <value>
		/// The user password.
		/// </value>
		public string UserPassword { get; set; }

		/// <summary>
		/// Gets or sets the user posts.
		/// </summary>
		/// <value>
		/// The user posts.
		/// </value>
		public List<UserPostsDto> UserPosts { get; set; }
	}
	
}
