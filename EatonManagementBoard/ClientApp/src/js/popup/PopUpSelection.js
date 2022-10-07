import React, { useEffect, useState } from 'react'
import {
    LIMIT,
    SAVE,
    CANCEL,
    SELECT
} from '../constants'

const PopUpSelection = ({ id, title, limit, setLimit }) => {
    const [currentLimit, setCurrentLimit] = useState()

    useEffect(() => {
        if (limit) {
            setCurrentLimit(limit)
        }
    }, [limit])

    useEffect(() => {
        window.jQuery('#' + id).on('hide.bs.modal', handleHideModal)
    }, [id])

    function handleHideModal() {
        setCurrentLimit(limit)
    }

    function handleChange(e) {
        setCurrentLimit(e.target.value)
    }

    function handleSubmit() {
        setLimit(currentLimit)
        window.jQuery('#' + id).modal('hide')
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
                            <select className="form-control" value={currentLimit} onChange={handleChange} >
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
                                <option value={parseInt(11, 10)}>11</option>
                                <option value={parseInt(12, 10)}>12</option>
                                <option value={parseInt(13, 10)}>13</option>
                                <option value={parseInt(14, 10)}>14</option>
                                <option value={parseInt(15, 10)}>15</option>
                                <option value={parseInt(16, 10)}>16</option>
                                <option value={parseInt(17, 10)}>17</option>
                                <option value={parseInt(18, 10)}>18</option>
                                <option value={parseInt(19, 10)}>19</option>
                                <option value={parseInt(20, 10)}>20</option>
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

export default PopUpSelection