import React from "react"
import { Switch, Route } from "react-router-dom";
import { HeaderPageConstants } from "@helpers/Constants";
import Header from "@components/Common/Header";
import PageNotFound from "@components/Common/PageNotFound";
import HomeComponent from "@components/HomeComponent";
import LoginComponent from "@components/Users/Login";
import RegisterComponent from "@components/Users/Register";

/**
 * @component
 * The Main App component.
 * 
 * @returns {JSX.Element} The App JSX element.
 */
function App() {
	const { Headings } = HeaderPageConstants
	return (
		<>
			<Header />
			<Switch>
				<Route path={Headings.Home.Link} component={HomeComponent} exact />
				<Route path={Headings.Login.Link} component={LoginComponent} exact />
				<Route path={Headings.Register.Link} component={RegisterComponent} exact />
				<Route component={PageNotFound} />
			</Switch>
		</>
	)
}

export default App;