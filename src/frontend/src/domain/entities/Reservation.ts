import type { IRemise } from '@/domain/entities/Remise';
import type { ILogement } from '@/domain/entities/Logement';
import type { IUtilisateur } from '@/domain/entities/Utilisateur';

export interface IReservation {
    id: number;
    numeroReservation: number;
    arriveeReservation: string; 
    departReservation: string;
    participantReservation: number;
    prixReservation: number;
    etatReservation: string;
    cartePaiement: number;
    statutPaiement: string;
    remise?: IRemise;
    logement?: ILogement;
    utilisateur?: IUtilisateur;
}

export class Reservation implements IReservation {
    constructor(
        public id: number,
        public numeroReservation: number,
        public arriveeReservation: string,
        public departReservation: string,
        public participantReservation: number,
        public prixReservation: number,
        public etatReservation: string,
        public cartePaiement: number,
        public statutPaiement: string,
        public remise?: IRemise,
        public logement?: ILogement,
        public utilisateur?: IUtilisateur
    ) {}
}