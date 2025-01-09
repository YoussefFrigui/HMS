// filepath: /hms.client/src/App.jsx
import React from 'react'
import { BrowserRouter as Router, Route, Routes, Navigate } from 'react-router-dom'
import HomePage from '@/pages/HomePage.jsx'
import Login from '@/pages/Login.jsx'
import Register from '@/pages/Register.jsx'
import AppointmentsPage from '@/pages/Appointments.jsx'
import DoctorAppointmentsPage from '@/pages/DoctorAppointmentsPage.jsx'
import AdminDashboard from '@/pages/AdminDashboard.jsx'
import { useAuth } from '@/hooks/useAuth.js'

function App() {
  const { user } = useAuth()

  return (
    <Router>
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/appointments" element={<AppointmentsPage />} />
        <Route path="/doctor/appointments" element={<DoctorAppointmentsPage />} />
        {user?.role === 'Admin' && <Route path="/admin" element={<AdminDashboard />} />}
        <Route path="*" element={<Navigate to="/" />} />
      </Routes>
    </Router>
  )
}

export default App