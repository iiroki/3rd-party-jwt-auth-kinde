import { useKindeAuth } from '@kinde-oss/kinde-auth-react'
import {} from '@kinde-oss/kinde-auth-react/'
import React from 'react'

const AuthenticationForm: React.FC = () => {
  const { login } = useKindeAuth()

  const handleLogin = () => {
    const state = 'qwertyuiop'
    login({ app_state: { state } })
  }

  return <button type='button' onClick={handleLogin}>To Login</button>
}

export default AuthenticationForm
