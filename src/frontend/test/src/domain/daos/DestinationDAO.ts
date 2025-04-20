import { Destination } from '@/domain/entities/Destination';
import type { IDAO } from '@/domain/daos/IDAO';
import axios from 'axios';

export class DestinationDAO implements IDAO<Destination> {
    private static instance: DestinationDAO;
    private apiUrl: string;

    private constructor() {
        this.apiUrl = import.meta.env.VITE_API_BASE_URL || 'http://localhost:5185/api';
    }

    public static getInstance(): DestinationDAO {
        if (!DestinationDAO.instance) {
            DestinationDAO.instance = new DestinationDAO();
        }
        return DestinationDAO.instance;
    }

    public async create(data: Destination): Promise<Destination> {
        try {
            const response = await axios.post(`${this.apiUrl}/Destination`, data);
            return response.data;
        } catch (error) {
            throw new Error('Impossible de créer la destination');
        }
    }

    public async get(id: number): Promise<Destination> {
        try {
            const response = await axios.get(`${this.apiUrl}/Destination/${id}`);
            return response.data;
        } catch (error) {
            throw new Error('Impossible de récupérer la destination');
        }
    }

    public async update(id: number, data: Destination): Promise<Destination> {
        try {
            const response = await axios.put(`${this.apiUrl}/Destination/${id}`, data);
            return response.data;
        } catch (error) {
            throw new Error('Impossible de modifier la destination');
        }
    }

    public async delete(id: number): Promise<void> {
        try {
            await axios.delete(`${this.apiUrl}/Destination/${id}`);
        } catch (error) {
            throw new Error('Impossible de supprimer la destination');
        }
    }

    public async list(): Promise<Destination[]> {
        try {
            const response = await axios.get(`${this.apiUrl}/Destination/getAll`);
            return response.data;
        } catch (error) {
            console.error('Error fetching destinations:', error);
            return [];
        }
    }

    public async isExisting(name: string): Promise<boolean> {
        try {
            const response = await axios.get(`${this.apiUrl}/Destination/check?name=${name}`);
            return response.data.exists;
        } catch (error) {
            throw new Error('Impossible de vérifier l\'existence de la destination');
        }
    }
}