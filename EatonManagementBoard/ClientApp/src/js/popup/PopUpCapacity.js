import React, { useEffect, useState } from 'react'
import $ from 'jquery'
import {
    LIMIT,
    SAVE,
    CANCEL,
    SELECT,
} from '../constants'
import {
    SaveLocationCapacity,
} from '../others/Cookie'

const PopUpCapacity = ({ id, title, capacity, setCapacity }) => {
    const [currentCapacity, setCurrentCapacity] = useState(6)

    useEffect(() => {
        const hashTagId = "#" + id
        $(hashTagId).on('hide.bs.modal', handleHideModal)
        function handleHideModal() {
            setCurrentCapacity(capacity)
        }
    }, [id, capacity])

    useEffect(() => {
        setCurrentCapacity(capacity)
    }, [capacity])

    function handleChange(e) {
        setCurrentCapacity(e.target.value)
    }

    function handleSubmit() {
        const hashTagId = "#" + id
        $(hashTagId).modal('hide')
        setCapacity(currentCapacity)
        SaveLocationCapacity(id, currentCapacity)
    }

    function render() {
        return <div className="modal fade" id={id} tabIndex="-1" role="dialog" aria-labelledby="ModalCenterTitle" aria-hidden="true">
            <div className="modal-dialog modal-dialog-centered" role="document">
                <div className="modal-content">
                    <div className="modal-header">
                        <h5 className="modal-title" id="ModalCenterTitle">{title}</h5>
                        <button type="button" className="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div className="modal-body">
                        <div className="form-group">
                            <label>{LIMIT}</label>
                            <select className="form-control" value={currentCapacity} onChange={handleChange} >
                                <option value={parseInt(-1, 10)}>{SELECT}</option>
                                <option value={parseInt(0, 10)}>0</option>
                                <option value={parseInt(1, 10)}>1</option>
                                <option value={parseInt(2, 10)}>2</option>
                                <option value={parseInt(3, 10)}>3</option>
                                <option value={parseInt(4, 10)}>4</option>
                                <option value={parseInt(5, 10)}>5</option>
                                <option value={parseInt(6, 10)}>6</option>
                                <option value={parseInt(7, 10)}>7</option>
                                <option value={parseInt(8, 10)}>8</option>
                                <option value={parseInt(9, 10)}>9</option>
                                <option value={parseInt(10, 10)}>10</option>
                            </select>
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

export default PopUpCapacity