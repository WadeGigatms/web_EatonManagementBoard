import React from 'react'
import {
    TH_LINE,
    TH_TASKNO,
    TH_PN,
    TH_QTY,
    TH_TIME
} from "../constants"
import EpcItem from './EpcItem'

const EpcList = ({ result, epcs, showTransTime, setTimelineEpc }) => {

    function render(result, epcs) {
        if (result === true && epcs) {
            return <table className="table table-hover text-nowrap">
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
                        <th width="5%">{TH_LINE}</th>
                        <th width="40%">{TH_TASKNO}</th>
                        <th width="40%">{TH_PN}</th>
                        <th width="5%">{TH_QTY}</th>
                        <th width="10%">{TH_TIME}</th>
                    </tr>
                </thead>
            }
            else {
                return <thead>
                    <tr>
                        <th width="5%">{TH_LINE}</th>
                        <th width="45%">{TH_TASKNO}</th>
                        <th width="45%">{TH_PN}</th>
                        <th width="5%">{TH_QTY}</th>
                    </tr>
                </thead>
            }
        }
        else {
            return <></>
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
        } else {
            return <></>
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