// *********************************************************************************
//	<copyright file="IBulletinServices.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Bulletin Services Interface.</summary>
// *********************************************************************************

namespace InternetBulletin.Business.Contracts
{
    using InternetBulletin.Shared.DTOs.Users;

    /// <summary>
    /// Bulletin services interface.
    /// </summary>
    public interface IBulletinServices
    {
        /// <summary>
        /// Gets graph user data async.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <returns>The graph user data dto.</returns>
        Task<GraphUserDTO> GetGraphUserDataAsync(string userName);
    }
}


