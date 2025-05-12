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
    /// <param name="dbContext">The SQL DB Context</param>
    /// <seealso cref="IProfilesDataService"/>
    public class ProfilesDataService(SqlDbContext dbContext, ILogger<ProfilesDataService> logger) : IProfilesDataService
    {
        /// <summary>
        /// The sql db context.
        /// </summary>
        private readonly SqlDbContext _dbContext = dbContext;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<ProfilesDataService> _logger = logger;

        /// <summary>
        /// Gets the user profile data asynchronous.
        /// </summary>
        /// <param name="userName">The user identifier.</param>
        /// <returns>The user profile data dto.</returns>
        public async Task<UserProfileDto> GetUserProfileDataAsync(string userName)
        {
            try
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(GetUserProfileDataAsync), DateTime.UtcNow, userName));
                var userPostsData = await this._dbContext.Posts.Where(x => x.IsActive && x.PostOwnerUserName == userName)
                    .Select(x => new { x.PostTitle, x.PostCreatedDate, x.PostId }).ToListAsync();

                return new UserProfileDto()
                {
                    UserName = userName,
                    UserPosts = [.. userPostsData.Select(x => new UserPostsDto
                    {
                        PostTitle = x.PostTitle,
                        PostCreatedDate = x.PostCreatedDate,
                        PostId = x.PostId
                    })]
                };
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(GetUserProfileDataAsync), DateTime.UtcNow, ex.Message));
                throw;
            }
            finally
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetUserProfileDataAsync), DateTime.UtcNow, userName));
            }
        }
    }
}