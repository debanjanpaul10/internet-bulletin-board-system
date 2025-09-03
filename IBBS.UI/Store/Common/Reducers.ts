import {
    TOGGLE_ERROR_TOASTER,
    TOGGLE_SIDE_BAR_STATUS,
    TOGGLE_SUCCESS_TOASTER,
} from "@/Store/Common/ActionTypes";

const initialState: any = {
    successToaster: {},
    errorToaster: {},
    isSideBarOpen: false,
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
        default: {
            return state;
        }
    }
};

export { CommonReducer };
