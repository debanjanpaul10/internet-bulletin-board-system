import { useState, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import {
    Button,
    Dialog,
    DialogSurface,
    DialogTitle,
    DialogBody,
    DialogContent,
    DialogActions,
} from "@fluentui/react-components";
import { useAuth0 } from "@auth0/auth0-react";

import { useStyles } from "./styles";
import {
    RewriteStoryWithAiAsync,
    HandlePostAiModerationTasksSuccess,
} from "@store/Posts/Actions";
import UserStoryRequestDtoModel from "@models/UserStoryRequestDto";
import {
    BlankTextErrorMessageConstant,
    CreatePostPageConstants,
} from "@helpers/ibbs.constants";
import Spinner from "@components/Common/Spinner";
import GradientText from "@animations/GradientText";
import AiButton from "@assets/Images/ai-icon.svg";

/**
 * A React component that provides AI-powered text rewriting functionality.
 * This component allows users to rewrite their text content using AI assistance,
 * displaying the rewritten content in a dialog and providing options to accept or reject the changes.
 *
 * @component
 * @param {Object} props - Component props
 * @param {string} props.originalText - The original text content to be rewritten
 * @param {Function} props.onTextChange - Callback function that is called when the user accepts the AI-rewritten text
 * @returns {JSX.Element} A button that triggers the AI rewrite process and a dialog to display the results
 *
 * @example
 * <RewriteTextComponent
 *   originalText="Your original text here"
 *   onTextChange={(newText) => handleTextChange(newText)}
 * />
 */
function RewriteTextComponent({ originalText, onTextChange }) {
    const styles = useStyles();
    const dispatch = useDispatch();
    const { user, isAuthenticated, getAccessTokenSilently } = useAuth0();

    const { AiContentConstants } = CreatePostPageConstants;

    const [isOpen, setIsOpen] = useState(false);
    const [isLoading, setIsLoading] = useState(false);
    const [hasRequestedRewrite, setHasRequestedRewrite] = useState(false);

    const AiRewrittenStoryStoreData = useSelector(
        (state) => state.PostsReducer.aiRewrittenStory
    );
    const IsRewriteLoadingStoreData = useSelector(
        (state) => state.PostsReducer.isRewriteLoading
    );

    // Reset AI response when component unmounts
    useEffect(() => {
        return () => {
            dispatch(HandlePostAiModerationTasksSuccess(null, null));
            setHasRequestedRewrite(false);
        };
    }, []);

    // Only show dialog when we have received a response and user has requested a rewrite
    useEffect(() => {
        if (
            AiRewrittenStoryStoreData &&
            AiRewrittenStoryStoreData !== originalText &&
            hasRequestedRewrite &&
            !IsRewriteLoadingStoreData
        ) {
            setIsOpen(true);
        }
    }, [
        AiRewrittenStoryStoreData,
        hasRequestedRewrite,
        IsRewriteLoadingStoreData,
    ]);

    /**
     * Acquires an access token silently using Auth0 for authentication.
     * @async
     * @returns {Promise<string>} A promise that resolves to the access token string
     * @throws {Error} If token acquisition fails
     */
    const getAccessToken = async () => {
        try {
            const token = await getAccessTokenSilently();
            return token;
        } catch (error) {
            console.error("Error getting access token:", error);
            return null;
        }
    };

    /**
     * Handles the AI text rewrite process.
     * Strips HTML tags from the original text and initiates the AI rewrite request.
     * @async
     * @throws {Error} If the AI rewrite process fails
     */
    const handleRewrite = async () => {
        const strippedContent = originalText.replace(/<[^>]*>?/gm, "").trim();
        if (strippedContent !== "") {
            setIsLoading(true);
            setHasRequestedRewrite(true);
            setIsOpen(true);
            try {
                const requestDto = new UserStoryRequestDtoModel(
                    strippedContent
                );
                const accessToken = await getAccessToken();
                dispatch(RewriteStoryWithAiAsync(requestDto, accessToken));
            } catch (error) {
                console.error("Error generating AI rewrite:", error);
                setHasRequestedRewrite(false);
            } finally {
                setIsLoading(false);
            }
        } else {
            alert(BlankTextErrorMessageConstant);
        }
    };

    /**
     * Handles the acceptance of the AI-rewritten text.
     * Updates the parent component with the new text and closes the dialog.
     */
    const handleAccept = () => {
        onTextChange(AiRewrittenStoryStoreData);
        setIsOpen(false);
        setHasRequestedRewrite(false);
    };

    /**
     * Handles the cancellation of the AI rewrite process.
     * Closes the dialog and resets the rewrite request state.
     */
    const handleCancel = () => {
        setIsOpen(false);
        setHasRequestedRewrite(false);
    };

    return (
        <>
            <Button
                onClick={handleRewrite}
                disabled={isLoading || IsRewriteLoadingStoreData}
                className={styles.rewriteButton}
            >
                <GradientText>
                    <img
                        src={AiButton}
                        style={{
                            height: "20px",
                            marginRight: "10px",
                        }}
                        alt="AI Star Icon"
                    />
                    {
                        CreatePostPageConstants.Headings.RewriteAIButtonTexts
                            .ButtonText
                    }
                </GradientText>
            </Button>

            <Dialog
                open={isOpen}
                onOpenChange={(e, data) => {
                    setIsOpen(data.open);
                    if (!data.open) {
                        setHasRequestedRewrite(false);
                    }
                }}
            >
                <DialogSurface className={styles.dialogContent}>
                    <DialogBody>
                        <DialogTitle>
                            {AiContentConstants.DialogHeader}
                        </DialogTitle>
                        <DialogContent>
                            {IsRewriteLoadingStoreData ? (
                                <Spinner isLoading={true} />
                            ) : (
                                <div className={styles.textContent}>
                                    {AiRewrittenStoryStoreData}
                                </div>
                            )}
                        </DialogContent>
                        <DialogActions className={styles.dialogActions}>
                            <Button
                                onClick={handleCancel}
                                className={styles.cancelButton}
                            >
                                {AiContentConstants.CancelButton}
                            </Button>
                            <Button
                                onClick={handleAccept}
                                className={styles.acceptChangeButton}
                            >
                                <GradientText>
                                    {AiContentConstants.AcceptButton}
                                </GradientText>
                            </Button>
                        </DialogActions>
                    </DialogBody>
                </DialogSurface>
            </Dialog>
        </>
    );
}

export default RewriteTextComponent;
