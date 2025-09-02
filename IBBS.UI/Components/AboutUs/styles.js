import { makeStyles, tokens } from "@fluentui/react-components";

const useStyles = makeStyles({
    aboutUsHeading: {
        fontFamily: "",
        textAlign: "center",
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        fontSize: tokens.fontSizeHero1000,
        lineHeight: 1.3,
        marginBottom: "24px",
        paddingTop: "20px",
        paddingBottom: "10px",
        overflow: "visible",
        color: tokens.colorNeutralForeground1,
        background:
            "linear-gradient(135deg, #3A29FF 0%, #FF94B4 50%, #FF3232 100%)",
        backgroundClip: "text",
        WebkitBackgroundClip: "text",
        WebkitTextFillColor: "transparent",
        MozBackgroundClip: "text",
        MozTextFillColor: "transparent",
        textShadow: "none",
        "@supports not (-webkit-background-clip: text)": {
            color: tokens.colorBrandForeground1,
            background: "none",
            WebkitTextFillColor: "initial",
        },
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
        marginTop: "0px",
        marginBottom: "32px",
        fontSize: tokens.fontSizeHero800,
        lineHeight: 1.2,
        color: tokens.colorNeutralForeground2,
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
