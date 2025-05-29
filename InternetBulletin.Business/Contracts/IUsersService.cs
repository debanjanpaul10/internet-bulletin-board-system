// *********************************************************************************
//	<copyright file="IUsersService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Users Services Interface.</summary>
// *********************************************************************************
namespace InternetBulletin.Business.Contracts
{
    using InternetBulletin.Shared.DTOs.Users;

    /// <summary>
    /// The users service interface.
    /// </summary>
    public interface IUsersService
    {
        /// <summary>
        /// Gets graph user data async.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <summary>
/// Retrieves user data from the graph service for the specified username.
/// </summary>
/// <param name="userName">The username to look up in the graph service.</param>
/// <returns>A task that represents the asynchronous operation. The task result contains the user's graph data.</returns>
        Task<GraphUserDTO> GetGraphUserDataAsync(string userName);

        /// <summary>
        /// Saves users data from azure ad async.
        /// </summary>
        /// <returns>The boolean for success / failure</returns>
        Task<bool> SaveUsersDataFromAzureAdAsync();
    }
}


