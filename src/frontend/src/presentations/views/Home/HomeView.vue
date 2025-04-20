<script setup lang="ts">
import SearchHome from '@/presentations/components/home/SearchHome.vue';
import OffersSection from '@/presentations/components/home/OffersSection.vue';
import { Logement } from '@/domain/entities/Logement';
import { Avis } from '@/domain/entities/Avis';
import { onMounted, ref } from 'vue';
import OrganizedTripsSection from '@/presentations/components/home/OrganizedTripsSection.vue';
import FavoriteDestinationsSection from '@/presentations/components/home/FavoriteDestinationsSection.vue';
import RatingSection from '@/presentations/components/home/RatingSection.vue';
import NewsletterSection from '@/presentations/components/home/NewsletterSection.vue';
import { LogementDAO } from '@/domain/daos/LogementDAO';
import { AvisDAO } from '@/domain/daos/AvisDAO';

const offers = ref<Logement[]>([]);
const ratings = ref<Avis[]>([]);
const loading = ref(true);

const fetchOffers = async () => {
  	const logementDAO = LogementDAO.getInstance();
  	offers.value = await logementDAO.getSpecialOffer(9); // Récupère 9 offres spéciales
	loading.value = false;
};

const fetchRatings = async () => {
  	const avisDAO = AvisDAO.getInstance();
  	ratings.value = await avisDAO.getGoodAvis(9); // Récupère 9 avis positifs
};


// Chargé directement dans le composant
// Liste des pays avec leurs informations
const countries = [
	{
		title: 'Japon',
		price: 'À partir de 899€',
		description:
		"Le Japon, terre de traditions et de modernité, vous invite à découvrir ses temples anciens, ses cerisiers en fleurs et ses métropoles vibrantes comme Tokyo et Kyoto.",
		image: new URL('@/assets/img/country/japan.jpg', import.meta.url).href,
	},
	{
		title: 'Italie',
		price: 'À partir de 499€',
		description:
		"L'Italie, berceau de la Renaissance, séduit par ses villes historiques comme Rome et Florence, ses paysages pittoresques et sa cuisine mondialement célèbre.",
		image: new URL('@/assets/img/country/italy.jpg', import.meta.url).href,
	},
	{
		title: 'Thaïlande',
		price: 'À partir de 699€',
		description:
		"La Thaïlande, paradis tropical, offre des plages de sable blanc, des temples dorés et une culture riche et accueillante.",
		image: new URL('@/assets/img/country/thailande.jpg', import.meta.url).href,
	},
	{
		title: 'Canada',
		price: 'À partir de 799€',
		description:
		"Le Canada, vaste et sauvage, vous émerveillera avec ses parcs nationaux, ses montagnes majestueuses et ses villes dynamiques comme Toronto et Vancouver.",
		image: new URL('@/assets/img/country/canada.jpg', import.meta.url).href,
	},
	{
		title: 'Australie',
		price: 'À partir de 999€',
		description:
		"L'Australie, terre des kangourous et des plages infinies, vous invite à explorer la Grande Barrière de Corail, les déserts rouges et les villes modernes comme Sydney.",
		image: new URL('@/assets/img/country/australie.jpg', import.meta.url).href,
	},
	{
		title: 'Islande',
		price: 'À partir de 799€',
		description:
		"L'Islande, île de feu et de glace, vous offre des paysages spectaculaires avec ses geysers, ses volcans et ses aurores boréales.",
		image: new URL('@/assets/img/country/islande.jpg', import.meta.url).href,
	},
	{
		title: 'Brésil',
		price: 'À partir de 699€',
		description:
		"Le Brésil, pays de la samba et du carnaval, vous invite à découvrir ses plages mythiques, sa forêt amazonienne et ses villes vibrantes comme Rio de Janeiro.",
		image: new URL('@/assets/img/country/brasil.jpg', import.meta.url).href,
	},
	{
		title: 'Égypte',
		price: 'À partir de 599€',
		description:
		"L'Égypte, terre des pharaons, vous transporte dans l'histoire avec ses pyramides majestueuses, ses temples anciens et le Nil légendaire.",
		image: new URL('@/assets/img/country/egypt.jpg', import.meta.url).href,
	},
	{
		title: 'Afrique du Sud',
		price: 'À partir de 899€',
		description:
		"L'Afrique du Sud, pays des safaris, vous invite à explorer ses parcs nationaux, ses plages sauvages et ses vignobles renommés.",
		image: new URL('@/assets/img/country/south-africa.jpg', import.meta.url).href,
	},
];


onMounted(() => {
	fetchOffers();
	fetchRatings();
});
</script>

<template>
    <img
        class="hero-image"
        src="@/assets/img/home.webp"
        alt="Un homme se tenant devant un lac et des montagnes prenant en photo le paysage."
    />

    <SearchHome id="search-home" class="mb-5 z-3"/>
	
    <div v-if="loading" class="text-center my-5">
        Chargement des offres...
    </div>
    <OffersSection v-else :offers="offers"></OffersSection>

	<OrganizedTripsSection></OrganizedTripsSection>

	<FavoriteDestinationsSection :destinations="countries"></FavoriteDestinationsSection>

	<RatingSection :ratings="ratings"></RatingSection>

	<NewsletterSection></NewsletterSection>
</template>

<style scoped>
.hero-image {
  	height: 100vh;
  	object-fit: cover;
  	object-position: center;
}

#search-home {
	min-height: 25px;
}

@media screen and (max-width: 768px) {
	#search-home {
		min-height: 25vw;
	}
}
</style>