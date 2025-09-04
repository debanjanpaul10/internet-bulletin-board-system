import { makeStyles } from "@fluentui/react-components";

export const useStyles = makeStyles({
	textType: {
		display: "inline-block",
		whiteSpace: "pre-wrap",
	},
	textTypeContent: {
		// Remove extra spacing from markdown/html content
		"& > :last-child": {
			marginBottom: 0,
		},
		"& p:last-child": {
			marginBottom: 0,
		},
		"& ul:last-child": {
			marginBottom: 0,
		},
		"& ol:last-child": {
			marginBottom: 0,
		},
	},
	textTypeCursor: {
		marginLeft: "0.25rem",
		display: "inline-block",
		opacity: 1,
	},
	textTypeCursorHidden: {
		display: "none",
	},
});
