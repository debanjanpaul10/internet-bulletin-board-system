import { tokens, makeStyles } from "@fluentui/react-components";

const useStyles = makeStyles({
	userDataSkeleton: {
		height: "200px",
		width: "100%",
		borderRadius: tokens.borderRadiusLarge,
	},
	userImgSkeleton: {
		width: "100%",
		height: "200px",
	},
	profileHeading: {
		fontFamily: "Architects Daughter",
		textAlign: "center",
		display: "flex",
		justifyContent: "center",
		alignItems: "center",
	},
	imageContainer: {
		display: "flex",
		alignItems: "center",
		justifyContent: "center",
		height: "100%",
		marginBottom: "1rem",
		"@media (min-width: 520px)": {
			marginBottom: "0",
		},
	},
	profileImage: {
		width: "100%",
		maxWidth: "200px",
		height: "200px",
		borderRadius: "50%",
		objectFit: "cover",
	},
	userDetailsContainer: {
		width: "100%",
		padding: "0 1rem",
		"@media (max-width: 520px)": {
			padding: "0",
			width: "100%",
			margin: "1rem 0px 0px 1rem",
		},
	},
	userPostsContainer: {
		width: "100%",
		padding: "0 1rem",
		"@media (max-width: 520px)": {
			width: "100%",
			padding: "0",
			marginTop: "1rem",
		},
	},
});

export { useStyles };
