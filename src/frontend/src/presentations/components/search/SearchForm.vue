<script setup lang="ts">
import { reactive, computed, watch, ref } from 'vue';
import DestinationFilter from '@/presentations/components/search/filters/DestinationFilter.vue';
import ParticipantsFilter from '@/presentations/components/search/filters/ParticipantsFilter.vue';
import LogementNameFilter from './filters/LogementNameFilter.vue';

interface SearchParams {
  destination: string;
  participants: number;
  nomLogement: string;
  page: number;
  limit: number;
}

const props = defineProps<{
  searchParams: SearchParams;
}>();

const emit = defineEmits<{
  submit: [];
  reset: [];
}>();

const formState = reactive({ ...props.searchParams });

watch(() => props.searchParams, (newParams) => {
  Object.assign(formState, newParams);
}, { deep: true });

const formErrors = reactive({
  destination: '',
  nomLogement: '',
});

const isDestinationValid = computed(() => {
  return !!formState.destination;
});

watch(isDestinationValid, (isValid) => {
  formErrors.destination = isValid ? '' : 'Veuillez saisir une destination';
});

const isFormValid = computed(() => {
  // Don't require destination if logement name is provided
  if (formState.nomLogement) {
    return true;
  }
  return isDestinationValid.value;
});

const handleSubmit = () => {
  // Reset errors
  formErrors.destination = '';
  formErrors.nomLogement = '';
  
  // If no name and no destination, require destination
  if (!formState.nomLogement && !formState.destination) {
    formErrors.destination = 'Veuillez saisir une destination ou un nom de logement';
    return;
  }
  
  if (isFormValid.value) {
    // Update the parent's searchParams with formState values
    Object.assign(props.searchParams, formState);
    emit('submit');
  }
};

const handleReset = () => {
  emit('reset');
};
</script>

<template>
  <div class="search-form-container">
    <form @submit.prevent="handleSubmit" class="search-form">
      <div class="form-row">
        <!-- Logement Name Filter -->
        <LogementNameFilter
          v-model="formState.nomLogement"
          :error="formErrors.nomLogement"
        />
        
        <!-- Destination Filter -->
        <DestinationFilter
          v-model="formState.destination"
          :error="formErrors.destination"
        />

        <!-- Participants Filter -->
        <ParticipantsFilter
          v-model="formState.participants"
        />
        
        <!-- Search Button -->
        <div class="form-group search-button-container">
          <div class="filter-header">
            <i class="filter-icon bi bi-search"></i>
            <label class="filter-label">Rechercher</label>
          </div>
          <button type="submit" class="search-btn">
            <i class="bi bi-search"></i> Rechercher
          </button>
        </div>
      </div>

      <!-- Reset button -->
      <div class="reset-container">
        <button type="button" class="btn btn-outline-secondary" @click="handleReset">
          Réinitialiser les filtres
        </button>
      </div>
    </form>
  </div>
</template>

<style scoped>
.search-form-container {
  background-color: var(--couleur-gris-clair);
  border-radius: 16px;
  padding: 2rem;
  margin-bottom: 2rem;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.05);
}
.search-form {
  display: flex;
  flex-direction: column;
  gap: 1.5rem;
}
.form-row {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
  gap: 1rem;
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
  gap: 0.5rem;
}

.search-btn:hover {
  background-color: var(--couleur-bleu-fonce, #0E3A6A);
}

.reset-container {
  display: flex;
  justify-content: flex-end;
  margin-top: 1rem;
}

@media (max-width: 768px) {
  .form-row {
    grid-template-columns: 1fr;
  }
}
</style>