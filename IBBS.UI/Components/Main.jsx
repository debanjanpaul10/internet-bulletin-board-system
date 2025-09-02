import React, { useEffect, useState } from "react";
import {
    FluentProvider,
    Spinner,
    webDarkTheme,
    webLightTheme,
} from "@fluentui/react-components";
import Cookies from "js-cookie";
import { useAuth0 } from "@auth0/auth0-react";

import IBBS from "@components/IBBS";
import ThemeContext from "@context/ThemeContext";
import { ConsoleLogMessage } from "@helpers/common.utility";
import { CookiesConstants, PageConstants } from "@helpers/ibbs.constants";

/**
 * @component
 * `Main` component that handles theme management and application initialization.
 *
 * @description
 * This component serves as the main wrapper for the application, managing:
 * - Theme state (light/dark mode)
 * - Theme persistence using cookies
 *
 * @state {string} themeMode - Current theme mode ('light' or 'dark')
 *
 * @effect
 * On mount:
 * - Loads saved theme preference from cookies
 *
 * @returns {JSX.Element} The main application wrapper with theme context
 */
function Main() {
    const { DarkModeConstant, LightConstant, DarkConstant } = PageConstants;
    const [themeMode, setThemeMode] = useState(DarkConstant);
    const { isLoading } = useAuth0();

    useEffect(() => {
        // Force dark mode
        setThemeMode(DarkConstant);
        document.body.classList.add(DarkModeConstant);

        // Save dark mode preference
        Cookies.set(CookiesConstants.DarkMode.Name, "true", {
            expires: CookiesConstants.DarkMode.Timeout,
        });
    }, [DarkModeConstant, DarkConstant, LightConstant]);

    /**
     * Toggles the theme mode.
     */
    const toggleThemeMode = () => {
        const newThemeMode =
            themeMode === LightConstant ? DarkConstant : LightConstant;
        setThemeMode(newThemeMode);

        document.body.classList.toggle(
            DarkModeConstant,
            newThemeMode === DarkConstant
        );
        Cookies.set(
            CookiesConstants.DarkMode.Name,
            newThemeMode === DarkConstant,
            {
                expires: CookiesConstants.DarkMode.Timeout,
            }
        );
    };

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
                <FluentProvider
                    theme={
                        themeMode === DarkConstant
                            ? webDarkTheme
                            : webLightTheme
                    }
                >
                    <React.Suspense fallback={<div>Loading app...</div>}>
                        <IBBS />
                    </React.Suspense>
                </FluentProvider>
            )}
            {ConsoleLogMessage}
        </ThemeContext.Provider>
    );
}

export default Main;
