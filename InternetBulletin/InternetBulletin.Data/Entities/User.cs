// *********************************************************************************
//	<copyright file="User.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The User Entity Class.</summary>
// *********************************************************************************

namespace InternetBulletin.Data.Entities
{
	/// <summary>
	/// The User Entity Class.
	/// </summary>
	public class User
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
		/// Gets or sets a value indicating whether this instance is active.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
		/// </value>
		public bool IsActive { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is admin.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is admin; otherwise, <c>false</c>.
		/// </value>
		public bool IsAdmin { get; set; }
	}
}
