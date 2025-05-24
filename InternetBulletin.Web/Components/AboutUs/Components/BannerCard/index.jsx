import { useStyles } from "@components/AboutUs/Components/BannerCard/styles";
import { CarouselCard, Image, Button } from "@fluentui/react-components";

/**
 * A card component that displays a banner with an image, heading, description, and a link button.
 * @component
 * @param {Object} props - Component props
 * @param {Object} props.data - The banner data object
 * @param {string} props.data.Image - URL of the banner image
 * @param {string} props.data.Heading - The main heading text
 * @param {string} props.data.Description - The descriptive text below the heading
 * @param {string} props.data.Link - URL for the button link
 * @param {number} props.index - The index of the banner in a carousel
 * @returns {JSX.Element} A carousel card with banner content
 */
function BannerCardComponent({ data, index }) {
	const styles = useStyles();

	return (
		<CarouselCard
			className={styles.bannerCard}
			aria-label={`${index + 1} of ${1}`}
			id={`test-${index}`}
		>
			<Image fit="cover" src={data.Image} role="presentation" />
			<div className={styles.cardContainer}>
				<div className={styles.title}>{data.Heading}</div>
				<div className={styles.subtext}>{data.Description}</div>
				<div>
					<Button
						size="small"
						shape="square"
						appearance="primary"
						onClick={() => window.open(data.Link, '_blank', 'noopener,noreferrer')}
					>
						Go to website
					</Button>
				</div>
			</div>
		</CarouselCard>
	);
}

export default BannerCardComponent;
