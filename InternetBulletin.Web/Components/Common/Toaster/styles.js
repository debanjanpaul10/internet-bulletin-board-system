import { makeStyles } from "@fluentui/react-components";

const useStyles = makeStyles({
	toastContainer: {
		position: "relative",
	},
	dismissButton: {
		position: "absolute",
		top: "8px",
		right: "8px",
		cursor: "pointer",
		padding: "4px",
		borderRadius: "4px",
		":hover": {
			backgroundColor: "var(--colorNeutralBackground3)",
		},
	},
});

export { useStyles };
