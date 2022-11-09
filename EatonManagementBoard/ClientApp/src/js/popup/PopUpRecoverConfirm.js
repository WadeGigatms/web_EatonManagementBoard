import React from 'react'
import $ from 'jquery'
import {
    CANCEL,
    CONFIRM,
    RECOVER,
    _recover_modal_target,
} from '../constants'

const PopUpRecoverConfirm = ({ del }) => {

    function handleConfirm() {
        const hasTagId = "#" + _recover_modal_target
        $(hasTagId).modal('hide')
        del()
    }

    function render() {
        return <div className="modal fade" id={_recover_modal_target} tabIndex="-1" role="dialog" aria-labelledby="ModalCenterTitle" aria-hidden="true">
            <div className="modal-dialog modal-dialog-centered" role="document">
                <div className="modal-content">
                    <div className="modal-header">
                        <h5 className="modal-title" id="ModalCenterTitle">{RECOVER} ?</h5>
                        <button type="button" className="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div className="modal-footer">
                        <button type="button" className="btn" data-dismiss="modal">{CANCEL}</button>
                        <button type="button" className="btn btn-primary" onClick={handleConfirm}>{CONFIRM}</button>
                    </div>
                </div>
            </div>
        </div>
    }

    return <>{render()}</>
}

export default PopUpRecoverConfirm