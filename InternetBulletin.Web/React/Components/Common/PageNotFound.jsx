import React from "react";
import { ErrorPageConstants } from "@helpers/Constants";
import Spinner from "@components/Common/Spinner";

/**
 * @component
 * The Page Not Found error component that is shown when an
 * invalid url is accessed.
 * @returns {JSX.Element} The page not found component JSX element.
 */
function PageNotFound() {
	return (
		<div className="container">
			<div className="row">
				<div className="col-sm-12 mt-5">
					<h1 className="text-center architectDaughterfont">
						{ErrorPageConstants.PageNotFoundMessage}
					</h1>
				</div>
			</div>
		</div>
	);
}

export default PageNotFound;
