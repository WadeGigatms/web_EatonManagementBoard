﻿import React, { useEffect, useState, useCallback } from 'react'
import SmallBlock from './block/SmallBlock'
import LeftBlock from './block/LeftBlock'
import RightBlock from './block/RightBlock'
import BigBlock from './block/BigBlock'
import PopupCapacity from './popup/PopupCapacity'
import PopupSearchForm from './popup/PopupSearchForm'
import PopupTimeline from './popup/PopupTimeline'
import PopupMoveConfirm from './popup/PopupMoveConfirm'
import PopupRecoverConfirm from './popup/PopupRecoverConfirm'
import PopupSetting from './popup/PopupSetting'
import {
    BLOCK_3F_A,
    BLOCK_3F_B,
    BLOCK_2F_A,
    BLOCK_1F_TEMP,
    BLOCK_1F,
    BLOCK_A,
    BLOCK_B,
    BLOCK_C,
    BLOCK_D,
    BLOCK_E,
    BLOCK_F,
    BLOCK_G,
    BLOCK_H,
    BLOCK_I,
    NAV_TERMINAL,
    URL_EPC,
    _3A_modal_target,
    _2A_modal_target,
    _3B_modal_target,
    _search_form_modal_target,
} from './constants'
import { LoadLocationCapacity } from './Cookie'

