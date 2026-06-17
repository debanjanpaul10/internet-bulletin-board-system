import { HTMLAttributes, ReactNode } from "react";

/**
 * Interface representing the properties of a magnet component.
 */
export interface MagnetProps extends Omit<
	HTMLAttributes<HTMLDivElement>,
	"children"
> {
	children: ReactNode;
	padding?: number;
	disabled?: boolean;
	magnetStrength?: number;
	activeTransition?: string;
	inactiveTransition?: string;
	wrapperClassName?: string;
	innerClassName?: string;
}
