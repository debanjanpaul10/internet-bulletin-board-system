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
	card: {
		margin: "20px auto",
		width: "100%",
		maxWidth: "100%",
		padding: "20px",
		boxShadow: "0 8px 32px rgba(0, 0, 0, 0.1)",
		borderRadius: "16px",
		backgroundColor: "rgba(255, 255, 255, 0.13)",
		backdropFilter: "blur(20px)",
		border: "1px solid rgba(255, 255, 255, 0.1)",
		transition: "all 0.3s ease",
	},
	cardHeader: {
		borderRadius: "8px",
		marginBottom: "16px",
	},
	cardHeaderText: {
		backdropFilter: "blur(20px)",
		border: "1px solid rgba(255, 255, 255, 0.1)",
		borderRadius: "12px",
		boxShadow: "0 4px 15px rgba(0, 0, 0, 0.1)",
		transition: "all 0.2s ease",
		"&:focus": {
			backgroundColor: "rgba(255, 255, 255, 0.08)",
			border: "1px solid rgba(255, 255, 255, 0.2)",
			boxShadow: "0 4px 20px rgba(255, 255, 255, 0.1)",
		},
		"&:hover": {
			backgroundColor: "rgba(255, 255, 255, 0.07)",
			border: "1px solid rgba(255, 255, 255, 0.15)",
		},
	},
	cardPreview: {
		marginTop: "auto",
		padding: "0px 10px 0px 10px",
		borderRadius: "8px",
	},
	postContent: {
		fontSize: tokens.fontSizeBase300,
		lineHeight: "1.6",
		overflow: "hidden",
		textOverflow: "ellipsis",
	},
	createButton: {
		color: tokens.colorBrandForegroundLink,
		margin: "10px 0px 10px 0px",
		cursor: "pointer",
		fontSize: tokens.fontSizeBase300,
		textDecoration: "none",
		"&:hover": {
			color: tokens.colorBrandForegroundLinkHover,
		},
		boxShadow: tokens.shadow16,
	},
	aiEditButton: {
		margin: "10px 0px 10px 0px",
		cursor: "pointer",
		fontSize: tokens.fontSizeBase300,
		fontWeight: tokens.fontWeightMedium,
		textDecoration: "none",
		padding: "10px 20px",
		backgroundColor: "rgba(139, 92, 246, 0.1)",
		backdropFilter: "blur(12px)",
		border: "1px solid rgba(139, 92, 246, 0.2)",
		borderRadius: "12px",
		boxShadow:
			"0 6px 20px rgba(139, 92, 246, 0.15), 0 2px 8px rgba(0, 0, 0, 0.1)",
		transition: "all 0.2s ease",
		minWidth: "100px",
		"&:hover": {
			backgroundColor: "rgba(139, 92, 246, 0.15)",
			border: "1px solid rgba(139, 92, 246, 0.3)",
			boxShadow:
				"0 8px 25px rgba(139, 92, 246, 0.2), 0 3px 10px rgba(0, 0, 0, 0.15)",
			transform: "translateY(-2px)",
		},
		"&:active": {
			transform: "translateY(0px)",
		},
	},
	addNewHeading: {
		fontFamily: "",
		textAlign: "center",
		display: "flex",
		justifyContent: "center",
		alignItems: "center",
		fontSize: tokens.fontSizeHero1000,
		lineHeight: 1.3,
		marginBottom: "48px",
		paddingTop: "20px",
		paddingBottom: "10px",
		fontWeight: tokens.fontWeightBold,
		color: tokens.colorNeutralForeground1,
		background:
			"linear-gradient(135deg, #3A29FF 0%, #FF94B4 50%, #FF3232 100%)",
		backgroundClip: "text",
		WebkitBackgroundClip: "text",
		WebkitTextFillColor: "transparent",
		MozBackgroundClip: "text",
		MozTextFillColor: "transparent",
		textShadow: "none",
		overflow: "visible",
		"@supports not (-webkit-background-clip: text)": {
			color: tokens.colorBrandForeground1,
			background: "none",
			WebkitTextFillColor: "initial",
		},
	},
	moderateWithAiButton: {
		border: "2px solid rgba(255, 148, 180, 0.3)",
		padding: "auto",
		minWidth: "140px",
		boxShadow: `
            0 8px 32px rgba(0, 0, 0, 0.1),
            0 4px 16px rgba(0, 0, 0, 0.05),
            inset 0 1px 0 rgba(255, 255, 255, 0.1)
        `,
		"&:active": {
			transform: "translateY(-1px) scale(0.98)",
		},
	},
	moderateWithAiButtonText: {
		color: tokens.colorPaletteSeafoamBackground2,
	},
	nsfwTag: {
		color: "#ffffff",
		backgroundColor: "rgba(220, 38, 38, 0.25)",
		backdropFilter: "blur(15px)",
		border: "1.5px solid rgba(220, 38, 38, 0.5)",
		borderRadius: "8px",
		padding: "6px 12px",
		fontSize: tokens.fontSizeBase300,
		fontWeight: tokens.fontWeightMedium,
		boxShadow:
			"0 4px 15px rgba(220, 38, 38, 0.2), 0 2px 8px rgba(0, 0, 0, 0.1)",
		transition: "all 0.2s ease",
		"&:hover": {
			backgroundColor: "rgba(220, 38, 38, 0.35)",
			border: "1.5px solid rgba(220, 38, 38, 0.7)",
			boxShadow:
				"0 6px 20px rgba(220, 38, 38, 0.3), 0 3px 10px rgba(0, 0, 0, 0.15)",
			transform: "translateY(-1px)",
		},
	},
	genreTag: {
		color: "#ffffff",
		backgroundColor: "rgba(58, 41, 255, 0.25)",
		backdropFilter: "blur(15px)",
		border: "1.5px solid rgba(58, 41, 255, 0.5)",
		borderRadius: "8px",
		padding: "6px 12px",
		fontSize: tokens.fontSizeBase300,
		fontWeight: tokens.fontWeightMedium,
		boxShadow:
			"0 4px 15px rgba(58, 41, 255, 0.2), 0 2px 8px rgba(0, 0, 0, 0.1)",
		transition: "all 0.2s ease",
		"&:hover": {
			backgroundColor: "rgba(58, 41, 255, 0.35)",
			border: "1.5px solid rgba(58, 41, 255, 0.7)",
			boxShadow:
				"0 6px 20px rgba(58, 41, 255, 0.3), 0 3px 10px rgba(0, 0, 0, 0.15)",
			transform: "translateY(-1px)",
		},
	},
	textEditor: {
		backgroundColor: "rgba(255, 255, 255, 0.05)",
		backdropFilter: "blur(10px)",
		border: "1px solid rgba(255, 255, 255, 0.1)",
		borderRadius: "12px",
		boxShadow: "0 4px 15px rgba(0, 0, 0, 0.1)",
		transition: "all 0.2s ease",
		"& .ql-container": {
			backgroundColor: "transparent",
			border: "none",
		},
		"& .ql-editor": {
			backgroundColor: "transparent",
			color: tokens.colorNeutralForeground1,
		},
		"& .ql-toolbar": {
			backgroundColor: "rgba(255, 255, 255, 0.03)",
			backdropFilter: "blur(5px)",
			border: "1px solid rgba(255, 255, 255, 0.08)",
			borderRadius: "8px 8px 0 0",
		},
		"&:focus-within": {
			backgroundColor: "rgba(255, 255, 255, 0.08)",
			border: "1px solid rgba(255, 255, 255, 0.2)",
			boxShadow: "0 4px 20px rgba(255, 255, 255, 0.1)",
		},
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
