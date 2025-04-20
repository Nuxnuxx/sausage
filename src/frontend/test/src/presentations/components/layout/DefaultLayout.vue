<script setup lang="ts">
import { computed } from "vue";
import { useRoute } from "vue-router";
import { useAuthStore } from "@/store/authStore";
import Header from "@/presentations/components/header/Header.vue";
import Footer from "@/presentations/components/footer/Footer.vue";

const route = useRoute();
const authStore = useAuthStore();

// Définition dynamique des props du header en fonction de la route
const headerOptions = computed(() => ({
    light: ["home", "connexion", "inscription", "mot-de-passe-oublie"].includes(route.name as string),
    fadeEffect: ["home", "connexion", "inscription", "mot-de-passe-oublie"].includes(route.name as string),
    posAbsolute: ["home", "connexion", "inscription", "mot-de-passe-oublie"].includes(route.name as string),
    withLinks: !["connexion", "inscription", "mot-de-passe-oublie"].includes(route.name as string),
}));

// Choix d'utilisation du footer selon la route
const withFooter = computed(() => !["connexion", "inscription", "mot-de-passe-oublie", "not-found"].includes(route.name as string));

const isLogged = computed(() => authStore.isLoggedIn);
</script>

<template>
    <div class="default-layout">
        <Header v-bind="headerOptions" :isLogged="isLogged"/>
      
        <main class="d-flex w-100 flex-column">
            <RouterView />
        </main>
  
        <Footer :isLogged="isLogged" v-if="withFooter" />
    </div>
</template>
  
<style scoped>
.default-layout {
    display: flex;
    flex-direction: column;
    height: 100vh;
}

main {
    flex-grow: 1;
}
</style>