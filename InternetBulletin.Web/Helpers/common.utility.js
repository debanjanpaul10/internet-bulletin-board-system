import { ConsoleMessage } from "@helpers/ibbs.constants";
import { Switch, styled } from "@mui/material";

/**
 * Custom Switch using custom icons for dark mode and light mode
 */
export const CustomDarkModeToggleSwitch = styled(Switch)(({ theme }) => ({
	width: 62,
	height: 34,
	padding: 7,
	"& .MuiSwitch-switchBase": {
		margin: 1,
		padding: 0,
		border: "1px solid white",
		transform: "translateX(6px)",
		"&.Mui-checked": {
			color: "#fff",
			transform: "translateX(22px)",
			"& .MuiSwitch-thumb:before": {
				content: '"🌙"',
				position: "absolute",
				width: "100%",
				height: "100%",
				left: 0,
				top: 0,
				display: "flex",
				alignItems: "center",
				justifyContent: "center",
				fontSize: 18,
			},
			"& + .MuiSwitch-track": {
				opacity: 1,
				backgroundColor:
					theme.palette.mode === "light" ? "#aab4be" : "#8796A5",
			},
		},
	},
	"& .MuiSwitch-thumb": {
		backgroundColor: theme.palette.mode === "light" ? "#001e3c" : "#003892",
		width: 32,
		height: 32,
		border: "1px solid white",
		"&:before": {
			content: '"☀️"',
			position: "absolute",
			width: "100%",
			height: "100%",
			left: 0,
			top: 0,
			display: "flex",
			alignItems: "center",
			justifyContent: "center",
			fontSize: 18,
		},
	},
	"& .MuiSwitch-track": {
		opacity: 1,
		backgroundColor: theme.palette.mode === "dark" ? "#8796A5" : "#aab4be",
		borderRadius: 20 / 2,
	},
}));

export const ConsoleLogMessage = console.log(
	"%c %s",
	"color:red; font-size: 22pt; font-family: 'Source Code Pro'",
	ConsoleMessage
);
