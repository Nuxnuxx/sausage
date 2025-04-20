import axios from 'axios';

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL;

const axiosInstance = axios.create({
    baseURL: API_BASE_URL + '/Auth',
    headers: {
        'Content-Type': 'application/json',
    },
});

export const register = async (data: {
  email: string;
  password: string;
  nom: string;
  prenom: string;
  civilite: string;
}) => {
    return axiosInstance.post('/register', data);
};

export const login = async (data: { email: string; password: string }) => {
    return axiosInstance.post('/login', data);
};