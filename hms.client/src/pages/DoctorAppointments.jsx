import React, { useState, useEffect } from 'react';
import { Calendar, momentLocalizer } from 'react-big-calendar';
import moment from 'moment';
import 'react-big-calendar/lib/css/react-big-calendar.css';
import { apiClient } from '@/services/apiClient';
import '../assets/styles/index.css';

const localizer = momentLocalizer(moment);

const DoctorAppointments = () => {
  const [appointments, setAppointments] = useState([]);

  useEffect(() => {
    const fetchAppointments = async () => {
      try {
        const response = await apiClient.get('/api/doctor/appointments');
        const formattedAppointments = response.data.map((appointment) => ({
          title: `${appointment.patientName} - ${appointment.details}`,
          start: new Date(appointment.appointmentDate),
          end: new Date(moment(appointment.appointmentDate).add(1, 'hours')), // Example duration
        }));
        setAppointments(formattedAppointments);
      } catch (error) {
        console.error('Error fetching appointments:', error);
      }
    };

    fetchAppointments();
  }, []);

  return (
    <div>
      <h1>Doctor's Appointments</h1>
      <Calendar
        localizer={localizer}
        events={appointments}
        startAccessor="start"
        endAccessor="end"
        style={{ height: 500 }}
      />
    </div>
  );
};

export default DoctorAppointments;