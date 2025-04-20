import { defineStore } from 'pinia';
import { login, register } from '@/domain/daos/AuthDAO';

export const useAuthStore = defineStore('auth', {
    state: () => ({
        user: JSON.parse(localStorage.getItem('authUser') || 'null'),
        token: localStorage.getItem('authToken') || null,
        tokenExpiration: localStorage.getItem('authTokenExpiration') || null,
    }),
    actions: {
        async loginUser(credentials: { email: string; password: string }) {
            try {
                const response = await login(credentials);
                this.user = response.data.user;
                this.token = response.data.token;

                // Définir une expiration d'une heure
                const expiration = new Date().getTime() + 3600 * 1000; // 1 heure en millisecondes
                this.tokenExpiration = expiration.toString();

                localStorage.setItem('authUser', JSON.stringify(this.user));
                localStorage.setItem('authToken', this.token!);
                localStorage.setItem('authTokenExpiration', expiration.toString());
            } catch (error) {
                console.error('Erreur lors de la connexion :', error);
                throw new Error('Impossible de se connecter. Vérifiez vos identifiants.');
            }
        },

        async registerUser(data: {
            email: string;
            password: string;
            nom: string;
            prenom: string;
            civilite: string;
        }) {
            try {
                const response = await register(data);
                this.user = response.data.user;
                this.token = response.data.token;

                // Définir une expiration d'une heure
                const expiration = new Date().getTime() + 3600 * 1000; // 1 heure en millisecondes
                this.tokenExpiration = expiration.toString();

                localStorage.setItem('authUser', JSON.stringify(this.user));
                localStorage.setItem('authToken', this.token!);
                localStorage.setItem('authTokenExpiration', expiration.toString());
            } catch (error) {
                console.error('Erreur lors de l\'inscription :', error);
                throw new Error('Impossible de s\'inscrire. Vérifiez vos informations.');
            }
        },

        logoutUser() {
            this.user = null;
            this.token = null;
            this.tokenExpiration = null;
            localStorage.removeItem('authUser');
            localStorage.removeItem('authToken');
            localStorage.removeItem('authTokenExpiration');
        },

        isTokenExpired(): boolean {
            const expiration = this.tokenExpiration ? parseInt(this.tokenExpiration, 10) : 0;
            return new Date().getTime() > expiration;
        },
    },
    getters: {
        isLoggedIn: (state) => !!state.token,
    },
});