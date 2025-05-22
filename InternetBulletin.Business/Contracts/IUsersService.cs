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
        /// <returns>The graph user data dto.</returns>
        Task<GraphUserDTO> GetGraphUserDataAsync(string userName);

        /// <summary>
        /// Gets all graph users data async.
        /// </summary>
        /// <returns>The list of graph user data</returns>
        Task<List<GraphUserDTO>> GetAllGraphUsersDataAsync();

        /// <summary>
        /// Saves users data from azure ad async.
        /// </summary>
        /// <returns>The boolean for success / failure</returns>
        Task<bool> SaveUsersDataFromAzureAdAsync();
    }
}


