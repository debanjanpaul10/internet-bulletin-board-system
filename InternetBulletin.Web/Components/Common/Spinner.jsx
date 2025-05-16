import React from "react";
import Cookies from "js-cookie";
import AppLogo from "@assets/Images/IBBS_logo.png";
import { CookiesConstants } from "@helpers/ibbs.constants";

/**
 * @component
 * The Spinner Component used when pages are loading. Is a blocking action.
 *
 * @param {Object} props - The component props.
 * @param {boolean} props.isLoading - The boolean for loading screen.
 *
 * @returns {JSX.Element}
 */
function Spinner({ isLoading }) {
	const isDarkMode = Cookies.get(CookiesConstants.DarkMode.Name) === "true";
	const spinnerClass = isDarkMode
		? "spinner-page dark-mode"
		: "spinner-page light-mode";

	return (
		isLoading && (
			<div className={spinnerClass}>
				<img
					src={AppLogo}
					height={"100px"}
					className="heartbeat"
					alt="Loading..."
				/>
			</div>
		)
	);
}

export default Spinner;
