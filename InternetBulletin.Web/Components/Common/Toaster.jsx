import {
	ToggleErrorToaster,
	ToggleSuccessToaster,
} from "@store/Common/Actions";
import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";

/**
 * @component
 * Toaster component to display error and success messages.
 *
 * @returns {JSX.Element} The rendered component.
 */
function Toaster() {
	const dispatch = useDispatch();

	const SuccessToasterStoreData = useSelector(
		(state) => state.CommonReducer.successToaster
	);
	const ErrorToasterStoreData = useSelector(
		(state) => state.CommonReducer.errorToaster
	);

	if (
		!SuccessToasterStoreData.shouldShow &&
		!ErrorToasterStoreData.shouldShow
	)
		return null;

	return (
		<>
			{ErrorToasterStoreData.shouldShow && (
				<div className="toaster-container position-absolute top-0 end-0">
					<div
						className="toast show text-white bg-danger border-0"
						role="alert"
						aria-live="assertive"
						aria-atomic="true"
					>
						<div
							className="toast-header bg-danger border-0"
							style={{ height: 0 }}
						>
							<button
								type="button"
								className="btn-close ms-auto"
								onClick={() =>
									dispatch(
										ToggleErrorToaster({
											shouldShow: false,
											errorMessage: "",
										})
									)
								}
								aria-label="Close"
							></button>
						</div>
						<div className="toast-body p-2">
							{ErrorToasterStoreData.errorMessage}
						</div>
					</div>
				</div>
			)}

			{SuccessToasterStoreData.shouldShow && (
				<div className="toaster-container position-absolute top-0 end-0">
					<div
						className="toast show text-white bg-success border-0"
						role="alert"
						aria-live="assertive"
						aria-atomic="true"
					>
						<div
							className="toast-header bg-success border-0"
							style={{ height: 0 }}
						>
							<button
								type="button"
								className="btn-close ms-auto"
								onClick={dispatch(
									ToggleSuccessToaster({
										shouldShow: false,
										successMessage: "",
									})
								)}
								aria-label="Close"
							></button>
						</div>
						<div className="toast-body p-2">
							{SuccessToasterStoreData.successMessage}
						</div>
					</div>
				</div>
			)}
		</>
	);
}

export default Toaster;
