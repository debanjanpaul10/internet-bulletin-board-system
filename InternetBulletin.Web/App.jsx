import React from "react";
import { Routes, Route } from "react-router-dom";

import { HeaderPageConstants } from "@helpers/ibbs.constants";
import Header from "@components/Common/Header";
import PageNotFound from "@components/Common/PageNotFound";
import HomeComponent from "@components/Home";
import CreatePostComponent from "@components/Posts/CreatePost";
import ToasterComponent from "@components/Common/Toaster";
import ProfileComponent from "@components/Profile";
import SideDrawerComponent from "@components/Common/SideDrawer";

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
      <ToasterComponent />
      <SideDrawerComponent />
      <Routes>
        <Route path={Headings.Home.Link} element={<HomeComponent />} />
        <Route
          path={Headings.CreatePost.Link}
          element={<CreatePostComponent />}
        />
        <Route path="*" element={<PageNotFound />} />
        <Route path={Headings.MyProfile.Link} element={<ProfileComponent />} />
      </Routes>
    </>
  );
}

export default App;
