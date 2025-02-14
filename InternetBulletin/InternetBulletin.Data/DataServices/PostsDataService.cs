// *********************************************************************************
//	<copyright file="PostsDataService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Posts Data Manager Class.</summary>
// *********************************************************************************

namespace InternetBulletin.Data.DataServices
{
	using InternetBulletin.Data.Contracts;
	using InternetBulletin.Data.Entities;
	using InternetBulletin.Shared.Constants;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Logging;
	using System.Threading.Tasks;

	/// <summary>
	/// The Posts DataManager Class.
	/// </summary>
	/// <param name="appDbContext">The Application DB Context.</param>
	/// <param name="logger">The Logger.</param>
	public class PostsDataService(InternetBulletinDbContext appDbContext, ILogger<PostsDataService> logger) : IPostsDataService
	{
		/// <summary>
		/// The application database context
		/// </summary>
		private readonly InternetBulletinDbContext _appDbContext = appDbContext;

		/// <summary>
		/// The logger
		/// </summary>
		private readonly ILogger<PostsDataService> _logger = logger;

		/// <summary>
		/// Gets the post asynchronous.
		/// </summary>
		/// <param name="postId">The post identifier.</param>
		/// <returns>
		/// The specific post.
		/// </returns>
		public async Task<Post> GetPostAsync(string postId)
		{
			if (Guid.TryParse(postId, out var postGuid))
			{
				postGuid = Guid.Parse(postId);
				var post = await this._appDbContext.Posts.FirstOrDefaultAsync(p => p.PostId == postGuid && p.IsActive);
				return post ?? new Post();
			}

			return new Post();
		}

		/// <summary>
		/// Adds the new post asynchronous.
		/// </summary>
		/// <param name="newPost">The new post.</param>
		/// <returns>The boolean for success or failure.</returns>
		public async Task<bool> AddNewPostAsync(Post newPost)
		{
			try
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(AddNewPostAsync), DateTime.UtcNow, newPost.PostId));

				await this._appDbContext.Posts.AddAsync(newPost);
				await this._appDbContext.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				this._logger.LogError(ex, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(AddNewPostAsync), DateTime.UtcNow, ex.Message));
				return false;
			}
			finally
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(AddNewPostAsync), DateTime.UtcNow, newPost.PostId));
			}
		}
	}
}
