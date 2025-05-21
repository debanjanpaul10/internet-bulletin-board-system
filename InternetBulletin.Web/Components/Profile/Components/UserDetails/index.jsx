import { useEffect, useState } from "react";
import { Card, CardHeader, Text, tokens } from "@fluentui/react-components";

import { useStyles } from "./styles";

function UserDetailsComponent({ displayName, emailAddress, userName }) {
  const styles = useStyles();

  const [userDetailsData, setUserDetailsData] = useState({
    displayName: "",
    emailAddress: "",
    userName: "",
  });

  useEffect(() => {
    if (displayName !== "" && userDetailsData.displayName !== displayName) {
      setUserDetailsData((prevState) => ({
        ...prevState,
        displayName: displayName,
      }));
    }

    if (emailAddress !== "" && userDetailsData.emailAddress !== emailAddress) {
      setUserDetailsData((prevState) => ({
        ...prevState,
        emailAddress: emailAddress,
      }));
    }

    if (userName !== "" && userDetailsData.userName !== userName) {
      setUserDetailsData((prevState) => ({
        ...prevState,
        userName: userName,
      }));
    }
  }, [displayName, emailAddress, userName]);

  return (
    <Card className={styles.card}>
      <CardHeader
        header={
          <div className={styles.detailsContainer}>
            <div className={styles.profileSection}>
              <div className={styles.userInfo}>
                <div className={styles.detailRow}>
                  <Text size={400}>Name: </Text>
                  <Text size={400} weight={tokens.fontWeightSemibold}>
                    {userDetailsData.displayName}
                  </Text>
                </div>
                <div className={styles.detailRow}>
                  <Text size={400}>Email: </Text>
                  <Text size={400} weight={tokens.fontWeightSemibold}>
                    {userDetailsData.emailAddress}
                  </Text>
                </div>
                <div className={styles.detailRow}>
                  <Text size={400}>Username: </Text>
                  <Text size={400} weight={tokens.fontWeightSemibold}>
                    {userDetailsData.userName}
                  </Text>
                </div>
              </div>
            </div>
          </div>
        }
      />
    </Card>
  );
}

export default UserDetailsComponent;
