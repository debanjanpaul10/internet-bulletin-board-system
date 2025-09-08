import { Action, Dispatch } from "redux";

import { BugReportDTO } from "@/Models/DTOs/bug-report-data.dto";
import {
	SAVE_BUG_REPORT_DATA,
	TOGGLE_BUG_REPORT_DRAWER,
	TOGGLE_BUG_REPORT_SPINNER,
	TOGGLE_ERROR_TOASTER,
	TOGGLE_SIDE_BAR_STATUS,
	TOGGLE_SUCCESS_TOASTER,
} from "@store/Common/ActionTypes";
import { SubmitBugReportDataApiAsync } from "@/Services/ibbs.apiservice";

/**
 * Saves the toggle of success toaster to redux store.
 * @param data The data object
 * @returns The action type and payload data.
 */
export const ToggleSuccessToaster = (data: any) => {
	return {
		type: TOGGLE_SUCCESS_TOASTER,
		payload: {
			shouldShow: data.shouldShow,
			successMessage: data.successMessage,
		},
	};
};

/**
 * Saves the toggle of error toaster to redux store.
 * @param data The data object
 * @returns The action type and payload data.
 */
export const ToggleErrorToaster = (data: any) => {
	return {
		type: TOGGLE_ERROR_TOASTER,
		payload: {
			shouldShow: data.shouldShow,
			errorMessage: data.errorMessage,
		},
	};
};

/**
 * Saves the toggle of side bar status to redux store.
 * @param isOpen The is open boolean flag.
 * @returns The action type and payload data.
 */
export const ToggleSideBar = (isOpen: boolean) => {
	return {
		type: TOGGLE_SIDE_BAR_STATUS,
		payload: isOpen,
	};
};

export const SubmitBugReportDataAsync = (
	bugReport: BugReportDTO,
	accessToken: string
) => {
	return async (dispatch: Dispatch<Action>) => {
		try {
			dispatch(ToggleBugReportSpinner(true));
			const response = await SubmitBugReportDataApiAsync(
				bugReport,
				accessToken
			);
			if (response?.isSuccess && response?.data) {
				dispatch(SubmitBugReportSuccess(response?.data));
			}
		} catch (error: any) {
			console.error(error);
			dispatch(
				ToggleErrorToaster({
					shouldShow: true,
					errorMessage: error,
				})
			);
			throw error;
		} finally {
			dispatch(ToggleBugReportSpinner(false));
		}
	};
};

export const ToggleBugReportSpinner = (isLoading: boolean) => {
	return {
		type: TOGGLE_BUG_REPORT_SPINNER,
		payload: isLoading,
	};
};

const SubmitBugReportSuccess = (data: boolean) => {
	return {
		type: SAVE_BUG_REPORT_DATA,
		payload: data,
	};
};

export const ToggleBugReportDrawer = (isOpen: boolean) => {
	return {
		type: TOGGLE_BUG_REPORT_DRAWER,
		payload: isOpen,
	};
};
