import type { ILogement } from '@/domain/entities/Logement';
import type { IPrestation } from '@/domain/entities/Prestation';

export interface IPrestationProposee {
    idPrestationProposee: number;
    prixPrestation: number;
    logement?: ILogement;
    prestation?: IPrestation;
}

export class PrestationProposee implements IPrestationProposee {
    constructor(
        public idPrestationProposee: number,
        public prixPrestation: number,
        public logement?: ILogement,
        public prestation?: IPrestation
    ) {}
}