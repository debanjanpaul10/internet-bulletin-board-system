import {
    TOGGLE_ERROR_TOASTER,
    TOGGLE_SIDE_BAR_STATUS,
    TOGGLE_SUCCESS_TOASTER,
} from "@store/Common/ActionTypes";

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
