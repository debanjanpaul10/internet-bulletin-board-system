import React from "react";
import { Routes, Route } from "react-router-dom";

import { HeaderPageConstants } from "@helpers/ibbs.constants";
import Header from "@components/Common/Header";
import PageNotFound from "@components/Common/PageNotFound";
import HomeComponent from "@components/HomeComponent";
import CreatePostComponent from "@components/Posts/CreatePost";
import Toaster from "@components/Common/Toaster";

/**
 * @component
 * The Main App component.
 *
 * @returns {JSX.Element} The App JSX element.
 */
function App() {
	const { Headings } = HeaderPageConstants;

	return (
		<>
			<Header />
			<Toaster />
			<Routes>
				<Route path={Headings.Home.Link} element={<HomeComponent />} />
				<Route
					path={Headings.CreatePost.Link}
					element={<CreatePostComponent />}
				/>
				<Route path="*" element={<PageNotFound />} />
			</Routes>
		</>
	);
}

export default App;
