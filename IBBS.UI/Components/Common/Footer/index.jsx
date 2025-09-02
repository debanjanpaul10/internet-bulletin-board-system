import { MailRegular } from "@fluentui/react-icons";
import {
    faFacebook,
    faGithub,
    faLinkedin,
} from "@fortawesome/free-brands-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

import { FooterConstants } from "@helpers/ibbs.constants";
import Dock from "@animations/Dock";

/**
 * @component
 * `FooterComponent` A React component that renders a footer section with social media and contact links.
 *
 * The component displays a row of buttons that link to:
 * - Email contact
 * - Facebook profile
 * - GitHub repository
 * - LinkedIn profile
 *
 * Each button uses either Fluent UI icons or FontAwesome icons and opens external links in a new tab.
 * The links are configured through the FooterConstants helper.
 *
 * @returns {JSX.Element} A div containing social media and contact buttons
 */
function FooterComponent() {
    const { EmailLink, FaceBookLink, GithubLink, LinkedinLink } =
        FooterConstants;

    const dockItems = [
        {
            icon: <FontAwesomeIcon icon={faFacebook} fontSize={30} />,
            label: "Facebook",
            onClick: () => window.open(FaceBookLink, "_blank"),
        },
        {
            icon: <MailRegular fontSize={30} />,
            label: "Email",
            onClick: () => window.open(EmailLink, "_blank"),
        },
        {
            icon: <FontAwesomeIcon icon={faGithub} fontSize={30} />,
            label: "GitHub",
            onClick: () => window.open(GithubLink, "_blank"),
        },
        {
            icon: <FontAwesomeIcon icon={faLinkedin} fontSize={30} />,
            label: "LinkedIn",
            onClick: () => window.open(LinkedinLink, "_blank"),
        },
    ];

    return (
        <div className="mb-0">
            <Dock
                items={dockItems}
                panelHeight={30}
                baseItemSize={30}
                magnification={70}
            />
        </div>
    );
}

export default FooterComponent;
