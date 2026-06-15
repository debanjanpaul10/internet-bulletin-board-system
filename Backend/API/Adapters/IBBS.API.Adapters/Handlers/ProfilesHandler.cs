using IBBS.API.Adapters.Contracts;
using IBBS.API.Adapters.Models.Users;
using IBBS.Domain.DrivingPorts;
using static IBBS.API.Adapters.Mapping.DomainToResponseMapper;

namespace IBBS.API.Adapters.Handlers;

/// <summary>
/// The Profiles API Handler class.
/// </summary>
/// <param name="profilesService">The profiles service.</param>
/// <seealso cref="IBBS.API.Adapters.Contracts.IProfilesHandler" />
public sealed class ProfilesHandler(IProfilesService profilesService) : IProfilesHandler
{
    /// <inheritdoc />
    public async Task<UserProfileDto> GetUserProfileDataAsync(
        string userEmail,
        CancellationToken cancellationToken = default
    )
    {
        var domainResult = await profilesService.GetUserProfileDataAsync(
            userEmail,
            cancellationToken
        ).ConfigureAwait(false);
        return MapToResponse(domain: domainResult);
    }
}
