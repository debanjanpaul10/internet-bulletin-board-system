export class BugReportDTO {
	bugTitle: string = "";
	bugDescription: string = "";
	bugSeverity: number = 0;
	bugStatus: number = 0;
	createdBy: string = "";
	pageUrl: string = "";

	constructor(
		BugTitle: string = "",
		BugDescription: string = "",
		BugSeverity: number = 0,
		BugStatus: number = 0,
		CreatedBy: string = "",
		PageUrl: string = ""
	) {
		this.bugTitle = BugTitle;
		this.bugDescription = BugDescription;
		this.bugSeverity = BugSeverity;
		this.bugStatus = BugStatus;
		this.createdBy = CreatedBy;
		this.pageUrl = PageUrl;
	}
}
