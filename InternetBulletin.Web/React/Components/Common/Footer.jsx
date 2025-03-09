import React from "react";

/**
 * @component
 * The Footer Component that shows the mandatory fields.
 * @returns {JSX.Element} The Footer JSX element.
 */
function FooterComponent() {
	return (
		<span className="footer">
			All <span className="red">*</span> marked fields are mandatory!
		</span>
	);
}

export default FooterComponent;
