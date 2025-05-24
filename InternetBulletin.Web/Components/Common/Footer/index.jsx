import { Button } from "@fluentui/react-components";
import { MailRegular } from "@fluentui/react-icons";
import {
	faFacebook,
	faGithub,
	faLinkedin,
} from "@fortawesome/free-brands-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

function FooterComponent() {
	return (
		<div className="row mt-4 mb-4">
			<div className="col-12 d-flex justify-content-center gap-3">
				<Button
					as="a"
					appearance="subtle"
					href="mailto:debanjanpaul10@gmail.com"
					icon={<MailRegular />}
				></Button>
				<Button
					icon={<FontAwesomeIcon icon={faFacebook} />}
					appearance="subtle"
					as="a"
					href="https://www.facebook.com/debanjan.paul.69"
					target="_blank"
				></Button>
				<Button
					icon={<FontAwesomeIcon icon={faGithub} />}
					appearance="subtle"
					as="a"
					href="https://github.com/debanjanpaul10"
					target="_blank"
				></Button>
				<Button
					icon={<FontAwesomeIcon icon={faLinkedin} />}
					appearance="subtle"
					as="a"
					href="https://www.linkedin.com/in/debanjan-paul/"
					target="_blank"
				></Button>
			</div>
		</div>
	);
}

export default FooterComponent;
