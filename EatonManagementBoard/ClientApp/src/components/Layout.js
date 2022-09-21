import React from 'react'
import NavBar from './NavBar'
import SideBar from './SideBar'
import Content from './Content'
import Footer from './Footer'

const Layout = (props) => {
    let children = props.children
    let version = props.version

    function render() {
        return <>
            <Content children={children} />
        </>
    }

    return <>{render()}</>
}

export default Layout