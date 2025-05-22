import { makeStyles } from "@fluentui/react-components";

const useStyles = makeStyles({
  headerNav: {
    position: "fixed",
    top: 0,
    left: 0,
    right: 0,
    zIndex: 1000,
  },
  bodyContent: {
    marginTop: "64px",
  },
});

export { useStyles };
