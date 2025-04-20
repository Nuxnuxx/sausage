<script setup lang="ts">
import { RouterLink } from 'vue-router'
import { ref, onMounted } from 'vue';
import logoLight from "@/assets/img/logo/logo-light.webp"; // Logo clair
import logoDark from "@/assets/img/logo/logo-dark.webp"; // Logo foncé
import { DestinationDAO } from '@/domain/daos/DestinationDAO';
import type { Destination } from '@/domain/entities/Destination';

withDefaults(defineProps<{
    light: boolean,
    fadeEffect: boolean,
    posAbsolute: boolean,
    withLinks: boolean,
    isLogged: boolean,
}>(), {
    light: false,
    fadeEffect: false,
    posAbsolute: false,
    withLinks: true,
});

const isMenuOpen = ref(false); // État du menu burger
const isDropdownOpen = ref(false); // État du dropdown
const destinations = ref<Destination[]>([]); // Liste des destinations

function toggleMenu() {
    isMenuOpen.value = !isMenuOpen.value;
}

const fetchDestinations = async () => {
    const destinationDAO = DestinationDAO.getInstance();
    destinations.value = await destinationDAO.list(); // Charge les destinations
};

onMounted(() => {
    fetchDestinations();
});
</script>

<template>
    <header class="header navbar navbar-expand-lg" :class="{ 'dark': light, 'fade-effect': fadeEffect, 'pos-absolute': posAbsolute, 'menu-open': isMenuOpen }">
        <div class="container-fluid">
            <!-- Logo -->
            <RouterLink to="/" style="width: 15vw; min-width: 200px;">
                <img :src="light ? logoLight : logoDark" alt="Logo Next Horizon Plus" class="logo"/>
            </RouterLink>

            <!-- Bouton burger (mobile) -->
            <button
                class="navbar-toggler"
                type="button"
                data-bs-toggle="collapse"
                data-bs-target="#navbarNav"
                aria-controls="navbarNav"
                aria-expanded="false"
                aria-label="Toggle navigation"
                @click="toggleMenu"
                :class="{ 'not-light-menu': !light }"
            >
                <span></span>
                <span></span>
                <span></span>
            </button>

            <!-- Menu -->
            <nav v-if="withLinks" class="collapse navbar-collapse" id="navbarNav">
                <ul class="navbar-nav ms-auto me-auto mb-2 mb-lg-0">

                    <li class="nav-item dropdown" :class="{ 'show': isDropdownOpen }">
                        <RouterLink
                            class="nav-link dropdown-toggle"
                            to="vos-envies"
                            id="vosEnviesDropdown"
                            role="button"
                            data-bs-toggle="dropdown"
                            aria-expanded="false"
                            active-class="active"
                        >
                            Vos envies
                        </RouterLink>
                        <ul class="dropdown-menu" aria-labelledby="vosEnviesDropdown">
                            <!-- Liste des destinations -->
                            <li v-for="destination in destinations" :key="String(destination.id)">
                                <RouterLink
                                    class="dropdown-item"
                                    :to="{ path: '/vos-envies', query: { destination: destination.name.toLowerCase() } }"
                                >
                                    {{ destination.name }}
                                </RouterLink>
                            </li>
                        </ul>
                    </li>

                    <li class="nav-item">
                        <RouterLink class="nav-link" active-class="active" to="/voyages-organises">Voyages organisés</RouterLink>
                    </li>

                    <li class="nav-item">
                        <RouterLink class="nav-link" active-class="active" to="/bons-plans">Nos bons plans</RouterLink>
                    </li>

                    <li class="nav-item" v-if="isLogged">
                        <RouterLink class="nav-link" active-class="active" to="/mon-compte">Mon compte</RouterLink>
                    </li>
                    
                    <li class="nav-item" v-else>
                        <RouterLink class="nav-link" active-class="active" to="/connexion">Connexion</RouterLink>
                    </li>
                </ul>
            </nav>
        </div>
    </header>
</template>

<style scoped>
.header {
    padding: 7vh 5vw;
    width: 100%;
    height: 120px;
}

.fade-effect {
    background: linear-gradient(180deg, #0081B0, rgba(1, 113, 155, 0.00));
}

.pos-absolute{
    position: absolute;
    top: 0;
}

.logo {
    max-height: 100%;
    max-width: 100%;
}

nav .navbar-nav {
    display: flex;
    list-style: none;
    gap: 9vw;
    margin-right: 5vw;
}

nav a {
    font-size: 16px;
    letter-spacing: 1.5px;
    padding: 10px;
    text-decoration: none;
    color: var(--couleur-bleu-fonce);
}

nav a:hover {
    color: var(--couleur-hover);
    transform: translateY(-2px);
}

nav a:active {
    color: var(--couleur-bleu-hover);
    border-radius: 5px;
}

nav li .active, #vosEnviesDropdown .active {
    color: var(--couleur-hover) !important;
}

/* Header mode sombre */
.dark a {
    color: var(--couleur-blanc);
}
/* FIN Header mode sombre */

@media screen and (max-width: 992px) {
    .header {
        z-index: 200;
    }

    .navbar-toggler {
        display: flex !important;
    }

    .navbar-nav {
        position: absolute;
        width: 100%;
        left: 0;
        top: 100%;
        background-color: #0e3b6a9f;
        transform: translateY(-20px);
        opacity: 0;
        transition: transform 0.3s ease-in-out, opacity 0.3s ease-in-out;
        gap: 4vh;
    }

    .navbar-nav li {
        padding-block: 2vh;
        text-align: center;
        background-color: #1c62ada2;
    }

    .header.menu-open {
        background-color: #0e3b6a9f;
        transition: background-color 0.3s ease-in-out;
    }

    .header.menu-open nav .navbar-nav {
        transform: translateY(0);
        opacity: 1;
    }
}

.navbar-toggler {
    background: none;
    border: none;
    display: none;
    box-shadow: none;
    padding: 0;
    flex-direction: column;
    justify-content: space-between;
    height: 30px;
    width: 30px;
    cursor: pointer;
}

.navbar-toggler span {
    display: block;
    height: 3px;
    width: 100%;
    background-color: var(--couleur-blanc);
    border-radius: 2px;
    transition: all 0.3s ease-in-out;
}

.navbar-toggler[aria-expanded="true"] span:nth-child(1) {
    transform: rotate(45deg) translate(7px, 7px);
}

.navbar-toggler[aria-expanded="true"] span:nth-child(2) {
    opacity: 0;
}

.navbar-toggler[aria-expanded="true"] span:nth-child(3) {
    transform: rotate(-45deg) translate(12px, -12px);
}

.dropdown-menu {
    background-color: #1c62ada2;
    border: none;
    border-radius: 5px;
    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
    transition: opacity 0.3s ease, transform 0.3s ease;
}

.dropdown-item {
    color: white;
    padding: 10px 20px;
    transition: background-color 0.3s ease-in-out;
}

.dropdown-item:hover {
    background-color: #0e3b6a;
    color: #fff;
}

.dropdown-item:focus {
    background-color: var(--couleur-principal);
    color: #fff;
}

.nav-item.dropdown:hover .dropdown-menu, .nav-item.dropdown:active .dropdown-menu {
    display: block;
    opacity: 1;
    transform: translateY(0);
}

.navbar-toggler.not-light-menu span {
    background-color: var(--couleur-principal) !important;
}
</style>