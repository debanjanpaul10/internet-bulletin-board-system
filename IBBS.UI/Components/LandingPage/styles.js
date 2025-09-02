import { makeStyles, tokens } from "@fluentui/react-components";

const waveGradientKeyframes = {
    "0%": {
        backgroundPosition: "0% 50%",
    },
    "50%": {
        backgroundPosition: "100% 50%",
    },
    "100%": {
        backgroundPosition: "0% 50%",
    },
};

const throbKeyframes = {
    "0%": {
        transform: "scale(1)",
        boxShadow: "0 0 0 0 rgba(58, 41, 255, 0.4)",
    },
    "50%": {
        transform: "scale(1.05)",
        boxShadow: "0 0 0 10px rgba(58, 41, 255, 0.1)",
    },
    "100%": {
        transform: "scale(1)",
        boxShadow: "0 0 0 0 rgba(58, 41, 255, 0)",
    },
};

const useStyles = makeStyles({
    landingContainer: {
        height: "100vh",
        overflow: "hidden", // Disable scrolling on main container
        paddingTop: "56px",
        position: "relative",
    },
    heroSection: {
        height: "calc(100vh - 56px)",
        display: "flex",
        alignItems: "center",
        justifyContent: "center",
        padding: "60px 20px 80px",
        textAlign: "center",
        overflow: "hidden",
    },
    heroContent: {
        maxWidth: "800px",
        margin: "0 auto",
    },
    mainHeading: {
        fontSize: tokens.fontSizeHero1000,
        fontWeight: tokens.fontWeightBold,
        lineHeight: 1.3,
        margin: "0 auto",
        marginBottom: "32px",
        maxWidth: "800px",
        textAlign: "center",
        color: tokens.colorBrandForeground1,
    },
    mainHeadingText: {
        fontFamily: "",
        textAlign: "center",
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        fontSize: tokens.fontSizeHero1000,
        fontWeight: tokens.fontWeightMedium,
        lineHeight: 1.3,
        marginBottom: "48px",
        paddingTop: "20px",
        paddingBottom: "10px",
        color: tokens.colorNeutralForeground1,
        background:
            "linear-gradient(135deg, #3A29FF 0%, #FF94B4 50%, #FF3232 100%)",
        backgroundClip: "text",
        WebkitBackgroundClip: "text",
        WebkitTextFillColor: "transparent",
        MozBackgroundClip: "text",
        MozTextFillColor: "transparent",
        textShadow: "none",
        overflow: "visible",
        "@supports not (-webkit-background-clip: text)": {
            color: tokens.colorBrandForeground1,
            background: "none",
            WebkitTextFillColor: "initial",
        },
    },
    heroSubtext: {
        fontSize: tokens.fontSizeHero700,
        lineHeight: 1.5,
        margin: "0 auto",
        maxWidth: "700px",
        fontWeight: tokens.fontWeightMedium,
        textAlign: "center",
        background:
            "linear-gradient(135deg, #1A1F71 0%, #8B5CF6 50%, #DC2626 100%)",
        backgroundSize: "200% 100%",
        backgroundClip: "text",
        WebkitBackgroundClip: "text",
        WebkitTextFillColor: "transparent",
        MozBackgroundClip: "text",
        MozTextFillColor: "transparent",
        animationName: waveGradientKeyframes,
        animationDuration: "3s",
        animationTimingFunction: "ease-in-out",
        animationIterationCount: "infinite",
        "@supports not (-webkit-background-clip: text)": {
            color: tokens.colorBrandForeground1,
            background: "none",
            WebkitTextFillColor: "initial",
            animationName: "none",
        },
    },
    contentSection: {
        height: "calc(100vh - 56px)",
        padding: "80px 20px 80px", // Added bottom padding to account for fixed footer
        maxWidth: "1200px",
        margin: "0 auto",
        overflowY: "auto",
        position: "relative",
        // Completely hide scrollbars but keep functionality
        scrollbarWidth: "none", // Firefox
        msOverflowStyle: "none", // IE and Edge
        "&::-webkit-scrollbar": {
            display: "none", // Chrome, Safari, Opera
        },
    },

    downArrowButton: {
        position: "fixed",
        bottom: "40px",
        left: "40px",
        width: "60px",
        height: "60px",
        borderRadius: "50%",
        border: "none",
        backgroundColor: "rgba(255, 255, 255, 0.1)",
        backdropFilter: "blur(10px)",
        color: tokens.colorBrandForeground1,
        cursor: "pointer",
        display: "flex",
        alignItems: "center",
        justifyContent: "center",
        transition: "all 0.3s ease",
        animationName: throbKeyframes,
        animationDuration: "2s",
        animationTimingFunction: "ease-in-out",
        animationIterationCount: "infinite",
        zIndex: 1001,
        "&:hover": {
            backgroundColor: "rgba(255, 255, 255, 0.2)",
            transform: "scale(1.1)",
            animationPlayState: "paused",
        },
        "&:active": {
            transform: "scale(0.95)",
        },
    },
    upArrowButton: {
        position: "fixed",
        top: "96px",
        left: "40px",
        width: "60px",
        height: "60px",
        borderRadius: "50%",
        border: "none",
        backgroundColor: "rgba(255, 255, 255, 0.1)",
        backdropFilter: "blur(10px)",
        color: tokens.colorBrandForeground1,
        cursor: "pointer",
        display: "flex",
        alignItems: "center",
        justifyContent: "center",
        transition: "all 0.3s ease",
        animationName: throbKeyframes,
        animationDuration: "2s",
        animationTimingFunction: "ease-in-out",
        animationIterationCount: "infinite",
        zIndex: 1001,
        "&:hover": {
            backgroundColor: "rgba(255, 255, 255, 0.2)",
            transform: "scale(1.1)",
            animationPlayState: "paused",
        },
        "&:active": {
            transform: "scale(0.95)",
        },
    },
});

export { useStyles };
