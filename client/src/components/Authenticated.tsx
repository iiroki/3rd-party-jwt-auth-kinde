import { useKindeAuth } from '@kinde-oss/kinde-auth-react'
import React from 'react'
import api from '../services/api'

const Authenticated: React.FC = () => {
  const [whoami, setWhoami] = React.useState<object | null>(null)
  const { user, getToken, logout } = useKindeAuth()
  if (!user) {
    throw new Error('Could not get Kinde user')
  }

  const handleCopyToken = async () => {
    const token = await getToken()
    navigator.clipboard.writeText(token ?? 'no-token')
  }

  const handleWhoami = async () => {
    const token = await getToken()
    if (token) {
      const whoami = await api.whoami(token)
      setWhoami(whoami)
    }
  }

  return (
    <div className='auth-form'>
      <h2>From Kinde:</h2>
      <div><b>Name:</b> {user.given_name} {user.family_name}</div>
      <div><b>Email:</b> {user.email}</div>
      <div><b>ID:</b> {user.id}</div>
      <div>
        <button onClick={handleCopyToken}>Copy token</button>
        <span style={{ paddingLeft: 10 }} />
        <button onClick={logout}>Log out</button>
      </div>
      <div style={{ paddingTop: 10 }} />
      <h2>From API:</h2>
      <div>
        <button onClick={handleWhoami}>Whoami</button>
      </div>
      <div>
        <b>Response:</b> {whoami ? JSON.stringify(whoami, undefined, 2) : '-'}
        </div>
    </div>
  )
}

export default Authenticated
