import { LoginPageConstants } from "@helpers/Constants";
import React from "react";

function LoginComponent() {
    return (
        <div className="container">
            <div className="row">
                <div className="col-sm-12 mt-5">
                    <h1 className="architectDaughterfont text-center">{LoginPageConstants.Headings.LoginUser}</h1>
                </div>
            </div>
        </div>
    )
}

export default LoginComponent;