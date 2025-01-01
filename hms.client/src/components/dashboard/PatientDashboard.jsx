import React from 'react'
import MedicalHistoryForm from '@/components/forms/MedicalHistoryForm.jsx'
import LabReportUpload from '@/components/lab/LabReportUpload.jsx'

function PatientDashboard() {
  return (
    <div>
      <h1>Patient Dashboard</h1>
      <MedicalHistoryForm />
      <LabReportUpload />
    </div>
  )
}

export default PatientDashboard