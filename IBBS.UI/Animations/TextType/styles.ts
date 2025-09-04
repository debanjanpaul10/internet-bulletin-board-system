import { makeStyles } from "@fluentui/react-components";

export const useStyles = makeStyles({
	textType: {
		display: "inline-block",
		whiteSpace: "pre-wrap",
	},
	textTypeContent: {
		// Reduce spacing in markdown/html content with higher specificity
		lineHeight: "1.4 !important",
		"& p": {
			marginTop: "0.3em !important",
			marginBottom: "0.3em !important",
			lineHeight: "1.4 !important",
			"&:first-child": {
				marginTop: "0 !important",
			},
			"&:last-child": {
				marginBottom: "0 !important",
			},
		},
		"& ul, & ol": {
			marginTop: "0.3em !important",
			marginBottom: "0.3em !important",
			lineHeight: "1.4 !important",
			"&:first-child": {
				marginTop: "0 !important",
			},
			"&:last-child": {
				marginBottom: "0 !important",
			},
		},
		"& li": {
			marginTop: "0.1em !important",
			marginBottom: "0.1em !important",
			lineHeight: "1.4 !important",
		},
		"& h1, & h2, & h3, & h4, & h5, & h6": {
			marginTop: "0.5em !important",
			marginBottom: "0.3em !important",
			lineHeight: "1.3 !important",
			"&:first-child": {
				marginTop: "0 !important",
			},
		},
		"& blockquote": {
			marginTop: "0.3em !important",
			marginBottom: "0.3em !important",
			lineHeight: "1.4 !important",
		},
		"& pre": {
			marginTop: "0.3em !important",
			marginBottom: "0.3em !important",
			lineHeight: "1.4 !important",
		},
		"& br": {
			lineHeight: "1.2 !important",
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
