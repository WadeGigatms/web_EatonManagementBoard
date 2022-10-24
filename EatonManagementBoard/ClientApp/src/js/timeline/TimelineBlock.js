import React from 'react'
import { NOW } from '../constants'

const TimelineBlock = ({ palletId, locationTimeDto }) => {
    function render(palletId, locationTimeDto) {
        if (palletId && locationTimeDto) {
            const { location, transTime, durationTime } = locationTimeDto
            return <div className="timeline">
                <div>
                    <i className="fas fa-clock bg-blue"></i>
                    <div className="timeline-item">
                        <span className="time">
                            {durationTime !== "" ? <label className="timeline-text"><i className="fas fa-clock"></i> {durationTime}</label> : <label className="timeline-text">{NOW}</label>}
                        </span>
                        <h4 className="timeline-header">{transTime}</h4>
                        <div className="timeline-body">
                            <label className="timeline-text">{location}</label>
                        </div>
                    </div>
                </div>
            </div>
        }
    }

    return <>{render(palletId, locationTimeDto)}</>
}

export default TimelineBlock