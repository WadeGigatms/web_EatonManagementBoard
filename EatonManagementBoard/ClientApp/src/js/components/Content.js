import React, { useEffect, useState, useRef, useCallback } from 'react'
import $ from 'jquery'
import * as FileSaver from "file-saver"
import * as XLSX from "xlsx"
import Logo from '../../img/eaton_logo.jpg'
import Dashboard from '../Dashboard'
import CarouselImage from '../CarouselImage'
import Output from '../Output'
import PopupReportSearchForm from '../popup/PopupReportSearchForm'
import PopupDownloadConfirm from '../popup/PopupDownloadConfirm'
import {
    LoadIdleSeconds,
    LoadCarouselSeconds,
} from '../Cookie'
import {
    NAV_TITLE,
    NAV_TERMINAL,
    NAV_WAREHOUSE,
    NAV_SETTING,
    NAV_REPORT,
    FORM_SEARCH,
    FORM_CANCELSEARCH,
    NAV_HOME,
    NAV_DOWNLOAD,
} from '../constants'

const Content = ({ children }) => {
    const activeBtnClass = "btn btn-app p-0 h-100 btn-app-active"
    const inactiveBtnClass = "btn btn-app p-0 h-100"
    const [searchBtnClass, setSearchBtnClass] = useState(inactiveBtnClass)
    const [warehouseBtnClass, setWarehouseBtnClass] = useState(activeBtnClass)
    const [terminalBtnClass, setTerminalBtnClass] = useState(inactiveBtnClass)
    const [dashboardState, setDashboardState] = useState(true)
    const outputRef = useRef(false)
    const searchStateRef = useRef(false)
    const [searchState, setSearchState] = useState(false)
    const [searchParameter, setSearchParameter] = useState()
    const [idleState, setIdleState] = useState(false)
    const [idleTimer, setIdleTimer] = useState(0)
    const [idleSeconds, setIdleSeconds] = useState(0)
    const [carouselMiniSeconds, setCarouselMiniSeconds] = useState(5000)
    const [epcFullDataDtos, setEpcFullDataDtos] = useState()
    const [epcFullDataJson, setEpcFullDataJson] = useState()
    const fileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8";
    const fileExtension = ".xlsx";

    const exportToExcel = async () => {
        try {
            const worksheet = XLSX.utils.json_to_sheet(epcFullDataJson);
            worksheet["!cols"] = [
                { wch: 20 },
                { wch: 20 },
                { wch: 20 },
                { wch: 20 },
                { wch: 20 },
                { wch: 20 },
                { wch: 20 },            ]
            const workbook = { Sheets: { data: worksheet }, SheetNames: ["data"] }
            const excelBuffer = XLSX.write(workbook, { bookType: "xlsx", type: "array" })
            const data = new Blob([excelBuffer], { type: fileType })

            FileSaver.saveAs(data, "test" + fileExtension)
        } catch (error) {
            console.log(error)
        }
    };

    useEffect(() => {
        window.addEventListener('click', handleWindowClick)
        const cookieIdleSeconds = LoadIdleSeconds() === undefined ? 0 : LoadIdleSeconds()
        const cookieCarouselSeconds = LoadCarouselSeconds() === undefined ? 5000 : LoadCarouselSeconds()
        setIdleSeconds(cookieIdleSeconds)
        setCarouselMiniSeconds(cookieCarouselSeconds)
    }, [])

    useEffect(() => {
        const idleInterval = setInterval(() => {
            setIdleTimer(idleTimer + 1)
            if (idleState === false && parseInt(idleTimer, 10) === parseInt(idleSeconds, 10) && parseInt(idleSeconds) > 0) {
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
        outputRef.current = epcFullDataDtos ? true : false
    }, [epcFullDataDtos])

    useEffect(() => {
        if (dashboardState === true) {
            setWarehouseBtnClass(activeBtnClass)
            setTerminalBtnClass(inactiveBtnClass)
        }
        else {
            setWarehouseBtnClass(inactiveBtnClass)
            setTerminalBtnClass(activeBtnClass)
        }
    }, [dashboardState])

    useEffect(() => {
        if (searchState === false) {
            setSearchBtnClass(inactiveBtnClass)
        }
        else {
            setSearchBtnClass(activeBtnClass)
        }
    }, [searchState])

    function handleHideModal() {
        $('#threeAModalTarget').modal('hide')
        $('#threeBModalTarget').modal('hide')
        $('#twoAModalTarget').modal('hide')
        $('#searchFormModalTarget').modal('hide')
        $('#reportSearchFormModalTarget').modal('hide')
        $('#downloadConfirmFormModalTarget').modal('hide')
        $('#timelineModalTarget').modal('hide')
        $('#settingModalTarget').modal('hide')
        $('#moveModalTarget').modal('hide')
        $('#recoverModalTarget').modal('hide')
    }

    function handleWindowClick() {
        setIdleTimer(0)
        setIdleState(false)
    }

    function handleWarehouseClick() {
        setDashboardState(true)
    }

    function handleTerminalClick() {
        setDashboardState(false)
    }

    function handleSearchClick() {
        if (searchStateRef.current === true) {
            searchStateRef.current = false
            setSearchState(false)
        }
    }

    function disableOutputState() {
        outputRef.current = false
    }

    function render() {
        if (idleState === true) {
            return renderCarousel()
        } else {
            return <>
                {renderNav()}
                {renderBody()}
            </>
        }
    }

    function renderNav() {
        return <section className="content-header content-header-p-2 vh-10">
            <div className="row h-100">
                <div className="col-sm-4 h-100">
                    <img src={Logo} className="h-100" alt="logo" />
                </div>
                <div className="col-sm-4 h-100">
                    <div className="text-title d-none d-sm-block h-100">{NAV_TITLE}</div>
                </div>
                <div className="col-sm-4 h-100">
                    <nav className="navbar navbar-expand h-100 p-0">
                        {outputRef.current === false ? renderNavForDashboard() : renderNavForOutput() }
                    </nav>
                </div>
            </div>
        </section>
    }

    function renderNavForDashboard() {
        return <ul className="navbar-nav ml-auto h-100">
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
                <button type="button" className={searchBtnClass} data-toggle="modal" data-target="#searchFormModalTarget" onClick={handleSearchClick}>
                    <i className="fas fa-search"></i>
                    <label className="navbar-item-text">{searchState === false ? FORM_SEARCH : FORM_CANCELSEARCH}</label>
                </button>
            </li>
            <li className="nav-item nav-link h-100">
                <button type="button" className={inactiveBtnClass} data-toggle="modal" data-target="#settingModalTarget">
                    <i className="fas fa-cog"></i>
                    <label className="navbar-item-text">{NAV_SETTING}</label>
                </button>
            </li>
            <li className="nav-item nav-link h-100">
                <button type="button" className={inactiveBtnClass} data-toggle="modal" data-target="#reportSearchFormModalTarget">
                    <i className="fas fa-share"></i>
                    <label className="navbar-item-text">{NAV_REPORT}</label>
                </button>
            </li>
        </ul>
    }

    function renderNavForOutput() {
        return <ul className="navbar-nav ml-auto h-100">
            <li className="nav-item nav-link h-100">
                <button type="button" className={inactiveBtnClass} onClick={disableOutputState}>
                    <i className="fas fa-home"></i>
                    <label className="navbar-item-text">{NAV_HOME}</label>
                </button>
            </li>
            <li className="nav-item nav-link h-100">
                <button type="button" className={inactiveBtnClass} data-toggle="modal" data-target="#downloadConfirmModalTarget" >
                    <i className="fas fa-download"></i>
                    <label className="navbar-item-text">{NAV_DOWNLOAD}</label>
                </button>
            </li>
        </ul>
    }

    function renderBody() {
        return outputRef.current === false ? renderBodyForDashboard() : renderBodyForOutput()
    }

    function renderBodyForDashboard() {
        return <section className="content vh-90">
            <div className="container-fluid h-100">
                <Dashboard
                    dashboardState={dashboardState}
                    searchParameter={searchParameter}
                    setSearchParameter={setSearchParameter}
                    searchState={searchState}
                    searchStateRef={searchStateRef}
                    idleState={idleState}
                    idleSeconds={idleSeconds}
                    setIdleSeconds={setIdleSeconds}
                    carouselMiniSeconds={carouselMiniSeconds}
                    setCarouselMiniSeconds={setCarouselMiniSeconds} />
            </div>
        </section>
    }

    function renderBodyForOutput() {
        return <section className="content vh-90">
            <div className="container-fluid h-100 p-3">
                <Output epcFullDataDtos={epcFullDataDtos} />
            </div>
        </section>
    }

    function renderCarousel() {
        return <CarouselImage carouselMiniSeconds={carouselMiniSeconds} />
    }

    return <div className="bg-eaton-b">
        {render()}

        <PopupReportSearchForm setEpcFullDataDtos={setEpcFullDataDtos} setEpcFullDataJson={setEpcFullDataJson} />
        <PopupDownloadConfirm exportToExcel={exportToExcel} />
    </div>
}

export default Content