<script setup lang="ts">
import { computed } from 'vue';
import ResultCard from './ResultCard.vue';
interface SearchResult {
  id: number;
  title: string;
  location: string;
  rating: number;
  price: number;
  image: string;
  description: string;
}
interface SearchParams {
  destination: string;
  depart: string;
  participants: number;
  page: number;
  limit: number;
}
const props = defineProps<{
  results: SearchResult[];
  isLoading: boolean;
  resultCount: number;
  currentPage: number;
  searchParams: SearchParams;
}>();
const emit = defineEmits<{
  'page-change': [page: number];
  'reserve': [resultId: number];
}>();
const hasResults = computed(() => {
  return props.results && props.results.length > 0;
});
const formattedResultCount = computed(() => {
  if (props.resultCount === 0) {
    return 'Aucun résultat trouvé';
  } else if (props.resultCount === 1) {
    return '1 résultat trouvé';
  } else {
    return `${props.resultCount} résultats trouvés`;
  }
});
const handleResultSelect = (resultId: number) => {
  emit('reserve', resultId);
};
</script>

<template>
  <div class="search-results">

    <div class="results-header">
      <h2 class="title-search">Résultats pour : <span class="search-term">{{ searchParams?.destination || "Toutes destinations" }}</span></h2>
      <p class="result-count">{{ formattedResultCount }}</p>
    </div>

    <div v-if="isLoading" class="loading-container">
      <div class="spinner-border text-primary" role="status">
        <span class="visually-hidden">Chargement...</span>
      </div>
      <p>Recherche en cours...</p>
    </div>

    <div v-else>
      <div v-if="hasResults" class="results-grid">
        <ResultCard
          v-for="result in results"
          :key="result.id"
          :result="result"
          @select="handleResultSelect"
        />
      </div>

      <div v-else class="no-results">
        <div class="icon">
          <i class="bi bi-search"></i>
        </div>
        <h3>Aucun résultat trouvé</h3>
        <p>Essayez de modifier vos critères de recherche pour trouver plus d'options.</p>
      </div>
    </div>
  </div>
</template>

<style scoped>
.search-results {
  margin-top: 2rem;
}
.results-header {
  margin-bottom: 1.5rem;
}
.title-search {
  color: var(--couleur-principal, #1c61ad);
  font-size: 1.8rem;
  font-weight: 500;
  margin-bottom: 0.5rem;
}
.search-term {
  font-weight: 600;
}
.result-count {
  color: #6c757d;
  font-size: 1rem;
}
.loading-container {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  padding: 3rem 0;
}
.loading-container p {
  margin-top: 1rem;
  color: #6c757d;
}
/* Nouvelle grille pour les résultats en largeur */
.results-grid {
  display: flex;
  flex-direction: column;
  gap: 1.5rem;
}
/* No results */
.no-results {
  text-align: center;
  padding: 3rem 1rem;
  background-color: #f8f9fa;
  border-radius: 16px;
  margin: 2rem 0;
}
.no-results .icon {
  font-size: 3rem;
  color: #6c757d;
  margin-bottom: 1rem;
}
.no-results h3 {
  font-size: 1.5rem;
  color: var(--couleur-principal, #1C61AD);
  margin-bottom: 1rem;
}
.no-results p {
  font-size: 1rem;
  color: #6c757d;
  margin-bottom: 1.5rem;
  max-width: 600px;
  margin-left: auto;
  margin-right: auto;
}
</style>