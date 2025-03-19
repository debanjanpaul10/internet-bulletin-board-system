// *********************************************************************************
//	<copyright file="IProfilesDataService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Profiles Data Service Interface.</summary>
// *********************************************************************************

namespace InternetBulletin.Data.Contracts
{
	using InternetBulletin.Shared.DTOs;

	/// <summary>
	/// The Profiles Data Service Interface.
	/// </summary>
	public interface IProfilesDataService
	{
		Task<UserProfileDto> GetUserProfileDataAsync(int userId);
	}
}
