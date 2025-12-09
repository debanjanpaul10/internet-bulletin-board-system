import { AIChatbotResponseDTO } from "@models/DTOs/ai-chatbot-response.dto";

export interface ChatMessage {
	id: string;
	type: "user" | "bot";
	content: string | AIChatbotResponseDTO;
	timestamp: Date;
}
