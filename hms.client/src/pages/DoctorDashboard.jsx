import React, { useEffect, useState } from 'react';
import { apiClient } from '@/services/apiClient';
import '../assets/styles/index.css';

const DoctorDashboard = () => {
  const [appointments, setAppointments] = useState([]);
  const [labReports, setLabReports] = useState([]);
  const [medicalHistory, setMedicalHistory] = useState([]);
  const [message, setMessage] = useState('');

  useEffect(() => {
    fetchAppointments();
    fetchLabReports();
  }, []);

  const fetchAppointments = async () => {
    try {
      const response = await apiClient.get('/api/doctor/appointments');
      setAppointments(response.data);
    } catch (error) {
      console.error('Error fetching appointments', error);
    }
  };

  const fetchLabReports = async () => {
    try {
      const response = await apiClient.get('/api/doctor/lab-reports');
      setLabReports(response.data);
    } catch (error) {
      console.error('Error fetching lab reports', error);
    }
  };

  const fetchMedicalHistory = async (patientId) => {
    try {
      const response = await apiClient.get(`/api/doctor/medical-history/${patientId}`);
      setMedicalHistory(response.data);
    } catch (error) {
      console.error('Error fetching medical history', error);
    }
  };

  const handleSendMessage = async () => {
    try {
      await apiClient.post('/api/doctor/send-message', { content: message });
      setMessage('');
      alert('Message sent successfully');
    } catch (error) {
      console.error('Error sending message', error);
    }
  };

  return (
    <div>
      <h1>Doctor Dashboard</h1>
      <section>
        <h2>Appointments</h2>
        <ul>
          {appointments.map((appointment) => (
            <li key={appointment.id}>
              {appointment.patientName} - {appointment.appointmentDate}
            </li>
          ))}
        </ul>
      </section>

      <section>
        <h2>Lab Reports</h2>
        <ul>
          {labReports.map((report) => (
            <li key={report.id}>{report.reportName} - {report.resultDetails}</li>
          ))}
        </ul>
      </section>

      <section>
        <h2>Medical History</h2>
        <button onClick={() => fetchMedicalHistory(1)}>
          Fetch Medical History for Patient 1
        </button>
        <ul>
          {medicalHistory.map((history) => (
            <li key={history.id}>{history.details}</li>
          ))}
        </ul>
      </section>

      <section>
        <h2>Send Message</h2>
        <textarea
          value={message}
          onChange={(e) => setMessage(e.target.value)}
          placeholder="Type your message here"
        />
        <button onClick={handleSendMessage}>Send Message</button>
      </section>
    </div>
  );
};

export default DoctorDashboard;