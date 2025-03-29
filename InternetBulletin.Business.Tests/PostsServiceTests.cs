// *********************************************************************************
//	<copyright file="PostsServiceTests.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Posts Services Test Class.</summary>
// *********************************************************************************

namespace InternetBulletin.UnitTests
{
	/// <summary>
	/// The Posts Services Test Class.
	/// </summary>
	public class PostsServiceTests
	{
		/// <summary>
		/// The mock posts data service
		/// </summary>
		private readonly Mock<IPostsDataService> _mockPostsDataService;

		/// <summary>
		/// The mock logger
		/// </summary>
		private readonly Mock<ILogger<PostsService>> _mockLogger;

		/// <summary>
		/// The posts service
		/// </summary>
		private readonly PostsService _postsService;

		/// <summary>
		/// Initializes a new instance of the <see cref="PostsServiceTests"/> class.
		/// </summary>
		public PostsServiceTests()
		{
			this._mockPostsDataService = new Mock<IPostsDataService>();
			this._mockLogger = new Mock<ILogger<PostsService>>();

			this._postsService = new PostsService(
				_mockPostsDataService.Object,
				_mockLogger.Object);
		}

		#region GetPostAsync

		/// <summary>
		/// Gets the post asynchronous should throw exception when post identifier is null or empty.
		/// </summary>
		[Fact]
		public async Task GetPostAsync_ShouldThrowException_WhenPostIdIsNullOrEmpty()
		{
			// Arrange
			string postId = string.Empty;

			// Act
			var result = await Assert.ThrowsAsync<Exception>(() => _postsService.GetPostAsync(postId));

			// Assert
			Assert.NotNull(result);
			Assert.Equal(ExceptionConstants.PostIdNotPresentMessageConstant, result.Message);
		}

		/// <summary>
		/// Gets the post asynchronous should return new post when post identifier is invalid unique identifier.
		/// </summary>
		[Fact]
		public async Task GetPostAsync_ShouldThrowException_WhenPostIdIsInvalidGuid()
		{
			// Arrange
			string postId = "invalid-guid";

			// Act
			var result = await Assert.ThrowsAsync<Exception>(() => _postsService.GetPostAsync(postId));

			// Assert
			Assert.NotNull(result);
			Assert.Equal(ExceptionConstants.PostGuidNotValidMessageConstant, result.Message);
		}

		/// <summary>
		/// Gets the post asynchronous should return new post when post not found.
		/// </summary>
		[Fact]
		public async Task GetPostAsync_ShouldThrowException_WhenPostNotFound()
		{
			// Arrange
			var postId = Convert.ToString(Guid.NewGuid(), CultureInfo.CurrentCulture) ?? string.Empty;
			var mockEmptyPost = new Post();
			_mockPostsDataService.Setup(x => x.GetPostAsync(It.IsAny<Guid>())).ReturnsAsync(mockEmptyPost);

			// Act
			var result = await Assert.ThrowsAsync<Exception>(() => _postsService.GetPostAsync(postId));

			// Assert
			Assert.NotNull(result);
			Assert.Equal(ExceptionConstants.PostNotFoundMessageConstant, result.Message);
		}

		/// <summary>
		/// Gets the post asynchronous should return post when post found.
		/// </summary>
		[Fact]
		public async Task GetPostAsync_ShouldReturnPost_WhenPostFound()
		{
			// Arrange
			var expectedPost = TestsHelper.CreateMockPostEntityData();
			var postId = expectedPost.PostId;
			_mockPostsDataService.Setup(x => x.GetPostAsync(It.IsAny<Guid>())).ReturnsAsync(expectedPost);

			// Act
			var result = await _postsService.GetPostAsync(postId.ToString());

			// Assert
			Assert.NotNull(result);
			Assert.IsType<Post>(result);
			Assert.Equal(postId, result.PostId);
		}

		#endregion

		#region AddNewPostAsync

		/// <summary>
		/// Adds the new post asynchronous should throw argument null exception when new post is null.
		/// </summary>
		[Fact]
		public async Task AddNewPostAsync_ShouldThrowArgumentNullException_WhenNewPostIsNull()
		{
			// Arrange
			AddPostDTO newPost = null!;

			// Act
			var result = await Assert.ThrowsAsync<Exception>(() => _postsService.AddNewPostAsync(newPost));

			// Assert
			Assert.NotNull(result);
			Assert.Equal(ExceptionConstants.NullPostMessageConstant, result.Message);
		}

		/// <summary>
		/// Adds the new post asynchronous should return true when post is added successfully.
		/// </summary>
		[Fact]
		public async Task AddNewPostAsync_ShouldReturnTrue_WhenPostIsAddedSuccessfully()
		{
			// Arrange
			var mockPost = TestsHelper.CreateMockPostDtoData();
			_mockPostsDataService.Setup(x => x.AddNewPostAsync(It.IsAny<AddPostDTO>())).ReturnsAsync(true);

			// Act
			var result = await _postsService.AddNewPostAsync(mockPost);

			// Assert
			Assert.True(result);
		}

