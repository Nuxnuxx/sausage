<script setup lang="ts">
import Carousel from '@/presentations/components/carousel/Carousel.vue';
import DestinationCard from '@/presentations/components/cards/DestinationCard.vue';
import IconArrowRight from '@/presentations/components/svg/IconArrowRight.vue';

defineProps({
  destinations: {
    type: Array as () => Array<any>,
    required: true,
  },
  carouselId: {
    type: String,
    required: true,
  },
});
</script>

<template>
    <Carousel :items="destinations" :chunkSize="3" :id="carouselId">
        <template #item="{ item }">
            <div class="col-md-3 d-flex justify-content-center">
                <DestinationCard
                    :image="item.image"
                >
                    <div class="card-title">
                        <h4 class="fs-3 fw-bold">{{ item.title }}</h4>
                        <h6>{{ item.price }}</h6>
                    </div>
                    <p class="my-4 description">{{ item.description }}</p>
                    <RouterLink
                        :to="{
                            path: '/vos-envies',
                            query: { search: item.title },
                        }"
                        class="discover-button mt-auto border-0 bg-transparent fw-bold py-2"
                    >
                        <IconArrowRight width="28px" height="28px" :color="'var(--couleur-principal)'" class="rounded-pill border me-2 p-1" style="border-color: var(--couleur-principal) !important;" />
                        Découvrir
                    </RouterLink>
                </DestinationCard>
            </div>
        </template>
    </Carousel>
</template>

<style scoped>
@media (min-width: 768px) {
    .col-md-3 {
        width: 30%;
    }
}
</style>