const Dashboard = ({
    dashboardState,
    searchParameter,
    setSearchParameter,
    searchState,
    searchStateRef,
    idleState,
    idleSeconds,
    setIdleSeconds,
    carouselMiniSeconds,
    setCarouselMiniSeconds
}) => {
    const [result, setResult] = useState()
    const [onDashboard, setOnDashboard] = useState({
        warehouseAEpcDtos: null,
        warehouseBEpcDtos: null,
        warehouseCEpcDtos: null,
        warehouseDEpcDtos: null,
        warehouseEEpcDtos: null,
        warehouseFEpcDtos: null,
        warehouseGEpcDtos: null,
        warehouseHEpcDtos: null,
        warehouseIEpcDtos: null,
        elevatorEpcDtos: null,
        secondFloorEpcDtos: null,
        thirdFloorAEpcDtos: null,
        thirdFloorBEpcDtos: null,
        terminalEpcDtos: null
    })
    const [_3A_capacity, set3ACapacity] = useState(6)
    const [_2A_capacity, set2ACapacity] = useState(6)
    const [_3B_capacity, set3BCapacity] = useState(6)
    const [timelineEpc, setTimelineEpc] = useState()

    const getRequest = useCallback((url) => {
        const getData = async () => {
            try {
                const response = await fetch(url)
                const { result, dashboardDto } = await response.json()
                if (result === true) {
                    setResult(result)
                    if (searchStateRef.current === false) {
                        setOnDashboard(dashboardDto)
                    }
                    else {
                        setOnDashboard(dashboardDto)
                    }
                }
                else {

                }
            } catch (error) {
            }
        }
        getData()
    }, [searchStateRef])

    const postRequest = useCallback((url, requestOptions, getRequest) => {
        const postData = async () => {
            try {
                const response = await fetch(url, requestOptions)
                await response.json()
                getRequest(getApiUrl())
            } catch (error) {
            }
        }
        postData()
    }, [])

    const delRequest = useCallback((url, requestOptions, getRequest) => {
        const delData = async () => {
            try {
                const response = await fetch(url, requestOptions)
                await response.json()
                getRequest(getApiUrl())
            } catch (error) {
            }
        }
        delData()
    }, [])

    useEffect(() => {
        const cookie3ALocationCapacity = LoadLocationCapacity(_3A_modal_target) === undefined ? 6 : LoadLocationCapacity(_3A_modal_target)
        set3ACapacity(cookie3ALocationCapacity)
    }, [_3A_capacity])

    useEffect(() => {
        const cookie2ALocationCapacity = LoadLocationCapacity(_2A_modal_target) === undefined ? 6 : LoadLocationCapacity(_2A_modal_target)
        set2ACapacity(cookie2ALocationCapacity)
    }, [_2A_capacity])

    useEffect(() => {
        const cookie3BLocationCapacity = LoadLocationCapacity(_3B_modal_target) === undefined ? 6 : LoadLocationCapacity(_3B_modal_target)
        set3BCapacity(cookie3BLocationCapacity)
    }, [_3B_capacity])

    useEffect(() => {
        const url = getApiUrl()
        getRequest(url)

        const interval = setInterval(() => {
            // if front-end goes to idle state or searching state, web stops requesting api
            if (searchStateRef.current === false && idleState === false) {
                const url = getApiUrl()
                getRequest(url)
            }
        }, 5000)

        return () => clearInterval(interval)
    }, [searchStateRef, getRequest, idleState])

    useEffect(() => {
        if (searchParameter) {
            const { wo, pn, palletId } = searchParameter
            if (wo || pn || palletId) {
                searchStateRef.current = true
                const url = getApiUrl() + "?wo=" + wo + "&pn=" + pn + "&palletId=" + palletId
                getRequest(url)
            }
        }
    }, [searchParameter, searchStateRef, getRequest, searchState])

    function getApiUrl() {
        const url = window.location.origin + URL_EPC
        return url
    }

    function getJson(epc) {
        return JSON.stringify({
            Epc: String(epc.epcContext.epc),
            ReaderId: "ManualTerminal",
            TransTime: epc.epcContext.timestamp
        })
    }

    function post() {
        const url = getApiUrl()
        const requestOptions = {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: getJson(timelineEpc)
        }
        postRequest(url, requestOptions, getRequest)
    }

    function del() {
        const url = getApiUrl() + "?id=" + timelineEpc.epcContext.id
        const requestOptions = {
            method: 'DELETE',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
        }
        delRequest(url, requestOptions, getRequest)
    }

    function renderDashboard() {
        return <>
            <div className="col-sm-9 h-100">
                <div className="row h-100">
                    <div className="h-100 bg-1f"></div>
                    <div className="col-sm-4 h-100 p-025">
                        <div className="h-100">
                            <LeftBlock
                                height={"h-5"}
                                title={BLOCK_E}
                                result={result}
                                epcs={onDashboard.warehouseEEpcDtos}
                                setTimelineEpc={setTimelineEpc} />
                            <LeftBlock
                                height={"h-5 card-margin-top"}
                                title={BLOCK_D}
                                result={result}
                                epcs={onDashboard.warehouseDEpcDtos}
                                setTimelineEpc={setTimelineEpc} />
                            <LeftBlock
                                height={"h-5 card-margin-top"}
                                title={BLOCK_C}
                                result={result}
                                epcs={onDashboard.warehouseCEpcDtos}
                                setTimelineEpc={setTimelineEpc} />
                            <LeftBlock
                                height={"h-5 card-margin-top"}
                                title={BLOCK_B}
                                result={result}
                                epcs={onDashboard.warehouseBEpcDtos}
                                setTimelineEpc={setTimelineEpc} />
                            <LeftBlock
                                height={"h-5 card-margin-top"}
                                title={BLOCK_A}
                                result={result}
                                epcs={onDashboard.warehouseAEpcDtos}
                                setTimelineEpc={setTimelineEpc} />
                        </div>
                    </div>
                    <div className="col-sm-4 h-100 p-025">
                        <div className="h-100">
                            <RightBlock
                                height={"h-5"}
                                title={BLOCK_I}
                                result={result}
                                epcs={onDashboard.warehouseIEpcDtos}
                                setTimelineEpc={setTimelineEpc} />
                            <RightBlock
                                height={"h-5 card-margin-top"}
                                title={BLOCK_H}
                                result={result}
                                epcs={onDashboard.warehouseHEpcDtos}
                                setTimelineEpc={setTimelineEpc} />
                            <RightBlock
                                height={"h-5 card-margin-top"}
                                title={BLOCK_G}
                                result={result}
                                epcs={onDashboard.warehouseGEpcDtos}
                                setTimelineEpc={setTimelineEpc} />
                            <RightBlock
                                height={"h-5 card-margin-top"}
                                title={BLOCK_F}
                                result={result}
                                epcs={onDashboard.warehouseFEpcDtos}
                                setTimelineEpc={setTimelineEpc} />
                            <SmallBlock
                                height={"h-5 card-margin-top"}
                                isLight={false}
                                title={BLOCK_1F}
                                modalId={null}
                                capacity={null}
                                result={result}
                                epcs={onDashboard.handheldEpcDtos}
                                setTimelineEpc={setTimelineEpc} />
                        </div>
                    </div>
                    <div className="col-sm-4 h-100 p-025">
                        <SmallBlock
                            height={"h-100"}
                            isLight={false}
                            title={BLOCK_1F_TEMP}
                            modalId={null}
                            capacity={null}
                            result={result}
                            epcs={onDashboard.elevatorEpcDtos}
                            setTimelineEpc={setTimelineEpc} />
                    </div>
                </div>
            </div>
            <div className="col-sm-3 h-100 p-025">
                <div className="h-3">
                    <div className="h-3 bg-a-3f"></div>
                    <SmallBlock
                        height={"h-100"}
                        isLight={true}
                        title={BLOCK_3F_B}
                        modalId={_3A_modal_target}
                        capacity={_3A_capacity}
                        result={result}
                        epcs={onDashboard.thirdFloorAEpcDtos}
                        setTimelineEpc={setTimelineEpc} />
                </div>
                <div className="h-3">
                    <div className="h-3 bg-a-2f"></div>
                    <SmallBlock
                        height={"h-100 card-margin-top"}
                        isLight={true}
                        title={BLOCK_2F_A}
                        modalId={_2A_modal_target}
                        capacity={_2A_capacity}
                        result={result}
                        epcs={onDashboard.secondFloorEpcDtos}
                        setTimelineEpc={setTimelineEpc} />
                </div>
                <div className="h-3">
                    <div className="h-3 bg-b-3f"></div>
                    <SmallBlock
                        height={"h-100 card-margin-top"}
                        isLight={true}
                        title={BLOCK_3F_A}
                        modalId={_3B_modal_target}
                        capacity={_3B_capacity}
                        result={result}
                        epcs={onDashboard.thirdFloorBEpcDtos}
                        setTimelineEpc={setTimelineEpc} />
                </div>
            </div>
        </>
    }

    function renderTerminal() {
        const { terminalEpcDtos } = onDashboard
        return <>
            <div className="col-sm h-100 p-025">
                <div className="row h-100">
                    <div className="h-100 bg-1f"></div>
                    <div className="col-sm h-100">
                        <BigBlock
                            height={"h-100"}
                            title={NAV_TERMINAL}
                            result={result}
                            epcs={terminalEpcDtos}
                            showTransTime={true}
                            setTimelineEpc={setTimelineEpc} />
                    </div>
                </div>
            </div>
        </>
    }

    function render() {
        return <div className="row h-100 p-3">
            <PopupSetting idleSeconds={idleSeconds} setIdleSeconds={setIdleSeconds} carouselMiniSeconds={carouselMiniSeconds} setCarouselMiniSeconds={setCarouselMiniSeconds} />
            <PopupTimeline epc={timelineEpc} />
            <PopupMoveConfirm post={post} />
            <PopupRecoverConfirm del={del} />
            <PopupSearchForm searchState={searchState} searchStateRef={searchStateRef} setSearchParameter={setSearchParameter} />
            <PopupCapacity id={_3A_modal_target} title={BLOCK_3F_A} capacity={_3A_capacity} setCapacity={set3ACapacity} />
            <PopupCapacity id={_2A_modal_target} title={BLOCK_2F_A} capacity={_2A_capacity} setCapacity={set2ACapacity} />
            <PopupCapacity id={_3B_modal_target} title={BLOCK_3F_B} capacity={_3B_capacity} setCapacity={set3BCapacity} />
            {dashboardState === true ? <>{renderDashboard()}</> : <>{renderTerminal()}</>}
        </div>
    }
     
    return <>{render()}</>
}

export default Dashboard