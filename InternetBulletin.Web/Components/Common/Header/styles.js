import { makeStyles, tokens } from "@fluentui/react-components";

const useStyles = makeStyles({
    sideBarButton: {
        padding: "0px !important",
        "&:hover": {
            backgroundColor: tokens.colorPaletteSeafoamBackground2,
        },
    },
    bugButton: {
        padding: "0px !important",
        "&:hover": {
            backgroundColor: tokens.colorPaletteDarkOrangeBorderActive,
        },
        width: "40px",
        height: "40px",
        minWidth: "40px",
        backgroundColor: tokens.colorPaletteDarkOrangeBackground3,
    },
    navbar: {
        padding: "10px 15px",
        "@media (max-width: 768px)": {
            padding: "15px",
        },
        backgroundColor: tokens.colorPaletteSeafoamBackground2,
    },
    navContent: {
        display: "flex",
        width: "100%",
        alignItems: "center",
    },
    homeButton: {
        "&:hover": {
            backgroundColor: tokens.colorPaletteSeafoamBackground2,
        },
    },
});

export default useStyles;
