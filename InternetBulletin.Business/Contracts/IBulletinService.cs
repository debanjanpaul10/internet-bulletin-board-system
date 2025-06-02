// *********************************************************************************
//	<copyright file="IBulletinService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary> The Bulletin Service interface.</summary>
// *********************************************************************************

namespace InternetBulletin.Business.Contracts
{
    using InternetBulletin.Shared.DTOs.ApplicationInfo;

    /// <summary>
    /// The Bulletin Service interface.
    /// </summary>
    public interface IBulletinService
    {
        /// <summary>
        /// Gets the application information data asynchronously.
        /// </summary>
        /// <returns>The application information data.</returns>
        Task<ApplicationInformationDataDTO> GetApplicationInformationDataAsync();
    }
}


