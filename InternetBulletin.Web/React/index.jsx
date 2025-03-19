import React from "react";
import ReactDOM from "react-dom/client";
import { BrowserRouter as Router } from "react-router-dom";
import { configureStore } from "@reduxjs/toolkit";
import { Provider } from "react-redux";

import "react-toastify/dist/ReactToastify.css";
import "font-awesome/css/font-awesome.min.css";
import "bootstrap/dist/css/bootstrap.css";
import "@fontsource/architects-daughter";
import "@styles/App.css";
import "@styles/App_Dark.css";
import App from "./App";
import PostsReducer from "@store/Posts/Reducers";
import UsersReducer from "@store/Users/Reducers";
import CommonReducer from "@store/Common/Reducers";
import { ConsoleMessage } from "@helpers/Constants";

const store = configureStore({
	reducer: {
		PostsReducer: PostsReducer,
		UsersReducer: UsersReducer,
		CommonReducer: CommonReducer,
	},
});

const root = ReactDOM.createRoot(document.getElementById("root"));
const customConsoleMessage = console.log(
	"%c %s",
	"color:red; font-size: 22pt; font-family: 'Source Code Pro'",
	ConsoleMessage
);

root.render(
	<Router>
		<Provider store={store}>
			<App />
			{customConsoleMessage}
		</Provider>
	</Router>
);
