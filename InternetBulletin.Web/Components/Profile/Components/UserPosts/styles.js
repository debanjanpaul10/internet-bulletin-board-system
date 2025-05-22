import { makeStyles } from "@fluentui/react-components";

const useStyles = makeStyles({
  card: {
    maxWidth: "100%",
    margin: "1rem",
    height: "300px",
    overflow: "auto",
  },
  postTable: {
    width: "100%",
  },
  dateText: {
    marginLeft: "auto",
    marginTop: "10px",
    paddingRight: "20px",
  },
  yourStoriesHeader: {
    fontFamily: "Architects Daughter",
    textAlign: "center",
    display: "flex",
    justifyContent: "center",
    alignItems: "center",
    marginBottom: "10px",
  },
  rowCell: {
    padding: "10px",
  },
});

export { useStyles };
