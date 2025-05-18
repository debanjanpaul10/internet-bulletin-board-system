import { ConsoleMessage } from "@helpers/ibbs.constants";

export const ConsoleLogMessage = console.log(
	"%c %s",
	"color:red; font-size: 22pt; font-family: 'Source Code Pro'",
	ConsoleMessage
);
