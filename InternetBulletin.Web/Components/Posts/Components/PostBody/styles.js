import {
	makeStyles,
	tokens,
} from "@fluentui/react-components";

const useStyles = makeStyles({
	card: {
		margin: "20px auto",
		width: "720px",
		maxWidth: "100%",
		padding: "20px",
		borderRadius: "8px",
		boxShadow: tokens.shadow28,
		backgroundColor: tokens.colorNeutralCardBackground,
		"&:hover": {
			backgroundColor: tokens.colorNeutralCardBackgroundHover
		}
	},
	cardHeader: {
		marginBottom: "10px",
	},
	cardPreview: {
		marginTop: "auto",
		padding: "10px 10px 0px 10px",
	},
	postContent: {
		fontSize: tokens.fontSizeBase300,
		lineHeight: "1.6",
		overflow: "hidden",
		textOverflow: "ellipsis",
	},
	textSizeButton: {
		marginTop: "10px",
		color: tokens.colorBrandForegroundOnLight,
		cursor: "pointer",
		fontSize: tokens.fontSizeBase300,
		textDecoration: "none",
	},
	headerContainer: {
		display: "flex",
		justifyContent: "space-between",
		alignItems: "center",
		width: "100%",
	},
	headerTitle: {
		flexGrow: 1,
	},
	headerButtons: {
		display: "flex",
		marginLeft: "auto",
	},
	editButton: {
		color: tokens.colorPaletteYellowBackground3,
		"&:hover": {
			color: tokens.colorPaletteYellowForeground2
		}
	},
	deleteButton: {
		color: tokens.colorPaletteRedBackground3,
		"&:hover": {
			color: tokens.colorPaletteRedForeground2
		}
	},
});

export { useStyles };
