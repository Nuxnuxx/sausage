<script setup lang="ts">
import { ref, onMounted, watch } from 'vue';
import { useRouter } from 'vue-router';
import SearchForm from '@/presentations/components/search/SearchForm.vue';
import SearchResult from '@/presentations/components/search/SearchResult.vue';
import Title from "@/presentations/components/title/Title.vue";
import { LogementDAO } from '@/domain/daos/LogementDAO';
import type { Logement } from '@/domain/entities/Logement';

interface SearchParams {
  destination: string;
  participants: number;
  nomLogement: string; // Added logement name filter
  page: number;
  limit: number;
}

interface SearchResult {
  id: number;
  title: string;
  location: string;
  rating: number;
  price: number;
  image: string;
  description: string;
}

const searchParams = ref<SearchParams>({
  destination: '',
  participants: 2,
  nomLogement: '',
  page: 1,
  limit: 10
});

const searchResults = ref<SearchResult[]>([]);
const totalResults = ref(0);
const isLoading = ref(false);
const error = ref<string | null>(null);
const hasSearched = ref(false);
const router = useRouter();
const backgroundImage = new URL('@/assets/img/home.webp', import.meta.url).href;

const performSearch = async () => {
  isLoading.value = true;
  error.value = null;
  hasSearched.value = true;
  try {
    const logementDAO = LogementDAO.getInstance();
    
    // Use the list method with both nomLogement and destination parameters
    const logements = await logementDAO.list(
      searchParams.value.nomLogement || undefined,  // Pass logement name if provided
      searchParams.value.destination || undefined   // Pass destination if provided
    );
    
    // Transform Logement objects to SearchResult objects
    const results = logements.map(logement => ({
      id: logement.id,
      title: logement.nomLogement,
      location: logement.adresse ? `${logement.adresse.ville}, ${logement.adresse.pays}` : 'Emplacement non spécifié',
      rating: 0, // We would need to calculate this from avis if available
      price: logement.prixLogement,
      image: logement.image?.urlImage || "",
      description: logement.descriptionLogement
    }));
    
    searchResults.value = results;
    totalResults.value = results.length;
  } catch (err) {
    console.error('Search error:', err);
    error.value = 'Une erreur s\'est produite lors de la recherche.';
  } finally {
    isLoading.value = false;
  }
};

const handleSearchSubmit = () => {
  searchParams.value.page = 1;
  performSearch();
};

const handleReset = () => {
  searchParams.value = {
    destination: '',
    participants: 2,
    nomLogement: '',
    page: 1,
    limit: 10
  };
  hasSearched.value = false;
  searchResults.value = [];
  totalResults.value = 0;
  router.replace({ query: {} });
};

const handleReserve = (resultId: number) => {
  router.push(`/voyage/${resultId}`);
};

onMounted(() => {
  const urlParams = new URLSearchParams(window.location.search);
  if (urlParams.toString()) {
    if (urlParams.get('destination')) {
      searchParams.value.destination = urlParams.get('destination') || '';
    }
    if (urlParams.get('page')) {
      searchParams.value.page = parseInt(urlParams.get('page') || '1', 10);
    }
    if (urlParams.get('participants')) {
      searchParams.value.participants = parseInt(urlParams.get('participants') || '2', 10);
    }
    if (urlParams.get('nomLogement')) {
      searchParams.value.nomLogement = urlParams.get('nomLogement') || '';
    }
    performSearch();
  }
});

watch(searchParams, () => {
  if (hasSearched.value) {
    const params = new URLSearchParams();
    if (searchParams.value.destination) {
      params.set('destination', searchParams.value.destination);
    }
    if (searchParams.value.participants !== 2) {
      params.set('participants', searchParams.value.participants.toString());
    }
    if (searchParams.value.page > 1) {
      params.set('page', searchParams.value.page.toString());
    }
    if (searchParams.value.nomLogement) {
      params.set('nomLogement', searchParams.value.nomLogement);
    }
    router.replace({ query: Object.fromEntries(params) });
  }
}, { deep: true });
</script>

<template>
  <div>
    <Title
      :backgroundImage="backgroundImage"
      :title="'Recherche de voyages'"
    />

    <div class="search-container">

      <SearchForm
        :searchParams="searchParams"
        @submit="handleSearchSubmit"
        @reset="handleReset"
      />

      <div v-if="error" class="alert alert-danger" role="alert">
        {{ error }}
      </div>

      <div v-if="isLoading" class="loading-container">
        <div class="spinner-border text-primary" role="status">
          <span class="visually-hidden">Chargement...</span>
        </div>
        <p>Recherche en cours...</p>
      </div>

      <SearchResult
        v-if="hasSearched && !isLoading"
        :results="searchResults"
        :isLoading="isLoading"
        :resultCount="totalResults"
        :currentPage="searchParams.page"
        :searchParams="searchParams"
        @reserve="handleReserve"
      />
    </div>
  </div>
</template>

<style scoped>
.search-container {
  max-width: 1200px;
  margin: 0 auto;
  padding: 2rem;
}

.loading-container {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  padding: 2rem;
  gap: 1rem;
}
</style>