import React from "react";
import { NoPostsMessageConstant } from "@helpers/Constants";

function NoPostsContainer() {
	return (
		<div className="text-center" style={{ marginTop: "200px" }}>
			<h1 className="mb-2">{NoPostsMessageConstant.Heading}</h1>
		</div>
	);
}

export default NoPostsContainer;
