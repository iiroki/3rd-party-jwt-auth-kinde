import React from 'react'
import ReactDOM from 'react-dom/client'
import { KindeProvider } from '@kinde-oss/kinde-auth-react'
import App from './App.tsx'
import { API_URL, KINDE_CLIENT_ID, KINDE_URL } from './config.ts'
import './index.css'

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <KindeProvider
      domain={KINDE_URL}
      clientId={KINDE_CLIENT_ID}
      audience={API_URL}
      redirectUri={window.location.origin}
      onRedirectCallback={(user, state) => {
        console.log('Callback - user:', user, 'state:', state)
      }}
    >
      <App />
    </KindeProvider>
  </React.StrictMode>,
)
