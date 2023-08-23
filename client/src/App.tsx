import React from 'react'
import { useKindeAuth } from '@kinde-oss/kinde-auth-react'
import Authenticated from './components/Authenticated'
import AuthenticationForm from './components/AuthenticationForm'
import ReactLogo from './assets/react.svg'
import ViteLogo from '/vite.svg'
import './App.css'

const App: React.FC = () => {
  const { isAuthenticated, isLoading } = useKindeAuth()
  if (isLoading) {
    return <>Loading...</>
  }

  return (
    <>
      <div>
        <a href="https://vitejs.dev" target="_blank">
          <img src={ViteLogo} className="logo" alt="Vite logo" />
        </a>
        <a href="https://react.dev" target="_blank">
          <img src={ReactLogo} className="logo react" alt="React logo" />
        </a>
      </div>
      <h1>3rd Party JWT Auth with Kinde</h1>
      <div className="card">
        {isAuthenticated ? <Authenticated /> : <AuthenticationForm />}
      </div>
    </>
  )
}

export default App
