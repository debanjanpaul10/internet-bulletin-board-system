import { createContext } from "react";
import { PageConstants } from "@helpers/ibbs.constants";

/**
 * The Theme Context for the application
 */
export const ThemeContext = createContext({
    themeMode: PageConstants.LightConstant,
    toggleThemeMode: () => {},
});
