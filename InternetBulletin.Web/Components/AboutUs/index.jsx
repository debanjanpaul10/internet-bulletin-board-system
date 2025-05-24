import BannerCardComponent from "@components/AboutUs/Components/BannerCard";
import { useStyles } from "@components/AboutUs/styles";
import {
	Carousel,
	CarouselNav,
	CarouselNavButton,
	CarouselNavContainer,
	CarouselSlider,
	CarouselViewport,
	LargeTitle,
	Title3,
} from "@fluentui/react-components";
import { HeartFilled } from "@fluentui/react-icons";

import { AboutUsData } from "@helpers/aboutus.data";
import { MyProfilePageConstants } from "@helpers/ibbs.constants";

/**
 * AboutUsComponent - A React component that displays the About Us section of the application.
 *
 * This component renders:
 * - A main heading using LargeTitle component
 * - A subheading with a heart icon
 * - A carousel that displays banner cards with information from AboutUsData
 *
 * The carousel features:
 * - Automatic sliding with 4-second intervals
 * - Navigation buttons with tooltips
 * - Circular navigation
 * - Single item display at a time
 *
 * @component
 * @returns {JSX.Element} The rendered About Us section with carousel
 */
function AboutUsComponent() {
	const styles = useStyles();
	const data = AboutUsData;

	/**
	 * The autoplay button props
	 */
	const autoPlayProps = {
		"aria-label": "Enable Autoplay",
		checked: true,
		hidden: true,
	};

	return (
		<div className="container">
			<div className="row">
				<div className="col-sm-12 mt-4">
					<LargeTitle className={styles.aboutUsHeading}>
						{MyProfilePageConstants.Headings.AboutUsMessage}
					</LargeTitle>
				</div>
			</div>
			<div className="row">
				<div className="col-12">
					<Title3 className={styles.subHeading}>
						Made with &nbsp;
						<HeartFilled />
						&nbsp; and
					</Title3>
				</div>
			</div>

			<div className="row mt-4">
				<Carousel groupSize={1} circular autoplayInterval={4000}>
					<CarouselViewport>
						<CarouselSlider>
							{data.map((data, index) => (
								<BannerCardComponent
									key={`image-${index}`}
									index={index}
									data={data}
								>
									Card {index + 1}
								</BannerCardComponent>
							))}
						</CarouselSlider>
					</CarouselViewport>
					<CarouselNavContainer
						layout="inline"
						autoplay={autoPlayProps}
						nextTooltip={{
							content: "Next slide",
							relationship: "label",
						}}
						prevTooltip={{
							content: "Previous slide",
							relationship: "label",
						}}
					>
						<CarouselNav className={styles.carouselNavButton}>
							{(index) => (
								<CarouselNavButton
									aria-label={`Carousel nav button ${index}`}
								/>
							)}
						</CarouselNav>
					</CarouselNavContainer>
				</Carousel>
			</div>
		</div>
	);
}

export default AboutUsComponent;
