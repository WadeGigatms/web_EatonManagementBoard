import React from 'react'

const EpcItem = ({ epc, showTransTime, setTimelineEpc }) => {

    function handleClick() {
        setTimelineEpc(epc)
    }

    function render(epc) {
        const dataTarget = "#timelineModalTarget"
        if (epc.error !== null || epc.error !== "") {
            if (showTransTime === true) {
                return <tr onClick={handleClick} data-toggle="modal" data-target={dataTarget} >
                    <td className="col-sm-1">{epc.line}</td>
                    <td className="col-sm-4">{epc.wo}</td>
                    <td className="col-sm-4">{epc.pn}</td>
                    <td className="col-sm-1">{epc.qty}</td>
                    <td className="col-sm-2">{epc.transTime}</td>
                </tr>

            }
            else {
                return <tr value={epc.epc} onClick={handleClick} data-toggle="modal" data-target={dataTarget}  >
                    <td className="col-sm-1">{epc.line}</td>
                    <td className="col-sm-5">{epc.wo}</td>
                    <td className="col-sm-5">{epc.pn}</td>
                    <td className="col-sm-1">{epc.qty}</td>
                </tr>
            }
        }
        else {
            if (showTransTime === true) {
                return <tr onClick={handleClick} data-toggle="modal" data-target={dataTarget} >
                    <td className="col-sm-10">{epc.error}</td>
                    <td className="col-sm-2">{epc.transTime}</td>
                </tr>

            }
            else {
                return <tr value={epc.epc} onClick={handleClick} data-toggle="modal" data-target={dataTarget}  >
                    <td className="col-sm-12">{epc.error}</td>
                </tr>
            }
        }
    }

    return <>{render(epc)}</>
}

export default EpcItem