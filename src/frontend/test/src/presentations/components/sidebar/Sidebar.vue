<script setup lang="ts">
import { useNotification } from '@kyvg/vue3-notification';
import { useRouter } from 'vue-router';
import { useAuthStore } from '@/store/authStore';

const router = useRouter();
const { notify } = useNotification();
const authStore = useAuthStore();

const handleLogout = () => {
  authStore.logoutUser();

  notify({
    title: 'Déconnexion réussie',
    text: 'Vous avez été déconnecté avec succès.',
    type: 'success',
  });

  router.push('/');
};
</script>

<template>
  <div class="d-flex flex-column align-items-center align-items-sm-start px-3 pt-2 text-white h-100 min-vh-100 flex-1" style="background-color: var(--couleur-gris-clair);">
    <span class="fs-10 d-none d-sm-inline" style="color: var(--couleur-bleu-fonce);">
      Bienvenue {{ authStore.user.prenomUtilisateur }} {{ authStore.user.nomUtilisateur }}
    </span>
    <hr class="w-100 my-2" style="border-top: 1px solid var(--couleur-bleu-fonce);">
    <ul class="nav nav-pills flex-column mb-sm-auto mb-0 align-items-center align-items-sm-start" id="menu">
      <li class="nav-item">
        <RouterLink to="/mon-compte" class="nav-link align-middle px-0" active-class="active-link" exact-active-class="exact-active-link">
          <i class="fs-4"></i> 
          <span class="ms-1 d-none d-sm-inline">Mon profil</span>
        </RouterLink>
      </li>
      <li>
        <RouterLink to="/mon-compte/reservations" class="nav-link align-middle px-0" active-class="active-link" exact-active-class="exact-active-link">
          <i class="fs-4"></i> 
          <span class="ms-1 d-none d-sm-inline">Mes réservations</span>
        </RouterLink>
      </li>
      <li>
        <button @click="handleLogout" class="nav-link px-0 align-middle" style="background: none; border: none; cursor: pointer;">
          <i class="fs-4"></i> 
          <span class="ms-1 d-none d-sm-inline">Déconnexion</span>
        </button>
      </li>
    </ul>
  </div>
</template>

<style scoped>
/* Couleur par défaut des liens */
.nav-link span {
  color: var(--couleur-bleu-fonce);
  transition: color 0.3s ease-in-out;
}

/* Couleur du lien actif */
.nav-link.exact-active-link span {
  color: var(--couleur-secondaire) !important;
}
</style>
