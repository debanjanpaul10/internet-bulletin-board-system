import { marked } from "marked";
import DOMPurify from "dompurify";

export const renderSafeMarkdown = (markdownText: string): string => {
	// Convert markdown to HTML (synchronously)
	const rawHtml = marked.parse(markdownText ?? "", {
		async: false,
	}) as string;
	// Sanitize HTML before injecting into DOM
	const sanitized = DOMPurify.sanitize(rawHtml);
	return sanitized;
};
