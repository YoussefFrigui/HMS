import React from 'react'
import { BrowserRouter as Router, Route, Routes, Navigate } from 'react-router-dom'
import HomePage from '@/pages/HomePage.jsx'
import Login from '@/pages/Login.jsx'
import Register from '@/pages/Register.jsx'
import DoctorAppointments from '@/pages/DoctorAppointments.jsx'
import AdminDashboard from '@/pages/AdminDashboard.jsx'
import './assets/styles/index.css'
//import { useAuth } from '@/hooks/useAuth.js'

function App() {
  const { user } = useAuth()

  return (
    <Router>
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/doctor/appointments" element={<DoctorAppointments />} />
        {user?.role === 'Admin' && <Route path="/admin" element={<AdminDashboard />} />}
        <Route path="*" element={<Navigate to="/" />} />
      </Routes>
    </Router>
  )
}

export default App