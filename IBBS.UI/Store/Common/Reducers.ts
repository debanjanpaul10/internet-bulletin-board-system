import {
	GET_LOOKUP_MASTER_DATA,
	SAVE_BUG_REPORT_DATA,
	TOGGLE_BUG_REPORT_DRAWER,
	TOGGLE_BUG_REPORT_SPINNER,
	TOGGLE_ERROR_TOASTER,
	TOGGLE_SIDE_BAR_STATUS,
	TOGGLE_SUCCESS_TOASTER,
} from "@/Store/Common/ActionTypes";

const initialState: any = {
	successToaster: {},
	errorToaster: {},
	isSideBarOpen: false,
	isBugReportSubmitted: false,
	isBugReportSpinnerLoading: false,
	isBugReportDrawerOpen: false,
	lookupMasterData: [],
};

/**
 * The common reducer.
 * @param state The state.
 * @param action The action data.
 * @returns The updated redux store object.
 */
const CommonReducer = (state = initialState, action: any) => {
	switch (action.type) {
		case TOGGLE_SUCCESS_TOASTER: {
			return {
				...state,
				successToaster: action.payload,
			};
		}
		case TOGGLE_ERROR_TOASTER: {
			return {
				...state,
				errorToaster: action.payload,
			};
		}
		case TOGGLE_SIDE_BAR_STATUS: {
			return {
				...state,
				isSideBarOpen: action.payload,
			};
		}
		case SAVE_BUG_REPORT_DATA: {
			return {
				...state,
				isBugReportSubmitted: action.payload,
			};
		}
		case TOGGLE_BUG_REPORT_SPINNER: {
			return {
				...state,
				isBugReportSpinnerLoading: action.payload,
			};
		}
		case TOGGLE_BUG_REPORT_DRAWER: {
			return {
				...state,
				isBugReportDrawerOpen: action.payload,
			};
		}
		case GET_LOOKUP_MASTER_DATA: {
			return {
				...state,
				lookupMasterData: action.payload,
			};
		}
		default: {
			return state;
		}
	}
};

export { CommonReducer };
