import { makeStyles, tokens } from "@fluentui/react-components";

export const useStyles = makeStyles({
    buttonOverride: {
        "&.fui-Button": {
            transition: "all 0.2s ease !important",
            ":hover": {
                backgroundColor: "rgba(255, 255, 255, 0.12) !important",
                boxShadow: "0 4px 15px rgba(255, 255, 255, 0.1) !important",
                border: "1px solid rgba(255, 255, 255, 0.2) !important",
                transform: "translateX(4px) !important",
            },
            ":active": {
                backgroundColor: "rgba(255, 255, 255, 0.18) !important",
                transform: "translateX(2px) !important",
            },
        },
    },
    content: {
        flex: "1",
        padding: "16px",
        display: "grid",
        justifyContent: "flex-start",
        alignItems: "flex-start",
        gridRowGap: tokens.spacingVerticalXXL,
        gridAutoRows: "max-content",
        height: "100%",
    },
    homeButton: {
        margin: "0px 5px !important",
        color: "white !important",
        fontWeight: tokens.fontWeightSemibold,
        transition: "all 0.2s ease !important",
        borderRadius: "8px !important",
        ":hover": {
            backgroundColor: "rgba(255, 255, 255, 0.15) !important",
            transform: "translateX(2px) !important",
            boxShadow: "0 2px 8px rgba(255, 255, 255, 0.1) !important",
        },
        ":active": {
            backgroundColor: "rgba(255, 255, 255, 0.2) !important",
            transform: "translateX(1px) !important",
        },
    },
    field: {
        display: "grid",
        gridRowGap: tokens.spacingVerticalS,
    },
    drawerHeader: {
        padding: "20px 16px",
        background:
            "linear-gradient(135deg, #3A29FF 0%, #FF94B4 50%, #FF3232 100%)",
        borderBottom: "none",
        backdropFilter: "blur(10px)",
        boxShadow: "0 4px 20px rgba(0, 0, 0, 0.1)",
    },
    drawerBody: {
        padding: "20px 16px",
        height: "calc(100% - 80px)",
        background:
            "linear-gradient(180deg, rgba(58, 41, 255, 0.05) 0%, rgba(255, 148, 180, 0.05) 50%, rgba(255, 50, 50, 0.05) 100%)",
        backdropFilter: "blur(20px)",
        overflow: "none",
    },
    createLink: {
        width: "100%",
    },
    button: {
        padding: "16px 20px !important",
        display: "flex !important",
        alignItems: "center !important",
        justifyContent: "flex-start !important",
        width: "100% !important",
        gap: "12px !important",
        borderRadius: "12px !important",
        margin: "4px 0 !important",
        backgroundColor: "rgba(255, 255, 255, 0.05) !important",
        backdropFilter: "blur(10px) !important",
        border: "1px solid rgba(255, 255, 255, 0.1) !important",
        color: `${tokens.colorNeutralForeground1} !important`,
        fontWeight: `${tokens.fontWeightMedium} !important`,
        transition: "all 0.2s ease !important",
        ":hover": {
            backgroundColor: "rgba(255, 255, 255, 0.12) !important",
            boxShadow: "0 4px 15px rgba(255, 255, 255, 0.1) !important",
            border: "1px solid rgba(255, 255, 255, 0.2) !important",
            transform: "translateX(4px) !important",
        },
        ":active": {
            backgroundColor: "rgba(255, 255, 255, 0.18) !important",
            transform: "translateX(2px) !important",
        },
        "& > svg": {
            color: `${tokens.colorBrandForeground1} !important`,
        },
    },
    signoutImg: {
        width: "28px !important",
        height: "28px !important",
        color: "#FF3232 !important",
    },
    closeButton: {
        borderRadius: "8px !important",
        transition: "all 0.2s ease !important",
        ":hover": {
            backgroundColor: "rgba(255, 255, 255, 0.2) !important",
            boxShadow: "0 2px 8px rgba(255, 255, 255, 0.15) !important",
            transform: "scale(1.05) !important",
        },
        ":active": {
            transform: "scale(0.98) !important",
        },
        "& > svg": {
            color: "white !important",
        },
        minWidth: "20px",
    },
});
