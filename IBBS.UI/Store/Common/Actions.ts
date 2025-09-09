import { Action, Dispatch } from "redux";

import { BugReportDTO } from "@/Models/DTOs/bug-report-data.dto";
import {
	GET_LOOKUP_MASTER_DATA,
	SAVE_BUG_REPORT_DATA,
	TOGGLE_BUG_REPORT_DRAWER,
	TOGGLE_BUG_REPORT_SPINNER,
	TOGGLE_ERROR_TOASTER,
	TOGGLE_SIDE_BAR_STATUS,
	TOGGLE_SUCCESS_TOASTER,
} from "@store/Common/ActionTypes";
import {
	GetLookupMasterDataApiAsync,
	SubmitBugReportDataApiAsync,
} from "@/Services/ibbs.apiservice";
import { StartLoader, StopLoader } from "../Posts/Actions";
import { LookupMasterDTO } from "@/Models/DTOs/lookup-master-data.dto";

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

/**
 * Submits the bug report data to api.
 * @param bugReport The bug report data.
 * @param accessToken The access token.
 * @returns The promise of the API response.
 */
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
				dispatch(
					ToggleSuccessToaster({
						shouldShow: true,
						successMessage: "Report saved succesfully",
					})
				);
				dispatch(ToggleBugReportDrawer(false));
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

/**
 * Handles the toggle event for bug report spinner in redux store.
 * @param isLoading The boolean flag for loading status.
 * @returns The action type and payload object.
 */
export const ToggleBugReportSpinner = (isLoading: boolean) => {
	return {
		type: TOGGLE_BUG_REPORT_SPINNER,
		payload: isLoading,
	};
};

/**
 * Saves the submit bug report data response in redux store.
 * @param data The api response data.
 * @returns The action type and payload data.
 */
const SubmitBugReportSuccess = (data: boolean) => {
	return {
		type: SAVE_BUG_REPORT_DATA,
		payload: data,
	};
};

/**
 * Handles the toggle event for bug report drawer in redux store.
 * @param isOpen The boolean flag for open status.
 * @returns The action type and payload data.
 */
export const ToggleBugReportDrawer = (isOpen: boolean) => {
	return {
		type: TOGGLE_BUG_REPORT_DRAWER,
		payload: isOpen,
	};
};

export const GetLookupMasterDataAsync = () => {
	return async (dispatch: Dispatch<Action>) => {
		try {
			dispatch(StartLoader());
			const response = await GetLookupMasterDataApiAsync();
			if (response?.data && response?.isSuccess) {
				dispatch({
					type: GET_LOOKUP_MASTER_DATA,
					payload: response?.data as LookupMasterDTO[],
				});
			}
		} catch (error) {
			console.error(error);
			dispatch(
				ToggleErrorToaster({
					shouldShow: true,
					errorMessage: error,
				})
			);
			throw error;
		} finally {
			dispatch(StopLoader());
		}
	};
};
