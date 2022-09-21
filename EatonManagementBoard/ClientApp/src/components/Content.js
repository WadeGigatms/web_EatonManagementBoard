﻿import React, { useState } from 'react'
import {
    NAV_TITLE,
    NAV_HOME,
    SEARCH,
    NAV_TERMINAL,
    NAV_WAREHOUSE
} from '../js/constants'
import Logo from '../img/eaton_logo.jpg'
import Dashboard from '../js/Dashboard'

const Content = ({ children }) => {
    const [showDashboard, setShowDashboard] = useState(true)

    function handleHomeClick() {
        setShowDashboard(true)
        window.location.href = "./"
    }

    function handleWarehouseClick() {
        setShowDashboard(true)
    }

    function handleTerminalClick() {
        setShowDashboard(false)
    }

    function render() {
        return <div className="bg-eaton-blue">
            {renderHeader()}
            {renderBody()}
        </div>
    }

    function renderHeader() {
        return <section className="content-header content-header-p-2 vh-10">
            <div className="row h-100">
                <div className="col-sm-4 h-100">
                    <img src={Logo} className="h-100" />
                </div>
                <div className="col-sm-4 h-100">
                    <div className="text-title d-none d-sm-block h-100">{NAV_TITLE}</div>
                </div>
                <div className="col-sm-4 h-100">
                    <nav className="navbar navbar-expand h-100 p-0">
                        <ul className="navbar-nav ml-auto h-100">
                            <li className="nav-item nav-link h-100">
                                <button type="button" className="btn btn-app color-w p-0 h-100" h-100 onClick={handleHomeClick} >
                                    <i className="fas fa-house"></i>
                                    <label className="navbar-item-text">{NAV_HOME}</label>
                                </button>
                            </li>
                            <li className="nav-item nav-link h-100">
                                <button type="button" className="btn btn-app color-w p-0 h-100" onClick={handleWarehouseClick} >
                                    <i className="fas fa-box"></i>
                                    <label className="navbar-item-text">{NAV_WAREHOUSE}</label>
                                </button>
                            </li>
                            <li className="nav-item nav-link h-100">
                                <button type="button" className="btn btn-app color-w p-0 h-100" onClick={handleTerminalClick} >
                                    <i className="fas fa-box"></i>
                                    <label className="navbar-item-text">{NAV_TERMINAL}</label>
                                </button>
                            </li>
                            <li className="nav-item nav-link h-100">
                                <button role="button" className="btn btn-app color-w p-0 h-100" data-toggle="modal" data-target="#searchModalTarget">
                                    <i className="fas fa-search"></i>
                                    <label className="navbar-item-text">{SEARCH}</label>
                                </button>
                            </li>
                        </ul>
                    </nav>
                </div>
            </div>
        </section>
    }

    function renderBody() {
        return <section className="content vh-90">
            <div className="container-fluid h-100">
                <Dashboard showDashboard={showDashboard} />
            </div>
        </section>
    }

    return <>{render()}</>
}

export default Content