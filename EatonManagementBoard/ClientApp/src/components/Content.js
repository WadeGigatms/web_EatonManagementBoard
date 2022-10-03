import React, { useEffect, useState, useRef } from 'react'
import {
    NAV_TITLE,
    SEARCH,
    CANCELSEARCH,
    NAV_TERMINAL,
    NAV_WAREHOUSE,
    _search_modal_target,
} from '../js/constants'
import Logo from '../img/eaton_logo.jpg'
import Dashboard from '../js/Dashboard'

const Content = ({ children }) => {
    const [showDashboard, setShowDashboard] = useState(true)
    const searchStateRef = useRef(false)
    const [searchState, setSearchState] = useState(false)
    const [searchParameter, setSearchParameter] = useState()
    const [searchBtnClass, setSearchBtnClass] = useState("btn btn-app color-w p-0 h-100")

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
        if (searchState === false) {
            setSearchBtnClass("btn btn-app color-w p-0 h-100")
        }
        else {
            setSearchBtnClass("btn btn-app btn-search p-0 h-100")
        }
    }, [searchState])

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
                                <button role="button" className={searchBtnClass} data-toggle="modal" data-target="#searchModalTarget" onClick={handleSearchClick}>
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
                    searchStateRef={searchStateRef} />
            </div>
        </section>
    }

    return <>{render()}</>
}

export default Content