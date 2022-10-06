import React, { useEffect, useState, useRef } from 'react'
import {
    NAV_TITLE,
    SEARCH,
    CANCELSEARCH,
    NAV_TERMINAL,
    NAV_WAREHOUSE,
} from '../js/constants'
import Logo from '../img/eaton_logo.jpg'
import Dashboard from '../js/Dashboard'
import CarouselImage from '../js/others/CarouselImage'

const Content = ({ children }) => {
    const activeBtnClass = "btn btn-app btn-bg-white p-0 h-100"
    const inactiveBtnClass = "btn btn-app color-w p-0 h-100"
    const [showDashboard, setShowDashboard] = useState(true)
    const searchStateRef = useRef(false)
    const [searchState, setSearchState] = useState(false)
    const [searchParameter, setSearchParameter] = useState()
    const [searchBtnClass, setSearchBtnClass] = useState(inactiveBtnClass)
    const [warehouseBtnClass, setWarehouseBtnClass] = useState(activeBtnClass)
    const [terminalBtnClass, setTerminalBtnClass] = useState(inactiveBtnClass)
    const [idleState, setIdleState] = useState(false)
    const [idleTimer, setIdleTimer] = useState(0)
    const [idleMinutes, setIdleMinutes] = useState(5)

    useEffect(() => {
        window.addEventListener('click', handleWindowClick)
    }, [])

    useEffect(() => {
        const idleInterval = setInterval(() => {
            setIdleTimer(idleTimer + 1)
            if (idleState === false && idleTimer === idleMinutes) {
                setIdleState(true)
            }
        }, 1000)

        return () => clearInterval(idleInterval)
    }, [idleState, idleTimer, idleMinutes])

    useEffect(() => {
        if (searchParameter) {
            const { wo, pn, palletId } = searchParameter
            if (wo || pn || palletId) {
                setSearchState(true)
            }
            else {
                setSearchState(false)
            }
        }
    }, [searchParameter])

    useEffect(() => {
        if (showDashboard === true) {
            setWarehouseBtnClass(activeBtnClass)
            setTerminalBtnClass(inactiveBtnClass)
        }
        else {
            setWarehouseBtnClass(inactiveBtnClass)
            setTerminalBtnClass(activeBtnClass)
        }
    }, [showDashboard])

    useEffect(() => {
        if (searchState === false) {
            setSearchBtnClass(inactiveBtnClass)
        }
        else {
            setSearchBtnClass(activeBtnClass)
        }
    }, [searchState])

    function handleWindowClick() {
        setIdleTimer(0)
        setIdleState(false)
    }

    function handleWarehouseClick() {
        setShowDashboard(true)
    }

    function handleTerminalClick() {
        setShowDashboard(false)
    }

    function handleSearchClick() {
        if (searchStateRef.current === true) {
            searchStateRef.current = false
            setSearchState(false)
        }
    }

    function render() {
        return <div className="bg-eaton-b">
            {renderHeader()}
            {renderBody()}
        </div>
    }

    function renderHeader() {
        return <section className="content-header content-header-p-2 vh-10">
            <div className="row h-100">
                <div className="col-sm-4 h-100">
                    <img src={Logo} className="h-100" alt="logo"/>
                </div>
                <div className="col-sm-4 h-100">
                    <div className="text-title d-none d-sm-block h-100">{NAV_TITLE}</div>
                </div>
                <div className="col-sm-4 h-100">
                    <nav className="navbar navbar-expand h-100 p-0">
                        <ul className="navbar-nav ml-auto h-100">
                            <li className="nav-item nav-link h-100">
                                <button type="button" className={warehouseBtnClass} onClick={handleWarehouseClick} >
                                    <i className="fas fa-box"></i>
                                    <label className="navbar-item-text">{NAV_WAREHOUSE}</label>
                                </button>
                            </li>
                            <li className="nav-item nav-link h-100">
                                <button type="button" className={terminalBtnClass} onClick={handleTerminalClick} >
                                    <i className="fas fa-box"></i>
                                    <label className="navbar-item-text">{NAV_TERMINAL}</label>
                                </button>
                            </li>
                            <li className="nav-item nav-link h-100">
                                <button type="button" className={searchBtnClass} data-toggle="modal" data-target="#searchModalTarget" onClick={handleSearchClick}>
                                    <i className="fas fa-search"></i>
                                    <label className="navbar-item-text">{searchState === false ? SEARCH : CANCELSEARCH}</label>
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
                <Dashboard
                    searchParameter={searchParameter}
                    setSearchParameter={setSearchParameter}
                    showDashboard={showDashboard}
                    searchStateRef={searchStateRef}
                    idleState={idleState} />
            </div>
        </section>
    }

    function renderCarousel() {
        return <CarouselImage />
    }

    return <>{idleState === false ? render() : renderCarousel()}</>
}

export default Content