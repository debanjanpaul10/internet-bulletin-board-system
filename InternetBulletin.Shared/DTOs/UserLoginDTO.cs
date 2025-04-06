// *********************************************************************************
//	<copyright file="UserLoginDTO.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>User login DTO.</summary>
// *********************************************************************************

namespace InternetBulletin.Shared.DTOs
{
	/// <summary>
	/// User login DTO.
	/// </summary>
	public class UserLoginDTO
	{
		/// <summary>
		/// Gets or sets the user email.
		/// </summary>
		/// <value>
		/// The user email.
		/// </value>
		public string UserEmail { get; set; }

		/// <summary>
		/// Gets or sets the user password.
		/// </summary>
		/// <value>
		/// The user password.
		/// </value>
		public string UserPassword { get; set; }
	}
}