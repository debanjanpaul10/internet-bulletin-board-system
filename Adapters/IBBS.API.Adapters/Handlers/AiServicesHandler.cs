using AutoMapper;
using IBBS.API.Adapters.Contracts;
using IBBS.API.Adapters.Models;
using IBBS.Domain.DomainEntities.AI;
using IBBS.Domain.DrivingPorts;
using InternetBulletin.Shared.DTOs.AI;

namespace IBBS.API.Adapters.Handlers;

/// <summary>
/// The AI Services API adapter handler.
/// </summary>
/// <param name="aiServices">The ai services.</param>
/// <param name="mapper">The mapper.</param>
/// <seealso cref="IBBS.API.Adapters.Contracts.IAiServicesHandler" />
public class AiServicesHandler(IAIService aiServices, IMapper mapper) : IAiServicesHandler
{
	/// <summary>
	/// Generates the tag for story asynchronous.
	/// </summary>
	/// <param name="userName">The current user name.</param>
	/// <param name="requestDTO">The story.</param>
	/// <returns>
	/// The genre tag response.
	/// </returns>
	public async Task<string> GenerateTagForStoryAsync(string userName, UserStoryRequestDTO requestDTO)
	{
		var domainRequest = mapper.Map<UserStoryRequestDomain>(requestDTO);
		return await aiServices.GenerateTagForStoryAsync(userName, domainRequest).ConfigureAwait(false);
	}

	/// <summary>
	/// Gets the application information data asynchronously.
	/// </summary>
	/// <returns>
	/// The about us details data <see cref="AboutUsAppInfoDataDTO" />
	/// </returns>
	public async Task<AboutUsAppInfoDataDTO> GetAboutUsDataAsync()
	{
		throw new NotImplementedException();
	}

	/// <summary>
	/// Moderates the content data asynchronous.
	/// </summary>
	/// <param name="userName">The current user name.</param>
	/// <param name="requestDTO">The story.</param>
	/// <returns>
	/// The moderation content response.
	/// </returns>
	public async Task<string> ModerateContentDataAsync(string userName, UserStoryRequestDTO requestDTO)
	{
		var domainRequest = mapper.Map<UserStoryRequestDomain>(requestDTO);
		return await aiServices.ModerateContentDataAsync(userName, domainRequest).ConfigureAwait(false);
	}

	/// <summary>
	/// Rewrites with AI asynchronously.
	/// </summary>
	/// <param name="userName">The current user name.</param>
	/// <param name="requestDTO">The story.</param>
	/// <returns>
	/// The AI response data
	/// </returns>
	public async Task<string> RewriteWithAIAsync(string userName, UserStoryRequestDTO requestDTO)
	{
		var domainRequest = mapper.Map<UserStoryRequestDomain>(requestDTO);
		return await aiServices.RewriteWithAIAsync(userName, domainRequest).ConfigureAwait(false);
	}
}
