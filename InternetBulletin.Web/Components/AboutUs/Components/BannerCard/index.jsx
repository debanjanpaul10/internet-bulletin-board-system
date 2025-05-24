import { useStyles } from "@components/AboutUs/Components/BannerCard/styles";
import { CarouselCard, Image, Button } from "@fluentui/react-components";

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
