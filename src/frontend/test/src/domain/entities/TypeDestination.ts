export interface ITypeDestination {
    id?: number;
    nomTypeDestination: string;
    descriptionTypeDestination?: string;
}

export class TypeDestination implements ITypeDestination {
    constructor(
        public nomTypeDestination: string,
        public descriptionTypeDestination?: string,
        public id?: number
    ) {}
}