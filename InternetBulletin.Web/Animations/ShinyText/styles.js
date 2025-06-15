import { makeStyles } from "@fluentui/react-components";

const useStyles = makeStyles({
    shinyText: {
        color: "#b5b5b5a4",
        background:
            "linear-gradient(120deg, rgba(255, 255, 255, 0) 40%, rgba(255, 255, 255, 0.8) 50%, rgba(255, 255, 255, 0) 60%)",
        backgroundSize: "200% 100%",
        WebkitBackgroundClip: "text",
        backgroundClip: "text",
        display: "inline-block",
        animationName: {
            from: { backgroundPosition: "100%" },
            to: { backgroundPosition: "-100%" },
        },
        animationDuration: "5s",
        animationTimingFunction: "linear",
        animationIterationCount: "infinite",
        "&.disabled": {
            animation: "none",
        },
    },
});

export { useStyles };
