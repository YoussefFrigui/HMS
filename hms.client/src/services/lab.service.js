import axios from 'axios'

export async function getLabReports() {
  const res = await axios.get('/api/lab/reports')
  return res.data
}

export async function uploadReport(file) {
  const formData = new FormData()
  formData.append('report', file)
  await axios.post('/api/lab/upload', formData)
}