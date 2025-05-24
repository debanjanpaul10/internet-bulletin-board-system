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
import { useEffect, useState } from "react";
import { useStyles } from "./styles";
import { MyProfilePageConstants } from "@helpers/ibbs.constants";
import { SparkleCircle28Regular } from "@fluentui/react-icons";
import { formatDate } from "@helpers/common.utility";

function UserRatingsComponent({ userPostRatings }) {
	const styles = useStyles();

	const [userPostRatingsState, setUserPostRatingsState] = useState({});

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
		<Card className={styles.card} appearance="filled-alternative">
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
							userPostRatingsState.map((item, index) => (
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
							))}
					</TableBody>
				</Table>
			</div>
		</Card>
	);
}

export default UserRatingsComponent;
