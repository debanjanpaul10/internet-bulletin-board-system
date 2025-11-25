import { useEffect, useState } from "react";
import {
    Table,
    TableBody,
    TableCell,
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

import { MyProfilePageConstants } from "@helpers/ibbs.constants";
import { formatDate } from "@helpers/common.utility";
import SpotlightCard from "@animations/SpotlightCard";
import { useStyles } from "./styles";

/**
 * @component
 * `UserPostsComponent` displays a table of user's posts with their titles, creation dates, and ratings.
 *
 * @param props - Component props
 * @param {Array<Object>} props.userPosts - Array of user post objects
 */
export default function UserPostsComponent({ userPosts }: { userPosts: any }) {
    const styles = useStyles();

    const [userPostsData, setUserPostsData] = useState([]);

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
                {Object.values(userPostsData).length > 0 ? (
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
                                            className={styles.headerCell}
                                        >
                                            <Text size={300}>{item}</Text>
                                        </TableHeaderCell>
                                    )
                                )}
                            </TableRow>
                        </TableHeader>
                        <TableBody>
                            {userPostsData?.map((item: any, index: number) => (
                                <TableRow key={index} className={styles.tableRow}>
                                    <TableCell className={styles.rowCell}>
                                        <div className={styles.cellContent}>
                                            <SparkleCircle28Regular className={styles.cellIcon} />
                                            <Text size={400} style={{ color: "rgba(255, 255, 255, 0.95)" }}>
                                                {item.postTitle}
                                            </Text>
                                        </div>
                                    </TableCell>

                                    <TableCell className={styles.rowCell}>
                                        <Text
                                            size={400}
                                            className={styles.dateText}
                                        >
                                            {formatDate(item.postCreatedDate)}
                                        </Text>
                                    </TableCell>

                                    <TableCell className={styles.rowCell}>
                                        <div className={styles.cellContent}>
                                            {item.ratings > 0 ? (
                                                <Star28Filled className={styles.starIcon} style={{ color: "#FFD700" }} />
                                            ) : (
                                                <Star28Regular className={styles.starIcon} style={{ color: "rgba(255, 255, 255, 0.6)" }} />
                                            )}
                                            <Text
                                                size={200}
                                                className={styles.dateText}
                                                style={{ color: "rgba(255, 255, 255, 0.8)" }}
                                            >
                                                {item.ratings}
                                            </Text>
                                        </div>
                                    </TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                ) : (
                    <div className={styles.emptyState}>
                        <div className={styles.emptyStateIcon}>
                            <SparkleCircle28Regular />
                        </div>
                        <Text className={styles.emptyStateTitle}>
                            No Stories Yet
                        </Text>
                        <Text className={styles.emptyStateMessage}>
                            You haven't created any stories yet. Start sharing your thoughts with the community!
                        </Text>
                    </div>
                )}
            </div>
        </SpotlightCard>
    );
}
