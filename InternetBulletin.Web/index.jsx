import React, { useEffect, useState } from "react";
import ReactDOM from "react-dom/client";
import { BrowserRouter as Router } from "react-router-dom";
import { configureStore } from "@reduxjs/toolkit";
import { Provider } from "react-redux";
import Cookies from "js-cookie";
import "font-awesome/css/font-awesome.min.css";
import "bootstrap/dist/css/bootstrap.css";
import "@fontsource/architects-daughter";
import { MsalProvider } from "@azure/msal-react";
import { PublicClientApplication } from "@azure/msal-browser";
import {
  FluentProvider,
  webDarkTheme,
  webLightTheme,
} from "@fluentui/react-components";

import "@styles/App.css";
import "@styles/App_Dark.css";
import App from "./App";
import { PostsReducer } from "@store/Posts/Reducers";
import { CommonReducer } from "@store/Common/Reducers";
import { CookiesConstants, PageConstants } from "@helpers/ibbs.constants";
import ThemeContext from "@context/ThemeContext";
import { ConsoleLogMessage } from "@helpers/common.utility";
import { msalConfig } from "@services/auth.config";
import Spinner from "@components/Common/Spinner";
import { UserReducer } from "@store/Users/Reducer";

/**
 * Configures the redux store.
 */
const store = configureStore({
  reducer: {
    PostsReducer: PostsReducer,
    CommonReducer: CommonReducer,
    UserReducer: UserReducer
  },
});

const root = ReactDOM.createRoot(document.getElementById("root"));

const msalInstance = new PublicClientApplication(msalConfig);

/**
 * @component
 * The Main entry component
 *
 * @returns {JSX.Element} The main JSX element.
 */
function Main() {
  const { DarkModeConstant, LightConstant, DarkConstant } = PageConstants;
  const [themeMode, setThemeMode] = useState(LightConstant);
  const [initialized, setInitialized] = useState(false);

  useEffect(() => {
    const savedDarkModeSettings =
      Cookies.get(CookiesConstants.DarkMode.Name) === "true";
    setThemeMode(savedDarkModeSettings ? DarkConstant : LightConstant);

    document.body.classList.toggle(DarkModeConstant, savedDarkModeSettings);

    msalInstance.initialize().then(() => setInitialized(true));
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
    Cookies.set(CookiesConstants.DarkMode.Name, newThemeMode === DarkConstant, {
      expires: CookiesConstants.DarkMode.Timeout,
    });
  };

  return (
    <ThemeContext.Provider value={{ themeMode, toggleThemeMode }}>
      <Spinner isLoading={!initialized} />
      {initialized && (
        <FluentProvider
          theme={themeMode === DarkConstant ? webDarkTheme : webLightTheme}
        >
          <App />
        </FluentProvider>
      )}
      {ConsoleLogMessage}
    </ThemeContext.Provider>
  );
}

root.render(
  <MsalProvider instance={msalInstance}>
    <Router>
      <Provider store={store}>
        <Main />
      </Provider>
    </Router>
  </MsalProvider>
);
