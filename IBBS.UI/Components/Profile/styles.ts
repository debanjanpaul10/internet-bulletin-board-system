import { tokens, makeStyles } from "@fluentui/react-components";

const useStyles = makeStyles({
    userDataSkeleton: {
        height: "200px",
        width: "100%",
        borderRadius: tokens.borderRadiusLarge,
    },
    userImgSkeleton: {
        width: "100%",
        height: "200px",
    },
    profileHeading: {
        fontFamily: "",
        textAlign: "center",
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        fontSize: tokens.fontSizeHero1000,
        lineHeight: 1.3,
        marginBottom: "48px",
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
    imageContainer: {
        display: "flex",
        alignItems: "center",
        justifyContent: "center",
        height: "100%",
        margin: "0 -1rem 1rem 1rem",
        "@media (min-width: 520px)": {
            marginBottom: "0",
        },
    },
    profileImage: {
        width: "100%",
        maxWidth: "200px",
        height: "200px",
        borderRadius: "50%",
        objectFit: "cover",
    },
    userDetailsContainer: {
        width: "100%",
        marginLeft: "1rem",
        display: "flex",
        justifyContent: "center",
        "@media (max-width: 520px)": {
            padding: "0",
            width: "100%",
            margin: "1rem 0 0 1rem",
        },
    },
    userPostsContainer: {
        width: "100%",
        padding: "0",
        "@media (max-width: 520px)": {
            width: "100%",
            padding: "0",
            marginTop: "1rem",
        },
    },
    userRatingsContainer: {
        width: "100%",
        padding: "0",
        "@media (max-width: 520px)": {
            width: "100%",
            padding: "0",
            marginTop: "1rem",
        },
    },
});

export { useStyles };
