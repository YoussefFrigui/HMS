import React from 'react'

function MedicalHistoryForm() {
  const [history, setHistory] = React.useState('')

  const handleSubmit = e => {
    e.preventDefault()
    // Submit medical history to API
  }

  return (
    <form onSubmit={handleSubmit}>
      <h3>Medical History</h3>
      <textarea
        value={history}
        onChange={e => setHistory(e.target.value)}
        placeholder="Enter your medical history"
      />
      <button type="submit">Submit</button>
    </form>
  )
}

export default MedicalHistoryForm