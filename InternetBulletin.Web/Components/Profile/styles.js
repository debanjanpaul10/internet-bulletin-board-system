import { tokens, makeStyles } from "@fluentui/react-components";

const useStyles = makeStyles({
  userDataSkeleton: {
    height: "200px",
    width: "100%",
    borderRadius: tokens.borderRadiusLarge,
  },
  userImgSkeleton: {
    width: "100%",
    height: "200px"
  },
  profileHeading: {
    fontFamily: "Architects Daughter",
    textAlign: "center",
    display: "flex",
    justifyContent: "center",
    alignItems: "center",
  },
  imageContainer: {
    flexShrink: 0,
  },
  profileImage: {
    width: "100%",
    height: "200px",
    borderRadius: "50%",
    objectFit: "cover",
  },
});

export { useStyles };
