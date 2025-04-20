import type { IUtilisateur } from '@/domain/entities/Utilisateur';
import type { IDAO } from '@/domain/daos/IDAO';

import axios from 'axios';

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL;

const axiosInstance = axios.create({
    baseURL: API_BASE_URL + '/Utilisateur',
    headers: {
        'Content-Type': 'application/json',
    },
});

export class UtilisateurDAO /* implements IDAO<IUtilisateur> */ {
    private static instance: UtilisateurDAO;
    // private mockData: IUtilisateur[];

    private constructor() {
        // Initialisation des données mockées
        // this.mockData = mockUtilisateurs;
    }

    public static getInstance(): UtilisateurDAO {
        if (!UtilisateurDAO.instance) {
            UtilisateurDAO.instance = new UtilisateurDAO();
        }
        return UtilisateurDAO.instance;
    }

    // public async create(data: IUtilisateur): Promise<IUtilisateur> {
    //     try {
    //         const newUtilisateur = { ...data, id: this.mockData.length + 1 }; // Génère un nouvel ID
    //         this.mockData.push(newUtilisateur);
    //         return Promise.resolve(newUtilisateur);
    //     } catch (error) {
    //         throw new Error('Impossible de créer l\'utilisateur');
    //     }
    // }

    // public async get(id: number): Promise<IUtilisateur> {
    //     try {
    //         const utilisateur = this.mockData.find((u) => u.id === id);
    //         if (!utilisateur) {
    //             throw new Error(`Utilisateur avec l'ID ${id} introuvable`);
    //         }
    //         return Promise.resolve(utilisateur);
    //     } catch (error) {
    //         throw new Error('Impossible de récupérer l\'utilisateur');
    //     }
    // }

    // Récupère un utilisateur par son email
    public async getUserByEmail(email: string): Promise<IUtilisateur> {
        try {
            const response = await axiosInstance.get(`/${email}`);
            return response.data;
        } catch (error) {
            console.error(`Erreur lors de la récupération de l'utilisateur avec l'email ${email}:`, error);
            throw new Error('Impossible de récupérer l\'utilisateur');
        }
    }

    // public async update(id: number, data: IUtilisateur): Promise<IUtilisateur> {
    //     try {
    //         const index = this.mockData.findIndex((u) => u.id === id);
    //         if (index === -1) {
    //             throw new Error(`Utilisateur avec l'ID ${id} introuvable`);
    //         }

    //         this.mockData[index] = { ...this.mockData[index], ...data };
    //         return Promise.resolve(this.mockData[index]);
    //     } catch (error) {
    //         throw new Error('Impossible de modifier l\'utilisateur');
    //     }
    // }

    // Met à jour un utilisateur
    public async updateUser(data: IUtilisateur): Promise<IUtilisateur> {
        try {
            const response = await axiosInstance.put('/update', data);
            return response.data;
        } catch (error) {
            console.error('Erreur lors de la mise à jour de l\'utilisateur:', error);
            throw new Error('Impossible de mettre à jour l\'utilisateur');
        }
    }

    // public async delete(id: number): Promise<void> {
    //     try {
    //         const index = this.mockData.findIndex((u) => u.id === id);
    //         if (index === -1) {
    //             throw new Error(`Utilisateur avec l'ID ${id} introuvable`);
    //         }

    //         this.mockData.splice(index, 1);
    //         return Promise.resolve();
    //     } catch (error) {
    //         throw new Error('Impossible de supprimer l\'utilisateur');
    //     }
    // }

    // Supprime un utilisateur par son email
    public async deleteUserByEmail(email: string): Promise<void> {
        try {
            await axiosInstance.delete(`/delete/${email}`);
        } catch (error) {
            console.error(`Erreur lors de la suppression de l'utilisateur avec l'email ${email}:`, error);
            throw new Error('Impossible de supprimer l\'utilisateur');
        }
    }

    public async list(): Promise<IUtilisateur[]> {
        try {
            // return Promise.resolve(this.mockData);
            const response = await axiosInstance.get('/getAll');
            return response.data;
        } catch (error) {
            throw new Error('Impossible de récupérer la liste des utilisateurs');
        }
    }

    // Récupère les utilisateurs par leur nom.
    // public async getByNom(nom: string): Promise<IUtilisateur[]> {
    //     try {
    //         const utilisateurs = this.mockData.filter((u) => u.nomUtilisateur?.toLowerCase() === nom.toLowerCase());
    //         return Promise.resolve(utilisateurs);
    //     } catch (error) {
    //         throw new Error('Impossible de récupérer les utilisateurs par nom');
    //     }
    // }

    // Récupère l'utilisateur actuellement connecté
    public async getMe(): Promise<IUtilisateur> {
        try {
            return await axiosInstance.get('/me');
        } catch (error) {
            console.error('Erreur lors de la récupération de l\'utilisateur connecté:', error);
            throw new Error('Impossible de récupérer l\'utilisateur connecté');
        }
    }
}