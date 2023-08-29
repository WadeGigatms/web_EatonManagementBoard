import React from 'react'
import {
    NAV_REPORT,
    TH_LINE,
    TH_TASKNO,
    TH_PN,
    TH_QTY,
    TH_PALLET,
    TH_LOCATION,
    TH_TIME,
    TH_DURATION
} from "./constants"

const Output = ({ epcFullDataDtos }) => {

    function render() {
        return <div className="card card-secondary card-display h-100">
            {renderHeader()}
            {renderBody()}
        </div>
    }

    function renderHeader() {
        return <div className="card-header">
            {NAV_REPORT}
        </div>
    }

    function renderBody() {
        if (!epcFullDataDtos) { return }

        var rows = []
        for (var i = 0; i < epcFullDataDtos.length; i++) {
            rows.push(<tr key={i}>
                <td className="th-width-5">{epcFullDataDtos[i].line}</td>
                <td className="th-width-15">{epcFullDataDtos[i].wo}</td>
                <td className="th-width-15">{epcFullDataDtos[i].pn}</td>
                <td className="th-width-5">{epcFullDataDtos[i].qty}</td>
                <td className="th-width-15">{epcFullDataDtos[i].pallet_id}</td>
                <td className="th-width-15">{epcFullDataDtos[i].location}</td>
                <td className="th-width-15">{epcFullDataDtos[i].timestamp}</td>
                <td className="th-width-15">{epcFullDataDtos[i].duration}</td>
            </tr>)
        }

        return <div className="card-body table-responsive p-0">
            <table className="table text-nowrap table-sticky">
                <thead>
                    <tr>
                        <th className="th-width-5">{TH_LINE}</th>
                        <th className="th-width-15">{TH_TASKNO}</th>
                        <th className="th-width-15">{TH_PN}</th>
                        <th className="th-width-5">{TH_QTY}</th>
                        <th className="th-width-15">{TH_PALLET}</th>
                        <th className="th-width-15">{TH_LOCATION}</th>
                        <th className="th-width-15">{TH_TIME}</th>
                        <td className="th-width-15">{TH_DURATION}</td>
                    </tr>
                </thead>
                <tbody>
                    {rows}
                </tbody>
            </table>
        </div>
    }

    return <>{render()}</>
}

export default Output