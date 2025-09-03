import { useAuth0 } from "@auth0/auth0-react";

import { useStyles } from "./styles";
import { useAppDispatch } from "@/index";

export default function ChatbotComponent() {
    const styles = useStyles();
    const dispatch = useAppDispatch();

    const { isAuthenticated } = useAuth0();

    return isAuthenticated && <div>Hello world!</div>;
}
