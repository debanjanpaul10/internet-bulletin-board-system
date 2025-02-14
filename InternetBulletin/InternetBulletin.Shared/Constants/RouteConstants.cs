// *********************************************************************************
//	<copyright file="RouteConstants.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Route Constants Class.</summary>
// *********************************************************************************

namespace InternetBulletin.Shared.Constants
{
	/// <summary>
	/// The Route Constants Class.
	/// </summary>
	public static class RouteConstants
	{
		#region WEB

		/// <summary>
		/// The get resource URL route prefix
		/// </summary>
		public const string GetResourceUrl_RoutePrefix = "GetResourceUrl";

		/// <summary>
		/// The post resource URL route prefix
		/// </summary>
		public const string PostResourceUrl_RoutePrefix = "PostResourceUrl";

		#endregion

		#region API

		/// <summary>
		/// The posts base route prefix
		/// </summary>
		public const string PostsBase_RoutePrefix = "Posts";

		/// <summary>
		/// The get post route
		/// </summary>
		public const string GetPost_Route = "GetPost";

		/// <summary>
		/// Creates new post route.
		/// </summary>
		public const string NewPost_Route = "AddPost";

		#endregion
	}
}
