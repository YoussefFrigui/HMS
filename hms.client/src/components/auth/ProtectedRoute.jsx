import React from 'react'
import { Navigate } from 'react-router-dom'
import { useAuth } from '@/hooks/useAuth.js'

function ProtectedRoute({ children, roles = [] }) {
  const { user } = useAuth()

  if (!user) return <Navigate to="/login" replace />
  if (roles.length && !roles.includes(user.role)) return <Navigate to="/" />

  return children
}

export default ProtectedRoute