import { makeStyles, tokens } from "@fluentui/react-components";

const useStyles = makeStyles({
    aboutUsHeading: {
        fontFamily: "Concert One",
        textAlign: "center",
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        fontSize: tokens.fontSizeHero1000,
        lineHeight: 1.0,
    },
    carouselNavButton: {
        backgroundColor: "none",
        marginTop: "-15px",
    },
    subHeading: {
        textAlign: "center",
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        marginTop: "10px",
    },
    container: {
        maxHeight: "90vh",
        overflow: "hidden",
    },
    carouselContainer: {
        maxHeight: "60vh",
        overflow: "hidden",
    },
    aboutUsContainer: {
        height: "100vh",
        overflow: "auto",
        display: "flex",
        flexDirection: "column",
    },
    heart: {
        color: tokens.colorStatusDangerBackground3,
    },
});

export { useStyles };
