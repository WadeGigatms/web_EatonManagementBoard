import React from 'react'
import EpcList from '../epc/EpcList'


const BigBlock = ({ height, title, result, epcs, showTransTime, setTimelineEpc }) => {

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
        </div>
    }

    function renderBody() {
        return <div className="card-body table-responsive p-0">
            <EpcList result={result} epcs={epcs} showTransTime={showTransTime} setTimelineEpc={setTimelineEpc} />
        </div>
    }

    return <>{render()}</>
}

export default BigBlock