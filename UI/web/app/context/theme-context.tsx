import { createContext } from "react";
import { PageConstants } from "@/app/helpers/ibbs.constants";

/**
 * The Theme Context for the application
 */
export const ThemeContext = createContext({
	themeMode: PageConstants.LightConstant,
	toggleThemeMode: () => {},
});
