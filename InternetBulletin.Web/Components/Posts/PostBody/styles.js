import {
	makeStyles,
	tokens,
	buttonClassNames,
} from "@fluentui/react-components";

const useStyles = makeStyles({
	card: {
		margin: "20px auto",
		width: "720px",
		maxWidth: "100%",
		padding: "20px",
		boxShadow: "0 4px 8px rgba(0, 0, 0, 0.1)",
		borderRadius: "8px",
	},
	cardHeader: {
		marginBottom: "10px",
	},
	cardPreview: {
		marginTop: "auto",
		padding: "10px 10px 0px 10px",
	},
	postContent: {
		fontSize: "14px",
		lineHeight: "1.6",
		overflow: "hidden",
		textOverflow: "ellipsis",
	},
	button: {
		marginTop: "10px",
		color: "#0078d4",
		cursor: "pointer",
		fontSize: "14px",
		textDecoration: "none",
	},
	headerContainer: {
		display: "flex",
		justifyContent: "space-between",
		alignItems: "center",
		width: "100%",
	},
	headerTitle: {
		flexGrow: 1,
	},
	headerButtons: {
		display: "flex",
		gap: "10px",
		marginLeft: "autoo",
	},
	editButton: {
		color: "#0078d4",
	},
	deleteButton: {
		color: "#d83b01",
	},
	buttonNonInteractive: {
		backgroundColor: tokens.colorNeutralBackground1,
		border: `${tokens.strokeWidthThin} solid ${tokens.colorNeutralStroke1}`,
		color: tokens.colorNeutralForeground1,
		cursor: "default",
		pointerEvents: "none",

		[`& .${buttonClassNames.icon}`]: {
			color: tokens.colorStatusSuccessForeground1,
		},
	},
});

export { useStyles };
