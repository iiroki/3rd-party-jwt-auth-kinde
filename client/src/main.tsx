import React from 'react'
import ReactDOM from 'react-dom/client'
import { KindeProvider } from '@kinde-oss/kinde-auth-react'
import App from './App.tsx'
import './index.css'

const KINDE_URL: string = import.meta.env.VITE_KINDE_URL
const KINDE_CLIENT_ID: string = import.meta.env.VITE_KINDE_CLIENT_ID
console.log('KINDE_URL:', KINDE_URL, 'KINDE_CLIENT_ID:', KINDE_CLIENT_ID)

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <KindeProvider
      domain={KINDE_URL}
      clientId={KINDE_CLIENT_ID}
      redirectUri={window.location.origin}
      onRedirectCallback={(user, state) => {
        console.log('user:', user, 'state:', state)
      }}
    >
      <App />
    </KindeProvider>
  </React.StrictMode>,
)
