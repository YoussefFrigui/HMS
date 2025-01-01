import React from 'react'
import { uploadReport } from '@/services/lab.service.js'

function LabReportUpload() {
  const [file, setFile] = React.useState(null)

  const handleUpload = () => {
    if (file) uploadReport(file)
  }

  return (
    <div>
      <h3>Upload Lab Report</h3>
      <input type="file" onChange={e => setFile(e.target.files[0])} />
      <button onClick={handleUpload}>Upload</button>
    </div>
  )
}

export default LabReportUpload