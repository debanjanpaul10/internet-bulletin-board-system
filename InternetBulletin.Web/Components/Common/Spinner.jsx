import React, { useContext } from "react";

import AppLogo from "@assets/Images/IBBS_logo.png";
import ThemeContext from "@context/ThemeContext";

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
	const { themeMode } = useContext(ThemeContext);
	const isDarkMode = themeMode === "dark";
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
