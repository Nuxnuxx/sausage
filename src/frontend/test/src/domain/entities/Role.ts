export const Role = {
    Admin: 'Admin',
    Agent: 'Agent',
    Client: 'Client',
} as const;

export type RoleType = typeof Role[keyof typeof Role];