import axios from 'axios'

const API_URL: string = import.meta.env.VITE_API_URL
console.log('API_URL:', API_URL)

const whoami = async (token: string): Promise<object> => {
  const res = await axios.get(
    `${API_URL}/whoami`,
    { headers: { Authorization: `Bearer ${token}` } }
  )

  return res.data
}

export default { whoami }
