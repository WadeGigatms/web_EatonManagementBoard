import React, { useState } from 'react'
import { Link } from 'react-router-dom'
import {
    SIDEBAR_BRAND,
    SIDEBAR_DASHBOARD,
    SIDEBAR_3F,
    SIDEBAR_2F,
    SIDEBAR_1F_TEMP,
    SIDEBAR_1F,
    SIDEBAR_A,
    SIDEBAR_B,
    SIDEBAR_C,
    SIDEBAR_D,
    SIDEBAR_E,
    SIDEBAR_F,
    SIDEBAR_G,
    BLOCK_H,
    Block_I,
} from '../js/constants'

const SideBar = () => {
    const [active, setActive] = useState(0)
    let dashboardClass = active === 1 ? "nav-link active" : "nav-link"
    let thdFloorClass = active === 2 ? "nav-link active" : "nav-link"
    let sndFloorClass = active === 3 ? "nav-link active" : "nav-link"
    let fstFloorTempClass = active === 4 ? "nav-link active" : "nav-link"
    let fstFloorClass = active === 5 ? "nav-link active" : "nav-link"
    let aClass = active === 6 ? "nav-link active" : "nav-link"
    let bClass = active === 7 ? "nav-link active" : "nav-link"
    let cClass = active === 8 ? "nav-link active" : "nav-link"
    let dClass = active === 9 ? "nav-link active" : "nav-link"
    let eClass = active === 10 ? "nav-link active" : "nav-link"
    let fClass = active === 11 ? "nav-link active" : "nav-link"
    let gClass = active === 12 ? "nav-link active" : "nav-link"
    let hClass = active === 13 ? "nav-link active" : "nav-link"
    let iClass = active === 14 ? "nav-link active" : "nav-link"

    function pageClick(page) {
        setActive(page)
    }

    return <aside className="main-sidebar sidebar-dark-primary elevation-4">
        <Link to="/" className="brand-link" onClick={() => pageClick(0)}>
            <span className="brand-text font-weight-light"></span>
        </Link>
        <div className="sidebar">
            <nav className="mt-2">
                <ul className="nav nav-pills nav-sidebar flex-column" data-widget="treeview" role="menu" data-accordion="false">
                    <li className="nav-item">
                        <Link to="/dashboard" className={dashboardClass} onClick={() => pageClick(1)} >
                            <i className="nav-icon fas fa-table"></i>
                            <p></p>
                        </Link>
                    </li>
                    {/*<li className="nav-item">
                        <Link to="/dashboard" className={thdFloorClass} onClick={() => pageClick(2)} >
                            <i className="nav-icon fas fa-box"></i>
                            <p>{SIDEBAR_3F}</p>
                        </Link>
                    </li>
                    <li className="nav-item">
                        <Link to="/dashboard" className={sndFloorClass} onClick={() => pageClick(3)} >
                            <i className="nav-icon fas fa-box"></i>
                            <p>{SIDEBAR_2F}</p>
                        </Link>
                    </li>
                    <li className="nav-item">
                        <Link to="/dashboard" className={fstFloorTempClass} onClick={() => pageClick(4)} >
                            <i className="nav-icon fas fa-box"></i>
                            <p>{SIDEBAR_1F_TEMP}</p>
                        </Link>
                    </li>
                    <li className="nav-item menu-is-opening menu-open">
                        <Link to="/dashboard" className={fstFloorClass} onClick={() => pageClick(5)} >
                            <i className="nav-icon fas fa-box"></i>
                            <p>{SIDEBAR_1F}</p>
                            <i className="fas fa-angle-left right"></i>
                        </Link>
                        <ul className="nav nav-treeview">
                            <li className="nav-item">
                                <Link to="/dashboard" className={aClass} onClick={() => pageClick(6)} >
                                    <i className="nav-icon fas fa-boxes-stacked"></i>
                                    <p>{SIDEBAR_A}</p>
                                </Link>
                            </li>
                            <li className="nav-item">
                                <Link to="/dashboard" className={bClass} onClick={() => pageClick(7)} >
                                    <i className="nav-icon fas fa-boxes-stacked"></i>
                                    <p>{SIDEBAR_B}</p>
                                </Link>
                            </li>
                            <li className="nav-item">
                                <Link to="/dashboard" className={cClass} onClick={() => pageClick(8)} >
                                    <i className="nav-icon fas fa-boxes-stacked"></i>
                                    <p>{SIDEBAR_C}</p>
                                </Link>
                            </li>
                            <li className="nav-item">
                                <Link to="/dashboard" className={dClass} onClick={() => pageClick(9)} >
                                    <i className="nav-icon fas fa-boxes-stacked"></i>
                                    <p>{SIDEBAR_D}</p>
                                </Link>
                            </li>
                            <li className="nav-item">
                                <Link to="/dashboard" className={eClass} onClick={() => pageClick(10)} >
                                    <i className="nav-icon fas fa-boxes-stacked"></i>
                                    <p>{SIDEBAR_E}</p>
                                </Link>
                            </li>
                            <li className="nav-item">
                                <Link to="/dashboard" className={fClass} onClick={() => pageClick(11)} >
                                    <i className="nav-icon fas fa-boxes-stacked"></i>
                                    <p>{SIDEBAR_F}</p>
                                </Link>
                            </li>
                            <li className="nav-item">
                                <Link to="/dashboard" className={gClass} onClick={() => pageClick(12)} >
                                    <i className="nav-icon fas fa-boxes-stacked"></i>
                                    <p>{SIDEBAR_G}</p>
                                </Link>
                            </li>
                            <li className="nav-item">
                                <Link to="/dashboard" className={hClass} onClick={() => pageClick(13)} >
                                    <i className="nav-icon fas fa-boxes-stacked"></i>
                                    <p>{SIDEBAR_H}</p>
                                </Link>
                            </li>
                            <li className="nav-item">
                                <Link to="/dashboard" className={iClass} onClick={() => pageClick(14)} >
                                    <i className="nav-icon fas fa-boxes-stacked"></i>
                                    <p>{SIDEBAR_I}</p>
                                </Link>
                            </li>
                        </ul>
                    </li>*/}
                </ul>
            </nav>
        </div>
    </aside>
}

export default SideBar