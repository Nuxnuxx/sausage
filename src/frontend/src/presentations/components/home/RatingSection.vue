<script setup lang="ts">
import BackgroundSection from '@/presentations/components/backgroundSection/BackgroundSection.vue';
import CarouselRatings from '@/presentations/components/carousel/CarouselRatings.vue';
import CarouselControls from '@/presentations/components/carousel/CarouselControls.vue';
import { ref, computed } from 'vue';
import { Avis } from '@/domain/entities/Avis';

const props = defineProps({
    ratings: { type: Array as () => Avis[], required: true },
});


const backgroundImage = new URL('@/assets/img/background/home-rating.webp', import.meta.url).href;
const carouselId = 'RatingsCarousel';

const avatarImages = [
    new URL('@/assets/img/avatar/avatar1.jpg', import.meta.url).href,
    new URL('@/assets/img/avatar/avatar2.jpg', import.meta.url).href,
    new URL('@/assets/img/avatar/avatar3.jpg', import.meta.url).href,
    new URL('@/assets/img/avatar/avatar4.jpg', import.meta.url).href,
    new URL('@/assets/img/avatar/avatar5.jpg', import.meta.url).href,
	new URL('@/assets/img/avatar/avatar6.jpg', import.meta.url).href,
    new URL('@/assets/img/avatar/avatar7.jpg', import.meta.url).href,
    new URL('@/assets/img/avatar/avatar8.jpg', import.meta.url).href,
    new URL('@/assets/img/avatar/avatar9.jpg', import.meta.url).href,
    new URL('@/assets/img/avatar/avatar10.jpg', import.meta.url).href,
];

// Mélange la liste des avatars
const shuffleArray = (array: string[]) => {
    return array.sort(() => Math.random() - 0.5);
};

// Ajout unique des photos de profil aux avis
const ratingsWithAvatars = computed(() => {
    const shuffledAvatars = shuffleArray([...avatarImages]); // Mélange les avatars
    return props.ratings.map((rating, index) => ({
        ...rating,
        photo: shuffledAvatars[index % shuffledAvatars.length], // Associe un avatar unique
    }));
});

// console.log(ratingsWithAvatars.value);
</script>

<template>
	<BackgroundSection :backgroundImage="backgroundImage" class="overflow-hidden">
		<div class="d-flex justify-content-between align-items-center header-section">
			<h2 class="fw-bold text-white">Vos avis</h2>
			<CarouselControls :targetId="carouselId" :light="true" />
		</div>
		<CarouselRatings :ratings="ratingsWithAvatars" :carouselId="carouselId" />
	</BackgroundSection>
</template>

<style scoped>
.header-section {
  padding-bottom: 7rem;
}
</style>