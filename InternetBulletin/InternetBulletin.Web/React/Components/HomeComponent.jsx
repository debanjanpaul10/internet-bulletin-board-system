import { HomePageConstants } from '@helpers/Constants';
import React from 'react'

function HomeComponent() {
    return (
        <div className="container">
            <div className="row">
                <div className="col-sm-12 mt-4">
                    <h1 className="architectDaughterfont text-center">
                        {HomePageConstants.Headings.WelcomeMessage}
                    </h1>
                </div>
            </div>
        </div>
    )
}

export default HomeComponent;