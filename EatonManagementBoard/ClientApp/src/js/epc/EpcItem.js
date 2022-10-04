import React from 'react'

const EpcItem = ({ epc, showTransTime, setTimelineEpc }) => {

    function handleClick() {
        setTimelineEpc(epc)
    }

    function render(epc) {
        const dataTarget = "#timelineModalTarget"
        if (showTransTime === true) {
            return <tr onClick={handleClick} data-toggle="modal" data-target={dataTarget} >
                <td width="5%">{epc.line}</td>
                <td width="40%">{epc.wo}</td>
                <td width="40%">{epc.pn}</td>
                <td width="5%">{epc.qty}</td>
                <td width="10%">{epc.transTime}</td>
            </tr>

        }
        else {
            return <tr value={epc.epc} onClick={handleClick} data-toggle="modal" data-target={dataTarget}  >
                <td width="5%">{epc.line}</td>
                <td width="45%">{epc.wo}</td>
                <td width="45%">{epc.pn}</td>
                <td width="5%">{epc.qty}</td>
            </tr>
        }
    }

    return <>{render(epc)}</>
}

export default EpcItem