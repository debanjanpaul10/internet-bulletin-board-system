// *********************************************************************************
//	<copyright file="IPostRatingsDataService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Post ratings data service interface.</summary>
// *********************************************************************************

namespace InternetBulletin.Data.Contracts
{
    using InternetBulletin.Data.Entities;

    /// <summary>
    /// Post ratings data service interface.
    /// </summary>
    public interface IPostRatingsDataService
    {
        /// <summary>
        /// Gets post rating async.
        /// </summary>
        /// <param name="postId">The post id.</param>
        /// <param name="userName">The user name.</param>
        /// <returns>The post rating data</returns>
        Task<PostRating> GetPostRatingAsync(Guid postId, string userName);

        /// <summary>
        /// Adds a new post rating async.
        /// </summary>
        /// <param name="postRating">The post rating data dto.</param>
        Task AddPostRatingAsync(PostRating postRating);

        /// <summary>
        /// Updates an existing rating async.
        /// </summary>
        /// <param name="postRating">The post rating.</param>
        Task UpdatePostRatingAsync(PostRating postRating);

        /// <summary>
        /// Gets all user post ratings async.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <returns>The list of post ratings</returns>
        Task<List<PostRating>> GetAllUserPostRatingsAsync(string userName);

    }
}


