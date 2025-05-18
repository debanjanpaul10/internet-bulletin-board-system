// *********************************************************************************
//	<copyright file="TestsHelper.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Tests Helper Class.</summary>
// *********************************************************************************

using InternetBulletin.Shared.DTOs.Posts;

namespace InternetBulletin.UnitTests
{
    /// <summary>
    /// The Tests Helper Class.
    /// </summary>
    public static class TestsHelper
    {
        /// <summary>
        /// Creates the mock post entity data.
        /// </summary>
        /// <returns>The post data dto.</returns>
        public static Post CreateMockPostEntityData(string userName)
        {
            return new Post()
            {
                IsActive = true,
                PostId = Guid.NewGuid(),
                PostTitle = "Lorem ipsum",
                PostContent = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                PostCreatedDate = DateTime.Now,
                PostOwnerUserName = userName
            };
        }

        /// <summary>
        /// Creates the mock list post entity data.
        /// </summary>
        /// <returns>The list of <see cref="Post"/></returns>
        public static List<Post> CreateMockListPostEntityData(string userName)
        {
            var iterator = new Random().Next(1, 20);
            var allPosts = new List<Post>();
            for (var i = 0; i < iterator; i++)
            {
                var mockPost = CreateMockPostEntityData(userName);
                allPosts.Add(mockPost);
            }

            return allPosts;
        }

        /// <summary>
        /// Creates the mock post data dto.
        /// </summary>
        /// <returns>The post data dto.</returns>
        public static AddPostDTO CreateMockPostDtoData()
        {
            return new AddPostDTO()
            {
                PostTitle = "Lorem ipsum",
                PostContent = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                PostCreatedBy = "Creation User",
            };
        }

        /// <summary>
        /// Creates the mock list post entity data.
        /// </summary>
        /// <returns>The list of <see cref="AddPostDTO"/></returns>
        public static List<AddPostDTO> CreateMockListPostDtoData()
        {
            var iterator = new Random().Next(1, 20);
            var allPosts = new List<AddPostDTO>();
            for (var i = 0; i < iterator; i++)
            {
                var mockPost = CreateMockPostDtoData();
                allPosts.Add(mockPost);
            }

            return allPosts;
        }

        /// <summary>
        /// Creates the mock user entity data.
        /// </summary>
        /// <returns>The user data entity.</returns>
        public static User CreateMockUserEntityData()
        {
            return new User()
            {
                IsActive = true,
                IsAdmin = false,
                Name = "Sample User",
                UserAlias = "user123",
                UserEmail = "user@email.com",
                UserPassword = "password",
            };
        }

        /// <summary>
        /// Creates the mock list user entity data.
        /// </summary>
        /// <returns>The list of <see cref="User"/></returns>
        public static List<User> CreateMockListUserEntityData()
        {
            return new List<User>()
            {
                new User()
                {
                    IsActive = true,
                    IsAdmin = false,
                    Name = "Sample User 1",
                    UserAlias = "user1",
                    UserEmail = "user1@email.com",
                    UserPassword = "password1",
                },
                new User()
                {
                    IsActive = true,
                    IsAdmin = true,
                    Name = "Sample User 2",
                    UserAlias = "user2",
                    UserEmail = "user2@email.com",
                    UserPassword = "password2",
                },
                new User()
                {
                    IsActive = true,
                    IsAdmin = false,
                    Name = "Sample User 3",
                    UserAlias = "user3",
                    UserEmail = "user3@email.com",
                    UserPassword = "password3",
                },
                new User()
                {
                    IsActive = true,
                    IsAdmin = false,
                    Name = "Sample User 4",
                    UserAlias = "user4",
                    UserEmail = "user4@email.com",
                    UserPassword = "password4",
                },
                new User()
                {
                    IsActive = true,
                    IsAdmin = false,
                    Name = "Sample User 5",
                    UserAlias = "user5",
                    UserEmail = "user5@email.com",
                    UserPassword = "password5",
                },
            };
        }

        /// <summary>
        /// Creates mock user profile dto.
        /// </summary>
        /// <param name="userId">The user id.</param>
        public static UserProfileDto CreateMockUserProfileDto(string userName)
        {
            return new UserProfileDto()
            {
                UserName = userName,
                UserPosts = new List<UserPostsDto>()
                {
                    new UserPostsDto()
                    {
                        PostTitle = "Sample post"
                    }
                }
            };
        }

        public static UpdatePostDTO CreateMockUpdatePostsDto()
        {
            return new UpdatePostDTO()
            {
                PostTitle = "Lorem ipsum",
                PostContent = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                PostId = Guid.NewGuid(),
                PostRating = new Random().Next(1, 10)
            };
        }
    }
}
