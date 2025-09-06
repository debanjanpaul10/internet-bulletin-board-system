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

const throbKeyframes = {
	"0%": {
		transform: "scale(1)",
		boxShadow: "0 0 0 0 rgba(58, 41, 255, 0.4)",
	},
	"50%": {
		transform: "scale(1.05)",
		boxShadow: "0 0 0 10px rgba(58, 41, 255, 0.1)",
	},
	"100%": {
		transform: "scale(1)",
		boxShadow: "0 0 0 0 rgba(58, 41, 255, 0)",
	},
};

const useStyles = makeStyles({
	landingContainer: {
		height: "100vh",
		overflow: "hidden",
		paddingTop: "56px",
		position: "relative",
		scrollBehavior: "smooth",
	},
	heroSection: {
		height: "calc(100vh - 56px)",
		display: "flex",
		alignItems: "center",
		justifyContent: "center",
		padding: "60px 20px 80px",
		textAlign: "center",
		overflow: "hidden",
		transition:
			"opacity 0.5s cubic-bezier(0.4, 0, 0.2, 1), transform 0.5s cubic-bezier(0.4, 0, 0.2, 1)",
		position: "absolute",
		top: "56px",
		left: 0,
		right: 0,
		willChange: "transform, opacity",
	},
	heroContent: {
		maxWidth: "800px",
		margin: "0 auto",
	},
	mainHeading: {
		fontSize: tokens.fontSizeHero1000,
		fontWeight: tokens.fontWeightBold,
		lineHeight: 1.3,
		margin: "0 auto",
		marginBottom: "32px",
		maxWidth: "800px",
		textAlign: "center",
		color: tokens.colorBrandForeground1,
	},
	mainHeadingText: {
		textAlign: "center",
		display: "flex",
		justifyContent: "center",
		alignItems: "center",
		fontSize: tokens.fontSizeHero1000,
		fontWeight: tokens.fontWeightBold,
		lineHeight: 1.3,
		marginBottom: "48px",
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
	heroSubtext: {
		fontSize: tokens.fontSizeHero700,
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
	contentWrapper: {
		height: "calc(100vh - 112px)", // Adjusted height to account for new top position
		display: "flex",
		flexDirection: "column",
		transition:
			"opacity 0.5s cubic-bezier(0.4, 0, 0.2, 1), transform 0.5s cubic-bezier(0.4, 0, 0.2, 1)",
		position: "absolute",
		top: "112px", // Increased to 112px (56px header + 56px offset) to prevent header cutoff
		left: 0,
		right: 0,
		transform: "translateY(0)",
		willChange: "transform, opacity",
	},
	contentSection: {
		flex: "1",
		overflowY: "auto",
		position: "relative",
		// Completely hide scrollbars but keep functionality
		scrollbarWidth: "none", // Firefox
		msOverflowStyle: "none", // IE and Edge
		"&::-webkit-scrollbar": {
			display: "none", // Chrome, Safari, Opera
		},
	},
	contentMain: {
		padding: "80px 20px 80px",
		maxWidth: "1200px",
		margin: "0 auto",
		minHeight: "calc(100vh - 136px)", // Account for header (56px) and footer (60px) + padding
	},
	footerWrapper: {
		flexShrink: 0,
		marginTop: "auto",
	},
	footerFixed: {
		position: "fixed",
		bottom: 0,
		left: 0,
		right: 0,
		width: "100%",
		height: "60px",
		display: "flex",
		justifyContent: "center",
		alignItems: "flex-end",
		zIndex: 999,
		padding: 0,
		margin: 0,
	},

	downArrowButton: {
		position: "fixed",
		bottom: "40px",
		left: "40px",
		width: "60px",
		height: "60px",
		borderRadius: "50%",
		border: "none",
		backgroundColor: "rgba(255, 255, 255, 0.1)",
		backdropFilter: "blur(10px)",
		color: tokens.colorBrandForeground1,
		cursor: "pointer",
		display: "flex",
		alignItems: "center",
		justifyContent: "center",
		transition: "all 0.3s ease",
		animationName: throbKeyframes,
		animationDuration: "2s",
		animationTimingFunction: "ease-in-out",
		animationIterationCount: "infinite",
		zIndex: 1001,
		"&:hover": {
			backgroundColor: "rgba(255, 255, 255, 0.2)",
			transform: "scale(1.1)",
			animationPlayState: "paused",
		},
		"&:active": {
			transform: "scale(0.95)",
		},
	},
	upArrowButton: {
		position: "fixed",
		top: "96px",
		left: "40px",
		width: "60px",
		height: "60px",
		borderRadius: "50%",
		border: "none",
		backgroundColor: "rgba(255, 255, 255, 0.1)",
		backdropFilter: "blur(10px)",
		color: tokens.colorBrandForeground1,
		cursor: "pointer",
		display: "flex",
		alignItems: "center",
		justifyContent: "center",
		transition: "all 0.3s ease",
		animationName: throbKeyframes,
		animationDuration: "2s",
		animationTimingFunction: "ease-in-out",
		animationIterationCount: "infinite",
		zIndex: 1001,
		"&:hover": {
			backgroundColor: "rgba(255, 255, 255, 0.2)",
			transform: "scale(1.1)",
			animationPlayState: "paused",
		},
		"&:active": {
			transform: "scale(0.95)",
		},
	},
});

export { useStyles };
