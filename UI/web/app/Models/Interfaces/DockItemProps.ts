import { MotionValue, SpringOptions } from "framer-motion";
import { MouseEvent, ReactNode } from "react";

/**
 * Interface representing the properties of a dock item component.
 */
export interface DockItemProps {
	children: ReactNode;
	className?: string;
	onClick?: (event: MouseEvent<HTMLDivElement>) => void;
	mouseX: MotionValue<number>;
	spring: SpringOptions;
	distance: number;
	magnification: number;
	baseItemSize: number;
}
