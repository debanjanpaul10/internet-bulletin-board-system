import { makeStyles } from "@fluentui/react-components";

const useStyles = makeStyles({
    dockOuter: {
        margin: "0 0.5rem",
        display: "flex",
        maxWidth: "100%",
        alignItems: "center",
    },

    dockPanel: {
        position: "absolute",
        bottom: "0.5rem",
        left: "50%",
        transform: "translateX(-50%)",
        display: "flex",
        alignItems: "flex-end",
        width: "fit-content",
        gap: "1rem",
        padding: "0 0.5rem 0.5rem",
    },

    dockItem: {
        position: "relative",
        display: "inline-flex",
        alignItems: "center",
        justifyContent: "center",
        borderRadius: "10px",
        cursor: "pointer",
        outline: "none",
    },

    dockIcon: {
        display: "flex",
        alignItems: "center",
        justifyContent: "center",
    },

    dockLabel: {
        position: "absolute",
        top: "-1.5rem",
        left: "50%",
        width: "fit-content",
        whiteSpace: "pre",
        borderRadius: "0.375rem",
        border: "1px solid #222",
        backgroundColor: "#060010",
        padding: "0.125rem 0.5rem",
        fontSize: "0.75rem",
        color: "#fff",
        transform: "translateX(-50%)",
    },
});

export { useStyles };
