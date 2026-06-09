import { AIChatbotResponseDTO } from "@/app/models/dto-models/ai-chatbot-response.dto";

export interface ChatMessage {
	id: string;
	type: "user" | "bot";
	content: string | AIChatbotResponseDTO;
	timestamp: Date;
}
