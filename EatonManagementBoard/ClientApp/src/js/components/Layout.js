import React from 'react'
import Content from './Content'

const Layout = (props) => {
    const children = props.children

    function render() {
        return <>
            <Content children={children} />
        </>
    }

    return <>{render()}</>
}

export default Layout