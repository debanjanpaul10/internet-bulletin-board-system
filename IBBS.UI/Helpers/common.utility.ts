import { ConsoleMessage } from "@helpers/ibbs.constants";

/**
 * Renders a custom console log message with author information
 */
export const ConsoleLogMessage = console.log(
	"%c %s",
	"color:red; font-size: 22pt; font-family: 'Source Code Pro'",
	ConsoleMessage
);

/**
 * Formats the date to date string format.
 * @param date The unformatted date
 * @returns The formatted date.
 */
export const formatDate = (date: Date) => {
	return new Date(date).toDateString();
};

/**
 * Handles the snap scroll effect.
 * @param container The container element.
 * @param currentSection The current section of container.
 * @param setCurrentSection The setting of current section container function.
 * @param contentSectionRef The content container sectionr reference.
 * @returns A tupple containing the helper methods.
 */
export const SnapScrollHandler = (
	container: any,
	currentSection: number,
	setCurrentSection: Function,
	contentSectionRef: any
) => {
	let isScrolling = false;
	let scrollTimeout: any;
	let touchStartY = 0;
	let touchEndY = 0;

	const handleWheel = (e: any) => {
		if (e.target.closest("button")) {
			return;
		}

		if (isScrolling) return;

		if (currentSection === 1) {
			const contentSection: any = contentSectionRef.current;
			if (contentSection && contentSection.contains(e.target)) {
				if (e.deltaY < 0 && contentSection.scrollTop <= 5) {
					e.preventDefault();
					isScrolling = true;
					setCurrentSection(0);

					scrollTimeout = setTimeout(() => {
						isScrolling = false;
					}, 300);
					return;
				}
				return;
			}

			if (e.deltaY < 0) {
				e.preventDefault();
				isScrolling = true;
				setCurrentSection(0);

				scrollTimeout = setTimeout(() => {
					isScrolling = false;
				}, 300);
				return;
			}
		}

		e.preventDefault();
		isScrolling = true;

		clearTimeout(scrollTimeout);

		if (currentSection === 0 && e.deltaY > 0) {
			setCurrentSection(1);
		}

		scrollTimeout = setTimeout(() => {
			isScrolling = false;
		}, 300);
	};

	const handleTouchStart = (e: any) => {
		touchStartY = e.touches[0].clientY;
	};

	const handleTouchMove = (e: Event) => {
		if (currentSection === 1) {
			const contentSection: any = contentSectionRef.current;
			if (contentSection && contentSection.contains(e.target)) {
				return;
			}
		}

		e.preventDefault();
	};

	const handleTouchEnd = (e: any) => {
		touchEndY = e.changedTouches[0].clientY;
		const touchDiff = touchStartY - touchEndY;
		const minSwipeDistance = 50;

		if (e.target.closest("button")) {
			return;
		}

		if (isScrolling) return;
		if (currentSection === 1) {
			const contentSection: any = contentSectionRef.current;
			if (contentSection && contentSection.contains(e.target)) {
				if (
					touchDiff < -minSwipeDistance &&
					contentSection.scrollTop <= 5
				) {
					isScrolling = true;
					setCurrentSection(0);
					setTimeout(() => {
						isScrolling = false;
					}, 300);
					return;
				}
				return;
			}

			if (touchDiff < -minSwipeDistance) {
				isScrolling = true;
				setCurrentSection(0);
				setTimeout(() => {
					isScrolling = false;
				}, 300);
				return;
			}
		}

		isScrolling = true;

		if (currentSection === 0 && touchDiff > minSwipeDistance) {
			setCurrentSection(1);
		} else if (currentSection === 1 && touchDiff < -minSwipeDistance) {
			setCurrentSection(0);
		}

		setTimeout(() => {
			isScrolling = false;
		}, 300);
	};

	container.addEventListener("wheel", handleWheel, { passive: false });
	container.addEventListener("touchstart", handleTouchStart, {
		passive: true,
	});
	container.addEventListener("touchmove", handleTouchMove, {
		passive: false,
	});
	container.addEventListener("touchend", handleTouchEnd, {
		passive: true,
	});

	return {
		handleWheel,
		handleTouchStart,
		handleTouchMove,
		handleTouchEnd,
		scrollTimeout,
	};
};
