﻿// *********************************************************************************
//	<copyright file="IPostsDataService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Posts DataManager Interface Class.</summary>
// *********************************************************************************

namespace InternetBulletin.Data.Contracts
{
	using IBBS.Domain.DomainEntities;
	using InternetBulletin.Data.Entities;
	using InternetBulletin.Shared.DTOs;
	using InternetBulletin.Shared.DTOs.Posts;

	/// <summary>
	/// The Posts DataManager Interface Class.
	/// </summary>
	public interface IPostsDataService
	{
		/// <summary>
		/// Gets the post asynchronous.
		/// </summary>
		/// <param name="postId">The post identifier.</param>
		/// <param name="userName">The user name.</param>
		/// <param name="isForCurrentUser">Checks if requested for the current user</param>
		/// <returns>The specific post.</returns>
		Task<Post> GetPostAsync(Guid postId, string userName, bool isForCurrentUser);

		/// <summary>
		/// Adds the new post asynchronous.
		/// </summary>
		/// <param name="newPost">The new post.</param>
		/// <param name="userName">The user name.</param>
		/// <returns>The boolean for success or failure.</returns>
		Task<bool> AddNewPostAsync(AddPostDomain newPost, string userName);

		/// <summary>
		/// Updates the post asynchronous.
		/// </summary>
		/// <param name="updatedPost">The updated post.</param>
		/// <param name="userName">The user name</param>
		/// <param name="isRatingUpdate">The boolean flag to signify rating update.</param>
		/// <returns>The updated post data.</returns>
		Task<Post> UpdatePostAsync(UpdatePostDomain updatedPost, string userName, bool isRatingUpdate);

		/// <summary>
		/// Deletes the post asynchronous.
		/// </summary>
		/// <param name="postId">The post identifier.</param>
		/// <param name="userName">The user name.</param>
		/// <returns>The boolean for success / failure</returns>
		Task<bool> DeletePostAsync(Guid postId, string userName);

		/// <summary>
		/// Gets all posts async.
		/// </summary>
		/// <returns>The list of posts</returns>
		Task<List<Post>> GetAllPostsAsync();
	}
}
