import { createRouter, createWebHistory } from 'vue-router';
import { useAuthStore } from '@/store/authStore';
import HomeView from '@/presentations/views/Home/HomeView.vue';
import WishesView from '@/presentations/views/Wishes/WishesView.vue';
import OrganizedTripsView from '@/presentations/views/OrganizedTrips/OrganizedTripsView.vue';
import DealsView from '@/presentations/views/Deals/DealsView.vue';
import NotFoundView from '@/presentations/views/NotFound/NotFoundView.vue';
import AboutView from '@/presentations/views/informations/about/AboutView.vue';
import FAQView from '@/presentations/views/informations/faq/FAQView.vue';
import GTCSView from '@/presentations/views/informations/gtcs/GTCSView.vue';
import LegalNoticesView from '@/presentations/views/informations/legalNotices/LegalNoticesView.vue';
import PrivacyPolicyView from '@/presentations/views/informations/privacyPolicy/PrivacyPolicyView.vue';
import LoginView from '@/presentations/views/Auth/Login/LoginView.vue';
import RegisterView from '@/presentations/views/Auth/Register/RegisterView.vue';
import ForgotPasswordView from '@/presentations/views/Auth/ForgotPassword/ForgotPasswordView.vue';
import AccountView from '@/presentations/views/Account/AccountView.vue';
import BookingsView from '@/presentations/views/Account/BookingsView.vue';
import AccountLayout from '@/presentations/components/layout/accountLayout.vue';
import ContactView from '@/presentations/views/Contact/ContactView.vue';
import TripView from '@/presentations/views/Trip/TripView.vue';
import SearchView from '@/presentations/views/Search/SearchView.vue';


const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
        path: '/',
        name: 'home',
        component: HomeView
    },
    {
        path: '/vos-envies',
        name: 'vos-envies',
        component: WishesView
    },
    {
        path: '/voyages-organises',
        name: 'voyages-organises',
        component: OrganizedTripsView
    },
    {
        path: '/bons-plans',
        name: 'bons-plans',
        component: DealsView,
    },
       {
        path: '/mon-compte',
        component: AccountLayout,
        meta: { requiresAuth: true },
        children: [
            {
                path: '',
                name: 'profil',
                component: AccountView
            },
            {
                path: 'reservations',
                name: 'reservations',
                component: BookingsView
            }
        ]
    },
    {
        path: '/contact',
        name: 'contact',
        component: ContactView
    },
    {
        path: '/inscription',
        name: 'inscription',
        component: RegisterView,
        meta: { requiresGuest: true }
    },
    {
        path: '/connexion',
        name: 'connexion',
        component: LoginView,
        meta: { requiresGuest: true }
    },
    {
        path: '/mot-de-passe-oublie',
        name: 'mot-de-passe-oublie',
        component: ForgotPasswordView
    },
    {
      path: '/recherche',
      name: 'recherche',
      component: SearchView
    },
    {
        path: '/voyage/:id',
        name: 'voyage-details',
        component: TripView
    },
    {
        path: '/faq',
        name: 'faq',
        component: FAQView
    },
    {
        path: '/a-propos',
        name: 'a-propos',
        component: AboutView
    },
    {
        path: '/mention-legales',
        name: 'mention-legales',
        component: LegalNoticesView
    },
    {
        path: '/politique-confidentialite',
        name: 'politique-confidentialite',
        component: PrivacyPolicyView
    },
    {
        path: '/conditions-generales-vente',
        name: 'conditions-generales-vente',
        component: GTCSView
    },

    // 404 Not Found pour le reste des pages
    {
        path: '/:pathMatch(.*)*',
        name: 'not-found',
        component: NotFoundView
    }
  ]
});

// Middleware global pour protéger les routes
router.beforeEach((to, from, next) => {
    const authStore = useAuthStore();

    if (to.meta.requiresAuth && !authStore.isLoggedIn) {
        // Si la route nécessite une connexion et que l'utilisateur n'est pas connecté
        next({ name: 'connexion' }); // Redirigez vers la page de connexion
    } else if (to.meta.requiresGuest && authStore.isLoggedIn) {
        // Si la route est réservée aux invités (non connecté) et que l'utilisateur est connecté
        next({ name: 'home' }); // Redirigez vers la page d'accueil
    } else {
        next();
    }
});

export default router;

