// *********************************************************************************
//	<copyright file="IUsersDataService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Users data service interface.</summary>
// *********************************************************************************
namespace InternetBulletin.Data.Contracts
{
    using InternetBulletin.Shared.DTOs.Users;

    /// <summary>
    /// Users data service interface.
    /// </summary>
    public interface IUsersDataService
    {
        /// <summary>
        /// Saves users data async.
        /// </summary>
        /// <param name="usersData">The users data.</param>
        /// <returns>The boolean for success / failure</returns>
        Task<bool> SaveUsersDataAsync(List<GraphUserDTO> usersData);
    }
}


