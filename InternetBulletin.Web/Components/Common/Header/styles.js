import { makeStyles } from "@fluentui/react-components";

const useStyles = makeStyles({
	button: {
		color: "#0078d4",
		margin: "0px 3px !important",
	},
	logoutButton: {
		color: "#ff4d4f",
		margin: "0px 3px !important",
	},
	homeButton: {
		margin: "0px 5px !important",
	},
	themeToggleButton: {
		width: "50px",
		minWidth: "50px",
	},
	sidecarButton: {
		padding: "0px !important",
	},
	navbar: {
		padding: "10px 15px",
		"@media (max-width: 768px)": {
			padding: "15px",
		},
	},
	navContent: {
		display: "flex",
		width: "100%",
		alignItems: "center",
	},
});

export default useStyles;
