// *********************************************************************************
//	<copyright file="PostsService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Posts BusinessManager Class.</summary>
// *********************************************************************************

namespace InternetBulletin.Business.Services
{
	using InternetBulletin.Business.Contracts;
	using InternetBulletin.Data.Contracts;
	using InternetBulletin.Data.Entities;
	using System.Threading.Tasks;

	/// <summary>
	/// The Posts BusinessManager Class.
	/// </summary>
	/// <param name="postsDataService">The Posts Data Service.</param>
	public class PostsService(IPostsDataService postsDataService) : IPostsService
	{
		/// <summary>
		/// The posts data service
		/// </summary>
		private readonly IPostsDataService _postsDataService = postsDataService;

		/// <summary>
		/// Adds the new post asynchronous.
		/// </summary>
		/// <param name="newPost">The new post.</param>
		/// <returns>
		/// The boolean for success or failure.
		/// </returns>
		public async Task<bool> AddNewPostAsync(Post newPost)
		{
			if (newPost is not null)
			{
				var result = await this._postsDataService.AddNewPostAsync(newPost);
				return result;
			}

			return false;
		}

		/// <summary>
		/// Gets the post asynchronous.
		/// </summary>
		/// <param name="postId">The post identifier.</param>
		/// <returns>
		/// The specific post.
		/// </returns>
		public async Task<Post> GetPostAsync(string postId)
		{
			if (!string.IsNullOrEmpty(postId))
			{
				var result = await this._postsDataService.GetPostAsync(postId);
				return result;
			}

			return new Post();
		}
	}
}
