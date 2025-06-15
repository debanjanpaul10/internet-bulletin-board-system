import { useState } from "react";
import {
    Button,
    Dialog,
    DialogActions,
    DialogBody,
    DialogContent,
    DialogSurface,
    DialogTitle,
    DialogTrigger,
} from "@fluentui/react-components";
import { useNavigate } from "react-router-dom";

import {
    CreatePostPageConstants,
    HeaderPageConstants,
} from "@helpers/ibbs.constants";
import { useStyles } from "./styles";

function CancelModalComponent() {
    const styles = useStyles();
    const navigate = useNavigate();
    const { DialogContentConstants } = CreatePostPageConstants;

    const [isDialogOpen, setIsDialogOpen] = useState(false);

    /**
     * Handles the cancel click event to show confirmation dialog.
     */
    const handleCancelClick = () => {
        setIsDialogOpen(true);
    };

    /**
     * Handles the confirmation of cancellation and navigates to home page.
     */
    const handleConfirmCancel = () => {
        setIsDialogOpen(false);
        navigate(HeaderPageConstants.Headings.Home.Link);
    };

    return (
        <Dialog
            open={isDialogOpen}
            onOpenChange={(e, data) => setIsDialogOpen(data.open)}
        >
            <DialogTrigger>
                <Button
                    className={styles.cancelButton}
                    onClick={handleCancelClick}
                >
                    {"Cancel"}
                </Button>
            </DialogTrigger>
            <DialogSurface>
                <DialogBody>
                    <DialogTitle>
                        {DialogContentConstants.HeadingMessage}
                    </DialogTitle>
                    <DialogContent>
                        {DialogContentConstants.ConfirmationMessage}
                    </DialogContent>
                    <DialogActions>
                        <Button
                            className={styles.createButton}
                            onClick={() => setIsDialogOpen(false)}
                        >
                            {DialogContentConstants.NoButtonText}
                        </Button>
                        <Button
                            className={styles.cancelButton}
                            onClick={handleConfirmCancel}
                        >
                            {DialogContentConstants.YesButtonText}
                        </Button>
                    </DialogActions>
                </DialogBody>
            </DialogSurface>
        </Dialog>
    );
}

export default CancelModalComponent;
