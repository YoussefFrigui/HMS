import React, { useState } from 'react'
import Input from '@/components/common/Input.jsx'
import Button from '@/components/common/Button.jsx'
import { createAppointment } from '@/services/appointment.service.js'
import { useAuth } from '@/hooks/useAuth.js'

function AppointmentForm() {
  const { user } = useAuth()
  const [form, setForm] = useState({ date: '', time: '', patientId: '' })

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value })
  }

  const handleSubmit = async (e) => {
    e.preventDefault()
    try {
      await createAppointment({ ...form, doctorId: user.id })
      alert('Appointment created successfully')
      setForm({ date: '', time: '', patientId: '' })
    } catch (error) {
      console.error(error)
      alert('Failed to create appointment')
    }
  }

  return (
    <form onSubmit={handleSubmit}>
      <h3>Create Appointment</h3>
      <Input
        label="Date"
        type="date"
        name="date"
        value={form.date}
        onChange={handleChange}
        required
      />
      <Input
        label="Time"
        type="time"
        name="time"
        value={form.time}
        onChange={handleChange}
        required
      />
      <Input
        label="Patient ID"
        type="text"
        name="patientId"
        value={form.patientId}
        onChange={handleChange}
        required
      />
      <Button type="submit">Create</Button>
    </form>
  )
}

export default AppointmentForm