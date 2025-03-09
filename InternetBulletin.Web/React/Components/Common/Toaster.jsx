import React, { useEffect, useState } from "react";

/**
 * @component
 * Toaster component to display error messages.
 *
 * @param {Object} props - The component props.
 * @param {string} props.errorMessage - The error message to display.
 * @param {Function} props.clearErrorMessage - Function to clear the error message.
 * 
 * @returns {JSX.Element} The rendered component.
 */
function Toaster({ errorMessage, clearErrorMessage }) {
	const [shouldShow, setShouldShow] = useState(false);

	useEffect(() => {
		if (errorMessage) {
			setShouldShow(true);
		} else {
			setShouldShow(false);
		}
	}, [errorMessage]);

	/**
	 * Handles the close button click event.
	 */
	const handleClose = () => {
		setShouldShow(false);
		clearErrorMessage();
	};

	return (
		shouldShow && (
			<div className="toaster-container position-absolute top-0 end-0">
				<div
					className="toast show text-white bg-danger border-0"
					role="alert"
					aria-live="assertive"
					aria-atomic="true"
				>
					<div className="d-flex top-0 end-0">
						<div className="toast-body">{errorMessage}</div>
						<button
							type="button"
							className="btn-close"
							onClick={handleClose}
							aria-label="Close"
						></button>
					</div>
				</div>
			</div>
		)
	);
}

export default Toaster;
