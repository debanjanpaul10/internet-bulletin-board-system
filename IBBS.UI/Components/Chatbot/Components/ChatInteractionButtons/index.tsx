import { Button, Tooltip } from "@fluentui/react-components";
import { FluentIcon } from "@fluentui/react-icons";

import { useAppDispatch } from "@/index";
import { AIChatbotResponseDTO } from "@/Models/DTOs/ai-chatbot-response.dto";
import { ChatMessage } from "@/types/chatmessage";
import { HandleAiResultFeedbackAsync } from "@/Store/AiServices/Actions";
import { useAuth0 } from "@auth0/auth0-react";
import { AIResponseFeedbackDTO } from "@/Models/DTOs/ai-response-feedback.dto";

export default function ChatInteractionButtonsComponent({
	content,
	className,
	icon,
	setCopiedMessageIndex,
	aiMessage,
	feedbackValue,
}: {
	content: string;
	className: string;
	icon: FluentIcon;
	setCopiedMessageIndex: Function | null;
	aiMessage: ChatMessage;
	feedbackValue: string | null;
}) {
	const dispatch = useAppDispatch();
	const { getIdTokenClaims } = useAuth0();

	async function handleFeedbackRequest(aiMessage: ChatMessage) {
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
	}

	const getAccessToken = async () => {
		try {
			const idToken = await getIdTokenClaims();
			return idToken?.__raw;
		} catch (error) {
			console.error(error);
			return null;
		}
	};

	async function handleCopyClick(message: ChatMessage, index?: number) {
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
	}

	return (
		<Tooltip content={content} relationship="label">
			<Button
				appearance="primary"
				size="small"
				className={className}
				onClick={() =>
					feedbackValue !== null || feedbackValue !== undefined
						? handleFeedbackRequest(aiMessage)
						: handleCopyClick(aiMessage)
				}
				icon={icon as any}
			></Button>
		</Tooltip>
	);
}
