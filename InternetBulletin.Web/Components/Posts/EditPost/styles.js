import { makeStyles } from "@fluentui/react-components";

const useStyles = makeStyles({
	card: {
		margin: "20px auto",
		width: "100%",
		maxWidth: "100%",
		padding: "20px",
		boxShadow: "0 4px 8px rgba(0, 0, 0, 0.1)",
		borderRadius: "8px",
	},
	cardPreview: {
		marginTop: "auto",
		padding: "0px 10px 0px 10px",
	},
	postContent: {
		fontSize: "14px",
		lineHeight: "1.6",
		overflow: "hidden",
		textOverflow: "ellipsis",
	},
	button: {
		margin: "10px 0px 10px 0px",
		cursor: "pointer",
		fontSize: "14px",
		textDecoration: "none",
	},
	editButton: {
		margin: "10px 0px 10px 0px",
		cursor: "pointer",
		fontSize: "14px",
		textDecoration: "none",
	},
	deleteButton: {
		color: "#d83b01",
		margin: "10px 0px 10px 0px",
		cursor: "pointer",
		fontSize: "14px",
		textDecoration: "none",
	}
});
export { useStyles };
