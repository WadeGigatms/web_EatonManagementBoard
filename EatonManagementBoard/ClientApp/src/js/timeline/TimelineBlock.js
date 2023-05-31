import React from 'react'
import { TH_NOW } from '../constants'

const TimelineBlock = ({ palletId, locationTimeDto }) => {
    function render(palletId, locationTimeDto) {
        if (palletId && locationTimeDto) {
            return <div className="timeline">
                <div>
                    <i className="fas fa-clock bg-blue"></i>
                    <div className="timeline-item">
                        <span className="time">
                            {locationTimeDto.durationTime !== "" ? <label className="timeline-text"><i className="fas fa-clock"></i> {locationTimeDto.durationTime}</label> : <label className="timeline-text">{TH_NOW}</label>}
                        </span>
                        <h6 className="timeline-header">{locationTimeDto.transTime}</h6>
                        <h6 className="timeline-body">{locationTimeDto.location}</h6>
                    </div>
                </div>
            </div>
        }
    }

    return <>{render(palletId, locationTimeDto)}</>
}

export default TimelineBlock