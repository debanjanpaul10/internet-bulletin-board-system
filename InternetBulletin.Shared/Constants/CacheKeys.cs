// *********************************************************************************
//	<copyright file="CacheKeys.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Cache keys.</summary>
// *********************************************************************************
using System.Globalization;

namespace InternetBulletin.Shared.Constants
{
    /// <summary>
    /// Cache keys.
    /// </summary>
    public static class CacheKeys
    {
        /// <summary>
        /// The default cache expiration timespan.
        /// </summary>
        public static readonly TimeSpan DefaultCacheExpiration = TimeSpan.FromMinutes(10);

        /// <summary>
        /// The all posts cache key.
        /// </summary>
        public const string AllPostsCacheKey = "AllPosts";

        /// <summary>
        /// The all user posts data key.
        /// </summary>
        /// <param name="userName">The user name.</param>
        public static string AllUserPostsDataCacheKey(string userName) => string.Format(CultureInfo.CurrentCulture, "User-{0}-PostsAndRatings", userName);

        /// <summary>
        /// The user ratings key.
        /// </summary>
        /// <param name="userName">The user name.</param>
        public static string UserRatingsCacheKey(string userName) => string.Format(CultureInfo.CurrentCulture, "User-{0}-Ratings", userName);

        /// <summary>
        /// The graph users data cache key.
        /// </summary>
        public const string FilteredGraphUsersDataCacheKey = "FilteredGraphUsersData";
    }
}