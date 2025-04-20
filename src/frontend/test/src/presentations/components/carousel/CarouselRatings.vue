<script setup lang="ts">
import Carousel from '@/presentations/components/carousel/Carousel.vue';
import RatingCard from '@/presentations/components/cards/RatingCard.vue';
import { ref, onMounted, onUnmounted } from 'vue';
import { Avis } from '@/domain/entities/Avis';

defineProps({
  ratings: {
    type: Array as () => Array<Avis>,
    required: true,
  },
  carouselId: {
    type: String,
    required: true,
  },
});

// ChunkSize dynamique pour gérer le responsive
const chunkSize = ref(window.innerWidth < 768 ? 1 : window.innerWidth < 1200 ? 2 : 3);

// Fonction pour ajuster le chunkSize en fonction de la taille de l'écran
const updateChunkSize = () => {
    if (window.innerWidth < 768) {
        chunkSize.value = 1;
    } else if (window.innerWidth < 1200) {
        chunkSize.value = 2;
    } else {
        chunkSize.value = 3;
    }
};

onMounted(() => {
  window.addEventListener('resize', updateChunkSize);
  updateChunkSize();
});

onUnmounted(() => {
  window.removeEventListener('resize', updateChunkSize);
});
</script>

<template>
    <Carousel :items="ratings" :chunkSize="chunkSize" :id="carouselId" :light="true">
        <template #item="{ item }">
            <div class="col-md-3 d-flex justify-content-center">
                <RatingCard
                    :name="item.prenom + ' ' + item.nom[0]"
                    :mark="item.noteAvis"
                    :text="item.texteAvis"
                    :photo="item.photo"
                />
            </div>
        </template>
    </Carousel>
</template>

<style scoped>
.carousel {
  overflow: visible !important;
}

@media (min-width: 768px) and (max-width: 1199px) {
  .col-md-3 {
    width: 45%; /* Ajuste la largeur pour afficher 2 éléments */
  }
}

@media (min-width: 1200px) {
  .col-md-3 {
    width: 30%; /* Ajuste la largeur pour afficher 3 éléments */
  }
}
</style>