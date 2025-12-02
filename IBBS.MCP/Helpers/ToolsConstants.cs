namespace IBBS.MCP.Helpers;

/// <summary>
/// Provides constant values used by tools for post data operations within the application.
/// </summary>
/// <remarks>This class contains nested static classes and string constants that define metadata and descriptions
/// for actions related to retrieving post data. These constants are intended for internal use by tools and services
/// that interact with post data APIs.</remarks>
internal static class ToolsConstants
{
    /// <summary>
    /// Provides utility actions for retrieving post data, including operations to fetch all posts or a specific post
    /// for the current user context.
    /// </summary>
    /// <remarks>This class contains static actions intended for internal use when working with post data
    /// retrieval. The actions encapsulate asynchronous operations and return response objects that include both data
    /// and status information. Errors and authorization issues are reported in the response rather than through
    /// exceptions.</remarks>
    internal static class PostsDataTool
    {
        /// <summary>
        /// Provides a description of the asynchronous operation that retrieves data for all available posts associated
        /// with the current user context.
        /// </summary>
        /// <remarks>The description includes details about the operation, such as its asynchronous
        /// nature, the inclusion of both result data and status or error information in the response, and the handling
        /// of missing data by reporting errors in the response object rather than throwing exceptions.</remarks>
        internal static class GetAllPostsDataAction
        {
            internal const string Description = """
                Role: Asynchronously retrieves data for all available posts.
                Description: 
                    - This method performs an asynchronous operation to fetch all posts associated with the current user context. 
                    - The returned response includes both the result data and any relevant status or error information.
                    - This method does not throw exceptions for missing data; errors are reported in the response object.
                Returns: A ResponseDTO containing the data for all posts. The response includes status information and the collection of post data. If no posts are available, the collection will be empty.
                """
            ;
        }

        /// <summary>
        /// Provides metadata and constants for the action that retrieves data for a single post by its identifier.
        /// </summary>
        /// <remarks>This class is intended for internal use to describe the behavior and requirements of
        /// the post data retrieval action. The action requires caller authorization and returns a response indicating
        /// success, error, or unauthorized access based on the post's existence and the caller's permissions.</remarks>
        internal static class GetPostDataAction
        {
            internal const string Description = """
                Role: Retrieves the data for a single post identified by the specified post ID.
                Description: 
                    - This method requires the caller to be authorized. If the post does not exist or the caller is not authorized, the response will indicate the appropriate error condition.
                Returns: A ResponseDTO containing the post data if found and authorized; otherwise, a response indicating an error or unauthorized access.

                """;

            internal const string InputDescription = "The unique identifier of the post to retrieve. Cannot be null or empty.";
        }

        /// <summary>
        /// Provides constants describing the action for retrieving all post ratings associated with a specified user
        /// email address.
        /// </summary>
        /// <remarks>This class is intended for internal use to supply descriptive metadata for the user
        /// ratings retrieval operation. It includes information about expected input and response behavior, such as
        /// handling cases where no ratings are found and logging execution details.</remarks>
        internal static class GetAllUserRatingsAction
        {
            internal const string Description = """
                Role: Retrieves all post ratings associated with the specified user email address asynchronously.
                Description: 
                    - If no ratings are found for the specified user, the response will indicate a bad request.
                    - This method logs its execution and may throw exceptions encountered during processing.
                Returns: A ResponseDTO containing the user's post ratings if found; otherwise, a response indicating a bad request.
                """;

            internal const string InputDescription = "The email address of the user whose post ratings are to be retrieved. Cannot be null or empty.";
        }
    }
}
