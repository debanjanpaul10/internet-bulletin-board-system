import { makeStyles, tokens } from "@fluentui/react-components";

const useStyles = makeStyles({
	card: {
		maxWidth: "100%",
		margin: "0",
		height: "400px",
		display: "flex",
		flexDirection: "column",
		borderRadius: "16px",
		backgroundColor: "rgba(255, 255, 255, 0.15)",
		backdropFilter: "blur(20px)",
		WebkitBackdropFilter: "blur(20px)",
		border: "1px solid rgba(255, 255, 255, 0.2)",
		boxShadow: `
            0 8px 32px rgba(0, 0, 0, 0.1),
            0 4px 16px rgba(0, 0, 0, 0.05),
            inset 0 1px 0 rgba(255, 255, 255, 0.1)
        `,
		transition: "all 0.3s ease",
		"&:hover": {
			transform: "translateY(-4px)",
			backgroundColor: "rgba(255, 255, 255, 0.2)",
			border: "1px solid rgba(255, 255, 255, 0.3)",
			boxShadow: `
                0 12px 40px rgba(0, 0, 0, 0.15),
                0 6px 20px rgba(0, 0, 0, 0.08),
                inset 0 1px 0 rgba(255, 255, 255, 0.15)
            `,
		},
	},
	scrollableItems: {
		flex: 1,
		overflowY: "auto",
		padding: "0 0 16px 0",
		"&::-webkit-scrollbar": {
			width: "6px",
		},
		"&::-webkit-scrollbar-track": {
			background: "rgba(255, 255, 255, 0.1)",
			borderRadius: "3px",
		},
		"&::-webkit-scrollbar-thumb": {
			background: "rgba(255, 255, 255, 0.3)",
			borderRadius: "3px",
			"&:hover": {
				background: "rgba(255, 255, 255, 0.5)",
			},
		},
		width: "100%",
	},
	postTable: {
		width: "100%",
		borderCollapse: "separate",
		borderSpacing: 0,
		tableLayout: "fixed",
		borderRadius: "8px",
		overflow: "hidden",
	},
	tableHeader: {
		position: "sticky",
		top: 0,
		zIndex: 1,
	},
	dateText: {
		color: "rgba(255, 255, 255, 0.8)",
		fontSize: tokens.fontSizeBase200,
	},
	yourStoriesHeader: {
		fontFamily: "",
		textAlign: "center",
		display: "flex",
		justifyContent: "center",
		alignItems: "center",
		marginBottom: "20px",
		padding: "12px 0 0 0",
		fontSize: tokens.fontSizeBase600,
		fontWeight: tokens.fontWeightSemibold,
		color: "rgba(255, 255, 255, 0.95)",
		textShadow: "0 1px 2px rgba(0, 0, 0, 0.3)",
	},
	rowCell: {
		padding: "16px 0",
		verticalAlign: "middle",
		"&:first-child": {
			paddingLeft: "16px",
		},
		"&:last-child": {
			paddingRight: "16px",
			borderBottom: "none",
		},
		borderBottom: "none",
	},
	stickyHeader: {
		position: "sticky",
		top: 0,
		zIndex: 1,
		backgroundColor: "rgba(255, 255, 255, 0.12)",
		backdropFilter: "blur(15px)",
		borderBottom: "2px solid rgba(255, 255, 255, 0.15)",
		borderRadius: "8px 8px 0 0",
	},
	headerCell: {
		padding: "16px 0",
		backgroundColor: "transparent",
		borderBottom: "none",
		fontWeight: tokens.fontWeightSemibold,
		fontSize: tokens.fontSizeBase300,
		color: "rgba(255, 255, 255, 0.9)",
		textShadow: "0 1px 2px rgba(0, 0, 0, 0.3)",
		"&:first-child": {
			paddingLeft: "16px",
		},
		"&:last-child": {
			paddingRight: "16px",
		},
	},
	tableRow: {
		transition: "background-color 0.2s ease",
		"&:hover": {
			backgroundColor: "rgba(255, 255, 255, 0.05)",
		},
		"&:last-child": {
			borderBottom: "none",
		},
		borderBottom: "none",
	},
	cellContent: {
		display: "flex",
		alignItems: "center",
		gap: "8px",
		width: "100%",
		borderBottom: "none",
	},
	cellIcon: {
		fontSize: "16px",
		flexShrink: 0,
	},
	starIcon: {
		fontSize: "16px",
		flexShrink: 0,
	},
	emptyState: {
		display: "flex",
		flexDirection: "column",
		alignItems: "center",
		justifyContent: "center",
		height: "100%",
		padding: "40px 20px",
		textAlign: "center",
	},
	emptyStateIcon: {
		fontSize: "48px",
		color: "rgba(255, 255, 255, 0.4)",
		marginBottom: "16px",
		opacity: 0.6,
	},
	emptyStateTitle: {
		fontSize: tokens.fontSizeBase500,
		fontWeight: tokens.fontWeightSemibold,
		color: "rgba(255, 255, 255, 0.9)",
		marginBottom: "8px",
		textShadow: "0 1px 2px rgba(0, 0, 0, 0.3)",
	},
	emptyStateMessage: {
		fontSize: tokens.fontSizeBase300,
		color: "rgba(255, 255, 255, 0.7)",
		lineHeight: 1.5,
		maxWidth: "280px",
		textShadow: "0 1px 2px rgba(0, 0, 0, 0.2)",
	},
});

export { useStyles };
