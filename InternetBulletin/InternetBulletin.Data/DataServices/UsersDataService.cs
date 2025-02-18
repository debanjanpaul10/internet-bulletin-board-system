// *********************************************************************************
//	<copyright file="UsersDataService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Users Data Manager Class.</summary>
// *********************************************************************************

namespace InternetBulletin.Data.DataServices
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using InternetBulletin.Data.Contracts;
	using InternetBulletin.Data.Entities;
	using InternetBulletin.Shared.Constants;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Logging;

	/// <summary>
	/// The Users Data Manager Services Class.
	/// </summary>
	/// <seealso cref="InternetBulletin.Data.Contracts.IUsersDataService" />
	/// <param name="sqlDbContext">The Sql DB Context.</param>
	/// <param name="logger">The Logger.</param>
	public class UsersDataService(SqlDbContext sqlDbContext, ILogger<UsersDataService> logger) : IUsersDataService
	{
		/// <summary>
		/// The SQL database context
		/// </summary>
		private readonly SqlDbContext _sqlDbContext = sqlDbContext;

		/// <summary>
		/// The logger
		/// </summary>
		private readonly ILogger<UsersDataService> _logger = logger;

		/// <summary>
		/// Gets the user details asynchronous.
		/// </summary>
		/// <param name="userId">The user id.</param>
		/// <returns>The user data dto.</returns>
		public async Task<User> GetUserDetailsAsync(int userId)
		{
			try
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(GetUserDetailsAsync), DateTime.UtcNow, userId));

				var user = await this._sqlDbContext.Users.FirstOrDefaultAsync(x => x.UserId == userId && x.IsActive);
				return user ?? new User();
			}
			catch (Exception ex)
			{
				this._logger.LogError(ex, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(GetUserDetailsAsync), DateTime.UtcNow, ex.Message));
				throw;
			}
			finally
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetUserDetailsAsync), DateTime.UtcNow, userId));
			}
		}

		/// <summary>
		/// Gets all users data asynchronous.
		/// </summary>
		/// <returns>The list of <see cref="User"/></returns>
		public async Task<List<User>> GetAllUsersDataAsync()
		{
			try
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(GetAllUsersDataAsync), DateTime.UtcNow, string.Empty));

				var users = await this._sqlDbContext.Users.Where(x => x.IsActive).ToListAsync();
				return users;
			}
			catch (Exception ex)
			{
				this._logger.LogError(ex, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(GetAllUsersDataAsync), DateTime.UtcNow, ex.Message));
				throw;
			}
			finally
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetAllUsersDataAsync), DateTime.UtcNow, string.Empty));
			}
		}

		/// <summary>
		/// Adds the new user asynchronous.
		/// </summary>
		/// <param name="newUser">The new user data.</param>
		/// <returns>The boolean for success/failure</returns>
		public async Task<bool> AddNewUserAsync(User newUser)
		{
			try
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(AddNewUserAsync), DateTime.UtcNow, newUser.UserAlias));

				var existingUser = await this._sqlDbContext.Users
					.AnyAsync(x => x.IsActive && (x.UserAlias == newUser.UserAlias || x.UserEmail == newUser.UserEmail));

				if (!existingUser)
				{
					await this._sqlDbContext.Users.AddAsync(newUser);
					await this._sqlDbContext.SaveChangesAsync();

					return true;
				}
				else
				{
					var exception = new Exception(ExceptionConstants.UserAlreadyExistsMessageConstant);
					this._logger.LogError(exception, exception.Message);

					throw exception;
				}
			}
			catch (DbUpdateException dbEx)
			{
				this._logger.LogError(dbEx, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(AddNewUserAsync), DateTime.UtcNow, dbEx.Message));
				return false;
			}
			catch (Exception ex)
			{
				this._logger.LogError(ex, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(AddNewUserAsync), DateTime.UtcNow, ex.Message));
				return false;
			}
			finally
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(AddNewUserAsync), DateTime.UtcNow, newUser.UserAlias));
			}
		}

		/// <summary>
		/// Updates the user asynchronous.
		/// </summary>
		/// <param name="updateUser">The updated user data.</param>
		/// <returns>The updated user data dto.</returns>
		public async Task<User> UpdateUserAsync(User updateUser)
		{
			try
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(UpdateUserAsync), DateTime.UtcNow, updateUser.UserId));
				
				var existingUser = await this._sqlDbContext.Users
					.AnyAsync(x => x.IsActive && (x.UserAlias == updateUser.UserAlias || x.UserEmail == updateUser.UserEmail));
				if (existingUser)
				{
					var exception = new Exception(ExceptionConstants.UserAlreadyExistsMessageConstant);
					this._logger.LogError(exception, exception.Message);

					throw exception;
				}

				var dbUserData = await this._sqlDbContext.Users.FirstOrDefaultAsync(x => x.UserId == updateUser.UserId && x.IsActive);
				if (dbUserData is not null)
				{
					dbUserData.Name = updateUser.Name;
					dbUserData.UserEmail = updateUser.UserEmail;
					dbUserData.UserAlias = updateUser.UserAlias;
					dbUserData.UserPassword = updateUser.UserPassword;
					await this._sqlDbContext.SaveChangesAsync();

					return dbUserData;
				}
				else
				{
					var exception = new Exception(ExceptionConstants.UserDoesNotExistsMessageConstant);
					this._logger.LogError(exception, exception.Message);
					throw exception;
				}
			}
			catch (DbUpdateException dbEx)
			{
				this._logger.LogError(dbEx, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(UpdateUserAsync), DateTime.UtcNow, dbEx.Message));
				throw;
			}
			catch (Exception ex)
			{
				this._logger.LogError(ex, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(UpdateUserAsync), DateTime.UtcNow, ex.Message));
				throw;
			}
			finally
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(UpdateUserAsync), DateTime.UtcNow, updateUser.UserId));
			}
		}

		/// <summary>
		/// Deletes the user asynchronous.
		/// </summary>
		/// <param name="userId">The user id.</param>
		/// <returns>The boolean for success/failure</returns>
		public async Task<bool> DeleteUserAsync(int userId)
		{
			try
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(DeleteUserAsync), DateTime.UtcNow, userId));

				var dbUserData = await this._sqlDbContext.Users.FirstOrDefaultAsync(x => x.UserId == userId && x.IsActive);
				if (dbUserData is not null)
				{
					dbUserData.IsActive = false;
					await this._sqlDbContext.SaveChangesAsync();

					return true;
				}
				else
				{
					var exception = new Exception(ExceptionConstants.UserDoesNotExistsMessageConstant);
					this._logger.LogError(exception, exception.Message);
					throw exception;
				}
			}
			catch (DbUpdateException dbEx)
			{
				this._logger.LogError(dbEx, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(DeleteUserAsync), DateTime.UtcNow, dbEx.Message));
				return false;
			}
			catch (Exception ex)
			{
				this._logger.LogError(ex, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(DeleteUserAsync), DateTime.UtcNow, ex.Message));
				return false;
			}
			finally
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(DeleteUserAsync), DateTime.UtcNow, userId));
			}
		}
	}
}
