import axios from 'axios'

const EpcRequest = axios.create({
    baseURL: window.location.origin + '/api/epc',
    headers: { 'Content-Type': 'application/json' }
})

const EpcDataRequest = axios.create({
    baseURL: window.location.origin + '/api/epcdata'
})

export const ApiGetEpcRequest = (params)
    => EpcRequest.get(`/search`, { params })

export const ApiPostEpcRequest = data
    => EpcRequest.post('', data)

export const ApiDeleteEpcRequest = (id)
    => EpcRequest.delete(`?id=${id}`)

export const ApiGetEpcDataRequest = (params)
    => EpcDataRequest.get(`/search`, { params })