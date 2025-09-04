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

const useStyles = makeStyles({
	card: {
		margin: "20px auto",
		width: "100%",
		maxWidth: "100%",
		padding: "20px",
		boxShadow: tokens.shadow28,
		borderRadius: "8px",
		backgroundColor: tokens.colorNeutralCardBackground,
	},
	cardHeaderText: {
		boxShadow: tokens.shadow16,
	},
	cardPreview: {
		marginTop: "auto",
		padding: "0px 10px 0px 10px",
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
	cancelButton: {
		color: tokens.colorStatusDangerBackground3,
		margin: "10px 0px 10px 0px",
		cursor: "pointer",
		fontSize: tokens.fontSizeBase300,
		textDecoration: "none",
		"&:hover": {
			color: tokens.colorStatusDangerBackground3Hover,
		},
		boxShadow: tokens.shadow16,
	},
	aiEditButton: {
		margin: "10px 0px 10px 0px",
		cursor: "pointer",
		fontSize: tokens.fontSizeBase300,
		textDecoration: "none",
		padding: "0.75em",
		boxShadow: tokens.shadow16,
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
		fontWeight: tokens.fontWeightMedium,
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
		boxShadow: tokens.shadow16,
	},
	moderateWithAiButtonText: {
		color: tokens.colorPaletteSeafoamBackground2,
	},
	nsfwTag: {
		color: tokens.colorStatusDangerBackground3,
		"&:hover": {
			color: tokens.colorStatusDangerBackground3Hover,
		},
		boxShadow: tokens.shadow16,
	},
	genreTag: {
		color: tokens.colorBrandForegroundLink,
		"&:hover": {
			color: tokens.colorBrandForegroundLinkHover,
		},
		boxShadow: tokens.shadow16,
	},
	textEditor: {
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

export { useStyles };
