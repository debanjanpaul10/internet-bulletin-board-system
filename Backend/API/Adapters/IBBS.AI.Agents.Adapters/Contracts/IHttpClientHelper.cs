namespace IBBS.AI.Agents.Adapters.Contracts;

/// <summary>
/// Http client helper interface.
/// </summary>
public interface IHttpClientHelper
{
    /// <summary>
    /// Gets the ai response asynchronous.
    /// </summary>
    /// <typeparam name="T">The input data.</typeparam>
    /// <param name="data">The data.</param>
    /// <param name="apiUrl">The api url.</param>
    /// <returns>The response from AI</returns>
    Task<HttpResponseMessage> GetAIResponseAsync<T>(T data, string apiUrl);
}
