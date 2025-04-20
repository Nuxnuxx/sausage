<script setup lang="ts">
import Carousel from '@/presentations/components/carousel/Carousel.vue';
import LocationButton from '@/presentations/components/cards/LocationCard.vue';
import { Logement } from '@/domain/entities/Logement';

defineProps({
	offers: { type: Array as () => Logement[], required: true },
	chunkSize: { type: Number, default: 3 },
});
</script>

<template>
	<Carousel :items="offers" :chunkSize="chunkSize" id="offersCarousel" :autoSlide="true" :cards-position="'center'">
		<template #item="{ item }">
			<div class="col-md-3 d-flex justify-content-center">
				<LocationButton :offer="item" />
			</div>
		</template>
	</Carousel>
</template>

<style scoped>
.carousel-inner .row {
	gap: 15px;
}
</style>