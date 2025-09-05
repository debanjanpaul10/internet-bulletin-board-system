import { makeStyles, tokens } from "@fluentui/react-components";

export const useStyles = makeStyles({
	followupQuestionsContainer: {
		marginTop: "8px",
		display: "flex",
		flexWrap: "wrap",
		gap: "6px",
		width: "100%",
	},
	questionBubble: {
		padding: "10px",
		borderRadius: "15px",
		backgroundColor: tokens.colorNeutralBackground3,
		color: tokens.colorNeutralForeground1,
		lineHeight: "20px",
		maxWidth: "100%",
		display: "inline-block",
		textAlign: "left",
		whiteSpace: "normal",
		wordBreak: "break-word",
		overflowWrap: "anywhere",
	},
});
