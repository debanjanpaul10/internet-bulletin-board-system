import { useStyles } from "./styles";
import {
	Button,
	DrawerBody,
	DrawerHeader,
	DrawerHeaderTitle,
	OverlayDrawer,
	useRestoreFocusSource,
} from "@fluentui/react-components";
import { Dismiss28Filled } from "@fluentui/react-icons";

import { useAppDispatch, useAppSelector } from "@/index";
import { ToggleBugReportDrawer } from "@/Store/Common/Actions";

export default function BugReportComponent() {
	const dispatch = useAppDispatch();
	const styles = useStyles();
	const restoreFocusSourceAttributes = useRestoreFocusSource();

	const IsBugReportDrawerOpenStoreData = useAppSelector(
		(state: any) => state.CommonReducer.isBugReportDrawerOpen
	);

	const handleSideBarClose = () => {
		dispatch(ToggleBugReportDrawer(false));
	};

	return (
		<OverlayDrawer
			{...restoreFocusSourceAttributes}
			as="aside"
			open={IsBugReportDrawerOpenStoreData}
			onOpenChange={(_, { open }) => {
				dispatch(ToggleBugReportDrawer(open));
			}}
			size="medium"
			position="end"
		>
			<DrawerHeader className={styles.drawerHeader}>
				<DrawerHeaderTitle
					action={
						<Button
							appearance="subtle"
							aria-label="Close drawer"
							onClick={handleSideBarClose}
							className={styles.closeButton}
							icon={<Dismiss28Filled />}
						></Button>
					}
				></DrawerHeaderTitle>
			</DrawerHeader>

			<DrawerBody className={styles.drawerBody}></DrawerBody>
		</OverlayDrawer>
	);
}
