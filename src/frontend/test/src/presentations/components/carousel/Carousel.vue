<script setup lang="ts">
import { computed } from 'vue';

const props = defineProps({
    items: { type: Array<any>, required: true }, // Tableau d'éléments à afficher
    chunkSize: { type: Number, default: 3 }, // Taille des groupes
    id: { type: String, required: true }, // Identifiant unique pour le carousel
    autoSlide: { type: Boolean, default: false }, // Indique si le carousel doit défiler automatiquement
    cardsPosition: { type: String, default: 'between' }, // Position des cartes dans le carousel
	light: { type: Boolean, default: false }, // Indique si le thème est clair
});

// Fonction pour diviser un tableau en groupes de taille donnée
function chunkArray<T>(array: T[], size: number): T[][] {
	const result: T[][] = [];
	for (let i = 0; i < array.length; i += size) {
		result.push(array.slice(i, i + size));
	}
	return result;
}

// Découpage des items en groupes
const groupedItems = computed(() => chunkArray(props.items, props.chunkSize));
</script>

<template>
	<div :id="id" class="carousel slide overflow-hidden" :data-bs-ride="autoSlide ? 'carousel' : undefined">
		<!-- Carousel items -->
		<div class="carousel-inner overflow-visible">
			<div
				v-for="(group, index) in groupedItems"
				:key="index"
				class="carousel-item"
				:class="{ active: index === 0 }"
			>
				<div class="row" :class="[`justify-content-${cardsPosition}`, { 'no-gap': props.chunkSize > 3 }]">
					<!-- Slot pour personnaliser l'affichage des éléments -->
					<slot name="item" v-for="item in group" :item="item" :key="item.id || item.title" />
				</div>
			</div>
		</div>

		<!-- Indicators -->
		<div class="carousel-indicators justify-content-center my-4 position-relative" :class="{ 'indicators-light': light }">
			<button
				v-for="(group, index) in groupedItems"
				:key="index"
				type="button"
				:data-bs-target="'#' + id"
				:data-bs-slide-to="index"
				:class="{ active: index === 0 }"
				aria-current="true"
			></button>
		</div>
	</div>
</template>

<style scoped>
.carousel-indicators button {
	width: 15px;
	height: 15px;
	border-radius: 100%;
	opacity: 0.2;
	background-color: var(--couleur-gris-fonce);
}

.indicators-light button{
	background-color: var(--couleur-blanc) !important;
	opacity: 0.5;
}

.carousel-indicators .active {
	opacity: 1;
}

.carousel-inner .row {
	gap: 15px;
}

.carousel-inner .row.no-gap {
    gap: 0;
}
</style>