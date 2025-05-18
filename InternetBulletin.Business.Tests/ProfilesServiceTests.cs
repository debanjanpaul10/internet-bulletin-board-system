// *********************************************************************************
//	<copyright file="ProfilesServiceTests.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Profiles service tests.</summary>
// *********************************************************************************

namespace InternetBulletin.UnitTests
{
    /// <summary>
    /// Profiles service tests.
    /// </summary>
    public class ProfilesServiceTests
    {
        /// <summary>
        /// The user name.
        /// </summary>
        private static readonly string UserName = "user@example.com";

        /// <summary>
        /// The mock profiles data service.
        /// </summary>
        private readonly Mock<IProfilesDataService> _mockProfilesDataService;

        /// <summary>
        /// The mock logger.
        /// </summary>
        private readonly Mock<ILogger<ProfilesService>> _mockLogger;

        /// <summary>
        /// The profiles service.
        /// </summary>
        private readonly ProfilesService _profilesService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfilesServiceTests"/> class.
        /// </summary>
        public ProfilesServiceTests()
        {
            this._mockProfilesDataService = new Mock<IProfilesDataService>();
            this._mockLogger = new Mock<ILogger<ProfilesService>>();

            this._profilesService = new ProfilesService(
                this._mockProfilesDataService.Object,
                this._mockLogger.Object
            );
        }

        #region GetUserProfileDataAsync

        /// <summary>
        /// Tests the user profile data async when user id is valid.
        /// </summary>
        /// <returns>A task to wait on.</returns>
        [Fact]
        public async Task GetUserProfileDataAsync_ValidUserId_ReturnsUserProfile()
        {
            // Arrange
            var userId = new Random().Next(1, 99);
            var userProfile = TestsHelper.CreateMockUserProfileDto(UserName);
            this._mockProfilesDataService.Setup(x => x.GetUserProfileDataAsync(It.IsAny<string>())).ReturnsAsync(userProfile);

            // Act
            var result = await this._profilesService.GetUserProfileDataAsync(UserName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(UserName, result.UserName);
        }

        /// <summary>
        /// Tests the user profile data async when user id is INVALID.
        /// </summary>
        /// <returns>A task to wait on.</returns>
        [Fact]
        public async Task GetUserProfileDataAsync_InvalidUserId_ThrowsException()
        {
            // Arrange
            var userId = 0;

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() => this._profilesService.GetUserProfileDataAsync(It.IsAny<string>()));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal(ExceptionConstants.UserIdCannotBeNullMessageConstant, exception.Message);
        }

        /// <summary>
        /// Tests the user profile data async when user does not exists.
        /// </summary>
        /// <returns>A task to wait on.</returns>
        [Fact]
        public async Task GetUserProfileDataAsync_UserDoesNotExist_ThrowsException()
        {
            // Arrange
            var userId = new Random().Next(1, 99);
            UserProfileDto userData = null!;
            this._mockProfilesDataService.Setup(x => x.GetUserProfileDataAsync(It.IsAny<string>())).ReturnsAsync(userData);

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() => this._profilesService.GetUserProfileDataAsync(UserName));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal(ExceptionConstants.UserDoesNotExistsMessageConstant, exception.Message);
        }

        #endregion
    }
}


