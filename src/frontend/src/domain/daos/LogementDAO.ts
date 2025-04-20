import { Logement } from '@/domain/entities/Logement'; 
import type { IDAO } from '@/domain/daos/IDAO'; 
import axios from 'axios'; 

export class LogementDAO implements IDAO<Logement> { 
    private static instance: LogementDAO; 
    private apiUrl: string;

    private constructor() {
        this.apiUrl = import.meta.env.VITE_API_BASE_URL || 'http://localhost:5185/api';
    }

    public static getInstance(): LogementDAO { 
        if (!LogementDAO.instance) { 
            LogementDAO.instance = new LogementDAO(); 
        } 
        return LogementDAO.instance; 
    } 

    public async create(data: Logement): Promise<Logement> { 
        try { 
            const response = await axios.post(`${this.apiUrl}/Logement`, data); 
            return response.data; 
        } catch (error) { 
            throw new Error('Impossible de créer le nouveau logement');
        } 
    } 

    public async get(id: number): Promise<Logement> { 
        try {
            const response = await axios.get(`${this.apiUrl}/Logement/${id}`);
            return response.data;
        } catch (error) {
            throw new Error('Impossible de récupérer le logement');
        }
    } 
    
    public async update(id: number, data: Logement): Promise<Logement> { 
        try {
            const response = await axios.put(`${this.apiUrl}/Logement/${id}`, data);
            return response.data;
        } catch (error) {
            throw new Error('Impossible de modifier le logement');
        }
    } 

    public async delete(id: number): Promise<void> { 
        try { 
            await axios.delete(`${this.apiUrl}/Logement/${id}`); 
        } catch (error) { 
            throw new Error('Impossible de supprimer le logement'); 
        } 
    } 
    
    public async list(nomLogement?: string, destinationNom?: string): Promise<Logement[]> { 
        try {
            // Build query params according to Swagger spec
            const params: Record<string, string> = {};
            if (nomLogement) params.nomLogement = nomLogement;
            if (destinationNom) params.destinationNom = destinationNom;
            
            const response = await axios.get(`${this.apiUrl}/Logement/getAll`, { params });
            return response.data;
        } catch (error) {
            console.error('Error fetching logements:', error);
            return [];
        }
    } 

    public async getRandomOffers(count: number = 9): Promise<Logement[]> {
        try {
            const response = await axios.get(`${this.apiUrl}/Logement/random?count=${count}`);
            return response.data;
        } catch (error) {
            console.error('Error fetching random logements:', error);
            return [];
        }
    }

    // Récupère les logements selon la destination.
    public async getByDestination(destinationName: string): Promise<Logement[]> {
        try {
            // Using the getAll endpoint with destinationNom param instead
            return this.list(undefined, destinationName);
        } catch (error) {
            console.error('Error fetching logements by destination:', error);
            return [];
        }
    }

    // Récupère les offres spéciales (logements avec les meilleurs avis).
    public async getSpecialOffer(count: number = 5): Promise<Logement[]> {
        try {
            const response = await axios.get(`${this.apiUrl}/Logement/special?count=${count}`);
            return response.data;
        } catch (error) {
            console.error('Error fetching special offers:', error);
            return [];
        }
    }

    // Récupère les offres spéciales pour une destination spécifique.
    public async getSpecialOfferByDestination(destinationName: string, count: number = 5): Promise<Logement[]> {
        try {
            const response = await axios.get(`${this.apiUrl}/Logement/special/${destinationName}?count=${count}`);
            return response.data;
        } catch (error) {
            console.error('Error fetching special offers by destination:', error);
            return [];
        }
    }
}