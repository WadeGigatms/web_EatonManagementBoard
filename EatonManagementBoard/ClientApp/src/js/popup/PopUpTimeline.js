import React from 'react'
import $ from 'jquery'
import Timeline from '../timeline/Timeline'
import {
    CLOSE,
    MOVE,
    RECOVER,
    TH_PALLET_TRACE,
    _timeline_modal_target,
    _move_modal_target,
    _recover_modal_target,
} from '../constants'

const PopUpTimeline = ({ epc }) => {

    function handleClick() {
        $('#timelineModalTarget').modal('hide')
    }

    function render(epc) {
        const moveDataTarget = "#" + _move_modal_target
        const recoverDataTarget = "#" + _recover_modal_target
        const modalFooter = () => {
            if (epc.epcContext.reader_id !== "Terminal" && epc.epcContext.reader_id !== "ManualTerminal") {
                return <div className="modal-footer justify-content-between">
                    <button type="button" className="btn btn-default" data-toggle="modal" data-target={moveDataTarget} onClick={handleClick}>{MOVE}</button>
                    <button type="button" className="btn btn-secondary" data-dismiss="modal">{CLOSE}</button>
                </div>
            }
            else if (epc.epcContext.reader_id === "ManualTerminal") {
                return <div className="modal-footer justify-content-between">
                    <button type="button" className="btn btn-default" data-toggle="modal" data-target={recoverDataTarget} onClick={handleClick}>{RECOVER}</button>
                    <button type="button" className="btn btn-secondary" data-dismiss="modal">{CLOSE}</button>
                </div>
            }
            else {
                return <div className="modal-footer">
                    <button type="button" className="btn btn-secondary" data-dismiss="modal">{CLOSE}</button>
                </div>
            }
        }
        if (epc) {
            return <div className="modal fade" id={_timeline_modal_target} tabIndex="-1" role="dialog" aria-labelledby="ModalCenterTitle" aria-hidden="true">
                <div className="modal-dialog modal-dialog-centered modal-dialog-scrollable" role="document">
                    <div className="modal-content">
                        <div className="modal-header">
                            <h5 className="modal-title" id="ModalCenterTitle">{TH_PALLET_TRACE}: {epc.epcDataDto.pallet_id}</h5>
                            <button type="button" className="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div className="modal-body">
                            <Timeline epc={epc} />
                        </div>
                        {modalFooter()}
                    </div>
                </div>
            </div>
        }
        else {
            return <></>
        }
    }

    return <>{render(epc)}</>
}

export default PopUpTimeline