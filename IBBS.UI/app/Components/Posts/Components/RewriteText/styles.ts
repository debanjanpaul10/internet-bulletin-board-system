import { makeStyles, tokens } from "@fluentui/react-components";

const waveGradientKeyframes = {
	"0%": {
		backgroundPosition: "0% 50%",
	},
	"50%": {
		backgroundPosition: "100% 50%",
	},
	"100%": {
		backgroundPosition: "0% 50%",
	},
};

export const useStyles = makeStyles({
	rewriteButton: {
		marginTop: "8px",
		marginBottom: "8px",
		boxShadow: tokens.shadow16,
	},
	dialogContent: {
		minWidth: "500px",
		maxWidth: "800px",
		maxHeight: "600px",
		overflowY: "auto",
	},
	dialogActions: {
		display: "flex",
		justifyContent: "flex-end",
		gap: "8px",
		marginTop: "16px",
	},
	textContent: {
		whiteSpace: "pre-wrap",
		marginBottom: "16px",
	},
	cancelButton: {
		color: tokens.colorStatusDangerBackground3,
		cursor: "pointer",
		fontSize: tokens.fontSizeBase300,
		textDecoration: "none",
		"&:hover": {
			color: tokens.colorStatusDangerBackground3Hover,
		},
		boxShadow: tokens.shadow16,
	},
	acceptChangeButton: {
		cursor: "pointer",
		fontSize: tokens.fontSizeBase300,
		textDecoration: "none",
		boxShadow: tokens.shadow16,
	},
	gradientTextButton: {
		lineHeight: 1.5,
		margin: "0 auto",
		maxWidth: "700px",
		fontWeight: tokens.fontWeightSemibold,
		textAlign: "center",
		background:
			"linear-gradient(135deg, #1A1F71 0%, #8B5CF6 50%, #DC2626 100%)",
		backgroundSize: "200% 100%",
		backgroundClip: "text",
		WebkitBackgroundClip: "text",
		WebkitTextFillColor: "transparent",
		MozBackgroundClip: "text",
		MozTextFillColor: "transparent",
		animationName: waveGradientKeyframes,
		animationDuration: "3s",
		animationTimingFunction: "ease-in-out",
		animationIterationCount: "infinite",
		"@supports not (-webkit-background-clip: text)": {
			color: tokens.colorBrandForeground1,
			background: "none",
			WebkitTextFillColor: "initial",
			animationName: "none",
		},
	},
});
