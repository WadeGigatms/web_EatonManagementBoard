import React, { useEffect, useState, useRef } from 'react'
import {
    NAV_TITLE,
    NAV_TERMINAL,
    NAV_WAREHOUSE,
    NAV_SETTING,
    SEARCH,
    CANCELSEARCH,
    _3A_modal_target,
    _2A_modal_target,
    _3B_modal_target,
    _search_modal_target,
    _timeline_modal_target,
    _setting_modal_target,
} from '../constants'
import Logo from '../../img/eaton_logo.jpg'
import Dashboard from '../Dashboard'
import CarouselImage from '../others/CarouselImage'

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
    const [idleSeconds, setIdleSeconds] = useState(300)
    const [carouselMiniSeconds, setCarouselMiniSeconds] = useState(5000)

    useEffect(() => {
        window.addEventListener('click', handleWindowClick)
    }, [])

    useEffect(() => {
        const idleInterval = setInterval(() => {
            setIdleTimer(idleTimer + 1)
            if (idleState === false && parseInt(idleTimer, 10) === parseInt(idleSeconds, 10)) {
                handleHideModal()
                setIdleState(true)
            }
        }, 1000)

        return () => clearInterval(idleInterval)
    }, [idleState, idleTimer, idleSeconds])

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

    function handleHideModal() {
        window.jQuery('#' + _3A_modal_target).modal('hide')
        window.jQuery('#' + _3B_modal_target).modal('hide')
        window.jQuery('#' + _2A_modal_target).modal('hide')
        window.jQuery('#' + _search_modal_target).modal('hide')
        window.jQuery('#' + _timeline_modal_target).modal('hide')
        window.jQuery('#' + _setting_modal_target).modal('hide')
    }

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

    function handleSettingClick() {

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
                            <li className="nav-item nav-link h-100">
                                <button type="button" className={inactiveBtnClass} data-toggle="modal" data-target="#settingModalTarget" onClick={handleSettingClick}>
                                    <i className="fas fa-cog"></i>
                                    <label className="navbar-item-text">{NAV_SETTING}</label>
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
                    idleState={idleState}
                    idleSeconds={idleSeconds}
                    setIdleSeconds={setIdleSeconds}
                    carouselMiniSeconds={carouselMiniSeconds}
                    setCarouselMiniSeconds={setCarouselMiniSeconds} />
            </div>
        </section>
    }

    function renderCarousel() {
        return <CarouselImage carouselMiniSeconds={carouselMiniSeconds} />
    }

    return <>{idleState === false ? render() : renderCarousel()}</>
}

export default Content