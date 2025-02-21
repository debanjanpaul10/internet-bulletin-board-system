import React from "react";
import { ErrorPageConstants } from "@helpers/Constants";

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
    )
}

export default PageNotFound;