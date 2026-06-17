/**
 * AIResponseFeedbackDTO is a Data Transfer Object (DTO) that represents the feedback provided by users for AI-generated responses,
 * including whether the feedback is negative or positive, any comments, the original user query, and the AI response.
 */
export class AIResponseFeedbackDTO {
	isNegativeFeedback: boolean = false;
	isPositiveFeedback: boolean = false;
	feedbackComments: string = "";
	userQuery: string = "";
	aiResponse: string = "";
}
