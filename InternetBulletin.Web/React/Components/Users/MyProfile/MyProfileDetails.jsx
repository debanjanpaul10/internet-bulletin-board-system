import { useEffect, useState } from "react";

function MyProfileDetailsComponent({ myProfileDetails }) {
	const [profileDetails, setProfileDetails] = useState({});

	useEffect(() => {
		if (
			Object.keys(myProfileDetails).length > 0 &&
			myProfileDetails !== profileDetails
		) {
			setProfileDetails(myProfileDetails);
		}
	}, [myProfileDetails]);

	return (
		Object.keys(profileDetails).length > 0 && (
			<div className="container">
                Hey There!
            </div>
		)
	);
}

export default MyProfileDetailsComponent;
