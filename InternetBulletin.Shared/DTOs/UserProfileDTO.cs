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
		public string UserName { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the user posts.
		/// </summary>
		/// <value>
		/// The user posts.
		/// </value>
		public List<UserPostsDto> UserPosts { get; set; } = [];
	}
	
}
