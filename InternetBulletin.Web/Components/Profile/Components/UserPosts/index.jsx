import { useEffect, useState } from "react";
import {
  Card,
  Table,
  TableBody,
  TableCell,
  TableCellLayout,
  TableHeader,
  TableHeaderCell,
  TableRow,
  Text,
  Title1,
} from "@fluentui/react-components";
import {
  SparkleCircle28Regular,
  Star28Filled,
  Star28Regular,
} from "@fluentui/react-icons";

import { useStyles } from "./styles";
import { MyProfilePageConstants } from "@helpers/ibbs.constants";

function UserPostsComponent({ userPosts }) {
  const styles = useStyles();

  const [userPostsData, setUserPostsData] = useState({});

  useEffect(() => {
    if (
      userPosts !== null &&
      userPosts !== undefined &&
      Object.values(userPosts).length > 0 &&
      userPosts !== userPostsData
    ) {
      setUserPostsData(userPosts);
    }
  }, [userPosts]);

  /**
   * Formats the date to date string format.
   * @param {Date} date The unformatted date
   * @returns {string} The formatted date.
   */
  const formatDate = (date) => {
    return new Date(date).toDateString();
  };

  return (
    <Card className={styles.card} appearance="filled-alternative">
      <Title1 className={styles.yourStoriesHeader}>
        {MyProfilePageConstants.Headings.YourPostsMessage}
      </Title1>
      <Table aria-label="User posts" className={styles.postTable} size="medium">
        <TableHeader>
          {MyProfilePageConstants.PostTableHeaders.map((item, index) => (
            <TableHeaderCell key={index} className={styles.rowCell}>
              <Text size={400}>{item}</Text>
            </TableHeaderCell>
          ))}
        </TableHeader>
        <TableBody>
          {userPosts.map((item, index) => (
            <TableRow key={index}>
              <TableCell className={styles.rowCell}>
                <TableCellLayout
                  media={
                    <Text size={400}>
                      <SparkleCircle28Regular />{" "}
                    </Text>
                  }
                >
                  <Text size={400}>{item.postTitle} </Text>
                </TableCellLayout>
              </TableCell>

              <TableCell className={styles.rowCell}>
                <TableCellLayout>
                  <Text size={200} className={styles.dateText}>
                    {formatDate(item.postCreatedDate)}{" "}
                  </Text>
                </TableCellLayout>
              </TableCell>

              <TableCell className={styles.rowCell}>
                <TableCellLayout
                  media={
                    item.ratings > 0 ? <Star28Filled /> : <Star28Regular />
                  }
                >
                  <Text size={200} className={styles.dateText}>
                    {item.ratings}
                  </Text>
                </TableCellLayout>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </Card>
  );
}

export default UserPostsComponent;
