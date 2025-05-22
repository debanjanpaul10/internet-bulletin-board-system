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

/**
 * UserPostsComponent displays a table of user's posts with their titles, creation dates, and ratings.
 * 
 * @component
 * @param {Object} props - Component props
 * @param {Array<Object>} props.userPosts - Array of user post objects
 * @param {string} props.userPosts[].postTitle - Title of the post
 * @param {string|Date} props.userPosts[].postCreatedDate - Creation date of the post
 * @param {number} props.userPosts[].ratings - Number of ratings for the post
 * 
 * @example
 * const userPosts = [
 *   {
 *     postTitle: "My First Post",
 *     postCreatedDate: "2024-03-20",
 *     ratings: 5
 *   }
 * ];
 * return <UserPostsComponent userPosts={userPosts} />;
 */
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
