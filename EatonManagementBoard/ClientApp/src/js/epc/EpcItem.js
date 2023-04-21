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
            if (epc.epcContext.reader_id === "Handheld") {
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
            return <tr onClick={handleClick} data-toggle="modal" data-target={modalTarget} className={trClass(epc)} >
                <td onClick={handleClick} className="th-width-5">{epc.epcDataDto.line}</td>
                <td onClick={handleClick} className="th-width-35">{epc.epcDataDto.wo}</td>
                <td onClick={handleClick} className="th-width-35">{epc.epcDataDto.pn}</td>
                <td onClick={handleClick} className="th-width-15">{epc.epcDataDto.qty}</td>
                <td onClick={handleClick} className="th-width-10">{epc.epcContext.timestamp} {epc.epcContext.reader_id === "ManualTerminal" ? "m" : ""}</td>
            </tr>
        }
        else {
            return <tr onClick={handleClick} data-toggle="modal" data-target={modalTarget} className={trClass(epc)} >
                <td onClick={handleClick} className="th-width-5">{epc.epcDataDto.line}</td>
                <td onClick={handleClick} className="th-width-20">{epc.epcDataDto.wo}</td>
                <td onClick={handleClick} className="th-width-35">{epc.epcDataDto.pn}</td>
                <td onClick={handleClick} className="th-width-5">{epc.epcDataDto.qty}</td>
                <td onClick={handleClick} className="th-width-35">{epc.epcDataDto.pallet_id}</td>
            </tr>
        }
    }

    return <>{render()}</>
}

export default EpcItem