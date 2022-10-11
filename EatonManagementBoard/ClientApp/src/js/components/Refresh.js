import React from 'react'

const Refresh = () => {
    return <div className="overlay">
        <div className="d-flex justify-content-center card-body">
            <button type="button" className="btn btn-tool" data-card-widget="card-refresh" data-source="widgets.html" data-source-selector="#card-refresh-content">
                <i className="fas fa-sync-alt fa-3x fa-spin"></i>
            </button>
        </div>
    </div>
}

export default Refresh