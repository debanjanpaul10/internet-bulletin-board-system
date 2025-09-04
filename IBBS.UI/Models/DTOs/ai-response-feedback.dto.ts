export class AIResponseFeedbackDTO {
	isNegativeFeedback: boolean = false;
	isPositiveFeedback: boolean = false;
	feedbackComments: string = "";
	userQuery: string = "";
	aiResponse: string = "";

	constructor(
		IsNegativeFeedback: false,
		IsPositiveFeedback: false,
		FeedbackComments: "",
		UserQuery: "",
		AIResponse: ""
	) {
		this.aiResponse = AIResponse;
		this.isNegativeFeedback = IsNegativeFeedback;
		this.isPositiveFeedback = IsPositiveFeedback;
		this.feedbackComments = FeedbackComments;
		this.userQuery = UserQuery;
	}
}
