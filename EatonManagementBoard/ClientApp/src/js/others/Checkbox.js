import React, { useEffect, useState } from 'react'
import {
    TH_QTY,
    TH_LINE,
    TH_TASKNO,
    TH_PN,
    TH_PALLET_ID,
} from '../constants'

const Checkbox = ({ checkbox, setCheckbox }) => {
    const [lineChecked, setLineChecked] = useState(true)
    const [tasknoChecked, setTasknoChecked] = useState(true)
    const [pnChecked, setPnChecked] = useState(true)
    const [qtyChecked, setQtyChecked] = useState(true)
    const [palletIdChecked, setPalletIdChecked] = useState(true)

    const lineCheckedChange = () => {
        setLineChecked(!lineChecked)
        updateCheckbox()
    }
    const tasknoCheckedChange = () => {
        setTasknoChecked(!tasknoChecked)
        updateCheckbox()
    }
    const pnCheckedChange = () => {
        setPnChecked(!pnChecked)
        updateCheckbox()
    }
    const qtyCheckedChange = () => {
        setQtyChecked(!qtyChecked)
        updateCheckbox()
    }
    const palletIdCheckedChange = () => {
        setPalletIdChecked(!palletIdChecked)
        updateCheckbox()
    }

    function updateCheckbox() {
        const newCheckbox = { lineChecked, tasknoChecked, pnChecked, qtyChecked, palletIdChecked }
        setCheckbox(newCheckbox)
    }

    return <div className="dropdown">
        <i className="fa fa-gear" data-toggle="dropdown"></i>
        <div className="dropdown-menu">
            <span className="dropdown-header">顯示資料欄位</span>
            <div className="dropdown-divider"></div>
            <div className="col">
                <div className="icheck-primary">
                    <input id="line" type="checkbox" onChange={lineCheckedChange} checked={checkbox.lineChecked} />
                    <label htmlFor="line">{TH_LINE}</label>
                </div>
            </div>
            <div className="col">
                <div className="icheck-primary">
                    <input id="taskno" type="checkbox" onChange={tasknoCheckedChange} checked={checkbox.tasknoChecked} />
                    <label htmlFor="taskno">{TH_TASKNO}</label>
                </div>
            </div>
            <div className="col">
                <div className="icheck-primary">
                    <input id="pn" type="checkbox" onChange={pnCheckedChange} checked={checkbox.pnChecked} />
                    <label htmlFor="pn">{TH_PN}</label>
                </div>
            </div>
            <div className="col">
                <div className="icheck-primary">
                    <input id="qty" type="checkbox" onChange={qtyCheckedChange} checked={checkbox.qtyChecked} />
                    <label htmlFor="qty">{TH_QTY}</label>
                </div>
            </div>
            <div className="col">
                <div className="icheck-primary">
                    <input id="palletId" type="checkbox" onChange={palletIdCheckedChange} checked={checkbox.palletIdChecked} />
                    <label htmlFor="palletId">{TH_PALLET_ID}</label>
                </div>
            </div>
        </div>
    </div>
}

export default Checkbox