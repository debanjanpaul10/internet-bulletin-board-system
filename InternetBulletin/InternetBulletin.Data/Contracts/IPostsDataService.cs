// *********************************************************************************
//	<copyright file="IPostsDataService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Posts DataManager Interface Class.</summary>
// *********************************************************************************

namespace InternetBulletin.Data.Contracts
{
	using InternetBulletin.Data.Entities;

	/// <summary>
	/// The Posts DataManager Interface Class.
	/// </summary>
	public interface IPostsDataService
	{
		/// <summary>
		/// Gets the post asynchronous.
		/// </summary>
		/// <param name="postId">The post identifier.</param>
		/// <returns>The specific post.</returns>
		Task<Post> GetPostAsync(string postId);

		/// <summary>
		/// Adds the new post asynchronous.
		/// </summary>
		/// <param name="newPost">The new post.</param>
		/// <returns>The boolean for success or failure.</returns>
		Task<bool> AddNewPostAsync(Post newPost);
	}
}
