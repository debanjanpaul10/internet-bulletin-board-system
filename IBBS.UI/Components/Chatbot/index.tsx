import { useAuth0 } from "@auth0/auth0-react";
import { useEffect, useState } from "react";
import {
	Button,
	Spinner,
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
	CopyRegular,
	ThumbLikeRegular,
	ThumbDislikeRegular,
} from "@fluentui/react-icons";
import { Action, ThunkDispatch } from "@reduxjs/toolkit";

import { useAppDispatch, useAppSelector } from "@/index";
import { UserQueryRequestDTO } from "@/Models/DTOs/user-query-request.dto";
import { HandleChatbotResponseAsync } from "@/Store/AiServices/Actions";
import { useStyles } from "./styles";
import { ChatbotConstants } from "@/Helpers/ibbs.constants";
import { AIChatbotResponseDTO } from "@/Models/DTOs/ai-chatbot-response.dto";
import { renderSafeMarkdown } from "@/Helpers/markdown.utility";
import FollowupQuestionsComponent from "./Components/FollowupQuestions";
import TextType from "@/Animations/TextType";
import ChatInteractionButtonsComponent from "./Components/ChatInteractionButtons";
import { ChatMessage } from "@/types/chatmessage";

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
	const [messages, setMessages] = useState<Array<ChatMessage>>([]);
	const [isLoading, setIsLoading] = useState(false);
	const [chatResponse, setChatResponse] = useState({});
	const [hoveredMessageIndex, setHoveredMessageIndex] = useState<
		number | null
	>(null);
	const [showFollowups, setShowFollowups] = useState<boolean>(true);
	const [completedMessageIndexes, setCompletedMessageIndexes] = useState<
		Record<number, boolean>
	>({});
	const [copiedMessageIndex, setCopiedMessageIndex] = useState<number | null>(
		null
	);

	useEffect(() => {
		if (chatResponse !== ChatbotResponseStoreData) {
			if (Object.values(ChatbotResponseStoreData).length > 0) {
				setChatResponse(ChatbotResponseStoreData);
			} else {
				setChatResponse({});
			}
		}
	}, [ChatbotResponseStoreData]);

	const sendMessage = async (text: string) => {
		if (!text.trim()) return;

		const userMessage = {
			type: "user" as const,
			content: text,
			timestamp: new Date(),
		};
		setMessages((prev) => [...prev, userMessage]);
		setUserQuery("");
		setIsLoading(true);
		setShowFollowups(false);

		try {
			const userQueryRequest: UserQueryRequestDTO = {
				userQuery: userMessage.content,
			};
			const accessToken = await getAccessToken();
			if (accessToken) {
				const aiData = (await (
					dispatch as ThunkDispatch<any, any, Action>
				)(
					HandleChatbotResponseAsync(userQueryRequest, accessToken)
				)) as AIChatbotResponseDTO | null;

				if (aiData) {
					const botMessage = {
						type: "bot" as const,
						content: aiData,
						timestamp: new Date(),
					};
					setMessages((prev) => [...prev, botMessage]);
					setShowFollowups(true);
				}
				setIsLoading(false);
			}
		} catch (error) {
			console.error(error);
			setIsLoading(false);
		}
	};

	const sendChatbotResponse = async (event: any) => {
		event.preventDefault();
		await sendMessage(userQuery);
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
		setShowFollowups(false);
	};
	const closeChat = () => setIsChatOpen(false);

	const lastBotMessage = [...messages]
		.reverse()
		.find((m) => m.type === "bot");

	const handleTypingComplete = (index: number) => {
		setCompletedMessageIndexes((prev) => ({ ...prev, [index]: true }));
	};

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
								onMouseEnter={() =>
									setHoveredMessageIndex(index)
								}
								onMouseLeave={() =>
									setHoveredMessageIndex(null)
								}
							>
								<div
									className={mergeClasses(
										styles.messageBubble,
										message.type === "user"
											? styles.userBubble
											: styles.botBubble
									)}
								>
									{message.type === "user" ? (
										<>
											<div>{String(message.content)}</div>
											{hoveredMessageIndex === index && (
												<ChatInteractionButtonsComponent
													className={mergeClasses(
														styles.copyButton,
														styles.copyButtonVisible
													)}
													content={
														ChatbotConstants
															.ChatbotWindow
															.CopyRequestTooltip
													}
													icon={
														(<CopyRegular />) as any
													}
													setCopiedMessageIndex={
														setCopiedMessageIndex
													}
													aiMessage={message}
													feedbackValue={null}
												/>
											)}
											{copiedMessageIndex === index && (
												<span
													className={
														styles.copiedBadge
													}
												>
													Copied!
												</span>
											)}
										</>
									) : (
										<>
											<TextType
												text={renderSafeMarkdown(
													typeof message.content ===
														"string"
														? message.content
														: (
																message.content as AIChatbotResponseDTO
														  ).aiResponseData ?? ""
												)}
												typingSpeed={10}
												pauseDuration={1500}
												showCursor={false}
												cursorCharacter="|"
												renderHtml={true}
												onTypingComplete={() =>
													handleTypingComplete(index)
												}
											/>
											{message.type === "bot" &&
												completedMessageIndexes[
													index
												] && (
													<div
														className={
															styles.botCopyFooter
														}
													>
														<ChatInteractionButtonsComponent
															className={
																styles.botActionsButton
															}
															content={
																ChatbotConstants
																	.ChatbotWindow
																	.CopyTooltip
															}
															icon={
																(
																	<CopyRegular />
																) as any
															}
															setCopiedMessageIndex={
																setCopiedMessageIndex
															}
															aiMessage={message}
															feedbackValue={null}
														/>
														{copiedMessageIndex ===
															index && (
															<span
																className={
																	styles.copiedInline
																}
															>
																Copied!
															</span>
														)}
														&nbsp;
														<ChatInteractionButtonsComponent
															className={
																styles.botActionsButton
															}
															content={
																ChatbotConstants
																	.ChatbotWindow
																	.LikeTooltip
															}
															icon={
																(
																	<ThumbLikeRegular />
																) as any
															}
															setCopiedMessageIndex={
																null
															}
															aiMessage={message}
															feedbackValue={
																"Positive"
															}
														/>
														&nbsp;
														<ChatInteractionButtonsComponent
															className={
																styles.botActionsButton
															}
															content={
																ChatbotConstants
																	.ChatbotWindow
																	.DislikeTooltip
															}
															icon={
																(
																	<ThumbDislikeRegular />
																) as any
															}
															setCopiedMessageIndex={
																null
															}
															aiMessage={message}
															feedbackValue={
																"Negative"
															}
														/>
													</div>
												)}
										</>
									)}
								</div>
							</div>
						))}

						{lastBotMessage && showFollowups && (
							<FollowupQuestionsComponent
								message={lastBotMessage}
								onSelect={(q) => {
									setShowFollowups(false);
									sendMessage(q);
								}}
							/>
						)}

						{isLoading && (
							<div className={styles.botMessage}>
								<div className={styles.loadingBotBubble}>
									<div
										style={{
											display: "flex",
											alignItems: "center",
											gap: "8px",
										}}
									>
										<Spinner size="tiny" />
										Typing...
									</div>
								</div>
							</div>
						)}
					</div>

					<div className={styles.chatInput}>
						<div className={styles.inputContainer}>
							<Textarea
								className={styles.textArea}
								value={userQuery}
								disabled={isLoading}
								onChange={(e) => setUserQuery(e.target.value)}
								placeholder="Type your message here..."
								resize="none"
								onKeyDown={(e) => {
									if (e.key === "Enter" && !e.shiftKey) {
										e.preventDefault();
										sendChatbotResponse(e);
									}
								}}
							/>
						</div>
						<Button
							className={styles.sendButton}
							onClick={sendChatbotResponse}
							disabled={!userQuery.trim() || isLoading}
							icon={
								!isLoading ? (
									<SendRegular />
								) : (
									<Spinner size="tiny" />
								)
							}
						/>
					</div>
				</div>
			)}
		</>
	);
}
