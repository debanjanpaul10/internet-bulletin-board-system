import { makeStyles, tokens } from "@fluentui/react-components";

const useStyles = makeStyles({
	card: {
		maxWidth: "100%",
		margin: "1rem",
		height: "250px",
		display: "flex",
		flexDirection: "column",
		boxShadow: tokens.shadow28,
		backgroundColor: tokens.colorNeutralCardBackground,
		"&:hover": {
			backgroundColor: tokens.colorNeutralCardBackgroundHover,
		},
	},
	scrollableItems: {
		flex: 1,
		overflowY: "auto",
		"&::-webkit-scrollbar": {
			width: "8px",
		},
		"&::-webkit-scrollbar-track": {
			background: "var(--colorNeutralBackground1)",
		},
		"&::-webkit-scrollbar-thumb": {
			background: "var(--colorNeutralForeground3)",
			borderRadius: "4px",
			"&:hover": {
				background: "var(--colorNeutralForeground2)",
			},
		},
		width: "100%",
	},
	postTable: {
		width: "100%",
		borderCollapse: "separate",
		borderSpacing: 0,
		tableLayout: "fixed",
	},
	tableHeader: {
		position: "sticky",
		top: 0,
		zIndex: 1,
	},
	dateText: {
		marginLeft: "auto",
		marginTop: "10px",
		paddingRight: "20px",
	},
	yourStoriesHeader: {
		fontFamily: "Architects Daughter",
		textAlign: "center",
		display: "flex",
		justifyContent: "center",
		alignItems: "center",
		marginBottom: "10px",
	},
	rowCell: {
		padding: "10px",
	},
	stickyHeader: {
		position: "sticky",
		top: 0,
		zIndex: 1,
		backgroundColor: tokens.colorNeutralCardBackgroundPressed,
	},
});

export { useStyles };
