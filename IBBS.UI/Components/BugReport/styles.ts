import { makeStyles } from "@fluentui/react-components";

export const useStyles = makeStyles({
	drawerHeader: {
		padding: "20px 16px",
		background:
			"linear-gradient(135deg, #3A29FF 0%, #FF94B4 50%, #FF3232 100%)",
		borderBottom: "none",
		backdropFilter: "blur(10px)",
		boxShadow: "0 4px 20px rgba(0, 0, 0, 0.1)",
	},
	closeButton: {
		borderRadius: "8px !important",
		transition: "all 0.2s ease !important",
		":hover": {
			backgroundColor: "rgba(255, 255, 255, 0.2) !important",
			boxShadow: "0 2px 8px rgba(255, 255, 255, 0.15) !important",
			transform: "scale(1.05) !important",
		},
		":active": {
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
	},
});
