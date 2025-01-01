import React from 'react'
import AdminDashboard from '@/components/dashboard/AdminDashboard.jsx'
import DoctorDashboard from '@/components/dashboard/DoctorDashboard.jsx'
import PatientDashboard from '@/components/dashboard/PatientDashboard.jsx'
import { useAuth } from '@/hooks/useAuth.js'

function Dashboard() {
  const { user } = useAuth()

  switch (user?.role) {
    case 'Admin':
      return <AdminDashboard />
    case 'Doctor':
      return <DoctorDashboard />
    default:
      return <PatientDashboard />
  }
}

export default Dashboard