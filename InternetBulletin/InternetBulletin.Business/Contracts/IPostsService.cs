// *********************************************************************************
//	<copyright file="IPostsService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Posts BusinessManager Interface Class.</summary>
// *********************************************************************************

namespace InternetBulletin.Business.Contracts
{
	using InternetBulletin.Data.Entities;

	/// <summary>
	/// The Posts BusinessManager Interface Class.
	/// </summary>
	public interface IPostsService
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
