using AutoMapper;
using IBBS.API.Adapters.Contracts;
using IBBS.API.Adapters.Models.Posts;
using IBBS.Domain.DomainEntities.Posts;
using IBBS.Domain.DrivingPorts;

namespace IBBS.API.Adapters.Handlers;

/// <summary>
/// The posts api adapter handler.
/// </summary>
/// <param name="mapper">The auto mapper.</param>
/// <param name="postsService">The posts service.</param>
/// <seealso cref="IBBS.API.Adapters.Contracts.IPostsHandler" />
public class PostsHandler(IPostsService postsService, IMapper mapper) : IPostsHandler
{
	/// <summary>
	/// Adds the new post asynchronous.
	/// </summary>
	/// <param name="newPost">The new post.</param>
	/// <param name="userName">The User name.</param>
	/// <returns>
	/// The boolean for success or failure.
	/// </returns>
	public async Task<bool> AddNewPostAsync(AddPostDTO newPost, string userName)
	{
		var domainInput = mapper.Map<AddPostDomain>(newPost);
		return await postsService.AddNewPostAsync(domainInput, userName).ConfigureAwait(false);
	}

	/// <summary>
	/// Deletes the post asynchronous.
	/// </summary>
	/// <param name="postId">The post identifier.</param>
	/// <param name="userName">The current user name.</param>
	/// <returns>
	/// The boolean for success / failure
	/// </returns>
	public async Task<bool> DeletePostAsync(string postId, string userName)
	{
		return await postsService.DeletePostAsync(postId, userName).ConfigureAwait(false);
	}

	/// <summary>
	/// Gets all posts asynchronous.
	/// </summary>
	/// <param name="userName">The user name</param>
	/// <returns>
	/// The list of <see cref="PostWithRatingsDTO" />
	/// </returns>
	public async Task<IEnumerable<PostWithRatingsDTO>> GetAllPostsAsync(string userName)
	{
		var domainResult = await postsService.GetAllPostsAsync(userName).ConfigureAwait(false);
		return mapper.Map<IEnumerable<PostWithRatingsDTO>>(domainResult);
	}

	/// <summary>
	/// Gets the post asynchronous.
	/// </summary>
	/// <param name="postId">The post identifier.</param>
	/// <param name="userName">Name of the user.</param>
	/// <returns>The post dto.</returns>
	public async Task<PostDTO> GetPostAsync(string postId, string userName)
	{
		var domainResult = await postsService.GetPostAsync(postId, userName).ConfigureAwait(false);
		return mapper.Map<PostDTO>(domainResult);
	}

	/// <summary>
	/// Updates the post asynchronous.
	/// </summary>
	/// <param name="updatedPost">The updated post.</param>
	/// <param name="userName">The user name.</param>
	/// <returns>
	/// The updated post data.
	/// </returns>
	public async Task<PostDTO> UpdatePostAsync(UpdatePostDTO updatedPost, string userName)
	{
		var domainInput = mapper.Map<UpdatePostDomain>(updatedPost);
		var domainResult = await postsService.UpdatePostAsync(domainInput, userName).ConfigureAwait(false);
		return mapper.Map<PostDTO>(domainResult);
	}
}
