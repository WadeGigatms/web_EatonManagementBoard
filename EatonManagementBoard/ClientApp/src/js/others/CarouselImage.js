﻿import Carousel from 'react-bootstrap/Carousel'

const CarouselImage = ({ carouselMiniSeconds }) => {
    function render() {
        return <Carousel indicators={false} controls={false} fade={false} slide={false} >
            <Carousel.Item interval={carouselMiniSeconds}>
                <div className="bg-role-meter">
                    <img className="vh-100" />
                </div>
            </Carousel.Item>
            <Carousel.Item interval={carouselMiniSeconds}>
                <div className="bg-role-5s1f">
                    <img className="vh-100" />
                </div>
            </Carousel.Item>
            <Carousel.Item interval={carouselMiniSeconds}>
                <div className="bg-role-ppe">
                    <img className="vh-100" />
                </div>
            </Carousel.Item>
        </Carousel>
    }
    return <>{render()}</>
}

export default CarouselImage