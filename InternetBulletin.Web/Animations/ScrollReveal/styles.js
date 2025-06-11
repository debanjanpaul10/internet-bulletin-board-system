import { makeStyles } from "@fluentui/react-components";

const useStyles = makeStyles({
    scrollReveal: {
        margin: "20px 0",
    },
    scrollRevealText: {
        fontSize: "clamp(1.6rem, 4vw, 3rem)",
        lineHeight: "1.5",
        fontWeight: 600,
    },
    word: {
        display: "inline-block",
    },
});

export { useStyles };
