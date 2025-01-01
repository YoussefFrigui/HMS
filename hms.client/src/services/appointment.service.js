import axios from 'axios'

export async function listAppointments() {
  const res = await axios.get('/api/appointments')
  return res.data
}

export async function createAppointment(data) {
  const res = await axios.post('/api/appointments', data)
  return res.data
}