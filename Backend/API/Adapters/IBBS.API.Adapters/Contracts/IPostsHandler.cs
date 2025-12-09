using IBBS.API.Adapters.Models.Posts;

namespace IBBS.API.Adapters.Contracts;

/// <summary>
/// The posts api adapter handler interface.
/// </summary>
public interface IPostsHandler
{
	/// <summary>
	/// Gets the post asynchronous.
	/// </summary>
	/// <param name="postId">The post identifier.</param>
	/// <param name="userName"The user name.</param>
	/// <returns>The specific post.</returns>
	Task<PostDTO> GetPostAsync(string postId, string userName);

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
	Task<PostDTO> UpdatePostAsync(UpdatePostDTO updatedPost, string userName);

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
	Task<IEnumerable<PostWithRatingsDTO>> GetAllPostsAsync(string userName);
}
