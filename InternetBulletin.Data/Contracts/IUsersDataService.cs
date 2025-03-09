// *********************************************************************************
//	<copyright file="IUsersDataService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Users Data Manager Interface.</summary>
// *********************************************************************************

namespace InternetBulletin.Data.Contracts
{
	using InternetBulletin.Data.Entities;
    using InternetBulletin.Shared.DTOs;

    /// <summary>
    /// The Users Data Manager Interface.
    /// </summary>
    public interface IUsersDataService
	{
		/// <summary>
		/// Gets the user details asynchronous.
		/// </summary>
		/// <param name="userLogin">The user identifier.</param>
		/// <returns>The user data dto.</returns>
		Task<User> GetUserDetailsAsync(UserLoginDTO userLogin);

		/// <summary>
		/// Gets all users data asynchronous.
		/// </summary>
		/// <returns>The list of <see cref="User"/></returns>
		Task<List<User>> GetAllUsersDataAsync();

		/// <summary>
		/// Adds the new user asynchronous.
		/// </summary>
		/// <param name="newUser">The new user data.</param>
		/// <returns>The boolean for success/failure</returns>
		Task<bool> AddNewUserAsync(User newUser);

		/// <summary>
		/// Updates the user asynchronous.
		/// </summary>
		/// <param name="updateUser">The updated user data.</param>
		/// <returns>The updated user data dto.</returns>
		Task<User> UpdateUserAsync(User updateUser);

		/// <summary>
		/// Deletes the user asynchronous.
		/// </summary>
		/// <param name="userId">The user id.</param>
		/// <returns>The boolean for success/failure</returns>
		Task<bool> DeleteUserAsync(int userId);
	}
}
