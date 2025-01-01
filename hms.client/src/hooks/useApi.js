import { useState } from 'react'
import api from '@/services/api.js'

function useApi() {
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState(null)

  const request = async (method, url, data = null) => {
    setLoading(true)
    setError(null)
    try {
      const response = await api.request({ method, url, data })
      setLoading(false)
      return response.data
    } catch (err) {
      setError(err.response?.data || err.message)
      setLoading(false)
      throw err
    }
  }

  return { request, loading, error }
}

export default useApi