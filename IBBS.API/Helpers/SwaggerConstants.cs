namespace IBBS.API.Helpers;

/// <summary>
/// The Swagger constants class.
/// </summary>
internal static class SwaggerConstants
{
	/// <summary>
	/// The Swagger UI Constants
	/// </summary>
	internal static class SwaggerUIConstants
	{
		/// The api version for Swagger documentation.
		/// </summary>
		internal const string ApiVersion = "v1";

		/// <summary>
		/// The swagger endpoint for the API documentation.
		/// </summary>
		internal const string SwaggerEndpointUrl = "/swagger/v1/swagger.json";

		/// <summary>
		/// The swagger ui endpoint prefix.
		/// </summary>
		internal const string SwaggerUiPrefix = "swaggerui";

		/// <summary>
		/// The description for the Swagger documentation.
		/// </summary>
		internal const string SwaggerDescription = "API documentation for Internet Bulletin Board Service";

		/// <summary>
		/// The Author Details class contains information about the author of the API.
		/// </summary>
		internal static class AuthorDetails
		{
			/// <summary>
			/// The author's name.
			/// </summary>
			internal static readonly string Name = "Debanjan Paul";

			/// <summary>
			/// The author's email address.
			/// </summary>
			internal static readonly string Email = "debanjanpaul10@gmail.com";
		}

		/// <summary>
		/// The API name for Swagger documentation.
		/// </summary>
		internal const string ApplicationAPIName = "IBBS.API";
	}

	/// <summary>
	/// Swagger documentation for AIServicesController
	/// </summary>
	internal static class AIServicesController
	{
		/// <summary>
		/// Swagger documentation for GetAboutUsDataAsync.
		/// </summary>
		internal static class GetAboutUsDataAction
		{
			internal const string Summary = "Gets the about us master data.";
			internal const string Description = "Gets the about us master data from the mongodb describing the overall functionalities for IBBS.";
			internal const string OperationId = nameof(GetAboutUsDataAction);
		}

		/// <summary>
		/// Swagger documentation for RewriteWithAIAsync.
		/// </summary>
		internal static class RewriteWithAIAction
		{
			internal const string Summary = "Rewrites user story content using AI.";
			internal const string Description = "Uses AI services to rewrite and improve user story content based on the provided request data.";
			internal const string OperationId = nameof(RewriteWithAIAction);
		}

		/// <summary>
		/// Swagger documentation for GenerateTagForStoryAsync.
		/// </summary>
		internal static class GenerateTagForStoryAction
		{
			internal const string Summary = "Generates genre tags for user stories using AI.";
			internal const string Description = "Analyzes user story content and generates appropriate genre tags using AI services.";
			internal const string OperationId = nameof(GenerateTagForStoryAction);
		}

		/// <summary>
		/// Swagger documentation for ModerateContentDataAsync.
		/// </summary>
		internal static class ModerateContentDataAction
		{
			internal const string Summary = "Moderates content using AI services.";
			internal const string Description = "Analyzes and moderates user-generated content to ensure it meets community guidelines using AI moderation services.";
			internal const string OperationId = nameof(ModerateContentDataAction);
		}

		/// <summary>
		/// Swagger documentation for GetChatbotResponseAsync.
		/// </summary>
		internal static class GetChatbotResponseAction
		{
			internal const string Summary = "Responds to user query asynchronously.";
			internal const string Description = "Calls the AI service to handle the user query.";
			internal const string OperationId = nameof(GetChatbotResponseAction);
		}

	}

	/// <summary>
	/// Swagger documentation for PostsController
	/// </summary>
	internal static class PostsController
	{
		/// <summary>
		/// Swagger documentation for GetAllPostsDataAsync.
		/// </summary>
		internal static class GetAllPostsDataAction
		{
			internal const string Summary = "Gets all posts data.";
			internal const string Description = "Retrieves all available posts from the bulletin board system. This endpoint is publicly accessible.";
			internal const string OperationId = nameof(GetAllPostsDataAction);
		}

		/// <summary>
		/// Swagger documentation for GetPostAsync.
		/// </summary>
		internal static class GetPostAction
		{
			internal const string Summary = "Gets a specific post by ID.";
			internal const string Description = "Retrieves detailed information about a specific post using its unique identifier. Requires user authentication.";
			internal const string OperationId = nameof(GetPostAction);
		}

		/// <summary>
		/// Swagger documentation for AddNewPostAsync.
		/// </summary>
		internal static class AddNewPostAction
		{
			internal const string Summary = "Creates a new post.";
			internal const string Description = "Creates a new post in the bulletin board system with the provided post data. Requires user authentication.";
			internal const string OperationId = nameof(AddNewPostAction);
		}

		/// <summary>
		/// Swagger documentation for UpdatePostAsync.
		/// </summary>
		internal static class UpdatePostAction
		{
			internal const string Summary = "Updates an existing post.";
			internal const string Description = "Updates an existing post with new information. Only the post author can update their own posts. Requires user authentication.";
			internal const string OperationId = nameof(UpdatePostAction);
		}

		/// <summary>
		/// Swagger documentation for DeletePostAsync.
		/// </summary>
		internal static class DeletePostAction
		{
			internal const string Summary = "Deletes a post.";
			internal const string Description = "Permanently deletes a post from the bulletin board system. Only the post author can delete their own posts. Requires user authentication.";
			internal const string OperationId = nameof(DeletePostAction);
		}
	}

	/// <summary>
	/// Swagger documentation for PostRatingsController
	/// </summary>
	internal static class PostRatingsController
	{
		/// <summary>
		/// Swagger documentation for GetAllUserRatingsAsync.
		/// </summary>
		internal static class GetAllUserRatingsAction
		{
			internal const string Summary = "Gets all user post ratings.";
			internal const string Description = "Retrieves all post ratings submitted by the authenticated user. Requires user authentication.";
			internal const string OperationId = nameof(GetAllUserRatingsAction);
		}

		/// <summary>
		/// Swagger documentation for UpdateRatingAsync.
		/// </summary>
		internal static class UpdateRatingAction
		{
			internal const string Summary = "Updates or creates a post rating.";
			internal const string Description = "Updates an existing rating or creates a new rating for a specific post. Requires user authentication.";
			internal const string OperationId = nameof(UpdateRatingAction);
		}
	}
}
