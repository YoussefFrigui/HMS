import React from 'react'
import { getAllUsers } from '@/services/user.service.js'

function UserManagment() {
  const [users, setUsers] = React.useState([])

  React.useEffect(() => {
    getAllUsers().then(setUsers)
  }, [])

  return (
    <div>
      <h2>User Management</h2>
      <ul>
        {users.map(u => (
          <li key={u.id}>
            {u.email} - {u.role}
          </li>
        ))}
      </ul>
    </div>
  )
}

export default UserManagment