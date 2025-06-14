import { makeStyles } from "@fluentui/react-components";

const useStyles = makeStyles({
    auroraContainer: {
        position: "fixed",
        top: 0,
        left: 0,
        width: "100vw",
        height: "100vh",
        zIndex: 0,
        pointerEvents: "none",
    },
});

export { useStyles };
