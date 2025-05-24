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
import { HeartFilled, MailRegular } from "@fluentui/react-icons";

import { AboutUsData } from "@helpers/aboutus.data";
import { MyProfilePageConstants } from "@helpers/ibbs.constants";

function AboutUsComponent() {
	const styles = useStyles();
	const data = AboutUsData;

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
						autoplayTooltip={{
							content: "Autoplay",
							relationship: "label",
							appearance: "inverted",
						}}
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
