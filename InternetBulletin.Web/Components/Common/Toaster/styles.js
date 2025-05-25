import { makeStyles, tokens } from "@fluentui/react-components";

const useStyles = makeStyles( {
	toastContainer: {
		position: "relative",
		boxShadow: tokens.shadow28,
	},
	dismissButton: {
		position: "absolute",
		top: "4px",
		right: "2px",
		cursor: "pointer",
	},
} );

export { useStyles };
