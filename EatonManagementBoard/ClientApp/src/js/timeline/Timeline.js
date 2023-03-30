import React from 'react'
import TimelineBlock from '../timeline/TimelineBlock'

const Timeline = ({ epc }) => {

    function render(epc) {
        if (epc) {
            var timelines = []
            for (var i = 0; i < epc.locationTimeDtos.length; i++) {
                timelines.push(<TimelineBlock key={i} palletId={epc.epcDataDto.palletId} locationTimeDto={epc.locationTimeDtos[i]} />)
            }
            return <>
                {timelines}
            </>
        }
    }

    return <>{render(epc)}</>
}

export default Timeline