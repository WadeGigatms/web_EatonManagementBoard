import React from 'react'
import {
    _timeline_modal_target,
} from '../constants'

const EpcItem = ({ epc, showTransTime, setTimelineEpc }) => {

    function handleClick() {
        setTimelineEpc(epc)
    }

    function render() {
        const modalTarget = "#" + _timeline_modal_target
        const trClass = () => {
            if (epc.readerId === "Handheld") {
                return "";
            }
            switch (epc.epcState) {
                case "OK":
                    return ""
                case "NG":
                    return "tr-bg-y"
                case "Return":
                    return "tr-bg-r"
                default:
                    return ""
            }
        }
        if (showTransTime === true) {
            if (epc.error === "") {
                return <tr onClick={handleClick} data-toggle="modal" data-target={modalTarget} className={trClass(epc)} >
                    <td onClick={handleClick} width="5%">{epc.line}</td>
                    <td onClick={handleClick} width="35%">{epc.wo}</td>
                    <td onClick={handleClick} width="35%">{epc.pn}</td>
                    <td onClick={handleClick} width="15%">{epc.qty}</td>
                    <td onClick={handleClick} width="10%">{epc.transTime} {epc.readerId === "ManualTerminal" ? "m" : ""}</td>
                </tr>
            }
            else {
                return <tr onClick={handleClick} data-toggle="modal" data-target={modalTarget} className={trClass(epc)}>
                    <td onClick={handleClick} width="90%" colSpan="4">{epc.error}</td>
                    <td onClick={handleClick} width="10%">{epc.transTime}</td>
                </tr>
            }
        }
        else {
            if (epc.error === "") {
                return <tr onClick={handleClick} data-toggle="modal" data-target={modalTarget} className={trClass(epc)} >
                    <td onClick={handleClick} width="5%">{epc.line}</td>
                    <td onClick={handleClick} width="20%">{epc.wo}</td>
                    <td onClick={handleClick} width="35%">{epc.pn}</td>
                    <td onClick={handleClick} width="5%">{epc.qty}</td>
                    <td onClick={handleClick} width="35%">{epc.barcode}</td>
                </tr>
            }
            else {
                return <tr onClick={handleClick} data-toggle="modal" data-target={modalTarget} className={trClass(epc)}>
                    <td onClick={handleClick} width="100%" colSpan="4">{epc.error}</td>
                </tr>
            }
        }
    }

    return <>{render()}</>
}

export default EpcItem