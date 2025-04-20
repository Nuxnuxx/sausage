<script setup lang="ts">
import Title from '@/presentations/components/title/Title.vue';
import LocationCarousel from '@/presentations/components/carousel/CarouselLocation.vue';
import LocationButton from '@/presentations/components/cards/LocationCard.vue';
import ButtonArrow from '@/presentations/components/buttons/ButtonArrow.vue';
import { ref, onMounted, computed } from 'vue';
import { Logement } from '@/domain/entities/Logement';
import { LogementDAO } from '@/domain/daos/LogementDAO';

const specialDeals = ref<Logement[]>([]);
const allDeals = ref<Logement[]>([]);
const searchQuery = ref<string>('');
const logementDAO = LogementDAO.getInstance();

// Génère un pourcentage aléatoire entre 1 et 99 car non pensé en back
const addRandomReduction = (deals: Logement[]): Logement[] => {
  return deals.map((deal) => ({
    ...deal,
    reduction: Math.floor(Math.random() * 99) + 1, 
  }));
};

const fetchSpecialDeals = async () => {
  const deals = await logementDAO.getSpecialOffer(8); // Récupère 8 bons plans phares
  specialDeals.value = addRandomReduction(deals);
};

const fetchAllDeals = async () => {
  const deals = await logementDAO.list(); // Récupère tous les bons plans
  allDeals.value = addRandomReduction(deals);
};

const loadPage = async () => {
  await fetchSpecialDeals();
  await fetchAllDeals();
};

const filteredDeals = computed(() =>
  allDeals.value.filter((deal) =>
    deal.nomLogement.toLowerCase().includes(searchQuery.value.toLowerCase())
  )
);

onMounted(() => {
  loadPage();
});
</script>

<template>
  <Title title="Nos bon plans" backgroundImage="/src/assets/img/background/bon_plans.jpg"></Title>

    <div class="deals-section page-bottom-margin">
    <!-- Barre de recherche -->
    <p class="card py-4 text-center">EN ATTENTE BARRE DE RECHERCHE</p>

    <!-- Section Bons Plans Phares -->
    <div class="mt-5">
      <h2 class="subtitle-section text-center">NOS BON PLANS PHARES</h2>
      <LocationCarousel :offers="specialDeals" :chunkSize="4" class="mt-4"></LocationCarousel>
    </div>

    <!-- Section Tous les Bons Plans -->
    <div class="container mt-5">
      <div class="row gy-4">
        <div class="col-12" v-for="offer in filteredDeals" :key="offer.id">
          <div class="row align-items-center p-3 h-100">
            <div class="col-md-4">
              <LocationButton :offer="offer" />
            </div>
            <div class="offer-description col-md-8 d-flex flex-column justify-content-between">
              <p class="h5 text-truncate-6">{{ offer.descriptionLogement }}</p>
              <RouterLink :to="{ path: `/voyage/${offer.id}` }">
                <ButtonArrow 
                  class="btn btn-primary mt-3"
                  :text="'Voir plus'"
                />
              </RouterLink>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
.deals-section {
  padding: 3em 6em;
}

.page-bottom-margin {
  margin-bottom: rem; 
}

.text-truncate-6 {
  display: -webkit-box;
  -webkit-line-clamp: 6;
  line-clamp: 6;
  -webkit-box-orient: vertical;
  overflow: hidden;
  text-overflow: ellipsis;
}

.offer-description {
  height: 60%;
}
</style>