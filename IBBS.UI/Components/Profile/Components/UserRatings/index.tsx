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
				{Object.values(userPostRatingsState).length > 0 ? (
					<Table
						aria-label="User ratings"
						className={styles.ratingsTable}
						size="medium"
					>
						<TableHeader className={styles.stickyHeader}>
							<TableRow>
								<TableHeaderCell className={styles.headerCell}>
									<Text size={300}>
										{
											MyProfilePageConstants
												.RatingsTableHeaders[0]
										}
									</Text>
								</TableHeaderCell>

								<TableHeaderCell className={styles.headerCellR}>
									<Text size={300}>
										{
											MyProfilePageConstants
												.RatingsTableHeaders[1]
										}
									</Text>
								</TableHeaderCell>
							</TableRow>
						</TableHeader>
						<TableBody className="row">
							{userPostRatingsState.map(
								(item: any, index: number) => (
									<TableRow
										key={index}
										className={styles.tableRow}
									>
										<TableCell className={styles.rowCell}>
											<div className={styles.cellContent}>
												<SparkleCircle28Regular
													className={styles.cellIcon}
												/>
												<Text
													size={400}
													style={{
														color: "rgba(255, 255, 255, 0.95)",
													}}
												>
													{item.postName}
												</Text>
											</div>
										</TableCell>

										<TableCell className={styles.rowCellR}>
											<Text
												size={400}
												className={styles.dateText}
												style={{
													color: "rgba(255, 255, 255, 0.8)",
												}}
											>
												{formatDate(item.ratedOn)}
											</Text>
										</TableCell>
									</TableRow>
								)
							)}
						</TableBody>
					</Table>
				) : (
					<div className={styles.emptyState}>
						<div className={styles.emptyStateIcon}>
							<SparkleCircle28Regular />
						</div>
						<Text className={styles.emptyStateTitle}>
							No Ratings Yet
						</Text>
						<Text className={styles.emptyStateMessage}>
							You haven't rated any stories yet. Explore the
							community and share your thoughts!
						</Text>
					</div>
				)}
			</div>
		</SpotlightCard>
	);
}
