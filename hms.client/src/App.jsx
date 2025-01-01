import React from 'react'
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'
import MainLayout from '@/components/layout/MainLayout.jsx'
import Header from '@/components/layout/Header.jsx'
import Dashboard from '@/pages/Dashboard.jsx'
import Login from '@/pages/Login.jsx'
import UserManagment from '@/pages/UserManagment.jsx'
import AppointmentsPage from '@/pages/Appointments.jsx'
import LabReportsPage from '@/pages/LabReports.jsx'
import MedicalHistoryPage from '@/pages/MedicalHistory.jsx'
import Messaging from '@/pages/Messaging.jsx'
import Messages from '@/pages/Messages.jsx'
import Profile from '@/pages/Profile.jsx'
import ProtectedRoute from '@/components/auth/ProtectedRoute.jsx'

function App() {
  return (
    <Router>
      <Header />
      <Routes>
        <Route path="/login" element={<Login />} />
        <Route
          path="/"
          element={
            <ProtectedRoute>
              <MainLayout />
            </ProtectedRoute>
          }
        >
          <Route path="dashboard" element={<Dashboard />} />
          <Route
            path="user-management"
            element={
              <ProtectedRoute roles={['Admin']}>
                <UserManagment />
              </ProtectedRoute>
            }
          />
          <Route
            path="appointments"
            element={
              <ProtectedRoute roles={['Doctor']}>
                <AppointmentsPage />
              </ProtectedRoute>
            }
          />
          <Route path="lab-reports" element={<LabReportsPage />} />
          <Route
            path="medical-history"
            element={
              <ProtectedRoute roles={['Patient']}>
                <MedicalHistoryPage />
              </ProtectedRoute>
            }
          />
          <Route path="messaging" element={<Messaging />} />
          <Route path="messages" element={<Messages />} />
          <Route path="profile" element={<Profile />} />
        </Route>
      </Routes>
    </Router>
  )
}

export default App