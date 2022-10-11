﻿import React, { useState, useEffect } from 'react'
import {
    VERSION,
    FORM_VERSION,
    NAV_SETTING,
    SAVE,
    CANCEL,
    FORM_IDLE,
    FORM_CAROUSEL,
    SELECT_0,
    SELECT_5,
    SELECT_10,
    SELECT_30,
    SELECT_60,
    SELECT_300,
    SELECT_600,
    SELECT_900,
    SELECT_1800,
    SELECT_3600,
    SELECT_7200,
} from '../constants'

const PopUpSetting = ({ id, idleSeconds, setIdleSeconds, carouselMiniSeconds, setCarouselMiniSeconds }) => {
    const [idleInterval, setIdleInterval] = useState(idleSeconds)
    const [carouselInterval, setCaoruselInterval] = useState(carouselMiniSeconds/1000)

    useEffect(() => {
        window.jQuery('#' + id).on('hide.bs.modal', handleHideModal)
    }, [id])

    function handleHideModal() {

    }

    function handleSubmit() {
        setIdleSeconds(idleInterval)
        setCarouselMiniSeconds(carouselInterval*1000)
        window.jQuery('#' + id).modal('hide')
    }

    function handleIdleIntervalChange(e) {
        setIdleInterval(e.target.value)
    }

    function handleCarouselIntervalChange(e) {
        setCaoruselInterval(e.target.value)
    }

    function render() {
        return <div className="modal fade" id={id} tabIndex="-1" role="dialog" aria-labelledby="ModalCenterTitle" aria-hidden="true">
            <div className="modal-dialog modal-dialog-centered" role="document">
                <div className="modal-content">
                    <div className="modal-header">
                        <h5 className="modal-title" id="ModalCenterTitle">{NAV_SETTING}</h5>
                        <button type="button" className="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div className="modal-body">
                        <div className="form-group">
                            <div className="form-group">
                                <label>{FORM_VERSION}</label>
                                <label className="form-control" readOnly>{VERSION}</label>
                            </div>
                            <div className="form-group">
                                <label>{FORM_IDLE}</label>
                                <select className="form-control" value={idleInterval} onChange={handleIdleIntervalChange} >
                                    <option value={parseInt(0, 10)}>{SELECT_0}</option>
                                    <option value={parseInt(5, 10)}>{SELECT_5}</option>
                                    <option value={parseInt(10, 10)}>{SELECT_10}</option>
                                    <option value={parseInt(30, 10)}>{SELECT_30}</option>
                                    <option value={parseInt(60, 10)}>{SELECT_60}</option>
                                    <option value={parseInt(300, 10)}>{SELECT_300}</option>
                                    <option value={parseInt(600, 10)}>{SELECT_600}</option>
                                    <option value={parseInt(900, 10)}>{SELECT_900}</option>
                                    <option value={parseInt(1800, 10)}>{SELECT_1800}</option>
                                    <option value={parseInt(3600, 10)}>{SELECT_3600}</option>
                                    <option value={parseInt(7200, 10)}>{SELECT_7200}</option>
                                </select>
                            </div>
                            <div className="form-group">
                                <label>{FORM_CAROUSEL}</label>
                                <select className="form-control" value={carouselInterval} onChange={handleCarouselIntervalChange} >
                                    <option value={parseInt(5, 10)}>{SELECT_5}</option>
                                    <option value={parseInt(10, 10)}>{SELECT_10}</option>
                                    <option value={parseInt(30, 10)}>{SELECT_30}</option>
                                    <option value={parseInt(60, 10)}>{SELECT_60}</option>
                                </select>
                            </div>
                        </div>
                    </div>
                    <div className="modal-footer">
                        <button type="button" className="btn" data-dismiss="modal">{CANCEL}</button>
                        <button type="button" className="btn btn-primary" onClick={handleSubmit}>{SAVE}</button>
                    </div>
                </div>
            </div>
        </div>
    }

    return <>{render()}</>
}

export default PopUpSetting