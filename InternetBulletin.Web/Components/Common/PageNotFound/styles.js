import { makeStyles, tokens } from "@fluentui/react-components";

const useStyles = makeStyles({
    notFoundHeader: {
        fontFamily: "Concert One",
        textAlign: "center",
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        fontSize: tokens.fontSizeHero1000,
        lineHeight: 1.0,
        position: "absolute",
        top: "50%",
        left: "50%",
        transform: "translate(-50%, -50%)",
        width: "100%",
        marginTop: 0,
        paddingLeft: "100px",
    },
    noiseDiv: {
        position: "absolute",
        left: 0,
        top: 0,
        width: "100%",
        height: "100vh",
        overflow: "hidden",
    },
});

export { useStyles };
