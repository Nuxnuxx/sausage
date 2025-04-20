<script setup lang="ts">
import { useRoute, useRouter } from 'vue-router';
import LocationButton from '@/presentations/components/cards/LocationCard.vue'
import ButtonArrow from '@/presentations/components/buttons/ButtonArrow.vue';
import Title from '@/presentations/components/title/Title.vue';
import LocationCarousel from '@/presentations/components/carousel/CarouselLocation.vue';
import { Logement } from '@/domain/entities/Logement';
import { onMounted, ref, computed, watch } from 'vue';
import { LogementDAO } from '@/domain/daos/LogementDAO';
import { DestinationDAO } from '@/domain/daos/DestinationDAO';

const route = useRoute();
const router = useRouter();
const specialOffers = ref<Logement[]>([]);
const offers = ref<Logement[]>([]);
const destinationType = computed(() => route.query.destination); // Récupérer le type de destination
const logementDAO = LogementDAO.getInstance();

const fetchSpecialOffers = async () => {
	// RECUPERER DES OFFRES AVEC DES BONS AVIS
	if (!destinationType.value) {
        specialOffers.value = await logementDAO.getSpecialOffer(12); // Récupère 12 offres spéciales sans destination
    } else {
		specialOffers.value = await logementDAO.getSpecialOfferByDestination(String(destinationType.value), 12); // Récupère 12 offres spéciales par destination
    }
};

const fetchOffers = async () => {
    if (!destinationType.value) {
        offers.value = await logementDAO.list(); // Récupère toutes les offres sans destination
    } else {
        offers.value = await logementDAO.getByDestination(String(destinationType.value)); // Récupère les offres par destination
    }
};

const checkTypeDestination = async () => {
	if (!destinationType.value) return;

	const destinationDAO = DestinationDAO.getInstance();
	const exists = await destinationDAO.isExisting(String(destinationType.value)); // Vérifie si la destination existe

	if (!exists) {
		router.replace({ path: route.path, query: {} }); // Redirige vers la même route sans le paramètre "destination"
	}
};

const loadPage = async () => {
	await checkTypeDestination();
    await fetchSpecialOffers();
	await fetchOffers();
};

onMounted(() => {
	loadPage();
});

watch(
    () => route.fullPath,
    async () => {
        await loadPage();
    }
);
</script>

<template>
	<Title title="Vos envies" backgroundImage="/src/assets/img/background/vos_envies_plage.jpg"></Title>
	<div class="offers-section">
		<p class="card py-4 text-center">EN ATTENTE BARRE DE RECHERCHE</p>
		<h1 class="title-section mt-4">
			Location Vacances 
			<span v-if="destinationType">
				à la 
				<span class="text-capitalize">
					{{destinationType}}
				</span>
			</span>
		</h1>

		<div class="mt-5">
			<h2 class="subtitle-section">
				NOS OFFRES PHARES
				<span v-if="destinationType">
					À LA 
					<span class="text-uppercase">
						{{destinationType}}
					</span>
				</span>
			</h2>
			<LocationCarousel :offers="specialOffers" :chunkSize="4" class="mt-4"></LocationCarousel>
		</div>

		<div class="container mt-5">
            <div class="row gy-4">
                <div class="col-12" v-for="offer in offers" :key="offer.id">
                    <div class="row align-items-center p-3 h-100">
                        <div class="col-md-4">
                            <LocationButton
                                :offer="offer"
                            />
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
.offers-section {
	padding: 3em 6em;
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
