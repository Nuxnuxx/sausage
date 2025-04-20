import type { ILogement } from '@/domain/entities/Logement';
import type { IUtilisateur } from '@/domain/entities/Utilisateur';

export interface IAvis {
    id: number;
    noteAvis: number;
    texteAvis?: string;
    logementId: number | ILogement;
    utilisateurId: number | IUtilisateur;
}

export class Avis implements IAvis {
    constructor(
        public id: number,
        public noteAvis: number,
        public texteAvis: string | undefined,
        public logementId: number | ILogement,
        public utilisateurId: number | IUtilisateur
    ) {}
}