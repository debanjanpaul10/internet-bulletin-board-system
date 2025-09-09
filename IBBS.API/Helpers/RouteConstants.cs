// *********************************************************************************
//	<copyright file="RouteConstants.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Route Constants Class.</summary>
// *********************************************************************************

namespace IBBS.API.Helpers;

/// <summary>
/// The Route Constants Class.
/// </summary>
internal static class RouteConstants
{
	/// <summary>
	/// The posts controller.
	/// </summary>
	internal static class PostsController
	{
		/// <summary>
		/// The posts base route prefix
		/// </summary>
		internal const string BaseRoute = "Posts";

		/// <summary>
		/// The get post route
		/// </summary>
		internal const string GetPost_Route = "GetPost";

		/// <summary>
		/// The get all posts route
		/// </summary>
		internal const string GetAllPosts_Route = "GetAllPosts";

		/// <summary>
		/// Creates new post route.
		/// </summary>
		internal const string NewPost_Route = "AddPost";

		/// <summary>
		/// The update post route
		/// </summary>
		internal const string UpdatePost_Route = "UpdatePost";

		/// <summary>
		/// The delete post route
		/// </summary>
		internal const string DeletePost_Route = "DeletePost";
	}

	internal static class PostRatingsController
	{
		/// <summary>
		/// The post ratings base_ route prefix.
		/// </summary>
		internal const string BaseRoute = "PostRatings";

		/// <summary>
		/// The update vote route.
		/// </summary>
		internal const string UpdateRating_Route = "UpdateRating";

		/// <summary>
		/// The get all user ratings route.
		/// </summary>
		internal const string GetAllUserRatings_Route = "GetAllUserRatings";
	}

	/// <summary>
	/// The users controller.
	/// </summary>
	internal static class UsersController
	{
		/// <summary>
		/// The users base route prefx
		/// </summary>
		internal const string BaseRoute = "Users";

		/// <summary>
		/// The get users data from graph_ route.
		/// </summary>
		internal const string GetUsersDataFromGraph_Route = "GetUsersDataGraph";

		/// <summary>
		/// The get user route
		/// </summary>
		internal const string GetUser_Route = "GetUser";

		/// <summary>
		/// The get all users route
		/// </summary>
		internal const string GetAllUsers_Route = "GetAllUsers";

		/// <summary>
		/// Creates new user_route.
		/// </summary>
		internal const string NewUser_Route = "NewUser";

		/// <summary>
		/// The update user route
		/// </summary>
		internal const string UpdateUser_Route = "UpdateUser";

		/// <summary>
		/// The delete user route
		/// </summary>
		internal const string DeleteUser_Route = "DeleteUser";
	}

	/// <summary>
	/// The profiles controller.
	/// </summary>
	internal static class ProfilesController
	{
		/// <summary>
		/// The profiles base_ route prefix.
		/// </summary>
		internal const string BaseRoute = "Profiles";

		/// <summary>
		/// The get user profile data route.
		/// </summary>
		internal const string GetUserProfileData_Route = "GetUserProfileData";
	}

	/// <summary>
	/// The ai services controller.
	/// </summary>
	internal static class AiServicesController
	{
		/// <summary>
		/// The base route
		/// </summary>
		internal const string BaseRoute = "AiServices";

		/// <summary>
		/// The rewrite with ai route
		/// </summary>
		internal const string RewriteWithAI_Route = "RewriteWithAI";

		/// <summary>
		/// The Application information data
		/// </summary>
		internal const string GetAboutUsData_Route = "GetAboutUsData";

		/// <summary>
		/// The generate genre tag route.
		/// </summary>
		internal const string GenerateGenreTag_Route = "GenerateGenreTag";

		/// <summary>
		/// The moderate content route.
		/// </summary>
		internal const string ModerateContent_Route = "ModerateContent";

		/// <summary>
		/// The chatbot api respond route
		/// </summary>
		internal const string ChatbotRespond_Route = "respond";

		/// <summary>
		/// The handle ai feedback route
		/// </summary>
		internal const string HandleAIFeedback_Route = "aifeedback";

		/// <summary>
		/// The get sample prompts route
		/// </summary>
		internal const string GetSamplePrompts_Route = "getsampleaiprompts";
	}

	/// <summary>
	/// The Common services controller constants.
	/// </summary>
	internal static class CommonServicesController
	{
		/// <summary>
		/// The base route
		/// </summary>
		internal const string BaseRoute = "CommonServices";

		/// <summary>
		/// The submit bug report route
		/// </summary>
		internal const string SubmitBugReport_Route = "SubmitBugReport";

		/// <summary>
		/// The get lookup master data route.
		/// </summary>
		internal const string GetLookupMasterData_Route = "GetLookupMaster";
	}
}
