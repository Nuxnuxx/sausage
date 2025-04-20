<script setup lang="ts">
import { reactive, computed, ref } from 'vue';
import { useRouter } from 'vue-router';
import DestinationFilter from '@/presentations/components/search/filters/DestinationFilter.vue';
import ParticipantsFilter from '@/presentations/components/search/filters/ParticipantsFilter.vue';
import LogementNameFilter from '@/presentations/components/search/filters/LogementNameFilter.vue';

// Interface for search parameters
interface SearchParams {
  destination: string;
  participants: number;
  nomLogement: string; // Added logement name
}

// Initialize with default values
const searchParams = reactive<SearchParams>({
  destination: '',
  participants: 2,
  nomLogement: ''
});

const router = useRouter();
const formErrors = reactive({
  destination: '',
  nomLogement: '',
});

// Validation functions
const isDestinationValid = computed(() => {
  return !!searchParams.destination;
});

const isFormValid = computed(() => {
  // Form is valid if either destination or name is provided
  return isDestinationValid.value || !!searchParams.nomLogement;
});

// Handle form submission
const handleSubmit = () => {
  // Reset validation errors
  formErrors.destination = '';
  formErrors.nomLogement = '';
  
  // If neither field is provided, show error
  if (!searchParams.destination && !searchParams.nomLogement) {
    formErrors.destination = 'Veuillez saisir une destination ou un nom de logement';
    return;
  }
  
  if (isFormValid.value) {
    // Navigate to search page with query parameters
    const query: Record<string, string> = {};
    
    if (searchParams.destination) {
      query.destination = searchParams.destination;
    }
    
    if (searchParams.nomLogement) {
      query.nomLogement = searchParams.nomLogement;
    }
    
    if (searchParams.participants !== 2) {
      query.participants = searchParams.participants.toString();
    }
    
    router.push({
      path: '/recherche',
      query
    });
  }
};
</script>

<template>
  <!-- Conteneur principal du composant -->
  <div class="position-relative">
    <div class="position-absolute start-50 translate-middle home-search-container">
      <h1 class="fw-bold display-3 mb-3">Bonjour !</h1>
      <h4 class="fs-3">Prêt(e) à explorer le monde avec nous ?</h4>

      <!-- Formulaire de recherche -->
      <form @submit.prevent="handleSubmit" class="search-form">
        <div class="search-form-row">
          <!-- Logement Name Filter -->
          <LogementNameFilter
            v-model="searchParams.nomLogement"
            :error="formErrors.nomLogement"
          />

          <!-- Destination Filter -->
          <DestinationFilter
            v-model="searchParams.destination"
            :error="formErrors.destination"
          />

          <!-- Participants Filter -->
          <ParticipantsFilter
            v-model="searchParams.participants"
          />

          <!-- Search Button -->
          <div class="form-group search-button-container">
            <div class="filter-header">
              <i class="filter-icon bi bi-search"></i>
              <label class="filter-label">Rechercher</label>
            </div>
            <button type="submit" class="search-btn">
              <i class="bi bi-search"></i>
            </button>
          </div>
        </div>
      </form>
    </div>
  </div>
</template>

<style scoped>
.position-absolute {
  top: -64px;
}

h1 {
  color: var(--couleur-principal);
  margin-bottom: 0.5rem;
}

h4 {
  margin-bottom: 2rem;
  color: var(--couleur-non-interactifs, #404040);
}

.home-search-container {
  width: 75%;
  background-color: white;
  border-radius: 1.5rem;
  padding: 2rem;
  box-shadow: 0 10px 30px rgba(0, 0, 0, 0.1);
}

.search-form {
  margin-bottom: 1rem;
}

.search-form-row {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(180px, 1fr));
  gap: 1.5rem;
  align-items: flex-end;
}

.search-button-container {
  display: flex;
  flex-direction: column;
  gap: 8px;
}

.filter-header {
  display: flex;
  align-items: center;
  gap: 8px;
}

.filter-icon {
  font-size: 20px;
  color: var(--couleur-principal, #1C61AD);
}

.filter-label {
  font-family: var(--font-lexend, 'Lexend', sans-serif);
  font-weight: 500;
  font-size: 16px;
  color: var(--couleur-non-interactifs, #404040);
}

.search-btn {
  width: 100%;
  background-color: var(--couleur-principal, #1C61AD);
  color: white;
  border: none;
  height: 38px;
  border-radius: 0.25rem;
  cursor: pointer;
  transition: background-color 0.2s;
  display: flex;
  align-items: center;
  justify-content: center;
}

.search-btn:hover {
  background-color: var(--couleur-bleu-fonce, #0E3A6A);
}

/* Media queries for responsive design */
@media (max-width: 992px) {
  .home-search-container {
    width: 90%;
    padding: 1.5rem;
  }
}

@media (max-width: 768px) {
  .search-form-row {
    grid-template-columns: 1fr;
    gap: 1rem;
  }
  
  .position-absolute {
    top: -100px;
  }
  
  h1 {
    font-size: 2rem;
  }
  
  h4 {
    font-size: 1.25rem;
    margin-bottom: 1.5rem;
  }
}
</style>