// *********************************************************************************
//	<copyright file="UsersServiceTests.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Users Services Test Class.</summary>
// *********************************************************************************

namespace InternetBulletin.UnitTests
{
    using InternetBulletin.Shared.DTOs;

    /// <summary>
    /// The Users Services Test Class.
    /// </summary>
    public class UsersServiceTests
    {
        /// <summary>
        /// The mock users data service
        /// </summary>
        private readonly Mock<IUsersDataService> _mockUsersDataService;

        /// <summary>
        /// The mock logger
        /// </summary>
        private readonly Mock<ILogger<UsersService>> _mockLogger;

        /// <summary>
        /// The users service
        /// </summary>
        private readonly UsersService _usersService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersServiceTests"/> class.
        /// </summary>
        public UsersServiceTests()
        {
            this._mockUsersDataService = new Mock<IUsersDataService>();
            this._mockLogger = new Mock<ILogger<UsersService>>();

            this._usersService = new UsersService(
                this._mockUsersDataService.Object,
                this._mockLogger.Object);
        }

        #region GetUserAsync Tests

        /// <summary>
        /// Gets the user asynchronous should throw exception when user identifier is invalid.
        /// </summary>
        [Fact]
        public async Task GetUserAsync_ShouldThrowException_WhenUserIdIsInvalid()
        {
            // Arrange
            var userData = new UserLoginDTO()
            {
                UserEmail = string.Empty,
                UserPassword = string.Empty
            };

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() => this._usersService.GetUserAsync(userData));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal(ExceptionConstants.UserIdNotCorrectMessageConstant, exception.Message);
        }

        /// <summary>
        /// Gets the user asynchronous should return user when user exists.
        /// </summary>
        [Fact]
        public async Task GetUserAsync_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var userData = new UserLoginDTO()
            {
                UserEmail = "user@mail.com",
                UserPassword = "userpassword"
            };
            var expectedUser = TestsHelper.CreateMockUserEntityData();
            expectedUser.UserId = new Random().Next(1, 9999);
            this._mockUsersDataService.Setup(x => x.GetUserDetailsAsync(It.IsAny<UserLoginDTO>())).ReturnsAsync(expectedUser);

