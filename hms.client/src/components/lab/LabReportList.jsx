import React from 'react'
import { getLabReports } from '@/services/lab.service.js'

function LabReportList() {
  const [reports, setReports] = React.useState([])

  React.useEffect(() => {
    getLabReports().then(setReports)
  }, [])

  return (
    <ul>
      {reports.map(r => (
        <li key={r.id}>{r.title}</li>
      ))}
    </ul>
  )
}

export default LabReportList