import api from './api.js'

export async function createUser(userData) {
  const res = await api.post('/users', userData)
  return res.data
}

export async function getAllUsers() {
  const res = await api.get('/users')
  return res.data
}

export async function getUserProfile() {
  const res = await api.get('/users/profile')
  return res.data
}

export async function updateUserProfile(profileData) {
  const res = await api.put('/users/profile', profileData)
  return res.data
}

export async function deleteUser(userId) {
  const res = await api.delete(`/users/${userId}`)
  return res.data
}