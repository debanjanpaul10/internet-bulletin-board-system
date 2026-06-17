/**
 * AIChatbotResponseDTO is a Data Transfer Object (DTO) that represents the response from an AI chatbot,
 * including the AI response data, the original user query, the identified user intent, any generated SQL query, and a list of follow-up questions.
 */
export class AIChatbotResponseDTO {
	aiResponseData: string = "";
	userQuery: string = "";
	userIntent: string = "";
	sqlQuery: string = "";
	followupQuestions: string[] = [];

	constructor(
		aiResponseData: string,
		userQuery: string,
		userIntent: string,
		sqlQuery: string,
		followupQuestions: string[],
	) {
		this.aiResponseData = aiResponseData;
		this.userQuery = userQuery;
		this.userIntent = userIntent;
		this.sqlQuery = sqlQuery;
		this.followupQuestions = followupQuestions;
	}
}
