import { makeStyles, tokens } from "@fluentui/react-components";

const useStyles = makeStyles({
    navbar: {
        padding: "8px 24px",
        backgroundColor: tokens.colorNeutralBackground1,
        backdropFilter: "blur(20px)",
        borderBottom: `1px solid ${tokens.colorNeutralStroke2}`,
        boxShadow: tokens.shadow8,
        minHeight: "56px",
        "@media (max-width: 768px)": {
            padding: "8px 16px",
        },
    },
    navContent: {
        display: "flex",
        width: "100%",
        alignItems: "center",
        height: "40px",
        position: "relative",
    },
    leftSection: {
        position: "absolute",
        left: "0",
        display: "flex",
        alignItems: "center",
        zIndex: 2,
    },
    centerSection: {
        position: "absolute",
        left: "50%",
        transform: "translateX(-50%)",
        display: "flex",
        alignItems: "center",
        justifyContent: "center",
        zIndex: 1,
    },
    rightSection: {
        position: "absolute",
        right: "0",
        display: "flex",
        alignItems: "center",
        zIndex: 2,
    },
    sideBarButton: {
        minWidth: "40px",
        borderRadius: "10px",
        backgroundColor: "transparent",
        border: `1px solid ${tokens.colorNeutralStroke2}`,
        transition: "all 0.2s ease",
        "&:hover": {
            backgroundColor: tokens.colorNeutralBackground1Hover,
            transform: "translateY(-1px)",
        },
    },
    homeButton: {
        display: "flex",
        alignItems: "center",
        gap: "8px",
        padding: "6px 12px",
        borderRadius: "10px",
        backgroundColor: "transparent",
        fontSize: tokens.fontSizeBase300,
        fontWeight: tokens.fontWeightSemibold,
        
        color: tokens.colorNeutralForeground1,
        transition: "all 0.2s ease",
        "&:hover": {
            backgroundColor: tokens.colorNeutralBackground1Hover,
            transform: "translateY(-1px)",
        },
        "& img": {
            borderRadius: "4px",
        },
    },
    bugButton: {
        minWidth: "40px",
        borderRadius: "10px",
        backgroundColor: "transparent",
        border: `1px solid ${tokens.colorNeutralStroke2}`,
        color: tokens.colorPaletteRedForeground1,
        transition: "all 0.2s ease",
        "&:hover": {
            backgroundColor: tokens.colorPaletteRedBackground1,
            transform: "translateY(-1px)",
        },
    },
});

export default useStyles;
