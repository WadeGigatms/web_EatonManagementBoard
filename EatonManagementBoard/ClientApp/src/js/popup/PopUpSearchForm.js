import React, { useEffect, useState } from 'react'
import $ from 'jquery'
import {
    SEARCH,
    TH_PN,
    TH_TASKNO,
    TH_PALLET,
    ERROR,
    _search_form_modal_target,
} from '../constants'

const PopUpSearchForm = ({ searchState, searchStateRef, setSearchParameter }) => {
    const [wo, setWo] = useState("")
    const [pn, setPn] = useState("")
    const [palletId, setPalletId] = useState("")
    const [error, setError] = useState("")

    useEffect(() => {
        $('#searchFormModalTarget').on('hide.bs.modal', handleHideModal)
        function handleHideModal() {
            setWo("")
            setPn("")
            setPalletId("")
            setError("")
        }
    }, [])

    useEffect(() => {
        if (searchState === true) {
            setWo("")
            setPn("")
            setPalletId("")
        }
    }, [searchState])

    function handleChangeWo(e) {
        setWo(e.target.value)
    }

    function handleChangePn(e) {
        setPn(e.target.value)
    }

    function handleChangePalletId(e) {
        setPalletId(e.target.value)
    }

    function handleSubmit() {
        if (wo || pn || palletId) {
            $('#searchFormModalTarget').modal('hide')
            setError("")
            setSearchParameter({ wo, pn, palletId })
        } else {
            setError(ERROR)
        }
    }

    function render() {
        const id = searchStateRef.current === false ? _search_form_modal_target : ""
        return <div className="modal fade" id={id} tabIndex="-1" role="dialog" aria-labelledby="ModalCenterTitle" aria-hidden="true">
            <div className="modal-dialog modal-dialog-centered" role="document">
                <div className="modal-content">
                    <div className="modal-header">
                        <h5 className="modal-title" id="ModalCenterTitle">{SEARCH}</h5>
                        <button type="button" className="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div className="modal-body">
                        <div className="form-group">
                            <div className="form-group">
                                <label>{TH_TASKNO}</label>
                                <input type="text" className="form-control" value={wo} onChange={handleChangeWo} />
                            </div>
                            <div className="form-group">
                                <label>{TH_PN}</label>
                                <input type="text" className="form-control" value={pn} onChange={handleChangePn} />
                            </div>
                            <div className="form-group">
                                <label>{TH_PALLET}</label>
                                <input type="text" className="form-control" value={palletId} onChange={handleChangePalletId} />
                            </div>
                            <div className="form-group">
                                <label className="red">{error}</label>
                            </div>
                        </div>
                    </div>
                    <div className="modal-footer">
                        <button type="button" className="btn btn-primary btn-block" onClick={handleSubmit}>{SEARCH}</button>
                    </div>
                </div>
            </div>
        </div>
    }

    return <>{render()}</>
}

export default PopUpSearchForm