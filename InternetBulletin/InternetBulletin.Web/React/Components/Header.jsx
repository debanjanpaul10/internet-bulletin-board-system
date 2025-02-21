import React, { useState } from "react";
import { NavLink } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { HeaderPageConstants } from "@helpers/Constants";
import { faHome, faMoon, faSun } from "@fortawesome/free-solid-svg-icons";

/**
 * @component
 * Header component that renders the navigation bar.
 * 
 * @returns {JSX.Element} The rendered component.
 */
function Header() {
    const [isDarkMode, setIsDarkMode] = useState(false);
    const activeStyle = { color: "#F15B2A" }
    const { Headings } = HeaderPageConstants;

    /**
     * Hanldes the dark mode - light moddle toggle.
     */
    const toggleDarkMode = () => {
        setIsDarkMode(!isDarkMode);
        document.body.classList.toggle('dark-mode', !isDarkMode);
    };

    /**
     * Handles the icon rendering.
     * @param {bool} isDarkMode The boolean flag for dark mode.
     * @returns {string} The icon props classes.
     */
    const handleShowIcon = (isDarkMode) => {
        var extraIcon = isDarkMode ? 'fa fa-sun-o lightgrey' : 'fa fa-moon-o lightgrey';
        return `buttonStyle ${extraIcon} p-2 mt-2`;
    }

    return (
        <nav className="navbar navbar-expand-lg navbar-dark bg-dark p-2">
            <div className="navbar-nav">
                <NavLink to={Headings.Home.Link} className="nav-link">
                    <FontAwesomeIcon className="buttonStyle" icon={faHome} />
                </NavLink>
            </div>
            <div className="navbar-nav ml-right">
                <NavLink to={Headings.Login.Link} activeStyle={activeStyle} className="nav-link">
                    {Headings.Login.Name}
                </NavLink>
                <NavLink to={Headings.Register.Link} activeStyle={activeStyle} className="nav-link">
                    {Headings.Register.Name}
                </NavLink>
                <icon onClick={toggleDarkMode} className={handleShowIcon(isDarkMode)}>
                </icon>
            </div>
        </nav>

    )
}

export default Header;