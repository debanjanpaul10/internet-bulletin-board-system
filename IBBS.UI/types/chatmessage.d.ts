import { AIChatbotResponseDTO } from "@/Models/DTOs/ai-chatbot-response.dto";

export interface ChatMessage {
	type: "user" | "bot";
	content: string | AIChatbotResponseDTO;
}
