import React from 'react'
import {
    TH_LINE,
    TH_TASKNO,
    TH_PN,
    TH_QTY,
    TH_TIME,
    TH_PALLET
} from "../constants"
import EpcItem from './EpcItem'

const EpcList = ({ result, epcs, showTransTime, setTimelineEpc }) => {

    function render(result, epcs) {
        if (result === true && epcs) {
            return <table className="table  text-nowrap table-sticky">
                {renderThead(result)}
                {renderTbody(result, epcs)}
                {renderTfoot()}
            </table>
        }
    }

    function renderThead(result) {
        if (result === true) {
            if (showTransTime === true) {
                return <thead>
                    <tr>
                        <th className="th-width-5">{TH_LINE}</th>
                        <th className="th-width-35">{TH_TASKNO}</th>
                        <th className="th-width-35">{TH_PN}</th>
                        <th className="th-width-15">{TH_QTY}</th>
                        <th className="th-width-10">{TH_TIME}</th>
                    </tr>
                </thead>
            }
            else {
                return <thead>
                    <tr>
                        <th className="th-width-5">{TH_LINE}</th>
                        <th className="th-width-20">{TH_TASKNO}</th>
                        <th className="th-width-35">{TH_PN}</th>
                        <th className="th-width-5">{TH_QTY}</th>
                        <th className="th-width-35">{TH_PALLET}</th>
                    </tr>
                </thead>
            }
        }
    }

    function renderTbody(result, epcs) {
        if (result === true && epcs) {
            var rows = []
            for (var i = 0; i < epcs.length; i++) {
                rows.push(<EpcItem
                    key={i}
                    epc={epcs[i]}
                    showTransTime={showTransTime}
                    setTimelineEpc={setTimelineEpc} />)
            }
            return <tbody>
                {rows}
            </tbody>
        }
    }

    function renderTfoot() {
        return <></>
    }

    return <>
        {render(result, epcs)}
    </>
}

export default EpcList