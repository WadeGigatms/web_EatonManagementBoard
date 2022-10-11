import React, { useEffect, useState } from 'react'
import {
    SELECT,
    SEARCH,
    TH_PN,
    TH_TASKNO,
    TH_PALLET,
    ERROR
} from '../constants'

const PopUpForm = ({ id, setSearchParameter, selection }) => {
    const [wo, setWo] = useState("")
    const [pn, setPn] = useState("")
    const [palletId, setPalletId] = useState("")
    const [error, setError] = useState("")

    useEffect(() => {
        window.jQuery('#' + id).on('hide.bs.modal', handleHideModal)
    }, [id])

    function handleHideModal() {
        setWo("")
        setPn("")
        setPalletId("")
    }

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
            window.jQuery('#' + id).modal('hide')
            setError("")
            setSearchParameter({ wo, pn, palletId })
        } else {
            setError(ERROR)
        }
    }

    function renderSelectWo(wos) {
        if (wos) {
            var wosRows = []
            wosRows.push(<option key={0} value={null}>{SELECT}</option>)
            for (var i = 0; i < wos.length; i++) {
                wosRows.push(<option key={wos[i]} value={wos[i]}>{wos[i]}</option>)
            }
            return <select className="form-control" value={wo} onChange={handleChangeWo} >
                {wosRows}
            </select>
        }
    }

    function renderSelectPn(pns) {
        if (pns) {
            var pnsRows = []
            pnsRows.push(<option key={0} value={null}>{SELECT}</option>)
            for (var i = 0; i < pns.length; i++) {
                pnsRows.push(<option key={pns[i]} value={pns[i]}>{pns[i]}</option>)
            }
            return <select className="form-control" value={pn} onChange={handleChangePn} >
                {pnsRows}
            </select>
        }
    }

    function renderSelectPalletId(palletIds) {
        if (palletIds) {
            var palletIdsRows = []
            palletIdsRows.push(<option key={0} value={null}>{SELECT}</option>)
            for (var i = 0; i < palletIds.length; i++) {
                palletIdsRows.push(<option key={palletIds[i]} value={palletIds[i]}>{palletIds[i]}</option>)
            }
            return <select className="form-control" value={palletId} onChange={handleChangePalletId} >
                {palletIdsRows}
            </select>
        }
    }

    function render(selection) {
        if (selection) {
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
                                    {renderSelectWo(selection.wos)}
                                </div>
                                <div className="form-group">
                                    <label>{TH_PN}</label>
                                    {renderSelectPn(selection.pns)}
                                </div>
                                <div className="form-group">
                                    <label>{TH_PALLET}</label>
                                    {renderSelectPalletId(selection.palletIds)}
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
    }

    return <>{render(selection)}</>
}

export default PopUpForm