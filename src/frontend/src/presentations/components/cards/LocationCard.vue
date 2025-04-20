<script setup lang="ts">
import { Logement } from '@/domain/entities/Logement';
import { AvisDAO } from '@/domain/daos/AvisDAO';
import Rating from '@/presentations/components/rating/Rating.vue';
import IconPosition from '@/presentations/components/svg/IconPosition.vue';
import { calculateAverageRating } from '@/presentations/components/utils/rating';
import { ref, onMounted } from 'vue';
import Deal from '@/presentations/components/deal/Deal.vue';

const props = defineProps({
  offer: { type: Object as () => Logement, required: true },
});

const emit = defineEmits(['reserve']); // Événement "reserve" émis lors du clic

const handleClick = (): void => {
  emit('reserve'); // Émet l'événement "reserve" pour informer le parent
};

const defaultImage = new URL('@/assets/img/background/no-image.webp', import.meta.url).href;

const round = (value: number): string => {
  const roundedValue = parseFloat(value.toFixed(2)); // Arrondi à 2 chiffres après la virgule
  return roundedValue % 1 === 0 ? roundedValue.toString() : roundedValue.toFixed(2); // Supprime les .00 si le nombre est entier
};

const avis = ref([]);
const averageRating = ref(0);

// Récupérer les avis pour le logement
const fetchAvis = async () => {
	const avisDAO = AvisDAO.getInstance();
	avis.value = await avisDAO.getByLogementId(props.offer.id); // Récupère les avis pour le logement
	averageRating.value = calculateAverageRating(avis.value); // Calcule la moyenne des avis
};

onMounted(() => {
  	fetchAvis();
});
</script>

<template>
  <!-- Carte cliquable représentant une offre -->
	<RouterLink :to="{ path: `/voyage/${offer.id}` }">
		<div class="card-button rounded-0" @click="handleClick" role="button">
			<div class="position-relative">
				<img class="object-fit-cover rounded-4" width="100%" height="300" :src="offer.image?.urlImage || defaultImage" :alt="offer.nomLogement" />

				<!-- Note de l'offre affichée en haut à droite -->
				<Rating v-if="averageRating" :mark="averageRating" />

				<!-- Pourcentage réduction de l'offre affiché en haut à droite -->
				<Deal v-if="offer.reduction" :percent="offer.reduction" />
			</div>

			<!-- Contenu textuel de la carte -->
			<div class="mt-3">
				<div v-if="offer.reduction" class="d-flex flex-column fs-5">
					<div class="price">
						<span class="fw-bold text-decoration-line-through text-danger fs-6">{{ offer.prixLogement }}€</span>
						<span class="fw-bold text-danger ms-2">{{ round(offer.prixLogement - (offer.prixLogement*(offer.reduction/100))) }}€</span>
					</div>
					
					<h3 class="fw-bold m-0 fs-5">{{ offer.nomLogement }}</h3>
				</div>

				<div v-else class="d-flex justify-content-between align-items-center fs-5">
					<h3 class="fw-bold m-0 fs-5">{{ offer.nomLogement }}</h3>
					<span class="price fw-bold">{{ offer.prixLogement }}€</span>
				</div>

				<div class="location d-flex align-items-center">
					<IconPosition class="me-1" width="24" height="24" />
					<small class="my-auto ">{{ offer.adresse?.ville }}, {{ offer.adresse?.pays }}</small>
				</div>
			</div>
		</div>
	</RouterLink>
	
</template>

<style scoped>
.card-button {
  max-width: 300px;
  transition: transform 0.2s;
}

.card-button:hover {
  transform: scale(1.02);
}

a {
  color: unset !important;
}
</style>

