using IBBS.Domain.DomainEntities;

namespace IBBS.Domain.DrivingPorts;

/// <summary>
/// The profiles service interface.
/// </summary>
public interface IProfilesService
{
	/// <summary>
	/// Gets the user profile data asynchronous.
	/// </summary>
	/// <param name="userEmail">The user email.</param>
	/// <returns>The user profile domain.</returns>
	Task<UserProfileDomain> GetUserProfileDataAsync(string userEmail);
}
