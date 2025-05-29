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
        /// <summary>
/// Generates a cache key for storing or retrieving a user's posts and ratings data.
/// </summary>
/// <param name="userName">The user name to include in the cache key.</param>
/// <returns>A cache key string in the format "User-{userName}-PostsAndRatings".</returns>
        public static string AllUserPostsDataCacheKey(string userName) => string.Format(CultureInfo.CurrentCulture, "User-{0}-PostsAndRatings", userName);

        /// <summary>
        /// The user ratings key.
        /// </summary>
        /// <summary>
/// Generates a cache key for storing or retrieving a user's ratings data.
/// </summary>
/// <param name="userName">The user name for which to generate the cache key.</param>
/// <returns>A cache key string in the format "User-{userName}-Ratings".</returns>
        public static string UserRatingsCacheKey(string userName) => string.Format(CultureInfo.CurrentCulture, "User-{0}-Ratings", userName);

        /// <summary>
        /// The graph users data cache key.
        /// </summary>
        public const string FilteredGraphUsersDataCacheKey = "FilteredGraphUsersData";
    }
}