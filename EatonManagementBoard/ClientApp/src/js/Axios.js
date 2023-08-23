import axios from 'axios'

const axiosEpcRequest = axios.create({
    baseURL: window.location.origin + '/api/epc',
    headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
    },
})

const axiosEpcDataRequest = axios.create({
    baseURL: window.location.origin + '/api/epcdata',
    headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
    },
})

export const axiosEpcGetApi = () => axiosEpcRequest.get()
export const axiosEpcSearchGetApi = (startId, endId) => axiosEpcRequest.get('search', {
    params: {
        startId: startId,
        endId: endId,
    }
})
export const axiosEpcPostApi = (data) => axiosEpcRequest.post('', data)
export const axiosEpcDeleteApi = (id) => axiosEpcRequest.delete(`?id=${id}`, {
    params: {
        id: id
    }
})
export const axiosGetEpcDataGetApi = (wo, pn, palletId, startDate, endDate) => axiosEpcDataRequest.get(`/search`, {
    params: {
        wo: wo,
        pn: pn,
        palletId: palletId,
        startDate: startDate,
        endDate: endDate
    }
})