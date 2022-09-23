import React from 'react'

const TimelineBlock = ({ palletId, locationTimeDto }) => {
    function render(palletId, locationTimeDto) {
        if (palletId && locationTimeDto) {
            const { location, transTime, durationTime } = locationTimeDto
            return <div className="timeline">
                <div>
                    <i className="fas fa-clock bg-blue"></i>
                    <div className="timeline-item">
                        <span className="time">
                            {durationTime !== "" ? <><i className="fas fa-clock"></i> {durationTime}</> : <></> }
                        </span>
                        <h3 className="timeline-header">{transTime}</h3>
                        <div className="timeline-body">{location}</div>
                    </div>
                </div>
            </div>
        }
    }

    return <>{render(palletId, locationTimeDto)}</>
}

export default TimelineBlock