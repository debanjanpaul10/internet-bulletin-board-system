using IBBS.Domain.DomainEntities.AI;
using Newtonsoft.Json;
using static IBBS.AI.Agents.Adapters.Helpers.Constants;

namespace IBBS.AI.Agents.Adapters.Helpers;

/// <summary>
/// The Common Utilities class.
/// </summary>
internal static class CommonUtilities
{
	/// <summary>
	/// Prepares the agent string response.
	/// </summary>
	/// <param name="responseString">The response string.</param>
	/// <returns>The prepared AI response.</returns>
	/// <exception cref="System.Exception"></exception>
	public static string PrepareAgentStringResponse(string responseString)
	{
		var aiResponse = JsonConvert.DeserializeObject<AIAgentResponse>(responseString) ?? new AIAgentResponse();
		return aiResponse.ResponseData.ToString() ?? throw new Exception(ExceptionConstants.AiServicesCannotBeAvailedExceptionConstant);
	}
}
