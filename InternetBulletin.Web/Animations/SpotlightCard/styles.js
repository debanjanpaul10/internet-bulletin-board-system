import { makeStyles, tokens } from "@fluentui/react-components";

const useStyles = makeStyles({
    cardSpotlight: {
        position: "relative",
        borderRadius: "1.5rem",
        border: "1px solid #222",
        backgroundColor: tokens.colorNeutralCardBackground,
        padding: "2rem",
        overflow: "hidden",
        "--mouse-x": "50%",
        "--mouse-y": "50%",
        "--spotlight-color": "rgba(255, 255, 255, 0.05)",
        minHeight: "100%",
        "&:before": {
            position: "absolute",
            top: 0,
            left: 0,
            right: 0,
            bottom: 0,
            background:
                "radial-gradient(circle at var(--mouse-x) var(--mouse-y), var(--spotlight-color), transparent 80%)",
            opacity: 0,
            transition: "opacity 0.5s ease",
            pointerEvents: "none",
            content: '""', // Added to ensure the pseudo-element renders
        },
        "&:hover::before, &:focus-within::before": {
            opacity: 0.6,
        },
    },
});

export { useStyles };
