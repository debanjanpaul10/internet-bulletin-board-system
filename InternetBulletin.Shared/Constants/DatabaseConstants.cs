// *********************************************************************************
//	<copyright file="DatabaseConstants.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Database Constants Class.</summary>
// *********************************************************************************

namespace InternetBulletin.Shared.Constants
{
	/// <summary>
	/// The Database Constants Class.
	/// </summary>
	public static class DatabaseConstants
	{
		#region Data Type Constants

		/// <summary>
		/// The integer data type constant
		/// </summary>
		public const string IntegerDataTypeConstant = "int";

		/// <summary>
		/// The n variable character maximum data type constant
		/// </summary>
		public const string NVarCharMaxDataTypeConstant = "nvarchar(max)";

		/// <summary>
		/// The bit data type constant
		/// </summary>
		public const string BitDataTypeConstant = "bit";

		#endregion

		/// <summary>
		/// The users table constant
		/// </summary>
		public const string UsersTableConstant = "Users";

		/// <summary>
		/// The primary key users constant
		/// </summary>
		public const string PrimaryKeyUsersConstant = "PK_Users";

		/// <summary>
		/// The user identifier constant
		/// </summary>
		public const string UserIdConstant = "UserId";

		/// <summary>
		/// The name constant
		/// </summary>
		public const string NameConstant = "Name";

		/// <summary>
		/// The user email constant
		/// </summary>
		public const string UserEmailConstant = "UserEmail";

		/// <summary>
		/// The user alias constant
		/// </summary>
		public const string UserAliasConstant = "UserAlias";

		/// <summary>
		/// The user password constant
		/// </summary>
		public const string UserPasswordConstant = "UserPassword";

		/// <summary>
		/// The is active constant
		/// </summary>
		public const string IsActiveConstant = "IsActive";

		/// <summary>
		/// The is admin constant
		/// </summary>
		public const string IsAdminConstant = "IsAdmin";
	}
}
