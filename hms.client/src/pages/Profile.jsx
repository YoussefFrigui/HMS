import React, { useEffect, useState } from 'react'
import { getUserProfile, updateUserProfile } from '@/services/user.service.js'
import Input from '@/components/common/Input.jsx'
import Button from '@/components/common/Button.jsx'

function Profile() {
  const [profile, setProfile] = useState({ email: '', role: '', name: '' })
  const [editing, setEditing] = useState(false)

  useEffect(() => {
    async function fetchProfile() {
      const data = await getUserProfile()
      setProfile(data)
    }
    fetchProfile()
  }, [])

  const handleChange = (e) => {
    setProfile({ ...profile, [e.target.name]: e.target.value })
  }

  const handleSave = async () => {
    await updateUserProfile(profile)
    setEditing(false)
    alert('Profile updated successfully')
  }

  return (
    <div>
      <h2>Profile</h2>
      <form>
        <Input
          label="Name"
          name="name"
          value={profile.name}
          onChange={handleChange}
          disabled={!editing}
        />
        <Input
          label="Email"
          name="email"
          type="email"
          value={profile.email}
          onChange={handleChange}
          disabled
        />
        <Input
          label="Role"
          name="role"
          value={profile.role}
          onChange={handleChange}
          disabled
        />
        {editing ? (
          <Button onClick={handleSave}>Save</Button>
        ) : (
          <Button onClick={() => setEditing(true)}>Edit</Button>
        )}
      </form>
    </div>
  )
}

export default Profile