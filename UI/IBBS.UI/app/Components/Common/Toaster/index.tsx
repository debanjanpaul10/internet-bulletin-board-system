import { useEffect, useState } from "react";
import { useDispatch } from "react-redux";
import {
    useId,
    Toaster,
    useToastController,
    Toast,
    ToastTitle,
    Button,
} from "@fluentui/react-components";
import { Dismiss28Regular } from "@fluentui/react-icons";

import {
    ToggleErrorToaster,
    ToggleSuccessToaster,
} from "@store/Common/Actions";
import { useStyles } from "@components/Common/Toaster/styles";
import { useAppSelector } from "@/index";

/**
 * @component
 * Toaster component to display error and success messages.
 *
 * @returns The rendered component.
 */
export default function ToasterComponent() {
    const dispatch = useDispatch();
    const toasterId = useId("toaster");
    const { dispatchToast, dismissToast } = useToastController(toasterId);
    const styles = useStyles();

    const SuccessToasterStoreData = useAppSelector(
        (state) => state.CommonReducer.successToaster
    );
    const ErrorToasterStoreData = useAppSelector(
        (state) => state.CommonReducer.errorToaster
    );

    const [sucessToasterShow, setSuccessToasterShow] = useState(false);
    const [failToasterShow, setFailToasterShow] = useState(false);

    useEffect(() => {
        if (
            Object.values(SuccessToasterStoreData).length > 0 &&
            sucessToasterShow !== SuccessToasterStoreData
        ) {
            setSuccessToasterShow(SuccessToasterStoreData);
            if (SuccessToasterStoreData.shouldShow) {
                showSuccessToast(SuccessToasterStoreData.successMessage);
            }
        }

        if (
            Object.values(ErrorToasterStoreData).length > 0 &&
            failToasterShow !== ErrorToasterStoreData
        ) {
            setFailToasterShow(ErrorToasterStoreData);
            if (ErrorToasterStoreData.shouldShow) {
                showErrorToast(ErrorToasterStoreData.errorMessage);
            }
        }
    }, [
        SuccessToasterStoreData,
        ErrorToasterStoreData,
        dispatchToast,
        dispatch,
    ]);

    /**
     * Handles the success toaster dismiss event.
     */
    const handleDismissSuccess = () => {
        dispatch(
            ToggleSuccessToaster({
                shouldShow: false,
                successMessage: "",
            })
        );
        dismissToast(toasterId);
    };

    /**
     * Handles the error toaster dismiss event.
     */
    const handleDismissError = () => {
        dispatch(
            ToggleErrorToaster({
                shouldShow: false,
                errorMessage: "",
            })
        );
        dismissToast(toasterId);
    };

    /**
     * Handles the success toaster show event.
     * @param message The toaster message.
     */
    const showSuccessToast = (message: string) => {
        dispatchToast(
            <div className={styles.toastContainer}>
                <Toast>
                    <ToastTitle>{message}</ToastTitle>
                    <Button
                        className={styles.dismissButton}
                        onClick={handleDismissSuccess}
                        appearance="transparent"
                        size="small"
                        aria-label="Dismiss notification"
                    >
                        <Dismiss28Regular />
                    </Button>
                </Toast>
            </div>,
            { timeout: -1, intent: "success", toastId: toasterId }
        );
    };

    /**
     * Handles the error toaster show event.
     * @param {string} message The toaster message.
     */
    const showErrorToast = (message: string) => {
        dispatchToast(
            <div className={styles.toastContainer}>
                <Toast>
                    <Button
                        className={styles.dismissButton}
                        onClick={handleDismissError}
                        appearance="transparent"
                        size="small"
                        aria-label="Dismiss notification"
                        icon={<Dismiss28Regular />}
                    ></Button>
                    <br />
                    <ToastTitle>{message}</ToastTitle>
                </Toast>
            </div>,
            { timeout: -1, intent: "error", toastId: toasterId }
        );
    };

    return <Toaster toasterId={toasterId} />;
}
