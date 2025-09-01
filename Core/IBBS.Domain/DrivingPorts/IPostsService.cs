﻿// *********************************************************************************
//	<copyright file="IPostsService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Posts BusinessManager Interface Class.</summary>
// *********************************************************************************

namespace InternetBulletin.Business.Contracts
{
	using InternetBulletin.Data.Entities;
	using InternetBulletin.Shared.DTOs;
	using InternetBulletin.Shared.DTOs.Posts;

	/// <summary>
	/// The Posts BusinessManager Interface Class.
	/// </summary>
	public interface IPostsService
	{
		/// <summary>
		/// Gets the post asynchronous.
		/// </summary>
		/// <param name="postId">The post identifier.</param>
		/// <param name="userName"The user name.</param>
		/// <returns>The specific post.</returns>
		Task<Post> GetPostAsync(string postId, string userName);

		/// <summary>
		/// Adds the new post asynchronous.
		/// </summary>
		/// <param name="newPost">The new post.</param>
		/// <param name="userName">The User name.</param>
		/// <returns>The boolean for success or failure.</returns>
		Task<bool> AddNewPostAsync(AddPostDTO newPost, string userName);

		/// <summary>
		/// Updates the post asynchronous.
		/// </summary>
		/// <param name="updatedPost">The updated post.</param>
		/// <param name="userName">The user name.</param>
		/// <returns>The updated post data.</returns>
		Task<Post> UpdatePostAsync(UpdatePostDTO updatedPost, string userName);

		/// <summary>
		/// Deletes the post asynchronous.
		/// </summary>
		/// <param name="postId">The post identifier.</param>
		/// <param name="userName">The current user name.</param>
		/// <returns>The boolean for success / failure</returns>
		Task<bool> DeletePostAsync(string postId, string userName);

		/// <summary>
		/// Gets all posts asynchronous.
		/// </summary>
		/// <param name="userName">The user name</param>
		/// <returns>The list of <see cref="PostWithRatingsDTO"/></returns>
		Task<List<PostWithRatingsDTO>> GetAllPostsAsync(string userName);
	}
}
