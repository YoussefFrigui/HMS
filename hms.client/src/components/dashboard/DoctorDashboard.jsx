import React from 'react'
import Appointments from '@/components/appointments/Appointments.jsx'
import LabReportList from '@/components/lab/LabReportList.jsx'

function DoctorDashboard() {
  return (
    <div>
      <h1>Doctor Dashboard</h1>
      <Appointments />
      <LabReportList />
    </div>
  )
}

export default DoctorDashboard