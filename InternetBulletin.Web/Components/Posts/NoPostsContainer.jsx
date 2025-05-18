import React from "react";
import { NoPostsMessageConstant } from "@helpers/ibbs.constants";

/**
 * @component
 * Displays a message when there are no posts.
 * 
 * @returns {JSX.Element} The No Posts container JSX element.
 */
function NoPostsContainer() {
	return (
		<div className="text-center" style={{ marginTop: "200px" }}>
			<h1 className="mb-2">{NoPostsMessageConstant.Heading}</h1>
		</div>
	);
}

export default NoPostsContainer;
