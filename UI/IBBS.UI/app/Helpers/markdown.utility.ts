import { marked } from "marked";
import DOMPurify from "dompurify";

export const renderSafeMarkdown = (markdownText: string): string => {
	// Preprocess text to reduce excessive line breaks
	const preprocessedText = markdownText
		?.replace(/\n{3,}/g, '\n\n') // Replace 3+ consecutive newlines with 2
		?.replace(/\n\s*\n/g, '\n\n') // Remove spaces between empty lines
		?? "";
	
	// Convert markdown to HTML (synchronously)
	const rawHtml = marked.parse(preprocessedText, {
		async: false,
		breaks: true, // Convert single line breaks to <br>
	}) as string;
	
	// Post-process HTML to reduce spacing
	const processedHtml = rawHtml
		.replace(/<p><\/p>/g, '') // Remove empty paragraphs
		.replace(/<p>\s*<\/p>/g, '') // Remove paragraphs with only whitespace
		.replace(/(<p[^>]*>)\s*(<\/p>)/g, '$1$2'); // Remove whitespace-only content in paragraphs
	
	// Sanitize HTML before injecting into DOM
	const sanitized = DOMPurify.sanitize(processedHtml);
	return sanitized;
};
