import { TypeDestination } from '@/domain/entities/TypeDestination';
import type { IDAO } from '@/domain/daos/IDAO';
import axios from 'axios';

export class TypeDestinationDAO implements IDAO<TypeDestination> {
    private static instance: TypeDestinationDAO;
    private apiUrl: string;

    private constructor() {
        this.apiUrl = import.meta.env.VITE_API_BASE_URL || 'http://localhost:5185/api';
    }

    public static getInstance(): TypeDestinationDAO {
        if (!TypeDestinationDAO.instance) {
            TypeDestinationDAO.instance = new TypeDestinationDAO();
        }
        return TypeDestinationDAO.instance;
    }

    public async create(data: TypeDestination): Promise<TypeDestination> {
        try {
            const response = await axios.post(`${this.apiUrl}/TypeDestination`, data);
            return new TypeDestination(response.data.nomTypeDestination, response.data.descriptionTypeDestination, response.data.id);
        } catch (error) {
            throw new Error('Impossible de créer le type de destination');
        }
    }

    public async get(id: number): Promise<TypeDestination> {
        try {
            const response = await axios.get(`${this.apiUrl}/TypeDestination/${id}`);
            return new TypeDestination(response.data.nomTypeDestination, response.data.descriptionTypeDestination, response.data.id);
        } catch (error) {
            throw new Error('Impossible de récupérer le type de destination');
        }
    }

    public async update(id: number, data: TypeDestination): Promise<TypeDestination> {
        try {
            const response = await axios.put(`${this.apiUrl}/TypeDestination/${data.nomTypeDestination}`, data);
            return new TypeDestination(response.data.nomTypeDestination, response.data.descriptionTypeDestination, response.data.id);
        } catch (error) {
            throw new Error('Impossible de modifier le type de destination');
        }
    }

    public async delete(id: number): Promise<void> {
        try {
            // Note: API uses name instead of ID for deletion according to swagger
            const typeDestination = await this.get(id);
            await axios.delete(`${this.apiUrl}/TypeDestination/${typeDestination.nomTypeDestination}`);
        } catch (error) {
            throw new Error('Impossible de supprimer le type de destination');
        }
    }

    public async list(): Promise<TypeDestination[]> {
        try {
            const response = await axios.get(`${this.apiUrl}/TypeDestination/getAll`);
            return response.data.map((item: any) => 
                new TypeDestination(item.nomTypeDestination, item.descriptionTypeDestination, item.id)
            );
        } catch (error) {
            console.error('Error fetching destination types:', error);
            // Return an empty array when there's an error to prevent breaking the UI
            return [];
        }
    }
}