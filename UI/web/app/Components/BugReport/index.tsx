import { useEffect, useState } from "react";
import {
	Button,
	DrawerBody,
	DrawerHeader,
	DrawerHeaderTitle,
	OverlayDrawer,
	useRestoreFocusSource,
	Label,
} from "@fluentui/react-components";
import { AgentsColor, Dismiss28Filled } from "@fluentui/react-icons";
import { useAuth0 } from "@auth0/auth0-react";

import { useStyles } from "./styles";
import { useAppDispatch, useAppSelector } from "@/index";
import {
	SubmitBugReportDataAsync,
	ToggleBugReportDrawer,
} from "@store/Common/Actions";
import { BugReportConstants } from "@helpers/ibbs.constants";
import { BugReportDTO } from "@models/DTOs/bug-report-data.dto";
import { DrawerMotion } from "./motion";
import Spinner from "../Common/Spinner";
import { GetBugSeverityStatusAsync } from "@store/AiServices/Actions";
import { BugSeverityAIRequestDTO } from "@models/DTOs/bug-severity-ai-request.dto";

export default function BugReportComponent() {
	const dispatch = useAppDispatch();
	const styles = useStyles();
	const restoreFocusSourceAttributes = useRestoreFocusSource();
	const { getIdTokenClaims } = useAuth0();

	const IsBugReportDrawerOpenStoreData = useAppSelector(
		(state: any) => state.CommonReducer.isBugReportDrawerOpen
	);
	const LookupMasterStoreData = useAppSelector(
		(state) => state.CommonReducer.lookupMasterData
	);
	const IsBugReportSpinnerLoadingStoreData = useAppSelector(
		(state) => state.CommonReducer.isBugReportSpinnerLoading
	);
	const BugSeverityStatusFromAi = useAppSelector(
		(state) => state.AiServicesReducer.bugSeverityStatus
	);

	const [bugReport, setBugReport] = useState<BugReportDTO>({
		bugDescription: "",
		bugSeverity: 4,
		bugStatus: 1,
		bugTitle: "",
		createdBy: "",
		pageUrl: window.location.origin,
	});

	const [errors, setErrors] = useState({
		title: "",
		description: "",
	});
	const [bugSeverity, setBugSeverity] = useState("");

	useEffect(() => {
		runValidationChecks();
	}, [bugReport]);

	useEffect(() => {
		if (BugSeverityStatusFromAi !== "") {
			setBugSeverity(BugSeverityStatusFromAi);
			const severityItem = LookupMasterStoreData.find(
				(item: any) =>
					item.type === "BugSeverity" &&
					item.keyName.toLowerCase() ===
						BugSeverityStatusFromAi.toLowerCase()
			);
			if (severityItem) {
				setBugReport((prev) => ({
					...prev,
					bugSeverity: Number(severityItem.keyValue),
				}));
			}
		}
	}, [BugSeverityStatusFromAi, LookupMasterStoreData]);

	const getAccessToken = async () => {
		try {
			const idToken = await getIdTokenClaims();
			return idToken?.__raw;
		} catch (error) {
			console.error(error);
			return null;
		}
	};

	const runValidationChecks = () => {
		if (bugReport.bugDescription.trim().length < 10) {
			setErrors((prevState: any) => ({
				...prevState,
				description:
					"The bug description must be more than 10 characters",
			}));
		} else if (bugReport.bugDescription.trim().length >= 10) {
			setErrors((prevState: any) => ({
				...prevState,
				description: "",
			}));
		}

		if (bugReport.bugTitle.trim().length < 3) {
			setErrors((prevState: any) => ({
				...prevState,
				title: "The bug title must be more than 3 characters",
			}));
		} else if (bugReport.bugTitle.trim().length >= 3) {
			setErrors((prevState: any) => ({
				...prevState,
				title: "",
			}));
		}
	};

	const handleSideBarClose = () => {
		dispatch(ToggleBugReportDrawer(false));
	};

	const handleInputChange = (
		e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
	) => {
		const { name, value } = e.target;
		setBugReport({ ...bugReport, [name]: value });
	};

	const handleSeverityChange = (e: any) => {
		setBugReport({ ...bugReport, bugSeverity: Number(e.target.value) });
	};

	const handleSubmit = async () => {
		if (
			errors.description.trim().length === 0 &&
			errors.title.trim().length === 0
		) {
			const accessToken = await getAccessToken();
			bugReport.bugStatus = Number(
				LookupMasterStoreData.filter(
					(item: any) =>
						item.type === "BugStatus" &&
						item.keyName === "Not Started"
				).map((data: any) => data.keyValue)
			);
			accessToken &&
				dispatch(SubmitBugReportDataAsync(bugReport, accessToken));
			resetForm();
		}
	};

	const getBugSeverityStatusFromAi = async () => {
		if (
			errors.description.trim().length === 0 &&
			errors.title.trim().length === 0
		) {
			const accessToken = await getAccessToken();
			const bugSeverityInput: BugSeverityAIRequestDTO = {
				bugDescription: bugReport.bugDescription.trim(),
				bugTitle: bugReport.bugTitle.trim(),
			};
			accessToken &&
				dispatch(
					GetBugSeverityStatusAsync(bugSeverityInput, accessToken)
				);
		}
	};

	const generateBugReportButton = () => {
		return bugSeverity !== "" ? (
			<Button
				appearance="primary"
				className={styles.submitButton}
				onClick={handleSubmit}
				disabled={
					errors.description.trim().length > 0 ||
					errors.title.trim().length > 0
				}
			>
				Submit Report
			</Button>
		) : (
			<Button
				appearance="primary"
				className={styles.submitButton}
				onClick={getBugSeverityStatusFromAi}
				disabled={
					errors.description.trim().length > 0 ||
					errors.title.trim().length > 0
				}
				icon={<AgentsColor />}
			>
				Get Bug Severity from AI
			</Button>
		);
	};

	const resetForm = () => {
		setBugReport((prevState) => ({
			...prevState,
			bugDescription: "",
			bugSeverity: 4,
			bugStatus: 1,
			bugTitle: "",
			createdBy: "",
			pageUrl: window.location.origin,
		}));
	};

	return (
		<OverlayDrawer
			className={styles.drawer}
			{...restoreFocusSourceAttributes}
			as="aside"
			open={IsBugReportDrawerOpenStoreData}
			onOpenChange={(_, { open }) => {
				dispatch(ToggleBugReportDrawer(open));
			}}
			size="medium"
			position="end"
			surfaceMotion={{
				children: (_, props) => <DrawerMotion {...props} />,
			}}
		>
			<Spinner
				isLoading={IsBugReportSpinnerLoadingStoreData}
				message={BugReportConstants.LoaderMessage}
			/>
			<DrawerHeader className={styles.drawerHeader}>
				<DrawerHeaderTitle
					action={
						<Button
							appearance="subtle"
							aria-label="Close drawer"
							onClick={handleSideBarClose}
							className={styles.closeButton}
							icon={<Dismiss28Filled />}
						/>
					}
				>
					<span className={styles.title}>
						{BugReportConstants.Header}
					</span>
				</DrawerHeaderTitle>
			</DrawerHeader>

			<DrawerBody className={styles.drawerBody}>
				<div className={styles.formField}>
					<Label className={styles.label} htmlFor="bugTitle" required>
						Bug Title
					</Label>
					<input
						className={styles.input}
						id="bugTitle"
						name="bugTitle"
						value={bugReport.bugTitle}
						onChange={handleInputChange}
						required
					/>
					{errors.title && (
						<span className="text-danger">{errors.title}</span>
					)}
				</div>
				<div className={styles.formField}>
					<Label
						className={styles.label}
						htmlFor="bugDescription"
						required
					>
						Bug Description
					</Label>
					<textarea
						className={styles.textarea}
						id="bugDescription"
						name="bugDescription"
						value={bugReport.bugDescription}
						onChange={handleInputChange}
						required
					/>
					{errors.description && (
						<span className="text-danger">
							{errors.description}
						</span>
					)}
				</div>
				<div className={styles.formField}>
					<Label
						className={styles.label}
						htmlFor="bugSeverity"
						required
					>
						Bug Severity
					</Label>
					<select
						className={styles.dropdown}
						onChange={handleSeverityChange}
						disabled={true}
						value={bugReport.bugSeverity}
					>
						{LookupMasterStoreData.filter(
							(item: any) => item.type === "BugSeverity"
						).map((severity: any) => (
							<option
								key={severity.keyValue}
								value={severity.keyValue}
							>
								{severity.keyName}
							</option>
						))}
					</select>
				</div>
				{generateBugReportButton()}
				<Button
					appearance="primary"
					className={styles.submitButton}
					onClick={resetForm}
					disabled={
						errors.description.trim().length > 0 ||
						errors.title.trim().length > 0
					}
				>
					Reset form
				</Button>
			</DrawerBody>
		</OverlayDrawer>
	);
}
