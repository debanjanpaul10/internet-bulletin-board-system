using IBBS.API.Adapters.Models.Users;

namespace IBBS.API.Adapters.Contracts;

/// <summary>
/// The Profiles adapter handler interface.
/// </summary>
public interface IProfilesHandler
{
	/// <summary>
	/// Gets the user profile data asynchronous.
	/// </summary>
	/// <param name="userEmail">The user email.</param>
	/// <returns>The user profile dto.</returns>
	Task<UserProfileDto> GetUserProfileDataAsync(string userEmail);
}
