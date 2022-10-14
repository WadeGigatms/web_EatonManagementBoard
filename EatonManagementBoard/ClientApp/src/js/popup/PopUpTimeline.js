import React, { useEffect } from 'react'
import $ from 'jquery'
import Timeline from '../timeline/Timeline'
import {
    CLOSE,
    TH_PALLET_TRACE,
    _timeline_modal_target,
} from '../constants'

const PopUpTimeline = ({ epc }) => {

    useEffect(() => {
        $('#timelineModalTarget').on('hide.bs.modal', handleHideModal)
        function handleHideModal() {

        }
    }, [])

    function render(epc) {
        if (epc) {
            return <div className="modal fade" id={_timeline_modal_target} tabIndex="-1" role="dialog" aria-labelledby="ModalCenterTitle" aria-hidden="true">
                <div className="modal-dialog modal-dialog-centered modal-dialog-scrollable" role="document">
                    <div className="modal-content">
                        <div className="modal-header">
                            <h5 className="modal-title" id="ModalCenterTitle">{TH_PALLET_TRACE}: {epc.barcode}</h5>
                            <button type="button" className="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div className="modal-body">
                            <Timeline epc={epc} />
                        </div>
                        <div className="modal-footer">
                            <button type="button" className="btn btn-secondary" data-dismiss="modal">{CLOSE}</button>
                        </div>
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