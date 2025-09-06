import { tokens, makeStyles } from "@fluentui/react-components";

const useStyles = makeStyles({
	profileContainer: {
		minHeight: "100vh",
		paddingTop: "76px",
		position: "relative",
		overflow: "hidden",
	},
	heroSection: {
		display: "flex",
		alignItems: "center",
		justifyContent: "center",
		minHeight: "60vh",
		padding: "60px 20px",
		textAlign: "center",
		position: "relative",
	},
	heroContent: {
		maxWidth: "800px",
		margin: "0 auto",
		display: "flex",
		flexDirection: "column",
		alignItems: "center",
		gap: "40px",
	},
	profileHeading: {
		fontFamily: "",
		textAlign: "center",
		display: "flex",
		justifyContent: "center",
		alignItems: "center",
		fontSize: tokens.fontSizeHero1000,
		lineHeight: 1.3,
		margin: "0",
		overflow: "visible",
		color: tokens.colorNeutralForeground1,
		background:
			"linear-gradient(135deg, #3A29FF 0%, #FF94B4 50%, #FF3232 100%)",
		backgroundClip: "text",
		WebkitBackgroundClip: "text",
		WebkitTextFillColor: "transparent",
		MozBackgroundClip: "text",
		MozTextFillColor: "transparent",
		textShadow: "none",
		"@supports not (-webkit-background-clip: text)": {
			color: tokens.colorBrandForeground1,
			background: "none",
			WebkitTextFillColor: "initial",
		},
	},
	profileImageSection: {
		display: "flex",
		alignItems: "center",
		justifyContent: "center",
		position: "relative",
	},
	profileImageWrapper: {
		position: "relative",
		display: "flex",
		alignItems: "center",
		justifyContent: "center",
	},
	profileImageContainer: {
		position: "relative",
		width: "200px",
		height: "200px",
		borderRadius: "50%",
		overflow: "hidden",
		border: "3px solid rgba(255, 255, 255, 0.2)",
		boxShadow: `
            0 8px 32px rgba(0, 0, 0, 0.3),
            0 4px 16px rgba(0, 0, 0, 0.2),
            inset 0 1px 0 rgba(255, 255, 255, 0.1)
        `,
		transition: "all 0.3s ease",
		"&:hover": {
			transform: "scale(1.05)",
			border: "3px solid rgba(255, 255, 255, 0.4)",
			boxShadow: `
                0 12px 40px rgba(0, 0, 0, 0.4),
                0 6px 20px rgba(0, 0, 0, 0.3),
                inset 0 1px 0 rgba(255, 255, 255, 0.2)
            `,
		},
	},
	profileImage: {
		width: "100%",
		height: "100%",
		objectFit: "cover",
		borderRadius: "50%",
		position: "relative",
		zIndex: 2,
	},
	profileImageGlow: {
		position: "absolute",
		top: "-10px",
		left: "-10px",
		right: "-10px",
		bottom: "-10px",
		borderRadius: "50%",
		background:
			"linear-gradient(135deg, #3A29FF 0%, #FF94B4 50%, #FF3232 100%)",
		opacity: 0.3,
		filter: "blur(20px)",
		zIndex: 1,
		animation: "pulse 2s infinite",
	},
	contentSection: {
		padding: "40px 20px",
		maxWidth: "1200px",
		margin: "0 auto",
	},
	cardsGrid: {
		display: "grid",
		gridTemplateColumns: "repeat(auto-fit, minmax(400px, 1fr))",
		gap: "32px",
		marginBottom: "60px",
		animation: "fadeInUp 0.6s ease-out",
	},
	cardWrapper: {
		width: "100%",
	},
	cardSkeleton: {
		width: "100%",
		height: "400px",
	},
	userDataSkeleton: {
		height: "400px",
		width: "100%",
		borderRadius: "16px",
		backgroundColor: "rgba(255, 255, 255, 0.1)",
		backdropFilter: "blur(20px)",
		border: "1px solid rgba(255, 255, 255, 0.2)",
	},
	userImgSkeleton: {
		width: "200px",
		height: "200px",
		borderRadius: "50%",
	},
	profileImageSkeleton: {
		display: "flex",
		alignItems: "center",
		justifyContent: "center",
	},
	cardsGrid768: {
		"@media screen and (max-width: 768px)": {
			gridTemplateColumns: "1fr",
			gap: "24px",
			marginBottom: "40px",
		},
	},
	cardsGrid480: {
		"@media screen and (max-width: 480px)": {
			gridTemplateColumns: "1fr",
			gap: "20px",
		},
	},
	profileHeading768: {
		"@media screen and (max-width: 768px)": {
			fontSize: "2.5rem",
		},
	},
	profileHeading480: {
		"@media screen and (max-width: 480px)": {
			fontSize: "2rem",
		},
	},
	heroSection768: {
		"@media screen and (max-width: 768px)": {
			minHeight: "50vh",
			padding: "40px 20px",
		},
	},
	heroContent768: {
		"@media screen and (max-width: 768px)": {
			gap: "30px",
		},
	},
	profileImageContainer768: {
		"@media screen and (max-width: 768px)": {
			width: "150px",
			height: "150px",
		},
	},
	profileImageContainer480: {
		"@media screen and (max-width: 480px)": {
			width: "120px",
			height: "120px",
		},
	},
	contentSection480: {
		"@media screen and (max-width: 480px)": {
			padding: "20px 16px",
		},
	},
});

export { useStyles };
