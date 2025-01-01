import React from 'react'
import { listAppointments } from '@/services/appointment.service.js'
import Calendar from './Calendar.jsx'

function Appointments() {
  const [appointments, setAppointments] = React.useState([])

  React.useEffect(() => {
    listAppointments().then(setAppointments)
  }, [])

  return (
    <div>
      <h2>Appointments</h2>
      <Calendar appointments={appointments} />
    </div>
  )
}

export default Appointments