		/// <summary>
		/// Adds the new post asynchronous should return false when post is not added.
		/// </summary>
		[Fact]
		public async Task AddNewPostAsync_ShouldReturnFalse_WhenPostIsNotAdded()
		{
			// Arrange
			var mockPost = TestsHelper.CreateMockPostDtoData();
			_mockPostsDataService.Setup(x => x.AddNewPostAsync(It.IsAny<AddPostDTO>())).ReturnsAsync(false);

			// Act
			var result = await _postsService.AddNewPostAsync(mockPost);

			// Assert
			Assert.False(result);
		}

		#endregion

		#region UpdatePostAsync

		/// <summary>
		/// Updates the post asynchronous should throw exception when updated post is null.
		/// </summary>
		[Fact]
		public async Task UpdatePostAsync_ShouldThrowException_WhenUpdatedPostIsNull()
		{
			// Arrange
			Post mockPost = null!;

			// Act
			var result = await Assert.ThrowsAsync<Exception>(() => _postsService.UpdatePostAsync(mockPost));

			// Assert
			Assert.NotNull(result);
			Assert.Equal(ExceptionConstants.NullPostMessageConstant, result.Message);
		}

		/// <summary>
		/// Updates the post asynchronous should return updated post when post is updated successfully.
		/// </summary>
		[Fact]
		public async Task UpdatePostAsync_ShouldReturnUpdatedPost_WhenPostIsUpdatedSuccessfully()
		{
			// Arrange
			var mockPost = TestsHelper.CreateMockPostEntityData();
			_mockPostsDataService.Setup(x => x.UpdatePostAsync(It.IsAny<Post>())).ReturnsAsync(mockPost);

			// Act
			var result = await _postsService.UpdatePostAsync(mockPost);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(mockPost.PostTitle, result.PostTitle);
		}

		/// <summary>
		/// Updates the post asynchronous should return exception when post is not updated.
		/// </summary>
		[Fact]
		public async Task UpdatePostAsync_ShouldReturnException_WhenPostIsNotUpdated()
		{
			// Arrange
			var mockPost = TestsHelper.CreateMockPostEntityData();
			Post expectedPost = null!;

			_mockPostsDataService.Setup(x => x.UpdatePostAsync(It.IsAny<Post>())).ReturnsAsync(expectedPost);

			// Act
			var result = await Assert.ThrowsAsync<Exception>(() => _postsService.UpdatePostAsync(mockPost));

			// Assert
			Assert.NotNull(result);
			Assert.Equal(ExceptionConstants.PostNotFoundMessageConstant, result.Message);
		}

		#endregion

		#region DeletePostAsync

		[Fact]
		public async Task DeletePostAsync_ShouldThrowException_WhenPostIdIsNullOrEmpty()
		{
			// Arrange
			string postId = string.Empty;

			// Act & Assert
			var exception = await Assert.ThrowsAsync<Exception>(() => _postsService.DeletePostAsync(postId));
			Assert.Equal(ExceptionConstants.PostIdNotPresentMessageConstant, exception.Message);
		}

		/// <summary>
		/// Deletes the post asynchronous should throw exception when post identifier is invalid unique identifier.
		/// </summary>
		[Fact]
		public async Task DeletePostAsync_ShouldThrowException_WhenPostIdIsInvalidGuid()
		{
			// Arrange
			string postId = "invalid-guid";

			// Act & Assert
			var exception = await Assert.ThrowsAsync<Exception>(() => _postsService.DeletePostAsync(postId));
			Assert.Equal(ExceptionConstants.PostGuidNotValidMessageConstant, exception.Message);
		}

		/// <summary>
		/// Deletes the post asynchronous should return true when post is deleted successfully.
		/// </summary>
		[Fact]
		public async Task DeletePostAsync_ShouldReturnTrue_WhenPostIsDeletedSuccessfully()
		{
			// Arrange
			var postId = Guid.NewGuid().ToString();
			_mockPostsDataService.Setup(x => x.DeletePostAsync(It.IsAny<Guid>())).ReturnsAsync(true);

			// Act
			var result = await _postsService.DeletePostAsync(postId);

			// Assert
			Assert.True(result);
		}

		/// <summary>
		/// Deletes the post asynchronous should return false when post is not deleted.
		/// </summary>
		[Fact]
		public async Task DeletePostAsync_ShouldReturnFalse_WhenPostIsNotDeleted()
		{
			// Arrange
			var postId = Guid.NewGuid().ToString();
			_mockPostsDataService.Setup(x => x.DeletePostAsync(It.IsAny<Guid>())).ReturnsAsync(false);

			// Act
			var result = await _postsService.DeletePostAsync(postId);

			// Assert
			Assert.False(result);
		}

		#endregion

		#region GetAllPostsAsync

		/// <summary>
		/// Gets all posts asynchronous should return list of all posts when posts are present.
		/// </summary>
		[Fact]
		public async Task GetAllPostsAsync_ShouldReturnListOfAllPosts_WhenPostsArePresent()
		{
			// Arrange
			var mockPostsList = TestsHelper.CreateMockListPostEntityData();
			_mockPostsDataService.Setup(x => x.GetAllPostsAsync()).ReturnsAsync(mockPostsList);

			// Act
			var result = await _postsService.GetAllPostsAsync();

			// Assert
			Assert.NotNull(result);
			Assert.Equal(mockPostsList.Count, result.Count);
			Assert.Equal(mockPostsList.First(), result.First());
			Assert.Equal(mockPostsList.Last(), result.Last());
		}

		#endregion
	}
}
