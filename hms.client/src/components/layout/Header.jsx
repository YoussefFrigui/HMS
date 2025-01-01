import React from 'react'
import { useAuth } from '@/hooks/useAuth.js'
import Button from '@/components/common/Button.jsx'

function Header() {
  const { user, logout } = useAuth()

  const handleLogout = () => {
    logout()
  }

  return (
    <header className="app-header">
      <h1>Healthcare System</h1>
      {user && (
        <div className="user-info">
          <span>{user.name}</span>
          <Button onClick={handleLogout}>Logout</Button>
        </div>
      )}
    </header>
  )
}

export default Header