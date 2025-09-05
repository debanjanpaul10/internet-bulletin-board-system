import { Button } from "@fluentui/react-components";
import { useStyles } from "./styles";

export default function FollowupQuestionsComponent({
	messageList,
	onSelect,
}: {
	messageList: [] | null;
	onSelect: (question: string) => void;
}) {
	const styles = useStyles();
	const questions = (messageList ?? []).filter(
		(q: string) => !!q && q.trim().length > 0
	);

	if (!questions.length) return null;

	return (
		<div className={styles.followupQuestionsContainer}>
			{questions.map((question, index) => (
				<Button
					key={`${question}-${index}`}
					appearance="secondary"
					size="small"
					className={styles.questionBubble}
					onClick={() => onSelect(question)}
				>
					{question}
				</Button>
			))}
		</div>
	);
}
