<script setup lang="ts">
import { useRouter } from 'vue-router';
import ButtonArrow from '@/presentations/components/buttons/ButtonArrow.vue';
import BackgroundSection from '@/presentations/components/backgroundSection/BackgroundSection.vue';

const router = useRouter();

const backgroundImage = new URL('@/assets/img/background/home-organized-trips.webp', import.meta.url).href;
</script>

<template>
	<BackgroundSection :backgroundImage="backgroundImage">
		<h2 class="fw-bold display-1 text-white mb-4">Laissez-vous porter par le hasard</h2>
		
		<hr class="mb-5 mt-2">
		
		<p class="lead text-white mb-5 fs-3">
		Pas d’idée ? Envie de partir en vacances sans se poser de question ? Notre programme voyages organisés vous permettra de profiter sans vous soucier de rien.
		</p>

		<RouterLink :to="{ path: '/voyages-organises' }">
			<ButtonArrow 
			:withArrow="true"
			:text="'Essayez'">
			</ButtonArrow>
		</RouterLink>
	</BackgroundSection>
</template>

<style scoped>
h2 {
	font-family: "Poppins", sans-serif;
}

hr {
	background: var(--couleur-blanc);
	opacity: 0.5;
}
</style>