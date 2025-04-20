import type { IAdresse } from '@/domain/entities/Adresse';
import type { IReservation } from '@/domain/entities/Reservation';
import type { IAvis } from '@/domain/entities/Avis';
import type { RoleType } from '@/domain/entities/Role';

export interface IUtilisateur {
    id: number;
    mailUtilisateur: string;
    nomUtilisateur?: string;
    prenomUtilisateur?: string;
    mdpUtilisateur?: string;
    civiliteUtilisateur?: string;
    telephoneUtilisateur?: string;
    newsLetterUtilisateur: boolean;
    role?: RoleType;
    adresse?: IAdresse;
    reservations?: IReservation[];
    avis?: IAvis[];
}

export class Utilisateur implements IUtilisateur {
    constructor(
        public id: number,
        public mailUtilisateur: string,
        public newsLetterUtilisateur: boolean,
        public nomUtilisateur?: string,
        public prenomUtilisateur?: string,
        public mdpUtilisateur?: string,
        public civiliteUtilisateur?: string,
        public telephoneUtilisateur?: string,
        public role?: RoleType,
        public adresse?: IAdresse,
        public reservations?: IReservation[],
        public avis?: IAvis[]
    ) {}
}