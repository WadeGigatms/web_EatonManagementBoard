import React, { useEffect } from 'react'
import $ from 'jquery'

const CarouselImage = ({ carouselMiniSeconds }) => {

    useEffect(() => {
        $('.carousel').carousel('cycle')
    }, [])

    function render() {
        return <div id="carouselExampleInterval" className="carousel slide carousel-fade" data-ride="carousel">
            <div className="carousel-inner">
                <div className="carousel-item active" data-interval={carouselMiniSeconds} >
                    <div className="bg-role-meter">
                        <img className="vh-100 d-block" />
                    </div>
                </div>
                <div className="carousel-item" data-interval={carouselMiniSeconds} >
                    <div className="bg-role-5s1f">
                        <img className="vh-100 d-block" />
                    </div>
                </div>
                <div className="carousel-item" data-interval={carouselMiniSeconds} >
                    <div className="bg-role-ppe">
                        <img className="vh-100 d-block" />
                    </div>
                </div>
            </div>
        </div>
    }

    return <>{render()}</>
}

export default CarouselImage