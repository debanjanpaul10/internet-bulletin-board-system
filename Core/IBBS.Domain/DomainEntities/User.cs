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
		public string Id { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string DisplayName { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the user name.
		/// </summary>
		/// <value>
		/// The user name.
		/// </value>
		public string UserName { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the email address.
		/// </summary>
		/// <value>
		/// The email address.
		/// </value>
		public string EmailAddress { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the date created.
		/// </summary>
		/// <value>
		/// The date created.
		/// </value>
		public DateTime DateCreated { get; set; }

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
