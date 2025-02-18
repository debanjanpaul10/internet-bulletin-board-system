// *********************************************************************************
//	<copyright file="UsersService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Users Service Class.</summary>
// *********************************************************************************

namespace InternetBulletin.Business.Services
{
	using InternetBulletin.Business.Contracts;
	using InternetBulletin.Data.Contracts;
	using InternetBulletin.Data.Entities;
	using InternetBulletin.Shared.Constants;
	using Microsoft.Extensions.Logging;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	/// <summary>
	/// The Users Service Class.
	/// </summary>
	/// <seealso cref="InternetBulletin.Business.Contracts.IUsersService" />
	/// <param name="usersDataService">The users data service.</param>
	/// <param name="logger">The Logger</param>
	public class UsersService(IUsersDataService usersDataService, ILogger<UsersService> logger) : IUsersService
	{
		/// <summary>
		/// The users data service
		/// </summary>
		private readonly IUsersDataService _usersDataService = usersDataService;

		/// <summary>
		/// The logger
		/// </summary>
		private readonly ILogger<UsersService> _logger = logger;

		/// <summary>
		/// Gets the user asynchronous.
		/// </summary>
		/// <param name="userId">The user identifier.</param>
		/// <returns>
		/// The user data.
		/// </returns>
		public async Task<User> GetUserAsync(int userId)
		{
			if (userId <= 0)
			{
				var exception = new Exception(ExceptionConstants.UserIdNotCorrectMessageConstant);
				this._logger.LogError(exception, exception.Message);

				throw exception;
			}

			var result = await this._usersDataService.GetUserDetailsAsync(userId);
			if (result is not null && result.UserId > 0)
			{
				return result;
			}
			else
			{
				var exception = new Exception(ExceptionConstants.UserDoesNotExistsMessageConstant);
				this._logger.LogError(exception, exception.Message);

				throw exception;
			}
		}

		/// <summary>
		/// Gets all users asynchronous.
		/// </summary>
		/// <returns>
		/// The list of <see cref="User" />
		/// </returns>
		public async Task<List<User>> GetAllUsersAsync()
		{
			var result = await this._usersDataService.GetAllUsersDataAsync();
			return result;
		}

		/// <summary>
		/// Adds the new user asynchronous.
		/// </summary>
		/// <param name="newUser">The new user.</param>
		/// <returns>
		/// The boolean for success/failure
		/// </returns>
		public async Task<bool> AddNewUserAsync(User newUser)
		{
			if (newUser is null)
			{
				var exception = new Exception(ExceptionConstants.NullUserMessageConstant);
				this._logger.LogError(exception, exception.Message);

				throw exception;
			}

			var result = await this._usersDataService.AddNewUserAsync(newUser);
			return result;
		}

		/// <summary>
		/// Updates the user asynchronous.
		/// </summary>
		/// <param name="updateUser">The update user.</param>
		/// <returns>
		/// The updated user.
		/// </returns>
		public async Task<User> UpdateUserAsync(User updateUser)
		{
			if (updateUser is null)
			{
				var exception = new Exception(ExceptionConstants.NullUserMessageConstant);
				this._logger.LogError(exception, exception.Message);

				throw exception;
			}

			var result = await this._usersDataService.UpdateUserAsync(updateUser);
			if (result is not null && result.UserId > 0)
			{
				return result;
			}
			else
			{
				var exception = new Exception(ExceptionConstants.UserDoesNotExistsMessageConstant);
				this._logger.LogError(exception, exception.Message);

				throw exception;
			}
		}

		/// <summary>
		/// Deletes the user asynchronous.
		/// </summary>
		/// <param name="userId">The user identifier.</param>
		/// <returns>
		/// The boolean for success/failure
		/// </returns>
		public async Task<bool> DeleteUserAsync(int userId)
		{
			if (userId <= 0)
			{
				var exception = new Exception(ExceptionConstants.UserIdNotCorrectMessageConstant);
				this._logger.LogError(exception, exception.Message);

				throw exception;
			}

			var result = await this._usersDataService.DeleteUserAsync(userId);
			return result;
		}
	}
}
