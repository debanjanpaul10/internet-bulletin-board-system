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
import { useEffect, useState } from "react";
import { MyProfilePageConstants } from "@helpers/ibbs.constants";
import { SparkleCircle28Regular } from "@fluentui/react-icons";
import { formatDate } from "@helpers/common.utility";
import SpotlightCard from "@animations/SpotlightCard";
import { useStyles } from "./styles";

/**
 * @component
 * `UserRatingsComponent` displays a user's post ratings in a card-based table layout.
 * The component shows a list of posts that the user has rated, including the post name
 * and the date when the rating was given. Each post entry is displayed with a sparkle icon
 * and formatted date.
 *
 * @param props - Component props
 * @param props.userPostRatings - Array of user's post ratings
 *
 * @returns A card containing a table with the following features:
 *  - A header showing "Your Ratings"
 *  - A table with columns for post name and rating date
 *  - Each row displays a sparkle icon, post name, and formatted date
 *  - Scrollable content area for multiple ratings
 */
export default function UserRatingsComponent({
    userPostRatings,
}: {
    userPostRatings: any;
}) {
    const styles = useStyles();

    const [userPostRatingsState, setUserPostRatingsState] = useState([]);

    useEffect(() => {
        if (
            userPostRatings !== null &&
            userPostRatings !== undefined &&
            userPostRatings !== userPostRatingsState
        ) {
            setUserPostRatingsState(userPostRatings);
        }
    }, [userPostRatings]);

    return (
        <SpotlightCard
            className={`custom-spotlight-card ${styles.card}`}
            spotlightColor="rgba(0, 229, 255, 0.2)"
        >
            <Title1 className={styles.yourPostsHeader}>
                {MyProfilePageConstants.Headings.YourRatings}
            </Title1>
            <div className={styles.scrollableItems}>
                <Table
                    aria-label="User ratings"
                    className={styles.ratingsTable}
                    size="medium"
                >
                    <TableHeader className={styles.stickyHeader}>
                        <TableRow>
                            {MyProfilePageConstants.RatingsTableHeaders.map(
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
                    <TableBody className="row">
                        {Object.values(userPostRatingsState).length > 0 &&
                            userPostRatingsState.map(
                                (item: any, index: number) => (
                                    <TableRow key={index}>
                                        <TableCell className={styles.rowCell}>
                                            <TableCellLayout
                                                media={
                                                    <Text size={400}>
                                                        <SparkleCircle28Regular />
                                                    </Text>
                                                }
                                            >
                                                <Text size={400}>
                                                    {item.postName}
                                                </Text>
                                            </TableCellLayout>
                                        </TableCell>

                                        <TableCell className={styles.rowCell}>
                                            <TableCellLayout>
                                                <Text
                                                    size={400}
                                                    className={styles.dateText}
                                                >
                                                    {formatDate(item.ratedOn)}{" "}
                                                </Text>
                                            </TableCellLayout>
                                        </TableCell>
                                    </TableRow>
                                )
                            )}
                    </TableBody>
                </Table>
            </div>
        </SpotlightCard>
    );
}
