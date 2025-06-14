import { makeStyles, tokens } from "@fluentui/react-components";

const useStyles = makeStyles({
    mainHeading: {
        fontFamily: "Concert One",
        textAlign: "center",
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        fontSize: tokens.fontSizeHero1000,
        lineHeight: 1.0,
    },
});

export { useStyles };
