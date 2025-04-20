<script setup lang="ts">
import { useRouter } from 'vue-router';
import LocationCarousel from '@/presentations/components/carousel/CarouselLocation.vue';
import ButtonArrow from '@/presentations/components/buttons/ButtonArrow.vue';
import { Logement } from '@/domain/entities/Logement';

defineProps<{ offers: Logement[] }>();

const router = useRouter();
</script>

<template>
    <section class="offers-section">
        <h2 class="subtitle-section text-uppercase mb-5">Nos offres phares</h2>
        
        <LocationCarousel :offers="offers"></LocationCarousel>

        <RouterLink :to="{ path: '/vos-envies' }">
			<ButtonArrow 
				class="d-flex mx-auto" 
				:text="'Voir plus'">
			</ButtonArrow>
        </RouterLink>
        
    </section>
</template>

<style scoped>
.offers-section {
	margin: 12vh 15vw !important;
}

.offers-display{
	gap: 7vw;
}
</style>