import React from "react";
import ReactDOM from "react-dom/client"
import { BrowserRouter as Router } from "react-router-dom";
import { configureStore } from "@reduxjs/toolkit";
import { Provider } from "react-redux";

import 'font-awesome/css/font-awesome.min.css';
import "bootstrap/dist/css/bootstrap.css";
import "@fontsource/architects-daughter";
import "@assets/App.css";
import App from "./App";
import PostsReducer from "@store/Posts/Reducers";
import UsersReducer from "@store/Users/Reducers";

const store = configureStore({
    reducer: {
        PostsReducer: PostsReducer,
        UsersReducer: UsersReducer
    }
});

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
    <Router>
        <Provider store={store}>
            <App />
        </Provider>
    </Router>
)

