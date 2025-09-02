import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import {
    Carousel,
    CarouselNav,
    CarouselNavButton,
    CarouselNavContainer,
    CarouselSlider,
    CarouselViewport,
} from "@fluentui/react-components";

import { useStyles } from "@components/AboutUs/styles";
import BannerCardComponent from "@components/AboutUs/Components/BannerCard";
import {
    AboutUsPageConstants,
    MyProfilePageConstants,
} from "@helpers/ibbs.constants";
import DescriptionComponent from "@components/AboutUs/Components/Description";
import { GetApplicationInformationDataAsync } from "@store/Common/Actions";
import Spinner from "@components/Common/Spinner";
import BlurText from "@animations/BlurText";
import PageNotFound from "@components/Common/PageNotFound";

/**
 * `AboutUsComponent` - A React component that displays the About Us section of the application.
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
    const dispatch = useDispatch();
    const styles = useStyles();

    const IsAboutUsLoading = useSelector(
        (state) => state.CommonReducer.isAboutUsLoading
    );
    const ApplicationInformationStoreData = useSelector(
        (state) => state.CommonReducer.applicationInformation
    );

    useEffect(() => {
        dispatch(GetApplicationInformationDataAsync());
    }, []);

    /**
     * The autoplay button props
     */
    const autoPlayProps = {
        "aria-label": "Enable Autoplay",
        checked: true,
        hidden: true,
    };

    return (
        <div
            className="container"
            style={{ marginTop: "76px", paddingTop: "20px" }}
        >
            {IsAboutUsLoading ? (
                <Spinner isLoading={IsAboutUsLoading} />
            ) : Object.values(ApplicationInformationStoreData).length > 0 ? (
                <>
                    <div className="row">
                        <div className="col-sm-12">
                            <h1 className={styles.aboutUsHeading}>
                                {MyProfilePageConstants.Headings.AboutUsMessage}
                            </h1>
                        </div>
                    </div>
                    <div className="row">
                        <div className="col-12">
                            <BlurText
                                text={`${AboutUsPageConstants.Subtitle}`}
                                delay={150}
                                animateBy="words"
                                direction="top"
                                className={styles.subHeading}
                            />
                        </div>
                    </div>

                    <div className="row" style={{ marginTop: "20px" }}>
                        <Carousel
                            groupSize={1}
                            circular
                            autoplayInterval={4000}
                        >
                            <CarouselViewport>
                                <CarouselSlider>
                                    {ApplicationInformationStoreData?.applicationTechnologiesData?.map(
                                        (data, index) => (
                                            <BannerCardComponent
                                                key={`image-${index}`}
                                                index={index}
                                                data={data}
                                            />
                                        )
                                    )}
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
                                <CarouselNav
                                    className={styles.carouselNavButton}
                                >
                                    {(index) => (
                                        <CarouselNavButton
                                            aria-label={`Carousel nav button ${index}`}
                                        />
                                    )}
                                </CarouselNav>
                            </CarouselNavContainer>
                        </Carousel>
                    </div>
                    <div className="row mt-4">
                        <DescriptionComponent
                            applicationDescriptionData={
                                ApplicationInformationStoreData?.applicationInformationData
                            }
                        />
                    </div>
                </>
            ) : (
                <PageNotFound />
            )}
        </div>
    );
}

export default AboutUsComponent;
