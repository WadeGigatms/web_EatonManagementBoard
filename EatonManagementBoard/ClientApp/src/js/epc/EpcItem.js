import React from 'react'

const EpcItem = ({ epc, showTransTime, setTimelineEpc }) => {

    function handleClick() {
        setTimelineEpc(epc)
    }

    function render(showTransTime, epc) {
        const dataTarget = "#timelineModalTarget"
        const trClass = (epc) => {
            switch (epc.epcState) {
                case "OK":
                    return ""
                case "NG":
                    return "tr-bg-r"
                case "Return":
                    return "tr-bg-y"
                default:
                    return ""
            }
        }
        const tds = (showTransTime, epc) => {
            if (showTransTime === true) {
                if (epc.error === "") {
                    return <tr onClick={handleClick} data-toggle="modal" data-target={dataTarget} className={trClass(epc)} >
                        <td width="5%">{epc.line}</td>
                        <td width="40%">{epc.wo}</td>
                        <td width="40%">{epc.pn}</td>
                        <td width="5%">{epc.qty}</td>
                        <td width="10%">{epc.transTime}</td>
                    </tr>
                }
                else {
                    return <tr onClick={handleClick} data-toggle="modal" data-target={dataTarget} className={trClass(epc)}>
                        <td width="10%" colSpan="4">{epc.error}</td>
                        <td width="10%">{epc.transTime}</td>
                    </tr>
                }
            }
            else {
                if (epc.error === "") {
                    return <tr onClick={handleClick} data-toggle="modal" data-target={dataTarget} className={trClass(epc)} >
                        <td width="5%">{epc.line}</td>
                        <td width="45%">{epc.wo}</td>
                        <td width="45%">{epc.pn}</td>
                        <td width="5%">{epc.qty}</td>
                    </tr>
                }
                else {
                    return <tr onClick={handleClick} data-toggle="modal" data-target={dataTarget} className={trClass(epc)}>
                        <td colSpan="4">{epc.error}</td>
                    </tr>
                }
            }
        }

        return <>{tds(showTransTime, epc)}</>
    }

    return <>{render(showTransTime, epc)}</>
}

export default EpcItem