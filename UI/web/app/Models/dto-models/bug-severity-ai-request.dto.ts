/**
 * BugSeverityAIRequestDTO is a Data Transfer Object (DTO) that represents the request for determining the severity of a bug based on its title and description.
 */
export class BugSeverityAIRequestDTO {
	bugTitle: string = "";
	bugDescription: string = "";
}
