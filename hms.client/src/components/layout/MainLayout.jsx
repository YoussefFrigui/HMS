import React from 'react'
import Sidebar from './Sidebar.jsx'
import { Outlet } from 'react-router-dom'

function MainLayout() {
  return (
    <div className="main-layout">
      <Sidebar />
      <div className="content">
        <Outlet />
      </div>
    </div>
  )
}

export default MainLayout