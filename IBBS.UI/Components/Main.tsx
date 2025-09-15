import React, { useEffect, useState } from "react";
import {
	FluentProvider,
	Spinner,
	webDarkTheme,
} from "@fluentui/react-components";
import { useAuth0 } from "@auth0/auth0-react";

import IBBSComponent from "@/Components/IBBS";
import { PageConstants } from "@/Helpers/ibbs.constants";
import { ThemeContext } from "@/Context/ThemeContext";

/**
 * @component
 * `Main` component that handles theme management and application initialization.
 *
 * @description
 * This component serves as the main wrapper for the application, managing:
 * - Theme state (light/dark mode)
 * - Theme persistence using cookies
 *
 * @returns The main application wrapper with theme context
 */
export default function MainComponent() {
	const { DarkModeConstant, DarkConstant } = PageConstants;
	const [themeMode, setThemeMode] = useState(DarkConstant);
	const { isLoading } = useAuth0();

	useEffect(() => {
		setThemeMode(DarkConstant);
		document.body.classList.add(DarkModeConstant);
	}, []);

	/**
	 * Toggles the theme mode.
	 */
	const toggleThemeMode = () => {};

	return (
		<ThemeContext.Provider value={{ themeMode, toggleThemeMode }}>
			{isLoading ? (
				<div
					style={{
						display: "flex",
						justifyContent: "center",
						alignItems: "center",
						height: "100vh",
					}}
				>
					<Spinner />
				</div>
			) : (
				<FluentProvider theme={webDarkTheme}>
					<React.Suspense fallback={<div>Loading app...</div>}>
						<IBBSComponent />
					</React.Suspense>
				</FluentProvider>
			)}
		</ThemeContext.Provider>
	);
}
