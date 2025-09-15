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
	DismissRegular,
	ArrowMaximizeRegular,
	ArrowMinimizeRegular,
	ArrowClockwiseRegular,
	SendRegular,
	HandWaveFilled,
	CopyRegular,
	ThumbLikeRegular,
	ThumbDislikeRegular,
	AgentsColor,
	Agents32Color,
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
	const LookupMasterStoreData = useAppSelector(
		(state) => state.CommonReducer.lookupMasterData
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
	const [showSamplePrompts, setShowSamplePrompts] = useState<boolean>(true);
	const [completedMessageIndexes, setCompletedMessageIndexes] = useState<
		Record<string, boolean>
	>(() => {
		if (typeof window !== "undefined") {
			const saved = localStorage.getItem("completedChatMessages");
			return saved ? JSON.parse(saved) : {};
		}
		return {};
	});
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

	const GetAccessTokenAsync = async () => {
		try {
			const idToken = await getIdTokenClaims();
			return idToken?.__raw;
		} catch (error) {
			console.error(error);
			return null;
		}
	};

	// Generate a unique ID for messages
	const generateMessageId = () => {
		return `msg_${Date.now()}_${Math.random().toString(36).substr(2, 9)}`;
	};

	const sendMessage = async (text: string) => {
		if (!text.trim()) return;

		const userMessage = {
			id: generateMessageId(),
			type: "user" as const,
			content: text,
			timestamp: new Date(),
		};
		setMessages((prev) => [...prev, userMessage]);
		setUserQuery("");
		setIsLoading(true);
		setShowFollowups(false);
		setShowSamplePrompts(false);

		try {
			const userQueryRequest: UserQueryRequestDTO = {
				userQuery: userMessage.content,
			};
			const accessToken = await GetAccessTokenAsync();
			if (accessToken) {
				const aiData = (await (
					dispatch as ThunkDispatch<any, any, Action>
				)(
					HandleChatbotResponseAsync(userQueryRequest, accessToken)
				)) as AIChatbotResponseDTO | null;

				if (aiData) {
					const botMessage = {
						id: generateMessageId(),
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

	const refreshChat = () => {
		setMessages([]);
		setUserQuery("");
		setShowFollowups(false);
		setShowSamplePrompts(true);
		setCompletedMessageIndexes({});
		if (typeof window !== "undefined") {
			localStorage.removeItem("completedChatMessages");
		}
	};

	const lastBotMessage = [...messages]
		.reverse()
		.find((m) => m.type === "bot");

	return (
		<>
			{/* FLOATING CHAT ICON */}
			<Tooltip
				content={ChatbotConstants.ChatbotFloatingIconTooltip}
				relationship="label"
			>
				<Button
					className={mergeClasses(
						styles.chatIcon,
						isChatOpen && styles.hidden
					)}
					onClick={() => setIsChatOpen(!isChatOpen)}
					appearance="transparent"
				>
					<AgentsColor fontSize={30} />
				</Button>
			</Tooltip>

			{/* CHAT WINDOW */}
			{isChatOpen && (
				<div
					className={mergeClasses(
						styles.chatWindow,
						isMaximized && styles.chatWindowMaximized
					)}
				>
					<div className={styles.chatHeader}>
						<div className={styles.chatTitle}>
							<Agents32Color />
							&nbsp;
							{ChatbotConstants.ChatbotWindow.AgentName}
						</div>
						<div className={styles.headerButtons}>
							<Button
								className={styles.headerButton}
								appearance="transparent"
								onClick={() => setIsMaximized(!isMaximized)}
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
								onClick={() => setIsChatOpen(false)}
								icon={<DismissRegular />}
							/>
						</div>
					</div>

					{/* CHAT CONTAINER */}
					<div className={styles.chatContent}>
						{messages.length === 0 && (
							<div className={styles.welcomeMessage}>
								<HandWaveFilled />
								&nbsp;
								{
									ChatbotConstants.ChatbotWindow
										.AiAssistantGreetingMessage
								}
								{/* AI DISCLAIMER */}
								<br />
								<em className={styles.warningText}>
									{
										ChatbotConstants.ChatbotWindow
											.WarningMessage
									}
								</em>
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
													hoveredMessageIndex={index}
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
											{completedMessageIndexes[
												message.id
											] ? (
												<div
													dangerouslySetInnerHTML={{
														__html: renderSafeMarkdown(
															typeof message.content ===
																"string"
																? message.content
																: (
																		message.content as AIChatbotResponseDTO
																  )
																		.aiResponseData ??
																		""
														),
													}}
												/>
											) : (
												<TextType
													text={renderSafeMarkdown(
														typeof message.content ===
															"string"
															? message.content
															: (
																	message.content as AIChatbotResponseDTO
															  )
																	.aiResponseData ??
																	""
													)}
													typingSpeed={10}
													pauseDuration={1500}
													showCursor={false}
													cursorCharacter="|"
													renderHtml={true}
													onTypingComplete={() => {
														setCompletedMessageIndexes(
															(prev) => {
																const updated =
																	{
																		...prev,
																		[message.id]:
																			true,
																	};
																if (
																	typeof window !==
																	"undefined"
																) {
																	localStorage.setItem(
																		"completedChatMessages",
																		JSON.stringify(
																			updated
																		)
																	);
																}
																return updated;
															}
														);
													}}
												/>
											)}
											{message.type === "bot" &&
												completedMessageIndexes[
													message.id
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
															hoveredMessageIndex={
																index
															}
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
															hoveredMessageIndex={
																index
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
															hoveredMessageIndex={
																index
															}
														/>
													</div>
												)}
										</>
									)}
								</div>
							</div>
						))}

						{/* FOLLOWUP QUESTIONS */}
						{lastBotMessage &&
							showFollowups &&
							(lastBotMessage.content as AIChatbotResponseDTO)
								.followupQuestions!.length > 0 && (
								<FollowupQuestionsComponent
									messageList={
										(
											lastBotMessage.content as AIChatbotResponseDTO
										).followupQuestions
									}
									onSelect={(question) => {
										setShowFollowups(false);
										sendMessage(question);
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
										{
											ChatbotConstants.ChatbotWindow
												.ThinkingMessage
										}
									</div>
								</div>
							</div>
						)}
					</div>

					{/* PINNED SAMPLE PROMPTS */}
					{LookupMasterStoreData.length > 0 && showSamplePrompts && (
						<div className={styles.pinnedPromptsContainer}>
							<FollowupQuestionsComponent
								messageList={LookupMasterStoreData.filter(
									(x: any) => x.type === "SamplePrompts"
								).map((prompt: any) => prompt.keyValue)}
								onSelect={(question) => {
									sendMessage(question);
								}}
							/>
						</div>
					)}

					{/* INPUT CONTAINER */}
					<div className={styles.chatInput}>
						<div className={styles.inputContainer}>
							<Textarea
								className={styles.textArea}
								value={userQuery}
								disabled={isLoading}
								onChange={(event) =>
									setUserQuery(event.target.value)
								}
								placeholder={
									ChatbotConstants.ChatbotWindow
										.TypingPlaceholder
								}
								resize="none"
								onKeyDown={(event) => {
									if (
										event.key === "Enter" &&
										!event.shiftKey
									) {
										event.preventDefault();
										sendChatbotResponse(event);
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
