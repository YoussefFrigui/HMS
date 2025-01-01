import React from 'react'

function Calendar({ appointments }) {
  return (
    <div>
      <h3>Calendar View</h3>
      <ul>
        {appointments.map(a => (
          <li key={a.id}>
            {a.date} - {a.patientName}
          </li>
        ))}
      </ul>
    </div>
  )
}

export default Calendar