import { useAuth0 } from "@auth0/auth0-react";
import { useEffect, useState } from "react";
import {
	Button,
	Textarea,
	Tooltip,
	mergeClasses,
} from "@fluentui/react-components";
import {
	Chat24Regular,
	DismissRegular,
	ArrowMaximizeRegular,
	ArrowMinimizeRegular,
	ArrowClockwiseRegular,
	SendRegular,
	HandWaveFilled,
} from "@fluentui/react-icons";

import { useAppDispatch, useAppSelector } from "@/index";
import { UserQueryRequestDTO } from "@/Models/DTOs/user-query-request.dto";
import { HandleChatbotResponseAsync } from "@/Store/AiServices/Actions";
import { useStyles } from "./styles";
import { ChatbotConstants } from "@/Helpers/ibbs.constants";
import { AIChatbotResponseDTO } from "@/Models/DTOs/ai-chatbot-response.dto";

export default function ChatbotComponent() {
	const styles = useStyles();
	const dispatch = useAppDispatch();
	const { getIdTokenClaims } = useAuth0();

	const ChatbotResponseStoreData = useAppSelector(
		(state) => state.AiServicesReducer.chatbotResponse
	);

	const [isChatOpen, setIsChatOpen] = useState(false);
	const [isMaximized, setIsMaximized] = useState(false);
	const [userQuery, setUserQuery] = useState("");
	const [messages, setMessages] = useState<
		Array<{
			type: "user" | "bot";
			content: string | AIChatbotResponseDTO;
			timestamp: Date;
		}>
	>([]);
	const [isLoading, setIsLoading] = useState(false);
	const [chatResponse, setChatResponse] = useState({});

	useEffect(() => {
		if (chatResponse !== ChatbotResponseStoreData) {
			if (Object.values(ChatbotResponseStoreData).length > 0) {
				setChatResponse(ChatbotResponseStoreData);
			} else {
				setChatResponse({});
			}
		}
	}, [ChatbotResponseStoreData]);

	const sendChatbotResponse = async (event: any) => {
		event.preventDefault();
		if (!userQuery.trim()) return;

		const userMessage = {
			type: "user" as const,
			content: userQuery,
			timestamp: new Date(),
		};
		setMessages((prev) => [...prev, userMessage]);
		setUserQuery("");
		setIsLoading(true);

		try {
			const userQueryRequest: UserQueryRequestDTO = {
				userQuery: userMessage.content,
			};
			const accessToken = await getAccessToken();
			if (accessToken) {
				await dispatch(
					HandleChatbotResponseAsync(userQueryRequest, accessToken)
				);

				const botMessage = {
					type: "bot" as const,
					content: ChatbotResponseStoreData as AIChatbotResponseDTO,
					timestamp: new Date(),
				};
				setMessages((prev) => [...prev, botMessage]);
				setIsLoading(false);
			}
		} catch (error) {
			console.error(error);
			setIsLoading(false);
		}
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

	const toggleChat = () => setIsChatOpen(!isChatOpen);
	const toggleMaximize = () => setIsMaximized(!isMaximized);
	const refreshChat = () => {
		setMessages([]);
		setUserQuery("");
	};
	const closeChat = () => setIsChatOpen(false);

	return (
		<>
			{/* Floating Chat Icon */}
			<Tooltip
				content={ChatbotConstants.ChatbotFloatingIconTooltip}
				relationship="label"
			>
				<Button
					className={mergeClasses(
						styles.chatIcon,
						isChatOpen && styles.hidden
					)}
					onClick={toggleChat}
					appearance="transparent"
				>
					<Chat24Regular />
				</Button>
			</Tooltip>

			{/* Chat Window */}
			{isChatOpen && (
				<div
					className={mergeClasses(
						styles.chatWindow,
						isMaximized && styles.chatWindowMaximized
					)}
				>
					<div className={styles.chatHeader}>
						<div className={styles.chatTitle}>
							{ChatbotConstants.ChatbotWindow.AgentName}
						</div>
						<div className={styles.headerButtons}>
							<Button
								className={styles.headerButton}
								appearance="transparent"
								onClick={toggleMaximize}
								icon={
									isMaximized ? (
										<ArrowMinimizeRegular />
									) : (
										<ArrowMaximizeRegular />
									)
								}
							/>
							<Button
								className={styles.headerButton}
								appearance="transparent"
								onClick={refreshChat}
								icon={<ArrowClockwiseRegular />}
							/>
							<Button
								className={styles.headerButton}
								appearance="transparent"
								onClick={closeChat}
								icon={<DismissRegular />}
							/>
						</div>
					</div>
					<div className={styles.chatContent}>
						{messages.length === 0 && (
							<div className={styles.welcomeMessage}>
								<HandWaveFilled />
								&nbsp;
								{
									ChatbotConstants.ChatbotWindow
										.AiAssistantGreetingMessage
								}
							</div>
						)}

						{messages.map((message, index) => (
							<div
								key={index}
								className={mergeClasses(
									styles.messageContainer,
									message.type === "user"
										? styles.userMessage
										: styles.botMessage
								)}
							>
								<div
									className={mergeClasses(
										styles.messageBubble,
										message.type === "user"
											? styles.userBubble
											: styles.botBubble
									)}
								>
									{message.type === "user"
										? String(message.content)
										: typeof message.content === "string"
										? message.content
										: message.content.aIResponseData}
								</div>
							</div>
						))}

						{isLoading && (
							<div className={styles.botMessage}>
								<div className={styles.botBubble}>
									<div
										style={{
											display: "flex",
											alignItems: "center",
											gap: "8px",
										}}
									>
										<div
											style={{
												width: "16px",
												height: "16px",
												border: "2px solid #ccc",
												borderTop: "2px solid #0078d4",
												borderRadius: "50%",
												animation:
													"spin 1s linear infinite",
											}}
										/>
										Typing...
									</div>
								</div>
							</div>
						)}
					</div>

					<div className={styles.chatInput}>
						<div className={styles.inputContainer}>
							{isLoading ? (
								<div className={styles.thinkingIndicator}>
									<div
										style={{
											display: "flex",
											alignItems: "center",
											gap: "8px",
										}}
									>
										<div
											style={{
												width: "16px",
												height: "16px",
												border: "2px solid #ccc",
												borderTop: "2px solid #0078d4",
												borderRadius: "50%",
												animation:
													"spin 1s linear infinite",
											}}
										/>
										AI is thinking...
									</div>
								</div>
							) : (
								<Textarea
									className={styles.textArea}
									value={userQuery}
									onChange={(e) =>
										setUserQuery(e.target.value)
									}
									placeholder="Type your message here..."
									resize="none"
									onKeyDown={(e) => {
										if (e.key === "Enter" && !e.shiftKey) {
											e.preventDefault();
											sendChatbotResponse(e);
										}
									}}
								/>
							)}
						</div>
						<Button
							className={styles.sendButton}
							onClick={sendChatbotResponse}
							disabled={!userQuery.trim() || isLoading}
							icon={<SendRegular />}
						/>
					</div>
				</div>
			)}

			<style>{`
                @keyframes spin {
                    0% { transform: rotate(0deg); }
                    100% { transform: rotate(360deg); }
                }
            `}</style>
		</>
	);
}
