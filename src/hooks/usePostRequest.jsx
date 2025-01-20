import { useState } from 'react';
import axios from 'axios';

const usePostRequest = () => {
  const [res, setRes] = useState({});
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  const postRequest = async (url, payload) => {
    setLoading(true);
    setError(null);
    
    try {
      let response = await axios.post(url, payload);
      setRes(response);  // Save the response;
    } catch (err) {
      setError(err.message || 'An error occurred');
    } finally {
      setLoading(false);
    }
  };

  return { res, loading, error, postRequest };
};

export default usePostRequest;
