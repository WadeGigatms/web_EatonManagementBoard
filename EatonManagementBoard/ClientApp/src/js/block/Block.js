import React from 'react'
import EpcList from '../epc/EpcList'


const Block = ({ height, isLight, title, modalId, limit, result, epcs, setTimelineEpc }) => {
    
    function render() {
        const cardClass = "card card-secondary card-display " + height
        return <div className={cardClass}>
            {renderHeader()}
            {renderBody()}
        </div>
    }

    function renderHeader() {
        return <div className="card-header">
            {title}
            <div className="card-tools">
                {renderStatusLight(epcs)}
            </div>
        </div>
    }

    function renderBody() {
        return <div className="card-body table-responsive p-0">
            <EpcList result={result} epcs={epcs} showTransTime={false} setTimelineEpc={setTimelineEpc} />
        </div>
    }

    function renderStatusLight(epcs) {
        const dataTarget = "#" + modalId
        if (isLight === true) {
            if (epcs && limit > 0 && epcs.length < limit) {
                return <button type="button" className="btn btn-tool" data-toggle="modal" data-target={dataTarget} >
                    <i className="fa fa-circle color-g circle-size"></i>
                </button>
            }
            else {
                return <button type="button" className="btn btn-tool" data-toggle="modal" data-target={dataTarget} >
                    <i className="fa fa-circle color-r circle-size"></i>
                </button>
            }
        }
    }

    return <>{render()}</>
}

export default Block