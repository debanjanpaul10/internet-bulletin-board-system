// *********************************************************************************
//	<copyright file="ExceptionConstants.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Exception Constants Class.</summary>
// *********************************************************************************
namespace InternetBulletin.Shared.Constants
{
	/// <summary>
	/// The Exception Constants Class.
	/// </summary>
	public static class ExceptionConstants
	{
		/// <summary>
		/// The user unauthorized message constant
		/// </summary>
		public static readonly string UserUnauthorizedMessageConstant = "User Not Authorized";

		/// <summary>
		/// The post not found message constant
		/// </summary>
		public static readonly string PostNotFoundMessageConstant = "It seems the post you are looking for does not exists anymore!";

		/// <summary>
		/// Something went wrong message constant
		/// </summary>
		public static readonly string SomethingWentWrongMessageConstant = "Oops! Something went wrong. Please try again after sometime.";
	}
}
