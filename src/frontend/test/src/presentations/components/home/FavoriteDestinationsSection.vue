<script setup lang="ts">
import CarouselControls from '@/presentations/components/carousel/CarouselControls.vue';
import CarouselFavoriteDestinations from '@/presentations/components/carousel/CarouselFavoriteDestinations.vue';

defineProps({
	destinations: {
		type: Array as () => Array<{
			name: string;
			imageUrl: string;
			description: string;
		}>,
		required: true,
	},
});

const carouselId = 'favoriteDestinationsCarousel';
</script>

<template>
	<section class="section-favorite-destinations">
		<div class="d-flex justify-content-between align-items-center mb-5">
			<h2 class="fw-bold">Vos destinations favorites</h2>
			<CarouselControls :targetId="carouselId" />
		</div>
		<CarouselFavoriteDestinations :destinations="destinations" :carouselId="carouselId" />
	</section>
</template>
