// *********************************************************************************
//	<copyright file="NewUserDTO.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The New User DTO.</summary>
// *********************************************************************************

namespace InternetBulletin.Shared.DTOs
{
	/// <summary>
	/// The New User DTO.
	/// </summary>
	public class NewUserDTO
	{
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

	}
}
