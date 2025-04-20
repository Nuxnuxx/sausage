import type { ILogement } from './Logement';
import type { IUtilisateur } from '@/domain/entities/Utilisateur';

export interface IAdresse {
    id: number;
    nomAdresse: string;
    complementAdresse?: string;
    ville: string;
    codePostal: number;
    pays: string;
    logements?: ILogement[];
    utilisateurs?: IUtilisateur[];
}

export class Adresse implements IAdresse {
    constructor(
        public id: number,
        public nomAdresse: string,
        public ville: string,
        public codePostal: number,
        public pays: string,
        public complementAdresse?: string,
        public logements?: ILogement[],
        public utilisateurs?: IUtilisateur[]
    ) {}
}