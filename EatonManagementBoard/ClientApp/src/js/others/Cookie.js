import cookie from 'react-cookies'

// Idle seconds
export const LoadIdleSeconds = () => {
    return cookie.load('idleSeconds')
}

export const SaveIdleSeconds = (idleSeconds) => {
    cookie.save('idleSeconds', idleSeconds, { path: '/'})
}

export const RemoveIdleSeconds = () => {
    cookie.remove('idleSeconds', { path: '/' })
}

// Carousel seconds
export const LoadCarouselSeconds = () => {
    return cookie.load('carouselSeconds')
}

export const SaveCarouselSeconds = (carouselSeconds) => {
    cookie.save('carouselSeconds', carouselSeconds, { path: '/' })
}

export const RemoveCarouselSeconds = () => {
    cookie.remove('carouselSeconds', { path: '/' })
}

// Location capacity
export const LoadLocationCapacity = (location) => {
    return cookie.load(location)
}

export const SaveLocationCapacity = (location, capacity) => {
    cookie.save(location, capacity, { path: '/' })
}

export const RemoveLocationCapacity = (location) => {
    cookie.remove(location, { path: '/' })
}