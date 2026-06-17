/**
 * BugReportDTO is a Data Transfer Object (DTO) that represents the data for a bug report, including the bug title, description, severity, status, creator, and page URL.
 */
export class BugReportDTO {
	bugTitle: string = "";
	bugDescription: string = "";
	bugSeverity: number = 0;
	bugStatus: number = 0;
	createdBy: string = "";
	pageUrl: string = "";
}
