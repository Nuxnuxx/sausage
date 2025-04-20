import { type IPrestationProposee } from '@/domain/entities/PrestationProposee';

export interface IPrestation {
    id: number;
    nomPrestation: string;
    prestationProposees: IPrestationProposee[];
}

export class Prestation implements IPrestation {
    constructor(
        public id: number,
        public nomPrestation: string,
        public prestationProposees: IPrestationProposee[]
    ) {}
}