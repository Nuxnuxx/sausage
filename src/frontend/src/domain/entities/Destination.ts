export interface IDestination {
    id: Number;
    name: string;
    description?: string;
}

export class Destination implements IDestination {
    constructor(
        public id: Number,
        public name: string,
        public description?: string
    ) {}
}