            // Act
            var result = await this._usersService.GetUserAsync(userData);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUser.UserId, result.UserId);
            Assert.Equal(expectedUser.UserEmail, result.UserEmail);
        }

        /// <summary>
        /// Gets the user asynchronous should throw exception when user does not exist.
        /// </summary>
        [Fact]
        public async Task GetUserAsync_ShouldThrowException_WhenUserDoesNotExist()
        {
            // Arrange
            var userData = new UserLoginDTO()
            {
                UserEmail = "user@mail.com",
                UserPassword = "userpassword"
            };
            User mockUsersData = null!;
            this._mockUsersDataService.Setup(x => x.GetUserDetailsAsync(It.IsAny<UserLoginDTO>())).ReturnsAsync(mockUsersData);

            // Act 
            var exception = await Assert.ThrowsAsync<Exception>(() => this._usersService.GetUserAsync(userData));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal(ExceptionConstants.UserDoesNotExistsMessageConstant, exception.Message);
        }

        #endregion

        #region GetAllUsersAsync Tests

        /// <summary>
        /// Gets all users asynchronous should return list of users.
        /// </summary>
        [Fact]
        public async Task GetAllUsersAsync_ShouldReturnListOfUsers()
        {
            // Arrange
            var expectedUsers = TestsHelper.CreateMockListUserEntityData();
            this._mockUsersDataService.Setup(x => x.GetAllUsersDataAsync()).ReturnsAsync(expectedUsers);

            // Act
            var result = await this._usersService.GetAllUsersAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUsers.Count, result.Count);
        }

        #endregion

        #region AddNewUserAsync Tests

        /// <summary>
        /// Adds the new user asynchronous should throw exception when new user is null.
        /// </summary>
        [Fact]
        public async Task AddNewUserAsync_ShouldThrowException_WhenNewUserIsNull()
        {
            // Arrange
            NewUserDTO newUser = null!;

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() => this._usersService.AddNewUserAsync(newUser));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal(ExceptionConstants.NullUserMessageConstant, exception.Message);
        }

        /// <summary>
        /// Adds the new user asynchronous should return true when user is added successfully.
        /// </summary>
        [Fact]
        public async Task AddNewUserAsync_ShouldReturnTrue_WhenUserIsAddedSuccessfully()
        {
            // Arrange
            var newUser = TestsHelper.CreateMockUserEntityData();
            var newUserDTO = new NewUserDTO()
            {
                Name = newUser.Name,
                UserAlias = newUser.UserAlias,
                UserPassword = newUser.UserPassword,
                UserEmail = newUser.UserEmail,
            };
            this._mockUsersDataService.Setup(x => x.AddNewUserAsync(It.IsAny<User>())).ReturnsAsync(true);

            // Act
            var result = await this._usersService.AddNewUserAsync(newUserDTO);

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Adds the new user asynchronous should return false when user is not added.
        /// </summary>
        [Fact]
        public async Task AddNewUserAsync_ShouldReturnFalse_WhenUserIsNotAdded()
        {
            // Arrange
            var newUser = TestsHelper.CreateMockUserEntityData();
            var newUserDTO = new NewUserDTO()
            {
                Name = newUser.Name,
                UserAlias = newUser.UserAlias,
                UserPassword = newUser.UserPassword,
                UserEmail = newUser.UserEmail,
            };
            this._mockUsersDataService.Setup(x => x.AddNewUserAsync(It.IsAny<User>())).ReturnsAsync(false);

            // Act
            var result = await this._usersService.AddNewUserAsync(newUserDTO);

            // Assert
            Assert.False(result);
        }

        #endregion

        #region UpdateUserAsync Tests

        /// <summary>
        /// Updates the user asynchronous should throw exception when update user is null.
        /// </summary>
        [Fact]
        public async Task UpdateUserAsync_ShouldThrowException_WhenUpdateUserIsNull()
        {
            // Arrange
            User updateUser = null!;

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() => this._usersService.UpdateUserAsync(updateUser));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal(ExceptionConstants.NullUserMessageConstant, exception.Message);
        }

        /// <summary>
        /// Updates the user asynchronous should return updated user when user is updated successfully.
        /// </summary>
        [Fact]
        public async Task UpdateUserAsync_ShouldReturnUpdatedUser_WhenUserIsUpdatedSuccessfully()
        {
            // Arrange
            var updateUser = TestsHelper.CreateMockUserEntityData();
            updateUser.UserId = new Random().Next(1, 999);
            this._mockUsersDataService.Setup(x => x.UpdateUserAsync(It.IsAny<User>())).ReturnsAsync(updateUser);

            // Act
            var result = await this._usersService.UpdateUserAsync(updateUser);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updateUser.UserId, result.UserId);
        }

        /// <summary>
        /// Updates the user asynchronous should throw exception when user does not exist.
        /// </summary>
        [Fact]
        public async Task UpdateUserAsync_ShouldThrowException_WhenUserDoesNotExist()
        {
            // Arrange
            User updateUser = TestsHelper.CreateMockUserEntityData();
            this._mockUsersDataService.Setup(x => x.UpdateUserAsync(It.IsAny<User>())).ReturnsAsync(updateUser);

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() => this._usersService.UpdateUserAsync(updateUser));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal(ExceptionConstants.UserDoesNotExistsMessageConstant, exception.Message);
        }

        #endregion

        #region DeleteUserAsync Tests

        /// <summary>
        /// Deletes the user asynchronous should throw exception when user identifier is invalid.
        /// </summary>
        [Fact]
        public async Task DeleteUserAsync_ShouldThrowException_WhenUserIdIsInvalid()
        {
            // Arrange
            int userId = -1;

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() => this._usersService.DeleteUserAsync(userId));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal(ExceptionConstants.UserIdNotCorrectMessageConstant, exception.Message);
        }

        /// <summary>
        /// Deletes the user asynchronous should return true when user is deleted successfully.
        /// </summary>
        [Fact]
        public async Task DeleteUserAsync_ShouldReturnTrue_WhenUserIsDeletedSuccessfully()
        {
            // Arrange
            int userId = new Random().Next(1, 999);
            this._mockUsersDataService.Setup(x => x.DeleteUserAsync(It.IsAny<int>())).ReturnsAsync(true);

            // Act
            var result = await this._usersService.DeleteUserAsync(userId);

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Deletes the user asynchronous should return false when user is not deleted.
        /// </summary>
        [Fact]
        public async Task DeleteUserAsync_ShouldReturnFalse_WhenUserIsNotDeleted()
        {
            // Arrange
            int userId = new Random().Next(1, 999);
            this._mockUsersDataService.Setup(x => x.DeleteUserAsync(It.IsAny<int>())).ReturnsAsync(false);

            // Act
            var result = await this._usersService.DeleteUserAsync(userId);

            // Assert
            Assert.False(result);
        }

        #endregion
    }
}
