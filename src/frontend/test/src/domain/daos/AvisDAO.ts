import type { IAvis } from '@/domain/entities/Avis';
import type { IDAO } from '@/domain/daos/IDAO';
import axios from 'axios';

export class AvisDAO implements IDAO<IAvis> {
    private static instance: AvisDAO;
    private apiUrl: string;

    private constructor() {
        this.apiUrl = import.meta.env.VITE_API_BASE_URL || 'http://localhost:5185/api';
    }

    public static getInstance(): AvisDAO {
        if (!AvisDAO.instance) {
            AvisDAO.instance = new AvisDAO();
        }
        return AvisDAO.instance;
    }

    public async create(data: IAvis): Promise<IAvis> {
        try {
            const response = await axios.post(`${this.apiUrl}/Avis`, data);
            return response.data;
        } catch (error) {
            throw new Error('Impossible de créer l\'avis');
        }
    }

    public async get(id: number): Promise<IAvis> {
        try {
            const response = await axios.get(`${this.apiUrl}/Avis/${id}`);
            return response.data;
        } catch (error) {
            throw new Error('Impossible de récupérer l\'avis');
        }
    }

    public async update(id: number, data: IAvis): Promise<IAvis> {
        try {
            const response = await axios.put(`${this.apiUrl}/Avis/${id}`, data);
            return response.data;
        } catch (error) {
            throw new Error('Impossible de modifier l\'avis');
        }
    }

    public async delete(id: number): Promise<void> {
        try {
            await axios.delete(`${this.apiUrl}/Avis/${id}`);
        } catch (error) {
            throw new Error('Impossible de supprimer l\'avis');
        }
    }

    public async list(): Promise<IAvis[]> {
        try {
            const response = await axios.get(`${this.apiUrl}/Avis/getAll`);
            return response.data;
        } catch (error) {
            console.error('Error fetching avis:', error);
            return [];
        }
    }

    // Récupère les avis associés à un logement spécifique.
    public async getByLogementId(logementId: number): Promise<IAvis[]> {
        try {
            const response = await axios.get(`${this.apiUrl}/Avis/logement/${logementId}`);
            return response.data;
        } catch (error) {
            console.error('Error fetching avis by logement:', error);
            return [];
        }
    }

    // Récupère les avis associés à un utilisateur spécifique.
    public async getByUtilisateurId(utilisateurId: number): Promise<IAvis[]> {
        try {
            const response = await axios.get(`${this.apiUrl}/Avis/utilisateur/${utilisateurId}`);
            return response.data;
        } catch (error) {
            console.error('Error fetching avis by utilisateur:', error);
            return [];
        }
    }

    // Récupère un certain nombre de bons avis (note supérieure ou égale à 4).
    // Si un logementId est fourni, filtre les avis pour ce logement.
    public async getGoodAvis(count: number = 3, logementId?: number): Promise<IAvis[]> {
        try {
            const endpoint = logementId 
                ? `${this.apiUrl}/Avis/good/${logementId}?count=${count}` 
                : `${this.apiUrl}/Avis/good?count=${count}`;
            
            const response = await axios.get(endpoint);
            return response.data;
        } catch (error) {
            console.error('Error fetching good avis:', error);
            return [];
        }
    }
}