import { useEffect, useState } from "react";

import { MyProfilePageConstants } from "@helpers/Constants";

/**
 * @component
 * This component displays the user's profile details in a tabular format.
 * It dynamically updates the displayed data when the `myProfileDetails` prop changes.
 * 
 * @param {Object} props - The component props.
 * @param {Object} props.myProfileDetails - The user's profile details.
 * @param {string} props.myProfileDetails.name - The user's name.
 * @param {string} props.myProfileDetails.userAlias - The user's alias.
 * @param {string} props.myProfileDetails.userEmail - The user's email address.
 * 
 * @returns {JSX.Element} The rendered component.
 */
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
			<div className="container mt-5">
				<div className="container d-flex flex-column">
					<h1 className="architectDaughterfont text-center">
						{MyProfilePageConstants.Headings.Header}
					</h1>

					<table className="table-borderless"
						style={{
							width: "80%",
							borderCollapse: "collapse",
							marginTop: "100px",
						}}
					>
						<tbody>
							<tr>
								<td className="text-end fw-bold pe-3 mt-2">Name</td>
								<td>
									<input
										name="Name"
										value={profileDetails.name}
										id="Name"
										disabled={true}
										className="form-control-plaintext"
									/>
								</td>
							</tr>
							<tr>
								<td className="text-end fw-bold pe-3 mt-2">Alias</td>
								<td>
									<input
										name="Alias"
										value={profileDetails.userAlias}
										id="Alias"
										disabled={true}
										className="form-control-plaintext"
									/>
								</td>
							</tr>
							<tr>
								<td className="text-end fw-bold pe-3 mt-2">
									Email Address
								</td>
								<td>
									<input
										name="Email"
										value={profileDetails.userEmail}
										id="Email"
										disabled={true}
										className="form-control-plaintext"
									/>
								</td>
							</tr>
						</tbody>
					</table>
				</div>
			</div>
		)
	);
}

export default MyProfileDetailsComponent;
