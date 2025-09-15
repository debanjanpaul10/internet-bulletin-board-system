export class AIChatbotResponseDTO {
	aiResponseData: string = "";
	userQuery: string = "";
	userIntent: string = "";
	sqlQuery: string | null = null;
	followupQuestions: [] | null = null;

	constructor(
		AIResponseData: string,
		UserQuery: string,
		UserIntent: string,
		SqlQuery: string | null,
		FollowupQuestions: [] | null
	) {
		this.aiResponseData = AIResponseData;
		this.userIntent = UserIntent;
		this.userQuery = UserQuery;
		this.sqlQuery = SqlQuery;
		this.followupQuestions = FollowupQuestions;
	}
}
