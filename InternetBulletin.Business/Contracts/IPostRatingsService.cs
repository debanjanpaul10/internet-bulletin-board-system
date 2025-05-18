// *********************************************************************************
//	<copyright file="IPostRatingsService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Post ratings service interface.</summary>
// *********************************************************************************

namespace InternetBulletin.Business.Contracts
{
    using InternetBulletin.Data.Entities;
    using InternetBulletin.Shared.DTOs.Posts;

    /// <summary>
    /// Post ratings service interface.
    /// </summary>
    public interface IPostRatingsService
    {
        /// <summary>
		/// Updates rating async.
		/// </summary>
		/// <param name="postId">The post id.</param>
		/// <param name="isIncrement">If the rating is increased.</param>
        /// <param name="userName">The user name.</param>
		/// <returns>The update rating data dto.</returns>
		Task<UpdateRatingDto> UpdateRatingAsync(string postId, bool isIncrement, string userName);

        /// <summary>
        /// Gets all user post ratings async.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <returns>The list of post ratings</returns>
        Task<List<PostRating>> GetAllUserPostRatingsAsync(string userName);
    }
}
