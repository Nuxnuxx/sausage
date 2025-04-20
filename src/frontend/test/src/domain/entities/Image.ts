import type { ILogement } from '@/domain/entities/Logement';

export interface IImage {
    id: number;
    urlImage: string;
    nomImage: string;
    logements?: ILogement[]; // Relation OneToMany
}

export class Image implements IImage {
    constructor(
        public id: number,
        public urlImage: string,
        public nomImage: string,
        public logements?: ILogement[]
    ) {}
}