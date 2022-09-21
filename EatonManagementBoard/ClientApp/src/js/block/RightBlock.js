import React from 'react'
import EpcList from '../epc/EpcList'

const RightBlock = ({ height, title, result, epcs, setTimelineEpc }) => {

    function render() {
        const cardClass = "card card-secondary card-display-right " + height
        return <div className={cardClass}>
            {renderHeader()}
            {renderBody()}
        </div>
    }

    function renderHeader() {
        return <div className="card-header card-flex-w">
            {title}
        </div>
    }

    function renderBody() {
        return <div className="card-body table-responsive p-0">
            <EpcList result={result} epcs={epcs} setTimelineEpc={setTimelineEpc} />
        </div>
    }

    return <>{render()}</>
}

export default RightBlock