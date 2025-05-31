import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
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

/**
 * @component
 * Toaster component to display error and success messages.
 *
 * @returns {JSX.Element} The rendered component.
 */
function ToasterComponent() {
	const dispatch = useDispatch();
	const toasterId = useId( "toaster" );
	const { dispatchToast, dismissToast } = useToastController( toasterId );
	const styles = useStyles();

	const SuccessToasterStoreData = useSelector(
		( state ) => state.CommonReducer.successToaster
	);
	const ErrorToasterStoreData = useSelector(
		( state ) => state.CommonReducer.errorToaster
	);

	const [ sucessToasterShow, setSuccessToasterShow ] = useState( false );
	const [ failToasterShow, setFailToasterShow ] = useState( false );

	useEffect( () => {
		if (
			Object.values( SuccessToasterStoreData ).length > 0 &&
			sucessToasterShow !== SuccessToasterStoreData
		) {
			setSuccessToasterShow( SuccessToasterStoreData );
			if ( SuccessToasterStoreData.shouldShow ) {
				showSuccessToast( SuccessToasterStoreData.successMessage );
			}
		}

		if (
			Object.values( ErrorToasterStoreData ).length > 0 &&
			failToasterShow !== ErrorToasterStoreData
		) {
			setFailToasterShow( ErrorToasterStoreData );
			if ( ErrorToasterStoreData.shouldShow ) {
				showErrorToast( ErrorToasterStoreData.errorMessage );
			}
		}
	}, [
		SuccessToasterStoreData,
		ErrorToasterStoreData,
		dispatchToast,
		dispatch,
	] );

	/**
	 * Handles the success toaster dismiss event.
	 */
	const handleDismissSuccess = () => {
		dispatch(
			ToggleSuccessToaster( {
				shouldShow: false,
				successMessage: "",
			} )
		);
		dismissToast( toasterId );
	};

	/**
	 * Handles the error toaster dismiss event.
	 */
	const handleDismissError = () => {
		dispatch(
			ToggleErrorToaster( {
				shouldShow: false,
				errorMessage: "",
			} )
		);
		dismissToast( toasterId );
	};

	/**
	 * Handles the success toaster show event.
	 * @param {string} message The toaster message.
	 */
	const showSuccessToast = ( message ) => {
		dispatchToast(
			<div className={ styles.toastContainer }>
				<Toast>
					<Button
						className={ styles.dismissButton }
						onClick={ handleDismissSuccess }
						appearance="transparent"
						size="small"
					>
						<Dismiss28Regular />
					</Button>
					<ToastTitle>{ message }</ToastTitle>
				</Toast>
			</div>,
			{ timeout: -1, intent: "success", toastId: toasterId }
		);
	};

	/**
	 * Handles the error toaster show event.
	 * @param {string} message The toaster message.
	 */
	const showErrorToast = ( message ) => {
		dispatchToast(
			<div className={ styles.toastContainer }>
				<Toast appearance="error">
					<Button
						className={ styles.dismissButton }
						onClick={ handleDismissError }
						appearance="transparent"
						size="small"
					>
						<Dismiss28Regular />
					</Button>
					<ToastTitle>{ message }</ToastTitle>
				</Toast>
			</div>,
			{ timeout: -1, intent: "error", toastId: toasterId }
		);
	};

	return <Toaster toasterId={ toasterId } />;
}

export default ToasterComponent;
