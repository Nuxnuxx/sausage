<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import Title from "@/presentations/components/title/Title.vue";
import ButtonArrow from "@/presentations/components/buttons/ButtonArrow.vue";
import { LogementDAO } from "@/domain/daos/LogementDAO";
import type { Logement } from "@/domain/entities/Logement";

const defaultImage = new URL('@/assets/img/background/no-image.webp', import.meta.url).href;
const backgroundImage = new URL('@/assets/img/background/offre_de_vacances.jpg', import.meta.url).href;

// Récupérer l'ID depuis l'URL
const route = useRoute();
const router = useRouter();
const logementDAO = LogementDAO.getInstance();
const tripId = ref<number | null>(null);
const trip = ref<Logement | null>(null);
const isLoading = ref(true);
const error = ref<string | null>(null);

// Fonction pour récupérer les données du logement
const fetchTrip = async () => {
  isLoading.value = true;
  error.value = null;
  
  try {
    const id = Number(route.params.id);
    tripId.value = id;
    
    if (isNaN(id) || id <= 0) {
      throw new Error(`ID de logement invalide: ${route.params.id}`);
    }
    
    // Use LogementDAO to get the logement data
    const result = await logementDAO.get(id);
    trip.value = result;
    
    console.log('Loaded logement data:', result);
    
  } catch (err) {
    console.error('Error fetching trip details:', err);
    if (err instanceof Error) {
      error.value = `Erreur: ${err.message}`;
    } else {
      error.value = 'Une erreur inconnue est survenue lors du chargement du logement.';
    }
  } finally {
    isLoading.value = false;
  }
};

// Go back to search results
const goBack = () => {
  router.back();
};

onMounted(async () => {
  await fetchTrip();
});
</script>

<template>
  <Title :title="'Offre de vacances'" :backgroundImage="backgroundImage"></Title>

  <div class="container my-5">
    <!-- Loading state -->
    <div v-if="isLoading" class="text-center my-5">
      <div class="spinner-border text-primary" role="status">
        <span class="visually-hidden">Chargement...</span>
      </div>
      <p class="mt-2">Chargement des détails du logement...</p>
    </div>
    
    <!-- Error state -->
    <div v-else-if="error" class="alert alert-danger text-center" role="alert">
      <h4 class="alert-heading">Erreur de chargement</h4>
      <p>{{ error }}</p>
      <div class="mt-3">
        <button @click="goBack" class="btn btn-outline-secondary">
          <i class="bi bi-arrow-left"></i> Retour aux résultats
        </button>
      </div>
    </div>

    <!-- Trip details - only show if we have data -->
    <div v-else-if="trip" class="trip-details">
      <div class="row">
        <!-- Image à gauche -->
        <div class="col-md-5 d-flex justify-content-center align-items-center">
          <img :src="trip.image?.urlImage || defaultImage" :alt="trip.nomLogement" class="img-fluid rounded-4 shadow trip-image" />
        </div>

        <!-- Informations principales à droite -->
        <div class="col-md-7 text-center my-md-auto mt-5 trip-principal-info">
          <h5 v-if="trip.destination">Location à la <span class="text-lowercase">{{ trip.destination.name }}</span></h5>
          <h4 class="fs-5" v-if="trip.adresse">{{ trip.adresse.ville }} | {{ trip.adresse.pays }}</h4>
          <h1 class="fw-bold">{{ trip.nomLogement }}</h1>
          <h3 class="price fs-5 mt-3">
            Prix à partir de 
            <br>
            <span class="fw-bold fs-3">{{ trip.prixLogement }}€</span>
          </h3>

          <RouterLink :to="{ path: `/reservation/${tripId}` }">
            <ButtonArrow
              class="mt-3"
              :text="'Disponibilités'"
            ></ButtonArrow>
          </RouterLink>
        </div>
      </div>

      <!-- Description -->
      <div class="mt-5 fs-5 trip-description">
        <p class="fs-5">{{ trip.descriptionLogement }}</p>

        <!-- Prestations -->
        <h4 class="fw-bold mt-4" v-if="trip.prestationProposes && trip.prestationProposes.length > 0">
          Services et équipements :
        </h4>
        <ul v-if="trip.prestationProposes && trip.prestationProposes.length > 0">
          <li v-for="prestationProposee in trip.prestationProposes" :key="prestationProposee.idPrestationProposee">
            {{ prestationProposee.prestation.nomPrestation }}
          </li>
        </ul>
      </div>
      
      <!-- Back button -->
      <div class="mt-4">
        <button @click="goBack" class="btn btn-outline-secondary">
          <i class="bi bi-arrow-left"></i> Retour aux résultats
        </button>
      </div>
    </div>

    <!-- No data state -->
    <div v-else class="text-center mt-5 alert alert-warning">
      <h4>Aucune information disponible</h4>
      <p>Aucune donnée n'a été trouvée pour ce logement.</p>
      <button @click="goBack" class="btn btn-outline-secondary mt-3">
        <i class="bi bi-arrow-left"></i> Retour aux résultats
      </button>
    </div>
  </div>
</template>

<style scoped>
.trip-principal-info {
  color: var(--couleur-principal);
}

.trip-description p, .trip-description h4, .trip-description li{
  font-family: 'Lora', serif !important;
}

.trip-image {
  width: 100%;
  max-width: 400px;
  height: 400px;
  object-fit: cover;
}

.trip-details {
  animation: fadeIn 0.5s ease-in-out;
}

@keyframes fadeIn {
  from { opacity: 0; }
  to { opacity: 1; }
}
</style>