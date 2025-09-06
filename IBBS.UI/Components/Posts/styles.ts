import { makeStyles, tokens } from "@fluentui/react-components";

export const useStyles = makeStyles({
	postsHeading: {
		lineHeight: 1.3,
		margin: "0 auto",
		marginBottom: "40px",
		maxWidth: "800px",
		textAlign: "center",
		color: tokens.colorBrandForeground1,
	},
	postsTitle: {
		marginTop: "30px",
		textAlign: "center",
		display: "flex",
		justifyContent: "center",
		alignItems: "center",
		fontSize: tokens.fontSizeHero900,
		fontWeight: tokens.fontWeightBold,
		lineHeight: 1.3,
		marginBottom: "20px",
		paddingTop: "20px",
		paddingBottom: "10px",
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
});
