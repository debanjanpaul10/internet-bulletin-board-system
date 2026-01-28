import ReactDOM from "react-dom/client";
import { BrowserRouter as Router } from "react-router-dom";
import { configureStore } from "@reduxjs/toolkit";
import { Provider, useDispatch, useSelector } from "react-redux";
import type { TypedUseSelectorHook } from "react-redux";
import "font-awesome/css/font-awesome.min.css";
import "bootstrap/dist/css/bootstrap.css";
import "@fontsource/concert-one";
import { Auth0Provider } from "@auth0/auth0-react";

import "@styles/App.css";
import "@styles/App_Dark.css";
import { PostsReducer } from "@store/Posts/Reducers";
import { CommonReducer } from "@store/Common/Reducers";
import { UserReducer } from "@store/Users/Reducer";
import Main from "@components/Main";
import { environment } from "@environment";
import { AiServicesReducer } from "@store/AiServices/Reducers";

/**
 * Configures the redux store.
 */
const store = configureStore({
	reducer: {
		PostsReducer: PostsReducer,
		CommonReducer: CommonReducer,
		UserReducer: UserReducer,
		AiServicesReducer: AiServicesReducer,
	},
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

export const useAppDispatch: () => AppDispatch = useDispatch;
export const useAppSelector: TypedUseSelectorHook<RootState> = useSelector;

declare global {
	interface Window {
		__IBBS_REACT_ROOT__?: ReturnType<typeof ReactDOM.createRoot>;
	}
}

const container = document.getElementById("root") as HTMLElement;
const root = (window.__IBBS_REACT_ROOT__ ||= ReactDOM.createRoot(container));

root.render(
	<Auth0Provider
		domain={environment.auth0Config.domain}
		clientId={environment.auth0Config.clientId}
		authorizationParams={{
			redirect_uri:
				environment.auth0Config.redirectUri || window.location.origin,
		}}
		useRefreshTokens={true}
		cacheLocation="localstorage"
		onRedirectCallback={(appState) => {
			window.history.replaceState(
				{},
				document.title,
				appState?.returnTo || window.location.pathname
			);
		}}
	>
		<Router>
			<Provider store={store}>
				<Main />
			</Provider>
		</Router>
	</Auth0Provider>
);
