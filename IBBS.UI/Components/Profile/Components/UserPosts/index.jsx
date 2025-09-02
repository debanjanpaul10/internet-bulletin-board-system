import { useEffect, useState } from "react";
import {
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
import { formatDate } from "@helpers/common.utility";
import SpotlightCard from "@animations/SpotlightCard";

/**
 * @component
 * `UserPostsComponent` displays a table of user's posts with their titles, creation dates, and ratings.
 *
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

    return (
        <SpotlightCard
            className={`custom-spotlight-card ${styles.card}`}
            spotlightColor="rgba(0, 229, 255, 0.2)"
        >
            <Title1 className={styles.yourStoriesHeader}>
                {MyProfilePageConstants.Headings.YourPostsMessage}
            </Title1>
            <div className={styles.scrollableItems}>
                <Table
                    aria-label="User posts"
                    className={styles.postTable}
                    size="medium"
                >
                    <TableHeader className={styles.stickyHeader}>
                        <TableRow>
                            {MyProfilePageConstants.PostTableHeaders.map(
                                (item, index) => (
                                    <TableHeaderCell
                                        key={index}
                                        className={styles.rowCell}
                                    >
                                        <Text size={400}>{item}</Text>
                                    </TableHeaderCell>
                                )
                            )}
                        </TableRow>
                    </TableHeader>
                    <TableBody>
                        {Object.values(userPostsData).length > 0 &&
                            userPostsData?.map((item, index) => (
                                <TableRow key={index}>
                                    <TableCell className={styles.rowCell}>
                                        <TableCellLayout
                                            media={
                                                <Text size={400}>
                                                    <SparkleCircle28Regular />{" "}
                                                </Text>
                                            }
                                        >
                                            <Text size={400}>
                                                {item.postTitle}{" "}
                                            </Text>
                                        </TableCellLayout>
                                    </TableCell>

                                    <TableCell className={styles.rowCell}>
                                        <TableCellLayout>
                                            <Text
                                                size={400}
                                                className={styles.dateText}
                                            >
                                                {formatDate(
                                                    item.postCreatedDate
                                                )}{" "}
                                            </Text>
                                        </TableCellLayout>
                                    </TableCell>

                                    <TableCell className={styles.rowCell}>
                                        <TableCellLayout
                                            media={
                                                item.ratings > 0 ? (
                                                    <Star28Filled />
                                                ) : (
                                                    <Star28Regular />
                                                )
                                            }
                                        >
                                            <Text
                                                size={200}
                                                className={styles.dateText}
                                            >
                                                {item.ratings}
                                            </Text>
                                        </TableCellLayout>
                                    </TableCell>
                                </TableRow>
                            ))}
                    </TableBody>
                </Table>
            </div>
        </SpotlightCard>
    );
}

export default UserPostsComponent;
