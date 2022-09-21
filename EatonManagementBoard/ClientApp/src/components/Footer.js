import React from 'react'

const Footer = ({ version }) => {
    return <footer className="main-footer footer vh-5">
        <div className="d-none d-sm-block text-right">
            Version {version}
        </div>
    </footer>
}

export default Footer