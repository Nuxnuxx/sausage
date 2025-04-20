import type { IImage } from '@/domain/entities/Image';
import type { IAdresse } from '@/domain/entities/Adresse';
import type { IAvis } from '@/domain/entities/Avis';
import type { IPrestationProposee } from '@/domain/entities/PrestationProposee';
import type { Destination } from '@/domain/entities/Destination';

export interface ILogement {
    id: number;
    nomLogement: string;
    prixLogement: number;
    descriptionLogement: string;
    image?: IImage;
    adresse?: IAdresse;
    prestationProposes?: IPrestationProposee[];
    destination: Destination;
    reduction?: number;
}

export class Logement implements ILogement {
    constructor(
        public id: number,
        public nomLogement: string,
        public prixLogement: number,
        public descriptionLogement: string,
        public destination: Destination,
        public image?: IImage,
        public adresse?: IAdresse,
        public prestationProposes?: IPrestationProposee[],
        public reduction?: number
    ) {}
}