// *********************************************************************************
//	<copyright file="ICacheService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Cache service interface.</summary>
// *********************************************************************************

namespace InternetBulletin.Business.Contracts
{
    /// <summary>
    /// Cache service interface.
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// Gets async.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <summary>
/// Retrieves cached data of the specified type associated with the given key.
/// </summary>
/// <typeparam name="T">The type of the cached data.</typeparam>
/// <param name="key">The unique key identifying the cached item.</param>
/// <returns>The cached value if found; otherwise, null.</returns>
        T? GetCachedData<T>(string key);

        /// <summary>
        /// Sets async.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expirationTime">The expiration time.</param>
        /// <summary>
/// Stores a value in the cache under the specified key with a defined expiration time.
/// </summary>
/// <typeparam name="T">The type of the value to cache.</typeparam>
/// <param name="key">The unique key identifying the cached data.</param>
/// <param name="value">The value to store in the cache.</param>
/// <param name="expirationTime">The duration after which the cached data expires.</param>
/// <returns>True if the data was successfully cached; otherwise, false.</returns>
        bool SetCacheData<T>(string key, T value, TimeSpan expirationTime);

        /// <summary>
        /// Removes data.
        /// </summary>
        /// <summary>
/// Removes the cached data associated with the specified key.
/// </summary>
/// <param name="key">The cache key to remove.</param>
/// <returns>True if the data was successfully removed; otherwise, false.</returns>
        bool RemoveCachedData(string key);
    }

}

