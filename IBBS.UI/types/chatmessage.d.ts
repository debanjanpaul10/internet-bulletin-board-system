import { AIChatbotResponseDTO } from "@/Models/DTOs/ai-chatbot-response.dto";

export interface ChatMessage {
	id: string;
	type: "user" | "bot";
	content: string | AIChatbotResponseDTO;
	timestamp: Date;
}
