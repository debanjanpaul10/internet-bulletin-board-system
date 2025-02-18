// *********************************************************************************
//	<copyright file="IUsersService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Users Service Interface.</summary>
// *********************************************************************************

namespace InternetBulletin.Business.Contracts
{
	using InternetBulletin.Data.Entities;

	/// <summary>
	/// The Users Service Interface.
	/// </summary>
	public interface IUsersService
	{
		/// <summary>
		/// Gets the user asynchronous.
		/// </summary>
		/// <param name="userId">The user identifier.</param>
		/// <returns>The user data.</returns>
		Task<User> GetUserAsync(int userId);

		/// <summary>
		/// Gets all users asynchronous.
		/// </summary>
		/// <returns>The list of <see cref="User"/></returns>
		Task<List<User>> GetAllUsersAsync();

		/// <summary>
		/// Adds the new user asynchronous.
		/// </summary>
		/// <param name="newUser">The new user.</param>
		/// <returns>The boolean for success/failure</returns>
		Task<bool> AddNewUserAsync(User newUser);

		/// <summary>
		/// Updates the user asynchronous.
		/// </summary>
		/// <param name="updateUser">The update user.</param>
		/// <returns>The updated user.</returns>
		Task<User> UpdateUserAsync(User updateUser);

		/// <summary>
		/// Deletes the user asynchronous.
		/// </summary>
		/// <param name="userId">The user identifier.</param>
		/// <returns>The boolean for success/failure</returns>
		Task<bool> DeleteUserAsync(int userId);
	}
}
