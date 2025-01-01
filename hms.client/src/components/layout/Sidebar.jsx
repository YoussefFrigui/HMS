import React from 'react'
import { Link } from 'react-router-dom'
import { useAuth } from '@/hooks/useAuth.js'

function Sidebar() {
  const { user } = useAuth()

  return (
    <aside className="sidebar">
      <h2>Healthcare System</h2>
      <nav>
        <ul>
          <li><Link to="/dashboard">Dashboard</Link></li>
          {user?.role === 'Admin' && <li><Link to="/user-management">User Management</Link></li>}
          {user?.role === 'Doctor' && <li><Link to="/appointments">Appointments</Link></li>}
          {user?.role === 'Patient' && <li><Link to="/medical-history">Medical History</Link></li>}
          <li><Link to="/lab-reports">Lab Reports</Link></li>
          <li><Link to="/messaging">Messaging</Link></li>
        </ul>
      </nav>
    </aside>
  )
}

export default Sidebar