import React, { useEffect, useState, useCallback } from 'react'
import Block from './block/Block'
import LeftBlock from './block/LeftBlock'
import RightBlock from './block/RightBlock'
import TerminalBlock from './block/TerminalBlock'
import Refresh from '../components/Refresh'
import PopUpSelection from './popup/PopUpSelection'
import PopUpForm from './popup/PopUpForm'
import PopUpTimeline from './popup/PopUpTimeline'
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
    URL_HOST,
    URL_EPC_ALL,
    URL_EPC,
    _3A_modal_target,
    _2A_modal_target,
    _3B_modal_target,
    _search_modal_target,
    _timeline_modal_target
} from './constants'

const Dashboard = ({ showDashboard }) => {
    const [result, setResult] = useState()
    const [dashboard, setDashboard] = useState({
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
    const [selection, setSelection] = useState()
    const [loading, setLoading] = useState(true)
    const [_3A_limit, set3ALimit] = useState(6)
    const [_2A_limit, set2ALimit] = useState(6)
    const [_3B_limit, set3BLimit] = useState(6)
    const [searchParameter, setSearchParameter] = useState()
    const [timelineEpc, setTimelineEpc] = useState()
    
    const fetchRequest = useCallback((url) => {
        const fetchData = async () => {
            try {
                setLoading(true)
                const response = await fetch(url)
                const { result, error, dashboardDto, selectionDto } = await response.json()
                if (result === true) {
                    setResult(result)
                    setDashboard(dashboardDto)
                    setSelection(selectionDto)
                }
                else {

                }
            } catch (error) {
            }
            setLoading(false)
        }
        fetchData()
    }, [])

    useEffect(() => {
        fetchRequest(URL_EPC_ALL)
    }, [])

    useEffect(() => {
        if (searchParameter) {
            const { wo, pn, palletId } = searchParameter
            if (wo || pn || palletId) {
                const url = URL_EPC + "wo=" + wo + "&pn=" + pn + "&palletId=" + palletId
                fetchRequest(url)
            }
        }
    }, [searchParameter])

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
                                epcs={dashboard.warehouseEEpcDtos}
                                setTimelineEpc={setTimelineEpc} />
                            <LeftBlock
                                height={"h-5 card-margin-top"}
                                title={BLOCK_D}
                                result={result}
                                epcs={dashboard.warehouseDEpcDtos}
                                setTimelineEpc={setTimelineEpc} />
                            <LeftBlock
                                height={"h-5 card-margin-top"}
                                title={BLOCK_C}
                                result={result}
                                epcs={dashboard.warehouseCEpcDtos}
                                setTimelineEpc={setTimelineEpc} />
                            <LeftBlock
                                height={"h-5 card-margin-top"}
                                title={BLOCK_B}
                                result={result}
                                epcs={dashboard.warehouseBEpcDtos}
                                setTimelineEpc={setTimelineEpc} />
                            <LeftBlock
                                height={"h-5 card-margin-top"}
                                title={BLOCK_A}
                                result={result}
                                epcs={dashboard.warehouseAEpcDtos}
                                setTimelineEpc={setTimelineEpc} />
                        </div>
                    </div>
                    <div className="col-sm-4 h-100 p-025">
                        <div className="h-100">
                            <Block
                                height={"h-5"}
                                isLight={false}
                                title={BLOCK_1F}
                                modalId={null}
                                limit={null}
                                result={false}
                                epcs={null}
                                setTimelineEpc={setTimelineEpc} />
                            <RightBlock
                                height={"h-5 card-margin-top"}
                                title={BLOCK_I}
                                result={result}
                                epcs={dashboard.warehouseIEpcDtos}
                                setTimelineEpc={setTimelineEpc} />
                            <RightBlock
                                height={"h-5 card-margin-top"}
                                title={BLOCK_H}
                                result={result}
                                epcs={dashboard.warehouseHEpcDtos}
                                setTimelineEpc={setTimelineEpc} />
                            <RightBlock
                                height={"h-5 card-margin-top"}
                                title={BLOCK_G}
                                result={result}
                                epcs={dashboard.warehouseGEpcDtos}
                                setTimelineEpc={setTimelineEpc} />
                            <RightBlock
                                height={"h-5 card-margin-top"}
                                title={BLOCK_F}
                                result={result}
                                epcs={dashboard.warehouseFEpcDtos}
                                setTimelineEpc={setTimelineEpc} />
                        </div>
                    </div>
                    <div className="col-sm-4 h-100 p-025">
                        <Block
                            height={"h-100"}
                            isLight={false}
                            title={BLOCK_1F_TEMP}
                            modalId={null}
                            limit={null}
                            result={result}
                            epcs={dashboard.elevatorEpcDtos}
                            setTimelineEpc={setTimelineEpc} />
                    </div>
                </div>
            </div>
            <div className="col-sm-3 h-100 p-025">
                <div className="h-3">
                    <div className="h-3 bg-a-3f"></div>
                    <Block
                        height={"h-100"}
                        isLight={true}
                        title={BLOCK_3F_A}
                        modalId={_3A_modal_target}
                        limit={_3A_limit}
                        result={result}
                        epcs={dashboard.thirdFloorAEpcDtos}
                        setTimelineEpc={setTimelineEpc} />
                </div>
                <div className="h-3">
                    <div className="h-3 bg-a-2f"></div>
                    <Block
                        height={"h-100 card-margin-top"}
                        isLight={true}
                        title={BLOCK_2F_A}
                        modalId={_2A_modal_target}
                        limit={_2A_limit}
                        result={result}
                        epcs={dashboard.secondFloorEpcDtos}
                        setTimelineEpc={setTimelineEpc} />
                </div>
                <div className="h-3">
                    <div className="h-3 bg-b-3f"></div>
                    <Block
                        height={"h-100 card-margin-top"}
                        isLight={true}
                        title={BLOCK_3F_B}
                        modalId={_3B_modal_target}
                        limit={_3B_limit}
                        result={result}
                        epcs={dashboard.thirdFloorBEpcDtos}
                        setTimelineEpc={setTimelineEpc} />
                </div>
            </div>
        </>
    }

    function renderTerminal() {
        const { terminalEpcDtos } = dashboard
        return <>
            <div className="col-sm h-100">
                <div className="row h-100">
                    <div className="h-100 bg-1f"></div>
                    <div className="col-sm h-100">
                        <TerminalBlock
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
            <PopUpTimeline id={_timeline_modal_target} epc={timelineEpc} />
            <PopUpForm id={_search_modal_target} setSearchParameter={setSearchParameter} selection={selection} />
            <PopUpSelection id={_3A_modal_target} title={BLOCK_3F_A} limit={_3A_limit} setLimit={set3ALimit} />
            <PopUpSelection id={_2A_modal_target} title={BLOCK_2F_A} limit={_2A_limit} setLimit={set2ALimit} />
            <PopUpSelection id={_3B_modal_target} title={BLOCK_3F_B} limit={_3B_limit} setLimit={set3BLimit} />
            {showDashboard === true ? <>{renderDashboard()}</> : <>{renderTerminal()}</>}
        </div>
    }

    return <>{loading === false ? render() : <Refresh />}</>
}

export default Dashboard