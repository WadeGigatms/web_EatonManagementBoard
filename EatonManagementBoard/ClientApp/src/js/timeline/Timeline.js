import React from 'react'
import TimelineBlock from '../timeline/TimelineBlock'

const Timeline = ({ epc }) => {

    function render(epc) {
        if (epc) {
            var timelines = []
            const { barcode, locationTimeDtos } = epc
            for (var i = 0; i < locationTimeDtos.length; i++) {
                timelines.push(<TimelineBlock key={i} palletId={barcode} locationTimeDto={locationTimeDtos[i]} />)
            }
            return <>
                {timelines}
            </>
        }
    }

    return <>{render(epc)}</>
}

export default Timeline