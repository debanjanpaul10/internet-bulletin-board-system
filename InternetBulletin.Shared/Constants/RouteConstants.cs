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

        #region Posts

        /// <summary>
        /// The posts base route prefix
        /// </summary>
        public const string PostsBase_RoutePrefix = "Posts";

        /// <summary>
        /// The get post route
        /// </summary>
        public const string GetPost_Route = "GetPost";

        /// <summary>
        /// The get all posts route
        /// </summary>
        public const string GetAllPosts_Route = "GetAllPosts";

        /// <summary>
        /// Creates new post route.
        /// </summary>
        public const string NewPost_Route = "AddPost";

        /// <summary>
        /// The update post route
        /// </summary>
        public const string UpdatePost_Route = "UpdatePost";

        /// <summary>
        /// The delete post route
        /// </summary>
        public const string DeletePost_Route = "DeletePost";

        #endregion

        #region Users

        /// <summary>
        /// The users base route prefx
        /// </summary>
        public const string UsersBase_RoutePrefx = "Users";

        /// <summary>
        /// The get user route
        /// </summary>
        public const string GetUser_Route = "GetUser";

        /// <summary>
        /// The get all users route
        /// </summary>
        public const string GetAllUsers_Route = "GetAllUsers";

        /// <summary>
        /// Creates new user_route.
        /// </summary>
        public const string NewUser_Route = "NewUser";

        /// <summary>
        /// The update user route
        /// </summary>
        public const string UpdateUser_Route = "UpdateUser";

        /// <summary>
        /// The delete user route
        /// </summary>
        public const string DeleteUser_Route = "DeleteUser";

        #endregion

        #region Configuration

        /// <summary>
        /// The configuration base_ route prefix.
        /// </summary>
        public const string ConfigurationBase_RoutePrefix = "Configuration";
        
        /// <summary>
        /// The get configuration_ route.
        /// </summary>
        public const string GetConfiguration_Route = "GetConfiguration";

        #endregion

        #endregion
    }
}
