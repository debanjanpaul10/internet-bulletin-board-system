﻿namespace IBBS.API.Adapters.Models.AI;

/// <summary>
/// The AI Chatbot Response dto model.
/// </summary>
public class AIChatbotResponseDTO
{
	/// <summary>
	/// Gets or sets the ai response data.
	/// </summary>
	/// <value>
	/// The ai response data.
	/// </value>
	public string AIResponseData { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the user query.
	/// </summary>
	/// <value>
	/// The user query.
	/// </value>
	public string UserQuery { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the user intent.
	/// </summary>
	/// <value>
	/// The user intent.
	/// </value>
	public string UserIntent { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the SQL query.
	/// </summary>
	/// <value>
	/// The SQL query.
	/// </value>
	public string? SqlQuery { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the Followup questions list.
	/// </summary>
	/// <value>
	/// The list of follow up questions.
	/// </value>
	public IEnumerable<string>? FollowupQuestions { get; set; } = [];
}
