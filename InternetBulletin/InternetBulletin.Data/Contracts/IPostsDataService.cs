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
		Task<Post> GetPostAsync(Guid postId);

		/// <summary>
		/// Adds the new post asynchronous.
		/// </summary>
		/// <param name="newPost">The new post.</param>
		/// <returns>The boolean for success or failure.</returns>
		Task<bool> AddNewPostAsync(Post newPost);

		/// <summary>
		/// Updates the post asynchronous.
		/// </summary>
		/// <param name="updatedPost">The updated post.</param>
		/// <returns>The updated post data.</returns>
		Task<Post> UpdatePostAsync(Post updatedPost);

		/// <summary>
		/// Deletes the post asynchronous.
		/// </summary>
		/// <param name="postId">The post identifier.</param>
		/// <returns>The boolean for success / failure</returns>
		Task<bool> DeletePostAsync(Guid postId);

		/// <summary>
		/// Gets all posts asynchronous.
		/// </summary>
		/// <returns>The list of <see cref="Post"/></returns>
		Task<List<Post>> GetAllPostsAsync();
	}
}
