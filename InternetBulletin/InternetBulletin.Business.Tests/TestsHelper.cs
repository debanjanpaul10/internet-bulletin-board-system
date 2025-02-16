// *********************************************************************************
//	<copyright file="TestsHelper.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Tests Helper Class.</summary>
// *********************************************************************************

namespace InternetBulletin.Business.Tests
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
		public static Post CreateMockPostEntityData()
		{
			return new Post()
			{
				IsActive = true,
				PostId = Guid.NewGuid(),
				PostTitle = "Lorem ipsum",
				PostContent = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
				PostCreatedBy = "Creation User",
				PostCreatedDate = DateTime.Now,
				PostModifiedBy = "Creation User",
				PostModifiedDate = DateTime.Now,
			};
		}

		/// <summary>
		/// Creates the mock list post entity data.
		/// </summary>
		/// <returns>The list of <see cref="Post"/></returns>
		public static List<Post> CreateMockListPostEntityData()
		{
			var iterator = new Random().Next(1, 20);
			var allPosts = new List<Post>();
			for (var i = 0; i < iterator; i++)
			{
				var mockPost = CreateMockPostEntityData();
				allPosts.Add(mockPost);
			}

			return allPosts;
		}
	}
}
