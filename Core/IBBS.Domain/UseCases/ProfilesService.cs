// *********************************************************************************
//	<copyright file="ProfilesService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Profiles Service Class.</summary>
// *********************************************************************************

namespace InternetBulletin.Business.Services
{
    using System.Threading.Tasks;
    using InternetBulletin.Business.Contracts;
    using InternetBulletin.Data.Contracts;
    using InternetBulletin.Shared.Constants;
    using InternetBulletin.Shared.DTOs.Users;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// The Profiles Data Service Class.
    /// </summary>
    /// <param name="logger">The Logger</param>
    /// <param name="profilesDataService">The profiles data service</param>
    /// <param name="usersService">The users service</param>
    public class ProfilesService(ILogger<ProfilesService> logger, IProfilesDataService profilesDataService, IUsersService usersService) : IProfilesService
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<ProfilesService> _logger = logger;

        /// <summary>
        /// The profiles data service.
        /// </summary>
        private readonly IProfilesDataService _profilesDataService = profilesDataService;

        /// <summary>
        /// The users services.
        /// </summary>
        private readonly IUsersService _userService = usersService;

        /// <summary>
        /// Gets user profile data async.
        /// </summary>
        /// <param name="userName">The user name.</param>
        public async Task<UserProfileDto> GetUserProfileDataAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                var exception = new Exception(ExceptionConstants.UserIdCannotBeNullMessageConstant);
                this._logger.LogError(exception, exception.Message);
                throw exception;
            }

            var graphUserData = await this._userService.GetGraphUserDataAsync(userName);
            if (graphUserData is null || string.IsNullOrEmpty(graphUserData.Id))
            {
                var exception = new Exception(ExceptionConstants.UserDoesNotExistsMessageConstant);
                this._logger.LogError(exception, exception.Message);
                throw exception;
            }

            var userPosts = await this._profilesDataService.GetUserPostsAsync(userName).ConfigureAwait(false);
            var userRatings = await this._profilesDataService.GetUserRatingsAsync(userName).ConfigureAwait(false);
            var userProfileData = new UserProfileDto()
            {
                DisplayName = graphUserData.DisplayName,
                EmailAddress = graphUserData.EmailAddress,
                UserName = graphUserData.UserName,
                UserPosts = [.. userPosts.Select(p => new UserPostDTO
                {
                    PostTitle = p.PostTitle,
                    PostCreatedDate = p.PostCreatedDate,
                    PostId = p.PostId,
                    PostOwnerUserName = p.PostOwnerUserName,
                    Ratings = p.Ratings
                })],
                UserPostRatings = userRatings
            };

            return userProfileData;
        }
    }
}