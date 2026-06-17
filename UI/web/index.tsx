import ReactDOM from "react-dom/client";
import { BrowserRouter as Router } from "react-router-dom";
import { configureStore } from "@reduxjs/toolkit";
import { Provider, useDispatch, useSelector } from "react-redux";
import type { TypedUseSelectorHook } from "react-redux";

// @ts-ignore: allow side-effect import of CSS modules when no declaration file is present
import "font-awesome/css/font-awesome.min.css";
// @ts-ignore: allow side-effect import of CSS modules when no declaration file is present
import "bootstrap/dist/css/bootstrap.css";
// @ts-ignore: allow side-effect import of CSS modules when no declaration file is present
import "@fontsource/concert-one";
import { Auth0Provider } from "@auth0/auth0-react";

// @ts-ignore: allow side-effect import of CSS modules when no declaration file is present
import "@styles/App.css";
// @ts-ignore: allow side-effect import of CSS modules when no declaration file is present
import "@styles/App_Dark.css";
import { PostsReducer } from "@store/posts/reducers";
import { CommonReducer } from "@store/common/reducers";
import { UserReducer } from "@store/users/reducers";
import Main from "@/app/components/main";
import { environment } from "@environment";
import { AiServicesReducer } from "@store/ai-services/reducers";

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
const root = (globalThis.window.__IBBS_REACT_ROOT__ ||=
	ReactDOM.createRoot(container));

root.render(
	<Auth0Provider
		domain={environment.auth0Config.domain}
		clientId={environment.auth0Config.clientId}
		authorizationParams={{
			redirect_uri:
				environment.auth0Config.redirectUri ||
				globalThis.window.location.origin,
		}}
		useRefreshTokens={true}
		cacheLocation="localstorage"
		onRedirectCallback={(appState) => {
			globalThis.window.history.replaceState(
				{},
				document.title,
				appState?.returnTo || globalThis.window.location.pathname,
			);
		}}
	>
		<Router>
			<Provider store={store}>
				<Main />
			</Provider>
		</Router>
	</Auth0Provider>,
);
