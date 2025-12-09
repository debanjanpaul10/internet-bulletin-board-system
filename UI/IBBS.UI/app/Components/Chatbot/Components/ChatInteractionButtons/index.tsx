import { Button, Tooltip } from "@fluentui/react-components";
import { FluentIcon } from "@fluentui/react-icons";
import { useAuth0 } from "@auth0/auth0-react";

import { useAppDispatch } from "@/index";
import { AIChatbotResponseDTO } from "@models/DTOs/ai-chatbot-response.dto";
import { HandleAiResultFeedbackAsync } from "@store/AiServices/Actions";
import { AIResponseFeedbackDTO } from "@models/DTOs/ai-response-feedback.dto";
import { ChatMessage } from "@/app/Types/chatmessage";

export default function ChatInteractionButtonsComponent({
	content,
	className,
	icon,
	setCopiedMessageIndex,
	aiMessage,
	feedbackValue,
	hoveredMessageIndex,
}: {
	content: string;
	className: string;
	icon: FluentIcon;
	setCopiedMessageIndex: Function | null;
	aiMessage: ChatMessage;
	feedbackValue: string | null;
	hoveredMessageIndex?: number | null;
}) {
	const dispatch = useAppDispatch();
	const { getIdTokenClaims } = useAuth0();

	const handleFeedbackRequest = async (aiMessage: ChatMessage) => {
		var aiResponseMessages = aiMessage.content as AIChatbotResponseDTO;
		const responseFeedbackDto: AIResponseFeedbackDTO = {
			aiResponse: aiResponseMessages.aiResponseData,
			feedbackComments: "",
			isNegativeFeedback: feedbackValue == "Negative" ? true : false,
			isPositiveFeedback: feedbackValue == "Negative" ? false : true,
			userQuery: aiResponseMessages.userQuery,
		};

		const accessToken = await getAccessToken();
		accessToken &&
			dispatch(
				HandleAiResultFeedbackAsync(responseFeedbackDto, accessToken)
			);
	};

	const getAccessToken = async () => {
		try {
			const idToken = await getIdTokenClaims();
			return idToken?.__raw;
		} catch (error) {
			console.error(error);
			return null;
		}
	};

	const handleCopyClick = async (message: ChatMessage, index?: number) => {
		try {
			const textToCopy =
				message.type === "user"
					? String(message.content)
					: typeof message.content === "string"
					? message.content
					: (message.content as AIChatbotResponseDTO)
							.aiResponseData ?? "";
			await navigator.clipboard.writeText(textToCopy);
			if (typeof index === "number") {
				setCopiedMessageIndex && setCopiedMessageIndex(index);
				setTimeout(
					() => setCopiedMessageIndex && setCopiedMessageIndex(null),
					1500
				);
			}
		} catch (err) {
			console.error(err);
		}
	};

	return (
		<Tooltip content={content} relationship="label">
			<Button
				appearance="primary"
				size="small"
				className={className}
				onClick={() => {
					const messageIndex =
						hoveredMessageIndex !== null
							? hoveredMessageIndex
							: null;
					return feedbackValue !== null && feedbackValue !== undefined
						? handleFeedbackRequest(aiMessage)
						: handleCopyClick(
								aiMessage,
								messageIndex === null ? undefined : messageIndex
						  );
				}}
				icon={icon as any}
			></Button>
		</Tooltip>
	);
}
