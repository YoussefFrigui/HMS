import React from 'react'
import { login } from '@/services/auth.service.js'
import { useAuth } from '@/hooks/useAuth.js'

function LoginForm() {
  const { setUser } = useAuth()
  const [email, setEmail] = React.useState('')
  const [password, setPassword] = React.useState('')

  const handleSubmit = async e => {
    e.preventDefault()
    const userData = await login(email, password)
    setUser(userData)
  }

  return (
    <form onSubmit={handleSubmit}>
      <h2>Login</h2>
      <input
        type="email"
        value={email}
        onChange={e => setEmail(e.target.value)}
        placeholder="Email"
      />
      <input
        type="password"
        value={password}
        onChange={e => setPassword(e.target.value)}
        placeholder="Password"
      />
      <button type="submit">Sign In</button>
    </form>
  )
}

export default LoginForm