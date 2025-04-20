import type { IReservation } from '@/domain/entities/Reservation';

export interface IRemise {
    id: number;
    nomRemise: string;
    codeRemise: string;
    pourcentageRemise: number;
    reservations?: IReservation[];
}

export class Remise implements IRemise {
    constructor(
        public id: number,
        public nomRemise: string,
        public codeRemise: string,
        public pourcentageRemise: number,
        public reservations?: IReservation[]
    ) {}
}