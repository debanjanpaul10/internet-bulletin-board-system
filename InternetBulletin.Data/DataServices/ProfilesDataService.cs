// *********************************************************************************
//	<copyright file="ProfilesDataService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Profiles Data Service Class.</summary>
// *********************************************************************************

namespace InternetBulletin.Data.DataServices
{
    using InternetBulletin.Data.Contracts;
    using InternetBulletin.Shared.Constants;
    using InternetBulletin.Shared.DTOs;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// The Profiles Data Service Class.
    /// </summary>
    /// <param name="cosmosDbContext">The Cosmos DB Context</param>
    /// <param name="sqlDbContext">The SQL DB Context</param>
    /// <param name="logger">The Logger</param>
    public class ProfilesDataService(CosmosDbContext cosmosDbContext, SqlDbContext sqlDbContext, ILogger<ProfilesDataService> logger) : IProfilesDataService
    {
        /// <summary>
        /// The cosmos database context
        /// </summary>
        private readonly CosmosDbContext _cosmosDbContext = cosmosDbContext;

        /// <summary>
        /// The SQL database context
        /// </summary>
        private readonly SqlDbContext _sqlDbContext = sqlDbContext;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<ProfilesDataService> _logger = logger;

        /// <summary>
        /// Gets the user profile data asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The User profile DTO.</returns>
        public async Task<UserProfileDto> GetUserProfileDataAsync(int userId)
        {
            try
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(GetUserProfileDataAsync), DateTime.UtcNow, userId));

                var result = new UserProfileDto();
                var userData = await this._sqlDbContext.Users.FirstOrDefaultAsync(x => x.UserId == userId);
                if (userData is not null)
                {
                    var userPosts = await this._cosmosDbContext.Posts.Where(x => x.IsActive && x.PostCreatedBy == userData.UserAlias).ToListAsync();
                    result = new UserProfileDto()
                    {
                        Name = userData.Name,
                        UserId = userData.UserId,
                        UserAlias = userData.UserAlias,
                        UserEmail = userData.UserEmail,
                        UserPassword = userData.UserPassword,
                        UserPosts = [.. userPosts.Select(x => new UserPostsDto()
                        {
                            PostId = x.PostId,
                            PostCreatedDate = x.PostCreatedDate,
                            PostTitle = x.PostTitle
                        })]
                    };
                }

                return result;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(GetUserProfileDataAsync), DateTime.UtcNow, ex.Message));
                throw;
            }
            finally
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetUserProfileDataAsync), DateTime.UtcNow, userId));
            }
        }
    }
}
