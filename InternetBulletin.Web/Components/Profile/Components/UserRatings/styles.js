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
	yourPostsHeader: {
		fontFamily: "Architects Daughter",
		textAlign: "center",
		display: "flex",
		justifyContent: "center",
		alignItems: "center",
		marginBottom: "10px",
	},
	ratingsTable: {
		width: "100%",
		borderCollapse: "separate",
		borderSpacing: 0,
		tableLayout: "fixed",
	},
	stickyHeader: {
		position: "sticky",
		top: 0,
		backgroundColor: tokens.colorNeutralCardBackgroundPressed,
		zIndex: 1,
	},
	rowCell: {
		padding: "10px",
	},
	dateText: {
		marginLeft: "auto",
		marginTop: "10px",
		paddingRight: "20px",
	},
});

export { useStyles };
