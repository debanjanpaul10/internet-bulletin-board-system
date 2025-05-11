import React, { useEffect, useState } from "react";
import ReactDOM from "react-dom/client";
import { BrowserRouter as Router } from "react-router-dom";
import { configureStore } from "@reduxjs/toolkit";
import { Provider } from "react-redux";
import Cookies from "js-cookie";
import "react-toastify/dist/ReactToastify.css";
import "font-awesome/css/font-awesome.min.css";
import "bootstrap/dist/css/bootstrap.css";
import "@fontsource/architects-daughter";

import "@styles/App.css";
import "@styles/App_Dark.css";
import App from "./App";
import PostsReducer from "@store/Posts/Reducers";
import CommonReducer from "@store/Common/Reducers";
import { CookiesConstants, PageConstants } from "@helpers/ibbs.constants";
import ThemeContext from "@context/ThemeContext";
import { ConsoleLogMessage } from "@helpers/CommonUtility";
import { Auth0Provider, useAuth0 } from "@auth0/auth0-react";
import { Auth0Constants } from "@helpers/config.constants";
import Spinner from "@components/Common/Spinner";

/**
 * Configures the redux store.
 */
const store = configureStore({
	reducer: {
		PostsReducer: PostsReducer,
		CommonReducer: CommonReducer,
	},
});

const root = ReactDOM.createRoot(document.getElementById("root"));

/**
 * @component
 * The Main entry component
 *
 * @returns {JSX.Element} The main JSX element.
 */
function Main() {
	const { DarkModeConstant, LightConstant, DarkConstant } = PageConstants;
	const { isLoading } = useAuth0();

	const [themeMode, setThemeMode] = useState(LightConstant);

	useEffect(() => {
		const savedDarkModeSettings =
			Cookies.get(CookiesConstants.DarkMode.Name) === "true";
		setThemeMode(savedDarkModeSettings ? DarkConstant : LightConstant);

		document.body.classList.toggle(DarkModeConstant, savedDarkModeSettings);
	}, []);

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
			<Spinner isLoading={isLoading} />
			{!isLoading && <App />}
			{ConsoleLogMessage}
		</ThemeContext.Provider>
	);
}

root.render(
	<Auth0Provider
		domain={Auth0Constants.Domain}
		clientId={Auth0Constants.ClientId}
		authorizationParams={{
			redirect_uri: window.location.origin,
			audience: Auth0Constants.Audience,
			scope: Auth0Constants.Scope,
		}}
	>
		<Router>
			<Provider store={store}>
				<Main />
			</Provider>
		</Router>
	</Auth0Provider>
);
