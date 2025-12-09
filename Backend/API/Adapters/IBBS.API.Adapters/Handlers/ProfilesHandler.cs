using AutoMapper;
using IBBS.API.Adapters.Contracts;
using IBBS.API.Adapters.Models.Users;
using IBBS.Domain.DrivingPorts;

namespace IBBS.API.Adapters.Handlers;

/// <summary>
/// The Profiles API Handler class.
/// </summary>
/// <param name="mapper">The auto mapper.</param>
/// <param name="profilesService">The profiles service.</param>
/// <seealso cref="IBBS.API.Adapters.Contracts.IProfilesHandler" />
public class ProfilesHandler(IMapper mapper, IProfilesService profilesService) : IProfilesHandler
{
	/// <summary>
	/// Gets user profile data async.
	/// </summary>
	/// <param name="userEmail">The user email.</param>
	/// <returns>The user profile dto.</returns>
	public async Task<UserProfileDto> GetUserProfileDataAsync(string userEmail)
	{
		var domainResult = await profilesService.GetUserProfileDataAsync(userEmail).ConfigureAwait(false);
		return mapper.Map<UserProfileDto>(domainResult);
	}
}
