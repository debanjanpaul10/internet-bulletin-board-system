import { makeStyles, tokens } from "@fluentui/react-components";

const glowKeyframes = {
    "0%": {
        boxShadow:
            "0 0 20px rgba(58, 41, 255, 0.3), 0 0 40px rgba(255, 148, 180, 0.2)",
    },
    "50%": {
        boxShadow:
            "0 0 30px rgba(58, 41, 255, 0.5), 0 0 60px rgba(255, 148, 180, 0.3)",
    },
    "100%": {
        boxShadow:
            "0 0 20px rgba(58, 41, 255, 0.3), 0 0 40px rgba(255, 148, 180, 0.2)",
    },
};

const useStyles = makeStyles({
    noPostsContainer: {
        marginTop: "120px",
        marginBottom: "120px",
        textAlign: "center",
        display: "flex",
        flexDirection: "column",
        justifyContent: "center",
        alignItems: "center",
        padding: "40px 20px",
        maxWidth: "600px",
        margin: "120px auto",
    },
    emptyStateIcon: {
        fontSize: "80px",
        color: tokens.colorNeutralForeground3,
        marginBottom: "24px",
        opacity: 0.7,
    },
    noPostsHeading: {
        fontFamily: "",
        textAlign: "center",
        marginBottom: "16px",
        color: tokens.colorNeutralForeground1,
        lineHeight: 1.2,
    },
    noPostsSubtext: {
        color: tokens.colorNeutralForeground2,
        marginBottom: "32px",
        lineHeight: 1.5,
        maxWidth: "480px",
        justifyContent: "center",
        textAlign: "center",
    },
    actionButtons: {
        display: "flex",
        gap: "12px",
        flexWrap: "wrap",
        justifyContent: "center",
    },
    createPostButton: {
        minWidth: "220px",
        height: "56px",
        fontSize: tokens.fontSizeBase400,
        fontWeight: tokens.fontWeightSemibold,
        border: "none",
        borderRadius: "28px",
        background:
            "linear-gradient(135deg, #3A29FF 0%, #FF94B4 50%, #FF3232 100%)",
        backgroundSize: "200% 100%",
        color: "white",
        position: "relative",
        overflow: "hidden",
        cursor: "pointer",
        transition: "all 0.3s ease",
        backdropFilter: "blur(10px)",
        animationName: glowKeyframes,
        animationDuration: "2s",
        animationTimingFunction: "ease-in-out",
        animationIterationCount: "infinite",
        "&::before": {
            content: '""',
            position: "absolute",
            top: 0,
            left: 0,
            right: 0,
            bottom: 0,
            background:
                "linear-gradient(135deg, rgba(255,255,255,0.1) 0%, rgba(255,255,255,0.05) 100%)",
            borderRadius: "28px",
            pointerEvents: "none",
        },
        "&:hover": {
            transform: "translateY(-2px) scale(1.02)",
            backgroundPosition: "100% 0",
            animationPlayState: "paused",
            boxShadow:
                "0 8px 32px rgba(58, 41, 255, 0.4), 0 0 60px rgba(255, 148, 180, 0.3)",
        },
        "&:active": {
            transform: "translateY(0) scale(0.98)",
        },
        "& .fui-Button__icon": {
            fontSize: "20px",
            marginRight: "8px",
        },
    },
});

export { useStyles };
