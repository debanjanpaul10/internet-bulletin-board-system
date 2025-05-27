import { makeStyles, tokens } from "@fluentui/react-components";

const useStyles = makeStyles( {
	themeToggleButton: {
		width: "50px",
		minWidth: "50px",
	},
	sideBarButton: {
		padding: "0px !important",
		"&:hover": {
			backgroundColor: tokens.colorNeutralBackground1Pressed,
		}
	},
	navbar: {
		padding: "10px 15px",
		"@media (max-width: 768px)": {
			padding: "15px",
		},
		backgroundColor: tokens.colorNeutralBackground1Pressed,
	},
	navContent: {
		display: "flex",
		width: "100%",
		alignItems: "center",
	},
} );

export default useStyles;
