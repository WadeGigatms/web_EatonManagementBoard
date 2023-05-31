import React, { useEffect, useState, useCallback } from "react"
import $ from 'jquery'
import DatePicker from 'react-datepicker'
import "react-datepicker/dist/react-datepicker.css"
import {
    URL_EPC_DATA,
    TH_TASKNO,
    TH_PN,
    TH_TIME,
    TH_PALLET,
    NAV_OUTPUT,
    NAV_REPORT,
    FORM_SPEC_TIMESTAMP,
    FORM_RANGE_TIMESTAMP,
    FORM_FROM_NOW,
    FORM_SELECT,
    FORM_START_DATE,
    FORM_END_DATE,
    FORM_TODAY,
    FORM_YESTERDAY,
    FORM_LAST_WEEK,
    FORM_LAST_MONTH,
    FORM_LAST_HALF_YEAR,
    FORM_LAST_YEAR,
    FORM_CONDITION,
    FORM_WO,
    FORM_PN,
    FORM_DATE,
    FORM_FULL,
    _report_search_form_modal_target
} from '../constants'
import moment from 'moment'

const PopupReportSearchForm = ({ setEpcFullDataDtos, setEpcFullDataJson }) => {
    const [formOption, setFormOption] = useState(-1)
    const [wo, setWo] = useState("")
    const [pn, setPn] = useState("")
    const [palletId, setPalletId] = useState("")
    const [dateSelect, setDateSelect] = useState(-1)
    const [start, setStart] = useState()
    const [end, setEnd] = useState()
    const [past, setPast] = useState(-1)
    const [error, setError] = useState("")
    const [enableSubmit, setEnableSubmit] = useState(false)
    const [outputParameter, setOutputParameter] = useState()

    const getRequest = useCallback((url) => {
        const getData = async () => {
            try {
                const response = await fetch(url)
                const { result, epcFullDataDtos } = await response.json()
                if (result === true && epcFullDataDtos) {
                    setEpcFullDataDtos(epcFullDataDtos)
                    const rows = epcFullDataDtos.map(row => ({
                        WO: row.wo,
                        PN: row.pn,
                        QTY: row.qty,
                        LINE: row.line,
                        PALLET_ID: row.pallet_id,
                        LOCATION: row.location,
                        TIMESTAMP: row.timestamp
                    }))
                    setEpcFullDataJson(rows)
                    $('#reportSearchFormModalTarget').modal('hide')
                }
                else {
                    setEpcFullDataDtos()
                    setEpcFullDataJson()
                    setError("無資料")
                }
            } catch (error) {
            }
        }
        getData()
    }, [])

    useEffect(() => {
        $('#reportSearchFormModalTarget').on('hide.bs.modal', handleHideModal)
        function handleHideModal() {
            setFormOption(-1)
            setError("")
        }
    }, [])

    useEffect(() => {
        if (outputParameter) {
            const api = getApiUrl(outputParameter)
            getRequest(api)
        }
    }, [outputParameter])

    function getApiUrl(data) {
        const url = window.location.origin + URL_EPC_DATA +
            "?wo=" + data.wo +
            "&pn=" + data.pn +
            "&palletId=" + data.palletId +
            "&startDate=" + data.startDate +
            "&endDate=" + data.endDate +
            "&pastDays=" + data.pastDays
        return url
    }

    function formOptionChange(e) {
        const value = parseInt(e.target.value, 10)
        setFormOption(value)
        setEnableSubmit(value !== -1 && dateSelect !== -1 ? true : false)
        initialize()
    }

    function woChange(e) {
        const value = e.target.value
        setWo(value)
        setEnableSubmit(value !== "" ? true : false)
    }

    function pnChange(e) {
        const value = e.target.value
        setPn(value)
        setEnableSubmit(value !== "" ? true : false)
    }

    function palletIdChange(e) {
        const value = e.target.value
        setPalletId(value)
        setEnableSubmit(value !== "" ? true : false)
    }

    function dateSelectChange(e) {
        const value = parseInt(e.target.value, 10)
        setDateSelect(value)
        setEnableSubmit(value !== -1 ? true : false)

        if (valuevalue === 0) {
            setStart(new Date())
        } else if (value === 1) {
            setStart(new Date())
            setEnd(new Date())
        } else if (value === 2) {
            setPast(0)
        }

        setWo("")
        setPn("")
        setPalletId("")
    }

    function startDateChange(date) {
        setStart(date)
    }

    function endDateChange(date) {
        setEnd(date)
    }

    function pastChange(e) {
        const value = parseInt(e.target.value, 10)
        setPast(value)
    }

    function initialize() {
        setWo("")
        setPn("")
        setPalletId("")
        setDateSelect(-1)
        setStart()
        setEnd()
        setPast(-1)
        setError("")
    }

    function handleSubmit() {
        const _start = start !== undefined ? moment(start).format("yyyy-MM-DD") : ""
        const _end = end !== undefined ? moment(end).format("yyyy-MM-DD") : ""
        const outputParameter = { wo: wo, pn: pn, palletId: palletId, startDate: _start, endDate: _end, pastDays: past }
        setOutputParameter(outputParameter)
    }

    function renderFormOption() {
        return <>
            <div className="form-group">
                <label>{FORM_CONDITION}</label>
                <select className="form-control" value={formOption} onChange={formOptionChange} >
                    <option value={parseInt(-1, 10)}>{FORM_SELECT}</option>
                    <option value={parseInt(0, 10)}>{FORM_WO}</option>
                    <option value={parseInt(1, 10)}>{FORM_PN}</option>
                    <option value={parseInt(2, 10)}>{TH_PALLET}</option>
                    <option value={parseInt(3, 10)}>{FORM_DATE}</option>
                </select>
            </div>
            {renderFormInput()}
        </>
    }

    function renderFormInput() {
        switch (parseInt(formOption, 10)) {
            case 0:
                return renderInputWoForm()
            case 1:
                return renderInputPnForm()
            case 2:
                return renderInputPallet()
            case 3:
                return renderDateSelectForm()
            default:
                break
        }
    }

    function renderInputWoForm() {
        return <>
            <div className="form-group">
                <label>{FORM_FULL}{TH_TASKNO}</label>
                <input type="text" className="form-control" value={wo} onChange={woChange} />
            </div>
        </>
    }

    function renderInputPnForm() {
        return <>
            <div className="form-group">
                <label>{FORM_FULL}{TH_PN}</label>
                <input type="text" className="form-control" value={pn} onChange={pnChange} />
            </div>
        </>
    }

    function renderInputPallet() {
        return <>
            <div className="form-group">
                <label>{FORM_FULL}{TH_PALLET}</label>
                <input type="text" className="form-control" value={palletId} onChange={palletIdChange} />
            </div>
        </>
    }

    function renderDateSelectForm() {
        return <>
            <div className="form-group">
                <label>{TH_TIME}</label>
                <select className="form-control" value={dateSelect} onChange={dateSelectChange} >
                    <option value={parseInt(-1, 10)}>{FORM_SELECT}</option>
                    <option value={parseInt(0, 10)}>{FORM_SPEC_TIMESTAMP}</option>
                    <option value={parseInt(1, 10)}>{FORM_RANGE_TIMESTAMP}</option>
                    <option value={parseInt(2, 10)}>{FORM_FROM_NOW}</option>
                </select>
            </div>
            <div className="form-group">
                {renderDateSelect(dateSelect)}
            </div>
        </>
    }

    function renderDateSelect(dateSelectMode) {
        switch (parseInt(dateSelectMode, 10)) {
            case -1:
                return <></>
            case 0:
                return renderDatePicker()
            case 1:
                return renderDateRangePicker()
            case 2:
                return renderSelectPastDays()
            default:
                break
        }
    }

    function renderDatePicker() {
        return <div className="input-group">
            <DatePicker
                className="form-control"
                dateFormat="yyyy-MM-dd"
                selected={start}
                onChange={(date) => startDateChange(date)} />
        </div>
    }

    function renderDateRangePicker() {
        return <>
            <div className="input-group">
                {FORM_START_DATE}
                <DatePicker
                    className="form-control"
                    dateFormat="yyyy-MM-dd"
                    selected={start}
                    onChange={(date) => startDateChange(date)}
                    selectsStart
                    startDate={start}
                    endDate={end} />
            </div>
            <div className="input-group">
                {FORM_END_DATE}
                <DatePicker
                    className="form-control"
                    dateFormat="yyyy-MM-dd"
                    selected={end}
                    onChange={(date) => endDateChange(date)}
                    selectsEnd
                    startDate={start}
                    endDate={end}
                    minDate={start} />
            </div>
        </>
    }

    function renderSelectPastDays() {
        let today = new Date()
        let yesterday = new Date()
        let lastWeek = new Date()
        let lastMonth = new Date()
        let lastHalfYear = new Date()
        let lastYear = new Date()
        yesterday.setDate(today.getDate() - 1)
        lastWeek.setDate(today.getDate() - 7)
        lastMonth.setMonth(today.getMonth() - 1)
        lastHalfYear.setMonth(today.getMonth() - 6)
        lastYear.setFullYear(today.getFullYear() - 1)
        const todayString = moment(today).format("yyyy-MM-DD")
        const yesterdayString = moment(yesterday).format("yyyy-MM-DD")
        const lastWeekString = moment(lastWeek).format("yyyy-MM-DD")
        const lastMonthString = moment(lastMonth).format("yyyy-MM-DD")
        const lastHalfYearString = moment(lastHalfYear).format("yyyy-MM-DD")
        const lastYearString = moment(lastYear).format("yyyy-MM-DD")

        return <>
            <div className="input-group">
                <select className="form-control" value={past} onChange={pastChange} >
                    <option value={parseInt(0, 10)}>{FORM_TODAY} - ({todayString})</option>
                    <option value={parseInt(1, 10)}>{FORM_YESTERDAY} - ({yesterdayString} ~ {todayString})</option>
                    <option value={parseInt(7, 10)}>{FORM_LAST_WEEK} - ({lastWeekString} ~ {todayString})</option>
                    <option value={parseInt(30, 10)}>{FORM_LAST_MONTH} - ({lastMonthString} ~ {todayString})</option>
                    <option value={parseInt(180, 10)}>{FORM_LAST_HALF_YEAR} - ({lastHalfYearString} ~ {todayString})</option>
                    <option value={parseInt(365, 10)}>{FORM_LAST_YEAR} - ({lastYearString} ~ {todayString})</option>
                </select>
            </div>
        </>
    }

    function renderSubmit(enableSubmit) {
        return <button type="button" className="btn btn-primary btn-block" onClick={handleSubmit} disabled={!enableSubmit}>{NAV_OUTPUT}{NAV_REPORT}</button>
    }

    return <div className="modal fade" id={_report_search_form_modal_target} tabIndex="-1" role="dialog" aria-labelledby="ModalCenterTitle" aria-hidden="true">
        <div className="modal-dialog modal-dialog-centered" role="document">
            <div className="modal-content">
                <div className="modal-header">
                    <h5 className="modal-title" id="ModalCenterTitle">{NAV_OUTPUT}</h5>
                    <button type="button" className="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div className="modal-body">
                    {renderFormOption()}
                    <label className="red">{error}</label>
                </div>
                <div className="modal-footer">
                    {renderSubmit(enableSubmit)}
                </div>
            </div>
        </div>
    </div>
}

export default PopupReportSearchForm