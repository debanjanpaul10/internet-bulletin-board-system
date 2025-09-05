import { makeStyles } from "@fluentui/react-components";

const useStyles = makeStyles({
    headerNav: {
        position: "fixed",
        top: 0,
        left: 0,
        right: 0,
        zIndex: 1000,
    },
    bodyContent: {
        overflow: "auto",
        minHeight: "calc(100vh - 80px)",
        position: "relative",
        zIndex: 1,
        paddingBottom: "100px",
    },
    footerContent: {
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
        padding: "0 20px 0 20px",
        margin: 0,
    },
});

export { useStyles };
