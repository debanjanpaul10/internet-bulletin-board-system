import { AIChatbotResponseDTO } from "@/Models/DTOs/ai-chatbot-response.dto";
import { Button } from "@fluentui/react-components";
import { useStyles } from "./styles";

export default function FollowupQuestionsComponent({
	message,
	onSelect,
}: {
	message: any;
	onSelect: (question: string) => void;
}) {
	const styles = useStyles();
	const questions = (
		(message.content as AIChatbotResponseDTO)?.followupQuestions ?? []
	).filter((q: string) => !!q && q.trim().length > 0);

	if (!questions.length) return null;

	return (
		<div className={styles.followupQuestionsContainer}>
			{questions.map((q, i) => (
				<Button
					key={`${q}-${i}`}
					appearance="secondary"
					size="small"
					className={styles.questionBubble}
					onClick={() => onSelect(q)}
				>
					{q}
				</Button>
			))}
		</div>
	);
}
