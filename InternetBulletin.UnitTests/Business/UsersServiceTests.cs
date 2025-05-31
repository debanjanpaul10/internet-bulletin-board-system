// *********************************************************************************
//	<copyright file="UsersServiceTests.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Users Service Tests Class.</summary>
// *********************************************************************************

namespace InternetBulletin.UnitTests.Business
{
    using InternetBulletin.Business.Services;
    using InternetBulletin.Data.Contracts;
    using InternetBulletin.Shared.DTOs.Users;
    using InternetBulletin.Shared.Helpers;
    using Moq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    /// <summary>
    /// The Users Service Tests Class.
    /// </summary>
    public class UsersServiceTests
    {
        /// <summary>
        /// The user name.
        /// </summary>
        private readonly static string UserName = "user1234";

        /// <summary>
        /// The http client helper mock.
        /// </summary>
        private readonly Mock<IHttpClientHelper> _httpClientHelperMock;

        /// <summary>
        /// The users data service mock.
        /// </summary>
        private readonly Mock<IUsersDataService> _usersDataServiceMock;

        /// <summary>
        /// The users service.
        /// </summary>
        private readonly UsersService _usersService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersServiceTests"/> class.
        /// </summary>
        public UsersServiceTests()
        {
            this._httpClientHelperMock = new Mock<IHttpClientHelper>();
            this._usersDataServiceMock = new Mock<IUsersDataService>();
            this._usersService = new UsersService(
                this._httpClientHelperMock.Object,
                this._usersDataServiceMock.Object);
        }

        /// <summary>
        /// Tests that GetGraphUserDataAsync returns correct user data when valid username is provided.
        /// </summary>
        [Fact]
        public async Task GetGraphUserDataAsync_ValidUserName_ReturnsUserData()
        {
            // Arrange
            var graphResponse = TestsHelper.PrepareGraphApiResponseForSingleUser(UserName);
            this._httpClientHelperMock.Setup(x => x.GetGraphApiDataAsync()).ReturnsAsync(graphResponse);

            // Act
            var result = await this._usersService.GetGraphUserDataAsync(UserName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(graphResponse?.Value?[0].Id, result.Id);
            Assert.Equal(graphResponse?.Value?[0].DisplayName, result.DisplayName);
            Assert.Equal(UserName, result.UserName);
        }

        /// <summary>
        /// Tests that GetGraphUserDataAsync returns empty DTO when user is not found.
        /// </summary>
        [Fact]
        public async Task GetGraphUserDataAsync_UserNotFound_ReturnsEmptyDTO()
        {
            // Arrange
            var graphResponse = TestsHelper.PrepareGraphApiResponse();
            this._httpClientHelperMock.Setup(x => x.GetGraphApiDataAsync()).ReturnsAsync(graphResponse);

            // Act
            var result = await this._usersService.GetGraphUserDataAsync(UserName);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result.Id);
            Assert.Empty(result.DisplayName);
            Assert.Empty(result.UserName);
            Assert.Empty(result.EmailAddress);
        }

        /// <summary>
        /// Tests that GetAllGraphUsersDataAsync returns all users data.
        /// </summary>
        [Fact]
        public async Task GetAllGraphUsersDataAsync_ReturnsAllUsersData()
        {
            // Arrange
            var graphResponse = TestsHelper.PrepareGraphApiResponse();
            this._httpClientHelperMock.Setup(x => x.GetGraphApiDataAsync()).ReturnsAsync(graphResponse);

            // Act
            var result = await this._usersService.GetAllGraphUsersDataAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(graphResponse?.Value?[0].Id, result[0].Id);
            Assert.Equal(graphResponse?.Value?[0].DisplayName, result[0].DisplayName);
            Assert.Equal(graphResponse?.Value?[0].AdditionalData[IbbsConstants.UserNameExtensionConstant], result[0].UserName);
        }

        /// <summary>
        /// Tests that GetAllGraphUsersDataAsync returns empty list when no users found.
        /// </summary>
        [Fact]
        public async Task GetAllGraphUsersDataAsync_NoUsers_ReturnsEmptyList()
        {
            // Arrange
            var graphResponse = TestsHelper.PrepareEmptyGraphApiResponse();
            this._httpClientHelperMock.Setup(x => x.GetGraphApiDataAsync()).ReturnsAsync(graphResponse);

            // Act
            var result = await this._usersService.GetAllGraphUsersDataAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        /// <summary>
        /// Tests that SaveUsersDataFromAzureAdAsync returns true when users are saved successfully.
        /// </summary>
        [Fact]
        public async Task SaveUsersDataFromAzureAdAsync_UsersSaved_ReturnsTrue()
        {
            // Arrange
            var graphResponse = TestsHelper.PrepareGraphApiResponse();
            this._httpClientHelperMock.Setup(x => x.GetGraphApiDataAsync()).ReturnsAsync(graphResponse);
            this._usersDataServiceMock.Setup(x => x.SaveUsersDataAsync(It.IsAny<List<GraphUserDTO>>())).ReturnsAsync(true);

            // Act
            var result = await this._usersService.SaveUsersDataFromAzureAdAsync();

            // Assert
            Assert.True(result);
            this._usersDataServiceMock.Verify(x => x.SaveUsersDataAsync(It.IsAny<List<GraphUserDTO>>()), Times.Once);
        }

        /// <summary>
        /// Tests that SaveUsersDataFromAzureAdAsync returns false when no users to save.
        /// </summary>
        [Fact]
        public async Task SaveUsersDataFromAzureAdAsync_NoUsers_ReturnsFalse()
        {
            // Arrange
            var graphResponse = TestsHelper.PrepareEmptyGraphApiResponse();
            this._httpClientHelperMock.Setup(x => x.GetGraphApiDataAsync()).ReturnsAsync(graphResponse);

            // Act
            var result = await this._usersService.SaveUsersDataFromAzureAdAsync();

            // Assert
            Assert.False(result);
            this._usersDataServiceMock.Verify(x => x.SaveUsersDataAsync(It.IsAny<List<GraphUserDTO>>()), Times.Never);
        }
    }
} 