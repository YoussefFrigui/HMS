import axios from 'axios';

export const apiClient = axios.create({
  baseURL: '/', // Proxy handles the API calls
  headers: {
    'Content-Type': 'application/json',
  },
});

// Add interceptor to include token in requests
apiClient.interceptors.request.use((config) => {
  const token = localStorage.getItem('token');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export default apiClient;