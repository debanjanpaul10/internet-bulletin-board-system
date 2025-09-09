import { makeStyles, tokens } from "@fluentui/react-components";

const useStyles = makeStyles({
	drawer: {
		backdropFilter: "blur(20px)",
	},
	drawerHeader: {
		padding: "20px 16px",
		background:
			"linear-gradient(135deg, #3A29FF 0%, #FF94B4 50%, #FF3232 100%)",
		borderBottom: "none",
		backdropFilter: "blur(10px)",
		boxShadow: "0 4px 20px rgba(0, 0, 0, 0.1)",
	},
	closeButton: {
		marginLeft: "auto",
		borderRadius: "8px !important",
		transition: "all 0.2s ease !important",
		color: "white !important",
		"&:hover": {
			backgroundColor: "rgba(255, 255, 255, 0.2) !important",
			boxShadow: "0 2px 8px rgba(255, 255, 255, 0.15) !important",
			transform: "scale(1.05) !important",
		},
		"&:active": {
			transform: "scale(0.98) !important",
		},
		"& > svg": {
			color: "white !important",
		},
		minWidth: "20px",
	},
	drawerBody: {
		padding: "20px 16px",
		height: "calc(100% - 80px)",
		background:
			"linear-gradient(180deg, rgba(58, 41, 255, 0.05) 0%, rgba(255, 148, 180, 0.05) 50%, rgba(255, 50, 50, 0.05) 100%)",
		backdropFilter: "blur(20px)",
		overflow: "none",
		display: "flex",
		flexDirection: "column",
		gap: "20px",
	},
	formField: {
		display: "flex",
		flexDirection: "column",
		gap: "8px",
	},
	submitButton: {
		marginTop: "auto",
		padding: "16px 20px !important",
		display: "flex !important",
		alignItems: "center !important",
		justifyContent: "center !important",
		width: "100% !important",
		gap: "12px !important",
		borderRadius: "12px !important",
		margin: "4px 0 !important",
		backgroundColor: "rgba(255, 255, 255, 0.05) !important",
		backdropFilter: "blur(10px) !important",
		border: "1px solid rgba(255, 255, 255, 0.1) !important",
		color: `${tokens.colorNeutralForeground1} !important`,
		fontWeight: `${tokens.fontWeightMedium} !important`,
		transition: "all 0.2s ease !important",
		"&:hover": {
			backgroundColor: "rgba(255, 255, 255, 0.12) !important",
			boxShadow: "0 4px 15px rgba(255, 255, 255, 0.1) !important",
			border: "1px solid rgba(255, 255, 255, 0.2) !important",
			transform: "translateX(4px) !important",
		},
		"&:active": {
			backgroundColor: "rgba(255, 255, 255, 0.18) !important",
			transform: "translateX(2px) !important",
		},
	},
	title: {
		color: "white",
		fontWeight: "600",
		fontSize: tokens.fontSizeBase500,
	},
	label: {
		color: tokens.colorNeutralForeground1,
	},
	dropdown: {
		backgroundColor: "rgba(0, 0, 0, 0.2) !important",
		border: "1px solid rgba(255, 255, 255, 0.1) !important",
		borderRadius: "12px !important",
		color: "white !important",
		"&:hover": {
			border: "1px solid rgba(255, 255, 255, 0.2) !important",
			backgroundColor: "rgba(0, 0, 0, 0.3) !important",
		},
		"&:focus-within": {
			border: "1px solid rgba(58, 41, 255, 0.5) !important",
			backgroundColor: "rgba(0, 0, 0, 0.4) !important",
		},
		padding: "10px !important",
		marginTop: "0px !important",
	},
	input: {
		backgroundColor: "rgba(0, 0, 0, 0.2) !important",
		border: "1px solid rgba(255, 255, 255, 0.1) !important",
		borderRadius: "12px !important",
		color: "white !important",
		"&:hover": {
			border: "1px solid rgba(255, 255, 255, 0.2) !important",
			backgroundColor: "rgba(0, 0, 0, 0.3) !important",
		},
		"&:focus-within": {
			border: "1px solid rgba(58, 41, 255, 0.5) !important",
			backgroundColor: "rgba(0, 0, 0, 0.4) !important",
		},
		padding: "10px !important",
		marginTop: "0px !important",
	},
	textarea: {
		padding: "12px 16px !important",
		backgroundColor: "rgba(0, 0, 0, 0.2) !important",
		border: "1px solid rgba(255, 255, 255, 0.1) !important",
		borderRadius: "12px !important",
		color: "white !important",
		minHeight: "120px !important",
		transition: "all 0.2s ease !important",
		"&:hover": {
			border: "1px solid rgba(255, 255, 255, 0.2) !important",
			backgroundColor: "rgba(0, 0, 0, 0.3) !important",
		},
		"&:focus-within": {
			border: "1px solid rgba(58, 41, 255, 0.5) !important",
			backgroundColor: "rgba(0, 0, 0, 0.4) !important",
		},
	},
});

export { useStyles };
