import React from 'react'

const NavBar = () => {
    return <nav className="main-header navbar navbar-expand navbar-white navbar-light">
        <ul className="navbar-nav ml-auto">
            <li className="nav-item">
                <a className="nav-link" data-widget="pushmenu" href="/" role="button">
                    <i className="fas fa-bars" />
                </a>
            </li>
        </ul>
    </nav>
}

export default NavBar