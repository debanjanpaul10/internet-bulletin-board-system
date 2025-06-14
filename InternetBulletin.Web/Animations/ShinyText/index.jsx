import { useStyles } from "./styles";

const ShinyText = ({ text, disabled = false, speed = 5, className = "" }) => {
    const animationDuration = `${speed}s`;
    const styles = useStyles();

    return (
        <div
            className={`${styles.shinyText} ${
                disabled ? "disabled" : ""
            } ${className}`}
            style={{ animationDuration }}
        >
            {text}
        </div>
    );
};

export default ShinyText;
