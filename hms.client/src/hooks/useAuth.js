import { useContext } from 'react'
import { useAuthContext } from '@/contexts/AuthContext.jsx'

export const useAuth = () => {
  return useContext(useAuthContext)
}