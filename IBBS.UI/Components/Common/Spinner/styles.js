import { makeStyles, tokens } from "@fluentui/react-components";

const useStyles = makeStyles({
    spinnerOverlay: {
        position: "fixed",
        top: 0,
        left: 0,
        width: "100%",
        height: "100%",
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        zIndex: 9999,
        backgroundColor: tokens.colorNeutralBackgroundAlpha,
        backdropFilter: "blur(20px)",
    },
    spinnerContent: {
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
        gap: "24px",
        padding: "40px",
        borderRadius: "16px",
        backgroundColor: tokens.colorNeutralBackground1,
        boxShadow: tokens.shadow28,
        border: `1px solid ${tokens.colorNeutralStroke2}`,
    },
    spinnerLogo: {
        borderRadius: "12px",
        opacity: 0.9,
    },
    spinner: {
        color: tokens.colorBrandBackground,
    },
    loadingText: {
        margin: 0,
        fontSize: tokens.fontSizeBase300,
        color: tokens.colorNeutralForeground2,
        fontWeight: tokens.fontWeightMedium,
        textAlign: "center",
    },
});

export { useStyles };
