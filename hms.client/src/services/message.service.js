import api from './api.js'

export async function getMessages() {
  const response = await api.get('/messaging/messages')
  return response.data
}

export async function postMessage(message) {
  const response = await api.post('/messaging/send', message)
  return response.data
}