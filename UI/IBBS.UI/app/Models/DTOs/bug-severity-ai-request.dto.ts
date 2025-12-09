export class BugSeverityAIRequestDTO {
	bugTitle: string = "";
	bugDescription: string = "";

	constructor(BugTitle: string, BugDescription: string) {
		this.bugDescription = BugDescription;
		this.bugTitle = BugTitle;
	}
}
