import axios from 'axios'
import { API_URL } from '../config'

const whoami = async (token: string): Promise<object> => {
  const res = await axios.get(
    `${API_URL}/whoami`,
    { headers: { Authorization: `Bearer ${token}` } }
  )

  return res.data
}

export default { whoami }
